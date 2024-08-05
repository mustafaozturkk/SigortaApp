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
    public class EFMessage2Repository : GenericRepository<Message2>, IMessage2Repository
    {
        private readonly Context _context;

        public EFMessage2Repository(Context context) : base(context) => _context = context;

        public List<Message2> GetSendBoxWithMessageByWriter(int id)
        {
            using (var c = _context)
            {
                return c.Message2s.Include(x => x.ReceiverUser).Where(w => w.SenderId == id).ToList();
            }
        }

        public List<Message2> GetInBoxWithMessageByWriter(int id)
        {
            using (var c = _context)
            {
                return c.Message2s.Include(w => w.SenderUser).Where(w => w.ReveiverId == id).ToList();
            }
        }
    }
}
