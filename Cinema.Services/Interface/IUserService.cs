using Cinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Services.Interface
{
    public interface IUserService
    {
        bool ChangeUserRole(string userId);
        List<CinemaApplicationUser> findAll();
        bool IsAdmin(string userId);
    }
}
