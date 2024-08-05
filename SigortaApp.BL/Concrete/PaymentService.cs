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
    public class PaymentService : IPaymentService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ITaskHistoryRepository _taskHistoryRepository;
        private readonly IPaymentTaskRepository _paymentTaskRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly IFileTaskRepository _fileTaskRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitRepository _unitRepository;

        public PaymentService(ITaskRepository taskRepository, ICompanyRepository companyRepository, IUserRepository userRepository, IStatusRepository statusRepository, ICityRepository cityRepository, ITaskHistoryRepository taskHistoryRepository, IPaymentTaskRepository paymentTaskRepository, IFilesRepository filesRepository, IFileTaskRepository fileTaskRepository, IPaymentRepository paymentRepository, IUnitRepository unitRepository)
        {
            _taskRepository = taskRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _cityRepository = cityRepository;
            _taskHistoryRepository = taskHistoryRepository;
            _paymentTaskRepository = paymentTaskRepository;
            _filesRepository = filesRepository;
            _fileTaskRepository = fileTaskRepository;
            _paymentRepository = paymentRepository;
            _unitRepository = unitRepository;
        }

        public List<PaymentDto> GetListByCompanyId(int companyId)
        {
            var result = (from pay in _paymentRepository.GetListAll()
                          join com in _companyRepository.GetListAll() on pay.CompanyId equals com.Id
                          join u in _unitRepository.GetListAll() on pay.StatusId equals u.Id
                          join u2 in _unitRepository.GetListAll() on pay.PaymentMethodId equals u2.Id
                          let alinan = _paymentRepository.GetListAll().Where(w => w.CompanyId == companyId && (w.StatusId == 1 || w.StatusId == 3)).Sum(s => s.Price)
                          let borc = _paymentRepository.GetListAll().Where(w => w.CompanyId == companyId && (w.StatusId == 2 || w.StatusId == 4)).Sum(s => s.Price)
                          where com.Id == companyId
                          select new PaymentDto
                          {
                              Id = pay.Id,
                              CompanyId = companyId,
                              StatusId = pay.StatusId,
                              CariBakiye = alinan - borc,
                              Alınan = alinan,
                              Borc = borc,
                              CaseId = pay.CaseId ?? 0,
                              Company = com,
                              Description = pay.Description,
                              PaymentMethodId = pay.PaymentMethodId,
                              Price = pay.Price.ToString(),
                              StatusName = u.Name,
                              PaymentMethodName = u2.Name,
                              CreatedBy = pay.CreatedBy,
                              CreatedDate = pay.CreatedDate,
                          }).ToList();
            return result;
        }
        public List<Payment> GetList()
        {
            return _paymentRepository.GetListAll();
        }

        public void TAdd(Payment p)
        {
            _paymentRepository.Insert(p);
        }

        public void TDelete(Payment p)
        {
            _paymentRepository.Delete(p);
        }

        public Payment TGetById(int id)
        {
            return _paymentRepository.GetById(id);
        }

        public void TUpdate(Payment p)
        {
            _paymentRepository.Update(p);
        }
    }
}
