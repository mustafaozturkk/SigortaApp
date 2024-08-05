using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class FileTask : BaseEntity
    {
        public int FilesId { get; set; }
        public Files Files { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
