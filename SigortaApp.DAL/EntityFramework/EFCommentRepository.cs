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
    public class EFCommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly Context _context;

        public EFCommentRepository(Context context) : base(context) => _context = context;

        public List<Comment> GetListWithBlog()
        {
            using (var c = _context)
            {
                return c.Comments.Include(x => x.Blog).ToList();
            }
        }
    }
}
