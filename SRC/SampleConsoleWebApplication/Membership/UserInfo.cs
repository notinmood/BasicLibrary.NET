using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLand.Framework.BusinessCore;

namespace WebApplicationConsole.Membership
{
    public class UserInfo : BusinessUser
    {
        public UserInfo(string userName)
        { 
            
        }

        private void LoadInfo(string userName)
        {
            this.UserName = userName;
            this.UserGuid = new Guid("AD6C7C77-71D0-4126-A12E-3CBA39E3FD51");
            BusinessRole role1 = new BusinessRole();
            this.Roles.Add(role1);

        }
    }
}