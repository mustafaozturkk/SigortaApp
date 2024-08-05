using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Unit : BaseEntity
    {
        public int? UstId { get; set; }
        public string Name { get; set; }
        public int UnitTypesId { get; set; }
        public UnitTypes UnitTypes { get; set; }
        public bool IsActive { get; set; }
    }
}
