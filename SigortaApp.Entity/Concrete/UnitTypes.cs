using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class UnitTypes : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Unit> Units { get; set; }
    }
}
