using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class PaymentTask : BaseEntity
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public bool CariMi { get; set; }
        public double Price { get; set; }
        public bool PaymentReceived { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
