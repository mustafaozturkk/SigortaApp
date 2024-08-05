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
using Task = SigortaApp.Entity.Concrete.Task;

namespace SigortaApp.DAL.EntityFramework
{
    public class EFPaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly Context _context;

        public EFPaymentRepository(Context context) : base(context) => _context = context;

    }
}
