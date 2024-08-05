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
    public class EFCompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly Context _context;

        public EFCompanyRepository(Context context) : base(context) => _context = context;
    }
}
