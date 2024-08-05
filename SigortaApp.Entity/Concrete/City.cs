using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int? UstId { get; set; }
        public int Controller { get; set; }
        public List<IdentityUser> Users { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
