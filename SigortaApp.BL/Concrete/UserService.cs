using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<AppUser> GetList()
        {
            return _userRepository.GetListAll();
        }

        public List<AppUser> GetList(List<int> ids)
        {
            return _userRepository.GetListAll(w => ids.Contains(w.Id)).ToList();
        }

        public void TAdd(AppUser t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(AppUser t)
        {
            throw new NotImplementedException();
        }

        public AppUser TGetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void TUpdate(AppUser t)
        {
            _userRepository.Update(t);
        }
    }
}
