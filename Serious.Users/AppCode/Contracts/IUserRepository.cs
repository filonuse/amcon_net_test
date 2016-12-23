using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serious.Users.AppCode.Contracts
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(string email);
    }
}