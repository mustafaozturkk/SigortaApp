using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class BankAndCaseDetailsDto : BankAndCaseDetails
    {
        public BankAccount BankAccount { get; set; }
        public string PaymentMethodName { get; set; }
        public bool IsVirman { get; set; }
        public int? BankVirman { get; set; }
        public int? CaseVirman { get; set; }
    }
}
