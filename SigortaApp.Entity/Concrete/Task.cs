using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Task : BaseEntity
    {
        public int OrderCompanyId { get; set; }
        public Company Company { get; set; }
        public int SendCompanyId { get; set; }
        public int CompanyWillPayId { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime BusDate { get; set; }
        public int? Carrier { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TaskHistory> TaskHistorys { get; set; }
        public List<PaymentTask> PaymentTasks { get; set; }
    }
}
