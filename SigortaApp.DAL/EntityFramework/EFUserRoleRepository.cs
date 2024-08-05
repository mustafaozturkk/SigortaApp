using System;
using SigortaApp.Entity;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.Repositories;

namespace SigortaApp.DAL;

public class EFUserRoleRepository : GenericRepository<AppUserRole>, IUserRoleRepository
{
    public EFUserRoleRepository(Context context) : base(context)
    {
    }
}
