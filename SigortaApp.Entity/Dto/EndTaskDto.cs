using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class EndTaskDto : BaseEntityDto
    {
        public List<IFormFile> Files { get; set; }
        public string OtoTel { get; set; }
        public string BusDate { get; set; }
        public string[] Task { get; set; }
        public List<IFormFile> FilesParca { get; set; }
        public string OtobüsUcret { get; set; }

    }
}
