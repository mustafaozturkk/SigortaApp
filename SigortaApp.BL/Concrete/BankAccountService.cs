using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBankAndCaseDetailsRepository _bankAndCaseDetailsRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ICompanyRepository _companyRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository, IPaymentRepository paymentRepository, IBankAndCaseDetailsRepository bankAndCaseDetailsRepository, IUnitRepository unitRepository, ICompanyRepository companyRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _paymentRepository = paymentRepository;
            _bankAndCaseDetailsRepository = bankAndCaseDetailsRepository;
            _unitRepository = unitRepository;
            _companyRepository = companyRepository;
        }

        public BankAccountDto GetBankAccountsWithPayment(int Id)
        {
            var payments = (from p in _paymentRepository.GetListAll()
                          join u in _unitRepository.GetListAll() on p.StatusId equals u.Id
                          join u2 in _unitRepository.GetListAll() on p.PaymentMethodId equals u2.Id
                          where (p.StatusId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Tahsilat || p.StatusId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Odeme)
                                && (p.PaymentMethodId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.KrediKartı || p.PaymentMethodId == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Havale) && p.BankAccountId == Id
                          select new PaymentDto
                          {
                              Id = p.Id,
                              CaseId = p.CaseId ?? 0,
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

            var banks = (from bc in _bankAndCaseDetailsRepository.GetListAll()
                         join u in _unitRepository.GetListAll() on bc.PaymentMethodId equals u.Id
                         where bc.BankId != null && bc.BankId.Value == Id && bc.CaseId == null
                         select new BankAndCaseDetailsDto
                         {
                             Id = bc.Id,
                             BankId = bc.BankId ?? null,
                             CaseId = bc.CaseId ?? null,
                             CreatedBy = bc.CreatedBy,
                             CreatedDate = bc.CreatedDate,
                             Date = bc.CreatedDate,
                             Description= bc.Description,
                             IsIn = bc.IsIn,
                             PaymentMethodId = bc.PaymentMethodId,
                             Price = bc.Price,
                             PaymentMethodName = u.Name
                         }).ToList();

            var bank = _bankAccountRepository.GetById(Id);

            BankAccountDto bankAccountDto = new BankAccountDto
            {
                BankAndCaseDetails = banks,
                Payments = payments,
                Bank = bank
            };

            return bankAccountDto;
        }

        public List<BankAccount> GetList()
        {
            return _bankAccountRepository.GetListAll();
        }

        public void TAdd(BankAccount t)
        {
            _bankAccountRepository.Insert(t);
        }

        public void TDelete(BankAccount t)
        {
            _bankAccountRepository.Delete(t);
        }

        public BankAccount TGetById(int id)
        {
            return _bankAccountRepository.GetById(id);
        }

        public void TUpdate(BankAccount t)
        {
            _bankAccountRepository.Update(t);
        }
    }
}
