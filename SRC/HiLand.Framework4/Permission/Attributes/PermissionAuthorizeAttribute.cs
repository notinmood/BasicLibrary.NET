using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HiLand.Utility.Setting;
using HiLand.Utility.Web;

namespace HiLand.Framework4.Permission.Attributes
{
    /// <summary>
    /// 用于自定义权限（比如根据管理员分配的权限）进行登录认证的场景
    /// </summary>
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        private PermissionAuthorizeModes permissionAuthorizeMode = PermissionAuthorizeModes.Normal;

        /// <summary>
        /// 权限验证构造函数
        /// </summary>
        /// <param name="permissionAuthorizeMode">验证模式类型</param>
        public PermissionAuthorizeAttribute(PermissionAuthorizeModes permissionAuthorizeMode = PermissionAuthorizeModes.Normal)
        {
            this.permissionAuthorizeMode = permissionAuthorizeMode;
        }

        /// <summary>
        /// 具体验证方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isSuccessful = PermissionValidationHelper.GeneralValidate(permissionAuthorizeMode);
            if (isSuccessful == false)
            {
                string noPermissionDisplayPage = Config.GetAppSetting("noPermissionDisplayPage");
                if (string.IsNullOrWhiteSpace(noPermissionDisplayPage))
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                else
                {
                    string returnUrl = RequestHelper.CurrentFullUrl;
                    HttpContext.Current.Response.Redirect(noPermissionDisplayPage);
                }
            }
        }
    }
}
