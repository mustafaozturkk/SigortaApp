using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class BankAndCaseDetails : BaseEntity
    {
        public int? BankId { get; set; }
        public int? CaseId { get; set; }
        public bool IsIn { get; set; }
        public int PaymentMethodId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
