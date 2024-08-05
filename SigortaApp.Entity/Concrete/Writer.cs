using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Writer : BaseEntity
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public List<Blog> Blogs { get; set; }
        public virtual ICollection<Message2> Sender { get; set; }
        public virtual ICollection<Message2> Receiver { get; set; }
    }
}
