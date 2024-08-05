using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.Abstract
{
    public interface IDevicesRepository : IGenericRepository<Devices>
    {
        List<DevicesUsersDto> GetListWithBrandAndTypes();
        DevicesUsersDto GetDevicesWithBrandAndTypes(int id);
        DevicesUsersDto GetDevicesBySerialNumber(string serino);
    }
}
