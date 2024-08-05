using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class BaseEntityDto
    {
        [Key]
        public int Id { get; set; }
    }
}
