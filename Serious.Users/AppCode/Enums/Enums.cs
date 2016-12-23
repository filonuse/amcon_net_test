using Serious.Users.AppCode.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serious.Users.AppCode.Enums
{
    public enum UserRoles
    {
        [StringValue("User")]
        User,
        [StringValue("Master")]
        Admin
    }
}