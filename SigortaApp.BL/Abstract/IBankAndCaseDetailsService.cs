using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Abstract
{
    public interface IBankAndCaseDetailsService : IGenericService<BankAndCaseDetails>
    {
        CaseDto GetCaseWithPayment(int Id);
    }
}
