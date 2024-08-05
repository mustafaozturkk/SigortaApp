using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Payment : BaseEntity
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public int? CaseId { get; set; }
        public int? BankAccountId { get; set; }
        public int? TaskId { get; set; }
        public Task Task { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
