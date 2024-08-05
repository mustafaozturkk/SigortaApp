﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Devices> Devices { get; set; }

    }
}
