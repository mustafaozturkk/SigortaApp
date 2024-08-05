using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Abstract
{
    public interface IBankAccountService : IGenericService<BankAccount>
    {
        BankAccountDto GetBankAccountsWithPayment(int Id);
    }
}
