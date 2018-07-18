using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HiLand.Framework4.Permission.Attributes
{
    /// <summary>
    /// 简单的登录授权判断
    /// （通过检查HttpContext.Current.User.Identity.Name属性，判断用户是否为登录用户）
    /// </summary>
    public class SimpleAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            int loginedUserID = 0;

            int.TryParse(HttpContext.Current.User.Identity.Name, out loginedUserID);
            if (loginedUserID <= 0)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}
