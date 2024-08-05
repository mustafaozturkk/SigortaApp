using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Concrete
{
    public class DevicesRelease : BaseEntity
    {
        public int DeviceId { get; set; }
        public Devices Devices { get; set; }
        public int GoingToUnit { get; set; }
        public string Description { get; set; }
        public string ShippingPostCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
