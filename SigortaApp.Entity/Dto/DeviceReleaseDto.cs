using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class DeviceReleaseDto : BaseEntityDto
    {
        public int DeviceId { get; set; }
        public Devices Devices { get; set; }
        public int GoingToUnit { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string ShippingPostCode { get; set; }
        public int Number { get; set; }
    }
}
