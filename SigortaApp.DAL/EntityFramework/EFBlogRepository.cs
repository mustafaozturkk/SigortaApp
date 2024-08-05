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
    public class EFBlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly Context _context;
        public EFBlogRepository(Context context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetListWithCategory()
        {
            using (var c = _context)
            {
                return c.Blogs.Include(x => x.Category).ToList();
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int Id)
        {
            using (var c = _context)
            {
                return c.Blogs.Include(x => x.Category).Where(x => x.WriterId == Id).ToList();
            }
        }
    }
}
