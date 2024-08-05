using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.EntityFramework
{
    public class EFStatusRepository : GenericRepository<Status>, IStatusRepository
    {
        private readonly Context _context;

        public EFStatusRepository(Context context) : base(context) => _context = context;
    }
}
