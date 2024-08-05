using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.Entity.Enums;
using SigortaApp.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IBankAndCaseDetailsRepository _bACDetailsRepository;
        private readonly IPaymentRepository _paymentRepository;

        public UnitService(IUnitRepository unitRepository, IBankAndCaseDetailsRepository bACDetailsRepository, IPaymentRepository paymentRepository)
        {
            _unitRepository = unitRepository;
            _bACDetailsRepository = bACDetailsRepository;
            _paymentRepository = paymentRepository;
        }

        public List<CaseDto> GetCases()
        {
            var result = (from u in _unitRepository.GetListAll()
                          where u.UnitTypesId == (int)UnitTypesEnum.KASALAR
                          select new CaseDto
                          {
                              Id = u.Id,
                              Name = u.Name,
                              IsActive=u.IsActive,
                              UnitTypesId = u.UnitTypesId,
                              UstId = u.UstId,
                              AccountPrice = (_bACDetailsRepository.GetListAll().Where(w => w.CaseId != null && w.CaseId.Value == u.Id && w.IsIn).Sum(s => s.Price) - _bACDetailsRepository.GetListAll().Where(w => w.CaseId != null && w.CaseId.Value == u.Id && !w.IsIn).Sum(s => s.Price)) + (_paymentRepository.GetListAll().Where(w => w.CaseId != null && w.CaseId.Value == u.Id && w.StatusId == (int)OdemeTuruEnum.Tahsilat).Sum(s => s.Price) - _paymentRepository.GetListAll().Where(w => w.CaseId != null && w.CaseId.Value == u.Id && w.StatusId == (int)OdemeTuruEnum.Odeme).Sum(s => s.Price))
                          }).ToList();
            return result;
        }

        public List<Unit> GetList()
        {
            return _unitRepository.GetListAll();
        }

        public void TAdd(Unit t)
        {
            _unitRepository.Insert(t);
        }

        public void TDelete(Unit t)
        {
            _unitRepository.Delete(t);
        }

        public Unit TGetById(int id)
        {
            return _unitRepository.GetById(id);
        }

        public void TUpdate(Unit t)
        {
            _unitRepository.Update(t);
        }
    }
}
