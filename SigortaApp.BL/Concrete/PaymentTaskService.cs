using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class PaymentTaskService : IPaymentTaskService
    {
        private readonly IPaymentTaskRepository _paymentTaskRepository;
        private readonly ICompanyRepository _companyRepository;

        public PaymentTaskService(IPaymentTaskRepository paymentTaskRepository, ICompanyRepository companyRepository)
        {
            _paymentTaskRepository = paymentTaskRepository;
            _companyRepository = companyRepository;
        }

        public List<PaymentTask> GetList()
        {
            return _paymentTaskRepository.GetAll();
        }

        public List<PaymentTask> GetListWithCompany()
        {
            var result = (from pt in _paymentTaskRepository.GetListAll()
                          select new PaymentTask
                          {
                              Id = pt.Id,
                              CompanyId = pt.CompanyId,
                              CariMi = pt.CariMi,
                              CreatedBy = pt.CreatedBy,
                              CreatedDate = pt.CreatedDate,
                              PaymentReceived = pt.PaymentReceived,
                              Price = pt.Price,
                              TaskId = pt.TaskId,
                              Company = _companyRepository.GetById(pt.CompanyId),
                          }).ToList();
            return result;
        }

        public void TAdd(PaymentTask t)
        {
            _paymentTaskRepository.Insert(t);
        }

        public void TDelete(PaymentTask t)
        {
            _paymentTaskRepository.Delete(t);
        }

        public PaymentTask TGetById(int id)
        {
            return _paymentTaskRepository.GetById(id);
        }

        public void TUpdate(PaymentTask t)
        {
            _paymentTaskRepository.Update(t);
        }
    }
}
