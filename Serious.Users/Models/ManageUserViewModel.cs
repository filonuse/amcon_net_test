using Serious.Users.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serious.Users.Models
{
    public class ManageUserViewModel
    {
        public ManageUserViewModel(User[] users)
        {
            Users = new List<User>(users);
        }

        public List<User> Users
        {
            get;
            private set;
        }
    }
}