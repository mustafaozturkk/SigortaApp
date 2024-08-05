using SigortaApp.Entity.Concrete;
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
    public class EFTypesRepository : GenericRepository<Types>, ITypesRepository
    {
        public EFTypesRepository(Context context) : base(context)
        {
        }

        public List<Types> GetListWithBrand()
        {
                return _context.Types.Include(x => x.Brand).Include(y => y.Category).ToList();
        }
    }
}
