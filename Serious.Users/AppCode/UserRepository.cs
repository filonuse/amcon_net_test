using Serious.Users.AppCode.Contracts;
using Serious.Users.AppCode.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Serious.Users.AppCode
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUsers()
        {
            return GetUsers();
        }

        public User GetUser(string email)
        {
            List<User> userList = GetUsers();
            return userList.FirstOrDefault(u => u.Email == email);
        }

        private List<User> GetUsers()
        {
            var result = new List<User>();
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");

            // Get the section.
            AuthenticationSection authenticationSection =
                (AuthenticationSection)configuration.GetSection(Constants.AUTH_PATH);
            FormsAuthenticationConfiguration formsAuthentication = authenticationSection.Forms;

            foreach (var autUser in formsAuthentication.Credentials.Users)
            {                
                var formAuthUser = autUser as FormsAuthenticationUser;
                if (formAuthUser != null)
                {
                    var user = new User
                    {
                        Email = formAuthUser.Name,
                        Password = formAuthUser.Password,
                        Role = formAuthUser.Name.Contains(Constants.MASTER) ? UserRoles.Admin : UserRoles.User
                    };

                    result.Add(user);
                }
            }
            return result;    
        }
        
    }
}