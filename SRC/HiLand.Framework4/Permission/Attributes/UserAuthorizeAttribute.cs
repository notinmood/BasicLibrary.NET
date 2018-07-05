//using System.Web.Mvc;
//using System.Web.Security;
//using HiLand.Framework.BusinessCore;
//using HiLand.Framework.BusinessCore.BLL;
//using HiLand.Utility.Enums;
//using HiLand.Utility4.Data;

//namespace HiLand.Framework4.Permission.Attributes
//{
//    /// <summary>
//    /// 用户类型使用授权的特性
//    /// </summary>
//    /// <remarks>
//    /// 允许某（几）种类型的用户使用授权功能的特性
//    /// </remarks>
//    public class UserAuthorizeAttribute : AuthorizeAttribute
//    {
//        UserTypes allowedUserType = UserTypes.SuperAdmin | UserTypes.Manager;

//        /// <summary>
//        /// 用户类型使用授权
//        /// </summary>
//        public UserAuthorizeAttribute()
//        {

//        }

//        /// <summary>
//        /// 用户类型使用授权
//        /// </summary>
//        /// <param name="allowedUserType">允许访问的用户类型</param>
//        public UserAuthorizeAttribute(UserTypes allowedUserType)
//        {
//            this.allowedUserType = allowedUserType;
//        }

//        public override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            string userName = BusinessUserBLL.CurrentUserName;
//            bool isAllowed = IsBelongToUserType(userName);

//            if (isAllowed == false)
//            {
//                FormsAuthentication.RedirectToLoginPage();
//            }
//        }

//        /// <summary>
//        /// 判断指定的用户是否属于允许的用户类型之列
//        /// </summary>
//        /// <param name="userName">指定的用户名称</param>
//        /// <returns></returns>
//        private bool IsBelongToUserType(string userName)
//        {
//            bool result = false;

//            if (string.IsNullOrWhiteSpace(userName) == false)
//            {
//                BusinessUser user = BusinessUserBLL.Get(userName);
//                if (user != null && ((int)user.UserType != 0) && (this.allowedUserType.ContainsFlag(user.UserType) == true))
//                {
//                    result = true;
//                }
//            }

//            return result;
//        }
//    }
//}