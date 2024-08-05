using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class DevicesReleaseUsersDto : BaseEntityDto
    {
        public int DeviceId { get; set; }
        public Devices Devices { get; set; }
        public int GoingToUnit { get; set; }
        public string GoingToUnitName { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string ShippingPostCode { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int TypesId { get; set; }
        public Types Types { get; set; }
        public int CupboardNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
    }
}
