using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int CurrentGroupId { get; set; }
        public int? PersonId { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Task> Tasks { get; set; }
        public List<PaymentTask> PaymentTasks { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
