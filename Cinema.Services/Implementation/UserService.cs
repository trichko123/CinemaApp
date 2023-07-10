using Cinema.Domain.Identity;
using Cinema.Repository.Interface;
using Cinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ChangeUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        public List<CinemaApplicationUser> findAll()
        {
            return (List<CinemaApplicationUser>)_userRepository.GetAll();
        }

        public bool IsAdmin(string userId)
        {
            CinemaApplicationUser user = _userRepository.Get(userId);
            if (user.Role == Role.ROLE_ADMIN) return true;
            return false;
        }
    }
}
