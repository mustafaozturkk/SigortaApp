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
    public class DevicesReleaseManager : IDevicesReleaseService
    {
        IDevicesReleaseRepository _devicesReleaseDal;

        public DevicesReleaseManager(IDevicesReleaseRepository devicesReleaseDal)
        {
            _devicesReleaseDal = devicesReleaseDal;
        }

        public List<DevicesRelease> GetList()
        {
            return _devicesReleaseDal.GetListAll();
        }

        public List<DevicesReleaseUsersDto> GetListWithDevices()
        {
            return _devicesReleaseDal.GetListWithDevices();
        }

        public void TAdd(DevicesRelease t)
        {
            _devicesReleaseDal.Insert(t);
        }

        public void TDelete(DevicesRelease t)
        {
            _devicesReleaseDal.Delete(t);
        }

        public DevicesRelease TGetById(int id)
        {
            return _devicesReleaseDal.GetById(id);
        }

        public void TUpdate(DevicesRelease t)
        {
            _devicesReleaseDal.Update(t);
        }
    }
}
