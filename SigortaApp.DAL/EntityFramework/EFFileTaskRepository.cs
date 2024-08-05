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
    public class EFFileTaskRepository : GenericRepository<FileTask>, IFileTaskRepository
    {
        public EFFileTaskRepository(Context context) : base(context)
        {
        }
    }
}
