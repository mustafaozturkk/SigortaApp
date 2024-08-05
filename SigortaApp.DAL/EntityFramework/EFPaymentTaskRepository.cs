using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.EntityFramework
{
    public class EFPaymentTaskRepository : GenericRepository<PaymentTask>, IPaymentTaskRepository
    {
        public EFPaymentTaskRepository(Context context) : base(context)
        {
        }

        public List<PaymentTask> GetListWithCompany()
        {
            using (var c = _context)
            {
                return c.PaymentTask.Include(x => x.Company).ToList();
            }
        }
    }
}
