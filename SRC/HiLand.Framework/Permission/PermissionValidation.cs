using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting;
using HiLand.Utility.Setting.SectionHandler;
using HiLand.Utility.Web;

namespace HiLand.Framework.Permission
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public static class PermissionValidation
    {
        #region Cookie的读写
        /// <summary>
        /// 为验证写入数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleList"></param>
        public static void WriteCookie(string userName, List<string> roleList)
        {
            string roles = string.Empty;
            if (roleList != null)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    roles += string.Format("[role]:{0}", roleList[i]);
                }
            }

            FormsAuthenticationTicket authTicket = new
                            FormsAuthenticationTicket(1,                    // version
                                                userName,                   // user name
                                                DateTime.Now,               // creation
                                                DateTime.Now.AddMinutes(60),// Expiration
                                                false,                      // Persistent
                                                roles);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);//Encrypt方法创建一个字符串，其中包含适用于 HTTP Cookie 的加密的 Forms 身份验证票证
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket); //将加密的票据加入到Cookie中，
            //FormsAuthentication.FormsCookieName获取的就是配置文件中名为AuthCookie的Cookie
            HttpContext.Current.Response.Cookies.Add(authCookie); //加入到Cookie中，这一步很重要，加入后代表已携带身份
        }

        /// <summary>
        /// 为验证写入数据并且跳转到登录前的页面
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleList"></param>
        public static void WriteCookieAndRedirect(string userName, List<string> roleList)
        {
            WriteCookie(userName, roleList);
            RedirectToOriginalRequestPage();
        }

        /// <summary>
        /// 通过读取cookie生成用户登录信息
        /// </summary>
        /// <returns></returns>
        public static bool ReadCookie()
        {
            bool isSuccessful = true;
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];//获取web.config中的名为“AuthCookie”的Cookie值
            if (authCookie == null)
            {
                isSuccessful = false;
                return isSuccessful;
            }

            FormsAuthenticationTicket authTicket = null;
            try
            {
                //解密身份票据
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (authTicket == null)
            {
                // 如果没有则返回
                isSuccessful = false;
                return isSuccessful;
            }

            //如果有角色可以获取传递的用户数据
            //用户数据在登入代码中实例化FormsAuthenticationTicket时设置的。string userdata
            string[] roles = authTicket.UserData.Split(new string[] { "[role]:" }, StringSplitOptions.RemoveEmptyEntries);

            //创建标识对象
            System.Security.Principal.IIdentity identity = new FormsIdentity(authTicket);

            //创建安全主体
            System.Security.Principal.IPrincipal principal = new System.Security.Principal.GenericPrincipal(identity, roles);

            // 添加到Http上下文中
            HttpContext.Current.User = principal;

            return isSuccessful;
        }
        #endregion

        #region 通用的页面权限验证
        /// <summary>
        /// 通用的页面权限验证（仅验证到页面级别，即List功能）
        /// </summary>
        /// <returns></returns>
        public static bool GeneralPageValidate()
        {
            string currentVirtualPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
            return GeneralValidate(currentVirtualPath, PermissionTypes.List);
        }

        /// <summary>
        /// 通用的页面权限验证（仅验证到页面级别，即List功能）
        /// </summary>
        /// <param name="pageToValidate">被验证的页面</param>
        /// <returns></returns>
        public static bool GeneralPageValidate(string pageToValidate)
        {
            return GeneralValidate(pageToValidate, PermissionTypes.List);
        }

        /// <summary>
        /// 通用的权限验证
        /// </summary>
        /// <param name="permissionType">被验证的权限</param>
        /// <returns></returns>
        public static bool GeneralValidate(PermissionTypes permissionType)
        {
            string currentVirtualPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
            return GeneralValidate(currentVirtualPath, permissionType);
        }

        /// <summary>
        /// 通用的权限验证
        /// </summary>
        /// <param name="pageToValidate">被验证的页面</param>
        /// <param name="permissionType">被验证的权限</param>
        /// <returns></returns>
        public static bool GeneralValidate(string pageToValidate, PermissionTypes permissionType)
        {
            bool isSuccessful = false;
            if (GeneralValidateConfig.FileSubModuleDic.ContainsKey(pageToValidate) == true)
            {
                Guid subModuelGuidToValidate = GeneralValidateConfig.FileSubModuleDic[pageToValidate];
                isSuccessful = GeneralValidate(subModuelGuidToValidate,permissionType);
            }
            else
            {
                isSuccessful = true;
            }

            return isSuccessful;
        }


        /// <summary>
        /// 通用的权限验证
        /// </summary>
        /// <param name="permissionItemGuid">被验证的权限Guid</param>
        /// <param name="permissionType">被验证的权限类型</param>
        /// <returns></returns>
        public static bool GeneralValidate(Guid permissionItemGuid, PermissionTypes permissionType)
        {
            bool isSuccessful = false;

            bool isCookieSuccessful = ReadCookie();
            if (isCookieSuccessful == false)
            {
                return false;
            }

            //string userName = HttpContext.Current.User.Identity.Name;
            //IUser currentUser = UserFactory.CreateUser(userName);
            IUser currentUser = BusinessUserBLL.CurrentUser;

            //对超级管理员类型的用户不做权限限制
            if (currentUser.UserType == UserTypes.SuperAdmin)
            {
                return true;
            }

            foreach (KeyValuePair<Guid, PermissionItem> kvp in currentUser.PermissionItems)
            {
                PermissionItem currentPermission = kvp.Value;
                if (currentPermission.PermissionKey == permissionItemGuid)
                {
                    if ((currentPermission.PermissionItemValue & (int)permissionType) == (int)permissionType)
                    {
                        isSuccessful = true;
                        return isSuccessful;
                    }
                }

                isSuccessful = false;
                return isSuccessful;
            }

            return isSuccessful;
        }
        #endregion

        #region 按照路径角色进行权限验证
        /// <summary>
        /// 按照路径角色进行权限验证
        /// </summary>
        /// <param name="virtualPathToValidate"></param>
        /// <param name="rolesToValidate"></param>
        /// <returns></returns>
        public static bool PathRoleValidate(string virtualPathToValidate, params string[] rolesToValidate)
        {
            bool isSuccessful = false;
            string currentVirtualPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
            virtualPathToValidate = WebHelper.GetRelativeVirtualPath(virtualPathToValidate).ToLower();

            if (currentVirtualPath.StartsWith(virtualPathToValidate) == false)
            {
                isSuccessful = true;
                return isSuccessful;
            }

            isSuccessful = ReadCookie();
            if (isSuccessful == false)
            {
                return isSuccessful;
            }

            virtualPathToValidate = WebHelper.GetRelativeVirtualPath(virtualPathToValidate).ToLower();

            if (currentVirtualPath.StartsWith(virtualPathToValidate))
            {
                //需要验证是否登录
                if (rolesToValidate != null)
                {
                    for (int i = 0; i < rolesToValidate.Length; i++)
                    {
                        isSuccessful = HttpContext.Current.User.IsInRole(rolesToValidate[i]);
                        if (isSuccessful == true)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //不需要验证
                isSuccessful = true;
            }

            return isSuccessful;
        }

        /// <summary>
        /// 按照路径角色进行权限验证
        /// </summary>
        /// <param name="virtualPathToValidate"></param>
        /// <param name="rolesToValidate"></param>
        public static void PathRoleValidateAndRedirect(string virtualPathToValidate, params string[] rolesToValidate)
        {
            bool isSuccessfule = PathRoleValidate(virtualPathToValidate, rolesToValidate);
            if (isSuccessfule == false)
            {
                RedirectToLoginPage();
            }
        }
        #endregion

        #region 页面跳转

        /// <summary>
        /// 重定向到原始的请求页
        /// </summary>
        public static void RedirectToOriginalRequestPage()
        {
            string originalUrl = HttpContext.Current.Request.QueryString["returnurl"];
            if (string.IsNullOrEmpty(originalUrl))
            {
                originalUrl = FormsAuthentication.DefaultUrl;
            }

            if (string.IsNullOrEmpty(originalUrl))
            {
                originalUrl = "~/";
            }

            originalUrl = HttpContext.Current.Server.UrlDecode(originalUrl);

            HttpContext.Current.Response.Redirect(originalUrl);
        }

        /// <summary>
        /// 重定向到登录页面
        /// </summary>
        public static void  RedirectToLoginPage()
        {
            string targetUrl = FormsAuthentication.LoginUrl;

            if (string.IsNullOrEmpty(targetUrl))
            {
                targetUrl = "~/login.aspx";
            }

            ResponseHelper.RedirectPage(targetUrl);
        }

        /// <summary>
        /// 重定向到无权限使用页面
        /// </summary>
        public static void RedirectToNoPermissionPage()
        {
            string targetUrl = Config.GetAppSetting("NoPermissionPage") ;

            if (string.IsNullOrEmpty(targetUrl))
            {
                targetUrl = "~/NoPermission.aspx";
            }

            ResponseHelper.RedirectPage(targetUrl);
        }

        #endregion
    }
}
