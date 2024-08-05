using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class TaskService : ITaskService
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

        public TaskService(ITaskRepository taskRepository, ICompanyRepository companyRepository, IUserRepository userRepository, IStatusRepository statusRepository, ICityRepository cityRepository, ITaskHistoryRepository taskHistoryRepository, IPaymentTaskRepository paymentTaskRepository, IFilesRepository filesRepository, IFileTaskRepository fileTaskRepository)
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
        }

        public List<Entity.Concrete.Task> GetList()
        {
            return _taskRepository.GetListAll();
        }

        public TaskDto GetTaskAndCityByTaskId(int Id)
        {
            var result = (from t in _taskRepository.GetListAll()
                          join c in _cityRepository.GetListAll() on t.CityId equals c.Id
                          join c2 in _cityRepository.GetListAll() on c.UstId equals c2.Id into ustunit
                          from c2 in ustunit.DefaultIfEmpty()
                          where t.Id == Id
                          select new TaskDto
                          {
                              Id = Id,
                              CompanyWillPayId = t.CompanyWillPayId,
                              BusDate = t.BusDate,
                              CityId = t.CityId,
                              City = c,
                              CityUst = c2,
                              CityUstId = c.UstId ?? 0,
                              Carrier = t.Carrier,
                              CreatedBy = t.CreatedBy,
                              CreatedDate = t.CreatedDate,
                              OrderCompanyId = t.OrderCompanyId,
                              SendCompanyId = t.SendCompanyId,
                              StatusId = t.StatusId,
                              IsActive = t.IsActive,
                              Description = t.Description,
                          }).FirstOrDefault();
            return result;
        }


        public List<TaskDto> GetListWithCompanyCityAndStatus()
        {
            var result = (from task in _taskRepository.GetListAll()
                          join user in _userRepository.GetListAll() on task.CreatedBy equals user.Id
                          join user2 in _userRepository.GetListAll() on task.Carrier equals user2.Id into us
                          from user2 in us.DefaultIfEmpty()
                          join companyOrder in _companyRepository.GetListAll() on task.OrderCompanyId equals companyOrder.Id
                          join companySend in _companyRepository.GetListAll() on task.SendCompanyId equals companySend.Id
                          join status in _statusRepository.GetListAll() on task.StatusId equals status.Id
                          join city in _cityRepository.GetListAll() on task.CityId equals city.Id
                          join city2 in _cityRepository.GetListAll() on city.UstId equals city2.Id into cit2
                          from city2 in cit2.DefaultIfEmpty()
                          select new TaskDto
                          {
                              Id = task.Id,
                              City = city,
                              CreatedBy = task.CreatedBy,
                              CityId = task.CityId,
                              CityUst = cit2.FirstOrDefault(),
                              OrderCompany = companyOrder,
                              OrderCompanyId = task.OrderCompanyId,
                              CreatedDate = task.CreatedDate,
                              BusDate = task.BusDate,
                              Description = task.Description,
                              Carrier = task.Carrier ?? null,
                              CarrierName = task.Carrier != null && user2 != null ? user2.NameSurname : "Boşta",
                              Status = status != null ? status : null,
                              StatusId = task.StatusId,
                              IsActive = task.IsActive,
                              SendCompany = companySend,
                              SendCompanyId = task.SendCompanyId,
                              CompanyWillPayId = task.CompanyWillPayId,
                              AliciMi = task.CompanyWillPayId == task.SendCompanyId ? "Alıcı" : "Gönderici",
                          }).ToList();
            return result;
        }

        public List<TaskDto> GetListWithCompanyAndCityAndStatus2()
        {
            var result = (from task in _taskRepository.GetListAll()
                          join user in _userRepository.GetListAll() on task.CreatedBy equals user.Id
                          join user2 in _userRepository.GetListAll() on task.Carrier equals user2.Id into us
                          from user2 in us.DefaultIfEmpty()
                          join companyOrder in _companyRepository.GetListAll() on task.OrderCompanyId equals companyOrder.Id
                          join companySend in _companyRepository.GetListAll() on task.SendCompanyId equals companySend.Id
                          join status in _statusRepository.GetListAll() on task.StatusId equals status.Id
                          join city in _cityRepository.GetListAll() on task.CityId equals city.Id
                          select new TaskDto
                          {
                              Id = task.Id,
                              City = city,
                              CreatedBy = task.CreatedBy,
                              CityId = task.CityId,
                              OrderCompany = companyOrder,
                              OrderCompanyId = task.OrderCompanyId,
                              CreatedDate = task.CreatedDate,
                              BusDate = task.BusDate,
                              Description = task.Description,
                              Carrier = task.Carrier ?? null,
                              CarrierName = task.Carrier != null && user2 != null ? user2.NameSurname : "Boşta",
                              Status = status != null ? status : null,
                              StatusId = task.StatusId,
                              IsActive = task.IsActive,
                              SendCompany = companySend,
                              SendCompanyId = task.SendCompanyId,
                              CompanyWillPayId = task.CompanyWillPayId,
                              AliciMi = task.CompanyWillPayId == task.OrderCompanyId ? "Alıcı" : "Gönderici",
                              TaskHistorys = (from th in _taskHistoryRepository.GetListAll()
                                              join st in _statusRepository.GetListAll() on th.StatusId equals st.Id
                                              where th.TaskId == task.Id
                                              select new TaskHistoryDto
                                              {
                                                  Id = th.Id,
                                                  TaskId = th.TaskId,
                                                  IsActive = th.IsActive,
                                                  Carrier = th.Carrier,
                                                  Status = st,
                                                  PutOnHoldDate = th.PutOnHoldDate,
                                                  CreatedBy = th.CreatedBy,
                                                  CreatedDate = th.CreatedDate,
                                                  StatusId = th.StatusId,
                                                  Description = th.Description,
                                                  CarrierName = th.Carrier != null ? _userRepository.GetListAll().Where(w => w.Id == th.Carrier).Select(s => s.NameSurname).FirstOrDefault() : "Boşta"
                                              }).ToList(),
                              PaymentTasks = (from paymenttask in _paymentTaskRepository.GetListAll()
                                              where paymenttask.TaskId == task.Id
                                              select new PaymentTask
                                              {
                                                  Id = paymenttask.Id,
                                                  TaskId = paymenttask.TaskId,
                                                  CariMi = paymenttask.CariMi,
                                                  Company = _companyRepository.GetListAll().Where(w => w.Id == task.CompanyWillPayId).FirstOrDefault(),
                                                  CompanyId = paymenttask.CompanyId,
                                                  CreatedBy = task.CreatedBy,
                                                  CreatedDate = paymenttask.CreatedDate,
                                                  PaymentReceived = paymenttask.PaymentReceived,
                                                  Price = paymenttask.Price,
                                              }).ToList(),
                              files = (from files in _filesRepository.GetListAll()
                                       join filetask in _fileTaskRepository.GetListAll() on files.Id equals filetask.FilesId
                                       where filetask.TaskId == task.Id
                                       select new Files
                                       {
                                           Id = files.Id,
                                           CreatedBy = files.CreatedBy,
                                           CreatedOn = files.CreatedOn,
                                           DataFiles = files.DataFiles,
                                           FileType = files.FileType,
                                           IsActive = files.IsActive,
                                           Name = files.Name
                                       }).ToList(),
                          }).ToList();
            return result;
        }



        public void TAdd(Entity.Concrete.Task t)
        {
            _taskRepository.Insert(t);
        }

        public void TDelete(Entity.Concrete.Task t)
        {
            throw new NotImplementedException();
        }

        public Entity.Concrete.Task TGetById(int id)
        {
            return _taskRepository.GetById(id);
        }

        public void TUpdate(Entity.Concrete.Task t)
        {
            _taskRepository.Update(t);
        }

        public TaskDto GetTaskWithCompanyAndCityAndStatusByTaskId(int Id)
        {
            var result = (from task in _taskRepository.GetListAll()
                          join user in _userRepository.GetListAll() on task.CreatedBy equals user.Id
                          join user2 in _userRepository.GetListAll() on task.Carrier equals user2.Id into us
                          from user2 in us.DefaultIfEmpty()
                          let companyOrder = _companyRepository.GetById(task.OrderCompanyId)
                          let companySend = _companyRepository.GetById(task.SendCompanyId)
                          let status = _statusRepository.GetById(task.StatusId)
                          let city = _cityRepository.GetById(task.CityId)
                          where task.Id == Id
                          select new TaskDto
                          {
                              Id = task.Id,
                              City = city,
                              CreatedBy = task.CreatedBy,
                              CityId = task.CityId,
                              OrderCompany = companyOrder,
                              OrderCompanyId = task.OrderCompanyId,
                              CreatedDate = task.CreatedDate,
                              BusDate = task.BusDate,
                              Description = task.Description,
                              Carrier = task.Carrier ?? null,
                              CarrierName = task.Carrier != null && user2 != null ? user2.NameSurname : "Boşta",
                              Status = status != null ? status : null,
                              StatusId = task.StatusId,
                              IsActive = task.IsActive,
                              SendCompany = companySend,
                              SendCompanyId = task.SendCompanyId,
                              CompanyWillPayId = task.CompanyWillPayId,
                              AliciMi = task.CompanyWillPayId == task.OrderCompanyId ? "Alıcı" : "Gönderici",
                              TaskHistorys = (from th in _taskHistoryRepository.GetListAll()
                                              let st = _statusRepository.GetById(th.StatusId)
                                              //join st in _statusRepository.GetListAll() on th.StatusId equals st.Id
                                              where th.TaskId == task.Id
                                              select new TaskHistoryDto
                                              {
                                                  Id = th.Id,
                                                  TaskId = th.TaskId,
                                                  IsActive = th.IsActive,
                                                  Carrier = th.Carrier,
                                                  Status = st,
                                                  PutOnHoldDate = th.PutOnHoldDate,
                                                  CreatedBy = th.CreatedBy,
                                                  CreatedDate = th.CreatedDate,
                                                  StatusId = th.StatusId,
                                                  Description = th.Description,
                                                  CarrierName = th.Carrier != null ? _userRepository.GetListAll().Where(w => w.Id == th.Carrier).Select(s => s.NameSurname).FirstOrDefault() : "Boşta"
                                              }).ToList(),
                              files = (from files in _filesRepository.GetListAll()
                                       join filetask in _fileTaskRepository.GetListAll() on files.Id equals filetask.FilesId
                                       where filetask.TaskId == task.Id
                                       select new Files
                                       {
                                           Id = files.Id,
                                           CreatedBy = files.CreatedBy,
                                           CreatedOn = files.CreatedOn,
                                           DataFiles = files.DataFiles,
                                           FileType = files.FileType,
                                           IsActive = files.IsActive,
                                           Name = files.Name
                                       }).ToList(),
                          }).FirstOrDefault();
            return result;
        }
    }
}
