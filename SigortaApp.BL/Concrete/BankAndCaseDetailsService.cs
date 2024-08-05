using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class BankAndCaseDetailsService : IBankAndCaseDetailsService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IBankAndCaseDetailsRepository _bankAndCaseDetailsRepository;

        public BankAndCaseDetailsService(ICompanyRepository companyRepository, IPaymentRepository paymentRepository, IUnitRepository unitRepository, IBankAndCaseDetailsRepository bankAndCaseDetailsRepository)
        {
            _companyRepository = companyRepository;
            _paymentRepository = paymentRepository;
            _unitRepository = unitRepository;
            _bankAndCaseDetailsRepository = bankAndCaseDetailsRepository;
        }

        public List<BankAndCaseDetails> GetList()
        {
            return _bankAndCaseDetailsRepository.GetAll();
        }

        public void TAdd(BankAndCaseDetails t)
        {
            _bankAndCaseDetailsRepository.Insert(t);
        }

        public void TDelete(BankAndCaseDetails t)
        {
            _bankAndCaseDetailsRepository.Delete(t);
        }

        public BankAndCaseDetails TGetById(int id)
        {
            return _bankAndCaseDetailsRepository.GetById(id);
        }

        public void TUpdate(BankAndCaseDetails t)
        {
            _bankAndCaseDetailsRepository.Update(t);
        }

        public CaseDto GetCaseWithPayment(int Id)
        {
            var payments = (from p in _paymentRepository.GetListAll()
                            join u in _unitRepository.GetListAll() on p.StatusId equals u.Id
                            join u2 in _unitRepository.GetListAll() on p.PaymentMethodId equals u2.Id
                            where (p.StatusId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Tahsilat || p.StatusId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Odeme)
                                  /*&& (p.PaymentMethodId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Nakit)*/ && p.CaseId == Id
                            select new PaymentDto
                            {
                                Id = p.Id,
                                CaseId = p.CaseId.Value,
                                CreatedBy = p.CreatedBy,
                                CreatedDate = p.CreatedDate,
                                Description = p.Description,
                                Price = p.Price.ToString(),
                                PaymentMethodId = p.PaymentMethodId,
                                StatusId = p.StatusId,
                                TaskId = p.TaskId,
                                StatusName = u.Name,
                                PaymentMethodName = u2.Name,
                                CompanyId = p.CompanyId,
                                Company = _companyRepository.GetById(p.CompanyId),
                            }).ToList();

            var cases = (from bc in _bankAndCaseDetailsRepository.GetListAll()
                         join u in _unitRepository.GetListAll() on bc.PaymentMethodId equals u.Id
                         where bc.BankId == null && bc.BankId == null && bc.CaseId != null && bc.CaseId.Value == Id
                         select new BankAndCaseDetailsDto
                         {
                             Id = bc.Id,
                             BankId = bc.BankId ?? null,
                             CaseId = bc.CaseId ?? null,
                             CreatedBy = bc.CreatedBy,
                             CreatedDate = bc.CreatedDate,
                             Date = bc.CreatedDate,
                             Description = bc.Description,
                             IsIn = bc.IsIn,
                             PaymentMethodId = bc.PaymentMethodId,
                             Price = bc.Price,
                             PaymentMethodName = u.Name
                         }).ToList();

            var case1 = _unitRepository.GetById(Id);

            CaseDto caseDto = new CaseDto
            {
                CaseAccount = cases,
                Payments = payments,
                Name =case1.Name,
                IsActive = case1.IsActive,
                UnitTypesId = case1.UnitTypesId,
                UstId = case1.UstId,
                Id = case1.Id
            };

            return caseDto;
        }
    }
}
