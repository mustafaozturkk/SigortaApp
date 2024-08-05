using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class BlogRayting :BaseEntity
    {
        public int BlogId { get; set; }
        public int TotalScore { get; set; }
        public int RaytingCount { get; set; }
    }
}
