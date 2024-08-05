using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class BankAccountDto : BaseEntityDto
    {
        public List<PaymentDto> Payments { get; set; }
        public BankAccount Bank { get; set; }
        public List<BankAndCaseDetailsDto> BankAndCaseDetails { get; set; }
    }
}
