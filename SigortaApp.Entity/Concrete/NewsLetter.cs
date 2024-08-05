using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class NewsLetter : BaseEntity
    {
        public string Mail { get; set; }
        public bool Status { get; set; }
    }
}
