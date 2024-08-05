using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SigortaApp.Entity.Concrete.Task;

namespace SigortaApp.DAL.EntityFramework
{
    public class EFTaskRepository : GenericRepository<Task>, ITaskRepository
    {
        private readonly Context _context;

        public EFTaskRepository(Context context) : base(context) => _context = context;

        //public List<TaskDto> GetListWithCompanyAndCityAndStatus()
        //{
        //    using (var c = _context)
        //    {
        //        return c.Task.Include(x => x.Company).Include(y => y.City).Include(z => z.Status).ToList().Join(c.Users.ToList(), task => task.CreatedBy, user => user.Id, (task, user) => new TaskDto
        //        {
        //            Id = task.Id,
        //            City = task.City,
        //            CreatedBy = task.CreatedBy,
        //            CityId = task.CityId,
        //            OrderCompany = c.Company.Where(w => w.Id == task.OrderCompanyId).FirstOrDefault(),
        //            OrderCompanyId = task.OrderCompanyId,
        //            CreatedDate = task.CreatedDate,
        //            BusDate = task.BusDate,
        //            Description = task.Description,
        //            Carrier = task.Carrier.Value,
        //            CarrierName = c.Users.Where(w => w.Id == task.Carrier).Select(s => s.NameSurname).FirstOrDefault(),
        //            Status = task.Status,
        //            StatusId = task.StatusId,
        //            //UserName = user.NameSurname,
        //            IsActive = task.IsActive,
        //            SendCompany = c.Company.Where(w => w.Id == task.SendCompanyId).FirstOrDefault(),
        //            SendCompanyId = task.SendCompanyId,
        //            TaskHistorys = c.TaskHistory.Include(th => th.Status).Where(w => w.TaskId == task.Id).ToList(),
        //            CompanyWillPayId =task.CompanyWillPayId,
        //            AliciMi = task.CompanyWillPayId == task.OrderCompanyId ? "Alıcı" : "Gönderici",
        //            PaymentTasks = c.PaymentTask.Include(x => x.Company).Where(w => w.TaskId == task.Id).ToList()
        //            //DevicesTypes = new List<DevicesUsersDto>()
        //        }).ToList();
        //    }
        //}

        //public List<TaskDto> GetListWithCompanyAndCityAndStatus2()
        //{

        //    var result = (from task in _tas )

        //    return result;

        //    //using (var c = _context)
        //    //{
        //    //    return c.Task.Include(x => x.Company).Include(y => y.City).Include(z => z.Status).ToList().Join(c.Users.ToList(), task => task.CreatedBy, user => user.Id, (task, user) => new TaskDto
        //    //    {
        //    //        Id = task.Id,
        //    //        City = task.City,
        //    //        CreatedBy = task.CreatedBy,
        //    //        CityId = task.CityId,
        //    //        OrderCompany = c.Company.Where(w => w.Id == task.OrderCompanyId).FirstOrDefault(),
        //    //        OrderCompanyId = task.OrderCompanyId,
        //    //        CreatedDate = task.CreatedDate,
        //    //        BusDate = task.BusDate,
        //    //        Description = task.Description,
        //    //        Carrier = task.Carrier,
        //    //        CarrierName = c.Users.Where(w => w.Id == task.Carrier).Select(s => s.NameSurname).FirstOrDefault(),
        //    //        Status = task.Status,
        //    //        StatusId = task.StatusId,
        //    //        //UserName = user.NameSurname,
        //    //        IsActive = task.IsActive,
        //    //        SendCompany = c.Company.Where(w => w.Id == task.SendCompanyId).FirstOrDefault(),
        //    //        SendCompanyId = task.SendCompanyId,
        //    //        TaskHistorys = c.TaskHistory.Include(th => th.Status).Where(w => w.TaskId == task.Id).ToList(),
        //    //        CompanyWillPayId = task.CompanyWillPayId,
        //    //        AliciMi = task.CompanyWillPayId == task.OrderCompanyId ? "Alıcı" : "Gönderici",
        //    //        PaymentTasks = c.PaymentTask.Include(x => x.Company).Where(w => w.TaskId == task.Id).ToList()
        //    //        //DevicesTypes = new List<DevicesUsersDto>()
        //    //    }).ToList();
        //    //}
        //}
    }
}
