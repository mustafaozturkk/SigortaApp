using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.EntityFramework
{
    public class EFDevicesRepository : GenericRepository<Devices>, IDevicesRepository
    {
        private readonly Context _context;

        public EFDevicesRepository(Context context) : base(context) => _context = context;

        public List<DevicesUsersDto> GetListWithBrandAndTypes()
        {
            using (var c = _context)
            {
                return c.Devices.Include(x => x.Category).Include(y => y.Brand).Include(z => z.Types).ToList().Join(c.Users.ToList(), device => device.CreatedBy, user => user.Id, (device, user) => new DevicesUsersDto 
                {
                    Id = device.Id,
                    Brand = device.Brand,
                    CreatedBy = device.CreatedBy,
                    BrandId = device.BrandId,
                    Category = device.Category,
                    CategoryId = device.CategoryId,
                    CreatedDate = device.CreatedDate,
                    CupboardNumber = device.CupboardNumber,
                    Description = device.Description,
                    SerialNumber = device.SerialNumber,
                    Types = device.Types,
                    TypesId = device.TypesId,
                    UserName = user.NameSurname,
                    IsActive = device.IsActive,
                    DevicesTypes =new List<DevicesUsersDto>()
                }).ToList();
            }
        }

        public DevicesUsersDto GetDevicesWithBrandAndTypes(int id)
        {
            using (var c = _context)
            {
                return c.Devices.Where(w => w.Id == id).Include(x => x.Category).Include(y => y.Brand).Include(z => z.Types).ToList().Join(c.Users.ToList(), device => device.CreatedBy, user => user.Id, (device, user) => new DevicesUsersDto
                {
                    Id = device.Id,
                    Brand = device.Brand,
                    CreatedBy = device.CreatedBy,
                    BrandId = device.BrandId,
                    Category = device.Category,
                    CategoryId = device.CategoryId,
                    CreatedDate = device.CreatedDate,
                    CupboardNumber = device.CupboardNumber,
                    Description = device.Description,
                    SerialNumber = device.SerialNumber,
                    Types = device.Types,
                    TypesId = device.TypesId,
                    UserName = user.NameSurname
                }).FirstOrDefault();
            }
        }

        public DevicesUsersDto GetDevicesBySerialNumber(string serino)
        {
            using (var c = _context)
            {
                return c.Devices.Where(w => w.SerialNumber == serino).Include(x => x.Category).Include(y => y.Brand).Include(z => z.Types).ToList().Join(c.Users.ToList(), device => device.CreatedBy, user => user.Id, (device, user) => new DevicesUsersDto
                {
                    Id = device.Id,
                    Brand = device.Brand,
                    CreatedBy = device.CreatedBy,
                    BrandId = device.BrandId,
                    Category = device.Category,
                    CategoryId = device.CategoryId,
                    CreatedDate = device.CreatedDate,
                    CupboardNumber = device.CupboardNumber,
                    Description = device.Description,
                    SerialNumber = device.SerialNumber,
                    Types = device.Types,
                    TypesId = device.TypesId,
                    UserName = user.NameSurname
                }).FirstOrDefault();
            }
        }
    }
}
