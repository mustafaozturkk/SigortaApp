using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class DevicesService : IDevicesService
    {
        IDevicesRepository _devicesDal;

        public DevicesService(IDevicesRepository devicesDal)
        {
            _devicesDal = devicesDal;
        }

        public List<Devices> GetList()
        {
            return _devicesDal.GetListAll();
        }

        public List<DevicesUsersDto> GetListWithBrandAndTypes()
        {
            return _devicesDal.GetListWithBrandAndTypes();
        }

        public DevicesUsersDto GetDevicesWithBrandAndTypes(int id)
        {
            return _devicesDal.GetDevicesWithBrandAndTypes(id);
        }

        public void TAdd(Devices t)
        {
            _devicesDal.Insert(t);
        }

        public void TDelete(Devices t)
        {
            _devicesDal.Delete(t);
        }

        public Devices TGetById(int id)
        {
            return _devicesDal.GetById(id);
        }

        public void TUpdate(Devices t)
        {
            _devicesDal.Update(t);
        }

        public DevicesUsersDto GetDevicesBySerialNumber(string serino)
        {
            return _devicesDal.GetDevicesBySerialNumber(serino);
        }
    }
}
