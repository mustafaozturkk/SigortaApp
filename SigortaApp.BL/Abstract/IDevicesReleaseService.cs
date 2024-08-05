using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Abstract
{
    public interface IDevicesReleaseService : IGenericService<DevicesRelease>
    {
        public List<DevicesReleaseUsersDto> GetListWithDevices();
    }
}
