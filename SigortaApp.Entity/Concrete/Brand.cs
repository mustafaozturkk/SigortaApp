using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public List<Devices> Devices { get; set; }
        public List<Types> Types { get; set; }
    }
}
