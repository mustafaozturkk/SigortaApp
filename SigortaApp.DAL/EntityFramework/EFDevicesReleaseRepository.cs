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
    public class EFDevicesReleaseRepository : GenericRepository<DevicesRelease>, IDevicesReleaseRepository
    {
        private readonly Context _context;

        public EFDevicesReleaseRepository(Context context) : base(context) => _context = context;

        public List<DevicesReleaseUsersDto> GetListWithDevices()
        {
            

            using (var c = _context)
            {
                var result = (from dre in c.DevicesRelease
                              join d in c.Devices on dre.DeviceId equals d.Id
                              join ca in c.Categories on d.CategoryId equals ca.Id
                              join br in c.Brands on d.BrandId equals br.Id
                              join ty in c.Types on d.TypesId equals ty.Id
                              join un in c.Unit on dre.GoingToUnit equals un.Id
                              join us in c.Users on dre.CreatedBy equals us.Id
                              select new DevicesReleaseUsersDto
                              {
                                  Id = dre.Id,
                                  BrandId = br.Id,
                                  TypesId = ty.Id,
                                  CategoryId = ca.Id,
                                  Brand = d.Brand,
                                  Category = d.Category,
                                  Types = d.Types,
                                  CreatedBy = dre.CreatedBy,
                                  CreatedDate = dre.CreatedDate,
                                  Description = dre.Description,
                                  GoingToUnit = un.Id,
                                  GoingToUnitName = un.Name,
                                  ShippingPostCode = dre.ShippingPostCode,
                                  UserName = us.NameSurname,
                                  SerialNumber = d.SerialNumber

                              }).ToList();

                return result;
                //return c.DevicesRelease.Include(a => a.Devices).Include(x => x.Category).Include(y => y.Brand).Include(z => z.Types).ToList().Join(c.Users.ToList(), device => device.CreatedBy, user => user.Id, (device, user) => new DevicesReleaseUsersDto 
                //{
                //    Id = device.Id,
                //    Brand = device.Brand,
                //    CreatedBy = device.CreatedBy,
                //    BrandId = device.BrandId,
                //    Category = device.Category,
                //    CategoryId = device.CategoryId,
                //    CreatedDate = device.CreatedDate,
                //    CupboardNumber = device.CupboardNumber,
                //    Description = device.Description,
                //    SerialNumber = device.SerialNumber,
                //    Types = device.Types,
                //    TypesId = device.TypesId,
                //    UserName = user.NameSurname
                //}).ToList();
            }
        }
    }
}
