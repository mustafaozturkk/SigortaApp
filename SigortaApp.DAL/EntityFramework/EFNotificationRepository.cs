﻿using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.EntityFramework
{
    public class EFNotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public EFNotificationRepository(Context context) : base(context)
        {
        }
    }
}
