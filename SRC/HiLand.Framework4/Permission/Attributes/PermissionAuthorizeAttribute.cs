using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HiLand.Utility.Enums;
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
            PermissionValidateStatuses permissionValidateStatuses = PermissionValidationHelper.GeneralValidate(permissionAuthorizeMode);

            switch (permissionValidateStatuses)
            {
                case PermissionValidateStatuses.FailureUnLogin:
                    FormsAuthentication.RedirectToLoginPage();
                    break;
                case PermissionValidateStatuses.FailureNoPermission:
                    string noPermissionDisplayPage = Config.GetAppSetting("noPermissionDisplayPage");
                    if (string.IsNullOrWhiteSpace(noPermissionDisplayPage))
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    else
                    {
                        string returnUrl = RequestHelper.CurrentFullUrl;
                        //TODO:xieran20130111 需要加入跳转回来的url参数
                        HttpContext.Current.Response.Redirect(noPermissionDisplayPage);
                    }
                    break; 
                case PermissionValidateStatuses.Successful:
                default:
                    break;
            }
        }
    }
}
