using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;

namespace SigortaApp.Entity.Dto
{
    public class DevicesUsersDto : BaseEntityDto
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int TypesId { get; set; }
        public Types Types { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public int CupboardNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public int Number { get; set; }
        public List<DevicesUsersDto> DevicesTypes { get; set; }
    }
}
