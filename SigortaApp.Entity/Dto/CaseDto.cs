using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class CaseDto : Unit
    {
        public decimal AccountPrice { get; set; }
        public List<BankAndCaseDetailsDto> CaseAccount { get; set; }
        public List<PaymentDto> Payments { get; set; }
        public string PaymentMethodName { get; set; }
        public int PaymentMethodId { get; set; }
        public bool IsVirman { get; set; }
        public int? BankVirman { get; set; }
        public int? CaseVirman { get; set; }
    }
}
