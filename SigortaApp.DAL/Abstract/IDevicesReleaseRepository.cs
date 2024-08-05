﻿using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.Abstract
{
    public interface IDevicesReleaseRepository : IGenericRepository<DevicesRelease>
    {
        public List<DevicesReleaseUsersDto> GetListWithDevices();
    }
}