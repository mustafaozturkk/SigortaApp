using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SigortaApp.Entity.Concrete.Task;

namespace SigortaApp.BL.Abstract
{
    public interface IPaymentService : IGenericService<Payment>
    {
        List<PaymentDto> GetListByCompanyId(int companyId);
    }
}
