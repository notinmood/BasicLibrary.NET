using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;
using HiLand.Utility.Web;

namespace HiLand.Framework.Membership
{
    /// <summary>
    /// 登录用户的Cookie
    /// </summary>
    public class UserCookie : CookieInfo
    {
        public int UserID { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public UserTypes UserType { get; set; }

        /// <summary>
        /// 清空用户Cookie
        /// </summary>
        public void Clear()
        {
            this.UserGuid = Guid.Empty;
            this.UserID = 0;
            this.UserName = string.Empty;
            this.Save(DateTime.Now.AddDays(-1));
        }
    }
}
