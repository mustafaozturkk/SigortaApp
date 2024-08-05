using System;
using System.Collections.Generic;
using SigortaApp.Entity;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL;

namespace SigortaApp.BL;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public List<AppUserRole> GetList()
    {
        return _userRoleRepository.GetListAll();
    }

    public void TAdd(AppUserRole t)
    {
        throw new NotImplementedException();
    }

    public void TDelete(AppUserRole t)
    {
        throw new NotImplementedException();
    }

    public AppUserRole TGetById(int id)
    {
        return _userRoleRepository.GetById(id);
    }

    public void TUpdate(AppUserRole t)
    {
        throw new NotImplementedException();
    }
}
