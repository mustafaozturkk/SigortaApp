using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitRepository _unitRepository;

        public CompanyService(ICompanyRepository companyRepository, IPaymentRepository paymentRepository, IUnitRepository unitRepository)
        {
            _companyRepository = companyRepository;
            _paymentRepository = paymentRepository;
            _unitRepository = unitRepository;
        }

        public List<PaymentDto> GetCompanyWithPaymentByCompanyId(int companyId)
        {
            var result = (from c in _companyRepository.GetListAll()
                          join p in _paymentRepository.GetListAll() on c.Id equals p.CompanyId
                          join u in _unitRepository.GetListAll() on p.StatusId equals u.Id
                          join u2 in _unitRepository.GetListAll() on p.PaymentMethodId equals u2.Id
                          join u3 in _unitRepository.GetListAll() on c.CurrentGroupId equals u3.Id
                          let alinan = _paymentRepository.GetListAll().Where(w => w.CompanyId == companyId && (w.StatusId == 1 || w.StatusId == 3)).Sum(s => s.Price)
                          let borc = _paymentRepository.GetListAll().Where(w => w.CompanyId == companyId && (w.StatusId == 2 || w.StatusId == 4)).Sum(s => s.Price)
                          where c.Id == companyId
                          select new PaymentDto
                          {
                              CompanyId = c.Id,
                              Id = p.Id,
                              CaseId = p.CaseId ?? 0,
                              Company = _companyRepository.GetById(companyId),
                              CreatedBy = p.CreatedBy,
                              CreatedDate = p.CreatedDate,
                              Description = p.Description,
                              Price = p.Price.ToString(),
                              PaymentMethodId = p.PaymentMethodId,
                              StatusId = p.StatusId,
                              TaskId = p.TaskId,
                              Borc = borc,
                              Alınan = alinan,
                              StatusName = u.Name,
                              PaymentMethodName = u2.Name,
                              CariBakiye = alinan - borc,
                              CurrentGroupName = u3.Name
                          }).ToList();

            return result;
        }

        public List<Company> GetList()
        {
            return _companyRepository.GetAll();
        }

        public void TAdd(Company t)
        {
            _companyRepository.Insert(t);
        }

        public void TDelete(Company t)
        {
            _companyRepository.Delete(t);
        }

        public Company TGetById(int id)
        {
            return _companyRepository.GetById(id);
        }

        public void TUpdate(Company t)
        {
            _companyRepository.Update(t);
        }
    }
}
