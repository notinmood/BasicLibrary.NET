using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using HiLand.Framework.BusinessCore.DAL;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Cache;
using HiLand.Utility.Core;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;
using HiLand.Utility.Enums;
using HiLand.Utility.Event;
using HiLand.Utility.Paging;
using HiLand.Utility.Pattern;
using HiLand.Utility.Web;

namespace HiLand.Framework.BusinessCore.BLL
{
    /// <summary>
    /// 人员业务逻辑类
    /// </summary>
    public class BusinessUserBLL
    {
        static BusinessUserBLL()
        {
            BusinessDepartmentBLL.Instance.FullPathChanged += Department_FullPathChanged;
        }

        /// <summary>
        /// 部门的全路径变更事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void Department_FullPathChanged(object sender, DataForChange<string> args)
        {
            ChangeFullPath(args.OriginalData, args.NewData);
        }

        /// <summary>
        /// 全路径改变的事件
        /// </summary>
        public static event CommonEventHandle<DataForChange<string>> FullPathChanged;

        private static IBusinessUserDAL dalSave = null;
        private static IBusinessUserDAL DALSave
        {
            get
            {
                if (dalSave == null)
                {
                    //ProxyGenerator proxy = new ProxyGenerator();
                    //dalSave = proxy.CreateClassProxy<UserDAL>(new SQLInjectionSaveBeforeInterceptor());
                    dalSave = DAL;
                }

                return dalSave;
            }
        }

        private static IBusinessUserDAL dal = null;
        private static IBusinessUserDAL DAL
        {
            get
            {
                if (dal == null)
                {
                    string dllName = ConfigurationManager.AppSettings["CoreDALDLLName"];
                    string nameSpace = ConfigurationManager.AppSettings["CoreDALNameSpace"];

                    if (string.IsNullOrEmpty(dllName) || string.IsNullOrEmpty(nameSpace))
                    {
                        dal = Singleton<BusinessUserDAL>.Instance;
                    }
                    else
                    {
                        string dalClassString = string.Format("{0}.{1},{2}", nameSpace, "BusinessUserDAL", dllName);
                        Type dalClassType = Type.GetType(dalClassString);
                        dal = Activator.CreateInstance(dalClassType) as IBusinessUserDAL;
                    }
                }

                return dal;
            }
        }

        #region 基本逻辑信息
        /// <summary>
        /// 判断用户的用户名和EMail是否在系统内存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        public static bool IsExistUser(string userName, string userEMail,string userIDCard)
        {
            return DALSave.IsExistUser(userName, userEMail, userIDCard);
        }

        /// <summary>
        /// 判断用户账号是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsExistUserName(string userName)
        {
            return DALSave.IsExistUserName(userName);
        }

        /// <summary>
        /// 判断用户的EMail是否存在
        /// </summary>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        public static bool IsExistUserEMail(string userEMail)
        {
            return DALSave.IsExistUserEMail(userEMail);
        }

        /// <summary>
        /// 判断用户的身份证是否存在
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <returns></returns>
        public static bool IsExistUserIDCard(string userIDCard)
        {
            return DALSave.IsExistUserIDCard(userIDCard);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static BusinessUser CreateUser(IBusinessUser entity, out CreateUserRoleStatuses status)
        {
            entity = BusinessUserDAL.DealWithPassword(entity);
            entity.UserFullPath = GetFullPath(entity);
            BusinessUser entityCreated = DALSave.CreateUser(entity, out status);
            if (status == CreateUserRoleStatuses.Successful)
            {
                CleanUpCache(entity);
                BuildUpCache(entityCreated);
            }
            return entityCreated;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool UpdateUser(IBusinessUser entity)
        {
            BusinessUser originalModel = BusinessUserBLL.Get(entity.UserGuid, true);
            if (string.IsNullOrEmpty(entity.UserFullPath) ||
                originalModel.UserNameDisplay != entity.UserNameDisplay ||
                originalModel.DepartmentGuid != entity.DepartmentGuid)
            {
                entity.UserFullPath = GetFullPath(entity);
            }

            bool isSuccessful = DALSave.UpdateUser(entity);
            if (isSuccessful == true)
            {
                if (entity is IModelExtensible)
                {
                    ((IModelExtensible)entity).PropertyNames = ((IModelExtensible)entity).ExtensiableRepository.GetSerializerData().Keys;
                    ((IModelExtensible)entity).PropertyValues = ((IModelExtensible)entity).ExtensiableRepository.GetSerializerData().Values;
                }

                if (entity.UserFullPath != originalModel.UserFullPath)
                {
                    ChangeFullPath(originalModel.UserFullPath, entity.UserFullPath);
                }

                CleanUpCache(entity);
                BuildUpCache(entity);
            }
            return isSuccessful;
        }

        /// <summary>
        /// 变更用户的全路径
        /// </summary>
        /// <param name="originalFullPath"></param>
        /// <param name="newFullpath"></param>
        public static bool ChangeFullPath(string originalFullPath, string newFullpath)
        {
            bool isSuccessful = false;

            DataForChange<string> changeData = new DataForChange<string>();
            changeData.NewData = newFullpath;
            changeData.OriginalData = originalFullPath;

            isSuccessful = DALSave.ChangeFullPath(changeData.OriginalData, changeData.NewData);

            if (FullPathChanged != null)
            {
                FullPathChanged(null, changeData);
            }

            if (isSuccessful == true)
            {
                CacheHelper.RemoveByPattern(CoreCacheKeys.GetUserPrefixKey());
            }

            return isSuccessful;
        }

        /// <summary>
        /// 获取部门内用户的全路径
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetFullPath(IBusinessUser entity)
        {
            return string.Format("{0}||{1}", BusinessDepartmentBLL.Instance.GetFullPath(entity.DepartmentGuid), entity.UserNameDisplay);
        }

        /// <summary>
        /// (真实)删除用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns>由于有很多业务更用户关联,请谨慎使用本方法,同时推进使用方法LogicDeleteUser</returns>
        public static bool DeleteUser(Guid userGuid)
        {
            BusinessUser entity = Get(userGuid);
            bool isSuccessful = DALSave.DeleteUser(userGuid);
            if (isSuccessful == true)
            {
                CleanUpCache(entity);
            }

            return isSuccessful;
        }

        /// <summary>
        ///  禁用(逻辑删除)用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static bool ProhibitUser(Guid userGuid)
        {
            return SetUserStatus(userGuid, UserStatuses.Stopped);
        }

        /// <summary>
        /// 恢复被逻辑删除的用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static bool RecoveryUser(Guid userGuid)
        {
            return SetUserStatus(userGuid, UserStatuses.Normal);
        }

        /// <summary>
        /// 更新用户的最后访问信息
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="lastIP"></param>
        /// <param name="lastTime"></param>
        public static void UpdateLastInfo(Guid userGuid, string lastIP, DateTime lastTime)
        {
            DALSave.UpdateLastInfo(userGuid, lastIP, lastTime);
        }

        /// <summary>
        /// 修改用户的口令
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <remarks>修改口令的时候不修改加密方式和加密盐,本方法主要用于用户自己修改口令</remarks>
        public static bool ChangePassword(Guid userGuid, string newPassword, string oldPassword)
        {
            bool isSuccessful = true;
            BusinessUser entity = Get(userGuid);
            //1.首先验证旧口令跟在系统内保持的是否一致
            string passwordInSystem = entity.Password;
            entity.Password = oldPassword;
            entity = BusinessUserDAL.DealWithPassword(entity);
            string oldPasswordEncrypted = entity.Password;

            if (passwordInSystem == oldPasswordEncrypted)
            {
                //2.加密新口令
                entity.Password = newPassword;
                entity = BusinessUserDAL.DealWithPassword(entity);

                //3.保存新口令
                int passwordEncrytType = Convert.ToInt32(entity.PasswordEncrytType);
                string passwordEncrytSalt = entity.PasswordEncrytSalt;
                newPassword = entity.Password;
                isSuccessful = DALSave.ChangePassword(userGuid, newPassword, passwordEncrytType, passwordEncrytSalt);
            }
            else
            {
                isSuccessful = false;
                return isSuccessful;
            }

            //CleanUpCache(entity);
            return isSuccessful;
        }

        /// <summary>
        /// 修改用户的口令
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="newPassword"></param>
        /// <remarks>修改口令的时候不修改加密方式和加密盐,本方法主要用于管理员修改用户的口令</remarks>
        public static bool ChangePassword(Guid userGuid, string newPassword)
        {
            BusinessUser entity = Get(userGuid);
            //1.加密新口令
            entity.Password = newPassword;
            entity = BusinessUserDAL.DealWithPassword(entity);

            //2.保存新口令
            int passwordEncrytType = Convert.ToInt32(entity.PasswordEncrytType);
            string passwordEncrytSalt = entity.PasswordEncrytSalt;
            newPassword = entity.Password;
            return DALSave.ChangePassword(userGuid, newPassword, passwordEncrytType, passwordEncrytSalt);
        }


        /// <summary>
        /// 改变用户的状态
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static bool SetUserStatus(Guid userGuid, UserStatuses newStatus)
        {
            bool isSuccessful = DALSave.SetUserStatus(userGuid, newStatus);
            if (isSuccessful == true)
            {
                BusinessUser entity = Get(userGuid);
                CleanUpCache(entity);
            }
            return isSuccessful;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static BusinessUser Get(Guid userGuid)
        {
            return Get(userGuid, false);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessUser Get(Guid userGuid, bool isForceUseNoCache)
        {
            BusinessUser result = BusinessUser.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.Get(userGuid);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetUserByGuidKey(userGuid);
                result = CacheHelper.Access<Guid, BusinessUser>(cacheKey, CacheHelper.AFewTime, DALSave.Get, userGuid);
            }

            return result;
        }

        /// <summary>
        /// 获取用户(根据用户账号)
        /// </summary>
        /// <param name="userAccount">此处的账号可以是用户名或者用户EMail</param>
        /// <returns></returns>
        public static BusinessUser Get(string userAccount)
        {
            return Get(userAccount, false);
        }

        /// <summary>
        /// 获取用户(根据用户账号)
        /// </summary>
        /// <param name="userAccount">此处的账号可以是用户名或者用户EMail</param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessUser Get(string userAccount, bool isForceUseNoCache)
        {
            BusinessUser result = BusinessUser.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.Get(userAccount);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetUserByAccountKey(userAccount);
                result = CacheHelper.Access<String, BusinessUser>(cacheKey, CacheHelper.AFewTime, DALSave.Get, userAccount);
            }

            return result;
        }

        /// <summary>
        /// 获取用户(根据用户名)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static BusinessUser GetByUserName(string userName)
        {
            return GetByUserName(userName, false);
        }

        /// <summary>
        /// 获取用户(根据用户名)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessUser GetByUserName(string userName, bool isForceUseNoCache)
        {
            BusinessUser result = BusinessUser.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.GetByUserName(userName);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetUserByNameKey(userName);
                result = CacheHelper.Access<String, BusinessUser>(cacheKey, CacheHelper.AFewTime, DALSave.GetByUserName, userName);
            }

            return result;
        }

        /// <summary>
        /// 获取用户(根据用户EMail)
        /// </summary>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        public static BusinessUser GetByUserEMail(string userEMail)
        {
            return GetByUserEMail(userEMail, false);
        }

        /// <summary>
        /// 获取用户(根据用户EMail)
        /// </summary>
        /// <param name="userEMail"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessUser GetByUserEMail(string userEMail, bool isForceUseNoCache)
        {
            BusinessUser result = BusinessUser.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.GetByUserEMail(userEMail);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetUserByEMailKey(userEMail);
                result = CacheHelper.Access<String, BusinessUser>(cacheKey, CacheHelper.AFewTime, DALSave.GetByUserEMail, userEMail);
            }
            return result;
        }

        /// <summary>
        /// 获取用户(根据用户IDCard)
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <returns></returns>
        public static BusinessUser GetByUserIDCard(string userIDCard)
        {
            return GetByUserIDCard(userIDCard, false);
        }

        /// <summary>
        /// 获取用户(根据用户IDCard)
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessUser GetByUserIDCard(string userIDCard, bool isForceUseNoCache)
        {
            BusinessUser result = BusinessUser.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.GetByUserIDCard(userIDCard);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetByUserIDCardKey(userIDCard);
                result = CacheHelper.Access<String, BusinessUser>(cacheKey, CacheHelper.AFewTime, DALSave.GetByUserIDCard, userIDCard);
            }
            return result;
        }

        /// <summary>
        /// 获取总的条目
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public static int GetTotalCount(string whereClause)
        {
            string cacheKey = CoreCacheKeys.GetUserCountKey(whereClause);
            return CacheHelper.Access<String, int>(cacheKey, CacheHelper.AFewTime, DALSave.GetTotalCount, whereClause);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public static List<BusinessUser> GetList(string whereClause)
        {
            string cacheKey = CoreCacheKeys.GetUserListKey(whereClause);
            return CacheHelper.Access<String, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetList, whereClause);
        }

        /// <summary>
        /// 获取一批用户
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <returns></returns>
        public static PagedEntityCollection<BusinessUser> GetPagedCollection(int startIndex, int endIndex, string whereClause, string orderClause)
        {
            string cacheKey = CoreCacheKeys.GetUserPagedKey(startIndex, endIndex, whereClause, orderClause);
            return CacheHelper.Access<int, int, string, string, PagedEntityCollection<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetPagedCollection, startIndex, endIndex, whereClause, orderClause);
        }
        #endregion

        #region 登录、退出相关逻辑信息
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userAccount">其可以是用户的UserName,也可以是其EMail</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static BusinessUser Login(string userAccount, string password, out LoginStatuses status)
        {
            BusinessUser entity = DALSave.Login(userAccount, password, out status);
            if (entity.IsEmpty == false)
            {
                BuildUpCache(entity);
            }

            if (status == LoginStatuses.Successful)
            {
                RecordCurrentUserInfoAndLoginInfo(userAccount, entity);
            }

            return entity;
        }

        /// <summary>
        /// 使用用户名称和口令登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static BusinessUser LoginWithUserName(string userName, string password, out LoginStatuses status)
        {
            BusinessUser entity = DALSave.LoginWithUserName(userName, password, out status);
            if (entity.IsEmpty == false)
            {
                BuildUpCache(entity);
            }

            if (status == LoginStatuses.Successful)
            {
                RecordCurrentUserInfoAndLoginInfo(userName, entity);
            }

            return entity;
        }

        /// <summary>
        /// 使用Email和口令进行登录
        /// </summary>
        /// <param name="userEMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static BusinessUser LoginWithUserEMail(string userEMail, string password, out LoginStatuses status)
        {
            BusinessUser entity = DALSave.LoginWithUserEMail(userEMail, password, out status);
            if (entity.IsEmpty == false)
            {
                BuildUpCache(entity);
            }

            if (status == LoginStatuses.Successful)
            {
                RecordCurrentUserInfoAndLoginInfo(userEMail, entity);
            }

            return entity;
        }

        /// <summary>
        /// 使用Email和口令进行登录
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static BusinessUser LoginWithUserIDCard(string userIDCard, string password, out LoginStatuses status)
        {
            BusinessUser entity = DALSave.LoginWithUserIDCard(userIDCard, password, out status);
            if (entity.IsEmpty == false)
            {
                BuildUpCache(entity);
            }

            if (status == LoginStatuses.Successful)
            {
                RecordCurrentUserInfoAndLoginInfo(userIDCard, entity);
            }

            return entity;
        }

        /// <summary>
        /// 记录当前用户信息和登录信息
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="entity"></param>
        private static void RecordCurrentUserInfoAndLoginInfo(string userAccount, BusinessUser entity)
        {
            //在客户端写入登录信息
            if (EnvironmentHelper.IsWebApplicatonMode == true)
            {
                //0.记录用户Cookie1
                FormsAuthentication.SetAuthCookie(userAccount, false);

                //1.记录用户Cookie2
                UserCookie userCookie = new UserCookie();
                userCookie.UserGuid = entity.UserGuid;
                userCookie.UserID = entity.UserID;
                userCookie.UserName = entity.UserName;
                userCookie.UserType = entity.UserType;
                userCookie.Save();
            }
            else
            {
                CurrentUser = entity;
            }

            UpdateLastInfo(entity.UserGuid, ClientBrowser.GetClientIP(), DateTimeHelper.RunningLocalNow);
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void Logout()
        {
            if (EnvironmentHelper.IsWebApplicatonMode == true)
            {
                FormsAuthentication.SignOut();
                UserCookie userCookie = UserCookie.Load<UserCookie>();
                userCookie.Clear();
            }
            else
            {
                CurrentUser = BusinessUser.Empty;
            }
        }
        #endregion

        #region 当前用户相关信息
        private static BusinessUser currentUser = BusinessUser.Empty;
        /// <summary>
        /// 获取当前登录的用户
        /// </summary>
        /// <remarks>
        /// 本属性在NativeApp和WebApp模式下均可以使用，但是其实现原理不同：
        /// 1.WebApp模式下，通过在请求中设置的用户名称，动态获取完整的用户信息
        /// 2.NativeApp模式下，需要在用户登录时，给此属性赋值，然后其他地方调用此属性
        /// （即本属性的Set方法仅仅适用于NativeApp模式）
        /// </remarks>
        public static BusinessUser CurrentUser
        {
            get
            {
                BusinessUser entity = BusinessUser.Empty;

                if (EnvironmentHelper.IsWebApplicatonMode == true)
                {
                    if (IsLogined == true)
                    {
                        string cacheKey = CoreCacheKeys.GetUserByNameKey(CurrentUserName);
                        entity = CacheHelper.Access<String, BusinessUser>(cacheKey, CacheHelper.TenMintues, DALSave.GetByUserName, CurrentUserName);
                    }

                    //即便是未登录用户也给其赋一个临时的Guid
                    entity.UserTempGuid = CookieHelper.ClientID();
                }
                else
                {
                    entity = currentUser;
                }

                return entity;
            }
            set
            {
                currentUser = value;
            }
        }

        /// <summary>
        /// 当前用户名
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                string result = string.Empty;

                if (EnvironmentHelper.IsWebApplicatonMode == true)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated == true)
                    {
                        result = HttpContext.Current.User.Identity.Name;
                    }

                    if (string.IsNullOrEmpty(result) == true)
                    {
                        UserCookie userCookie = UserCookie.Load<UserCookie>();
                        if (string.IsNullOrEmpty(userCookie.UserName) == false)
                        {
                            result = userCookie.UserName;
                        }
                    }
                }
                else
                {
                    if (currentUser != null && currentUser.IsEmpty == false)
                    {
                        result = currentUser.UserName;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 当前用户Guid
        /// </summary>
        public static Guid CurrentUserGuid
        {
            get
            {
                return CurrentUser.UserGuid;
            }
        }


        /// <summary>
        /// 当前用户是否已经登录
        /// </summary>
        public static bool IsLogined
        {
            get
            {
                bool result = true;
                if (string.IsNullOrEmpty(CurrentUserName))
                {
                    result = false;
                }

                return result;
            }
        }
        #endregion

        #region 部门相关信息
        /// <summary>
        /// 根据部门获取用户Guid集合
        /// </summary>
        /// <param name="departmentFullPath">部门全路径</param>
        /// <param name="isIncludeSubDepartment">是否包含子部门数据</param>
        /// <returns></returns>
        public static List<Guid> GetUserGuidsByDepartment(string departmentFullPath, bool isIncludeSubDepartment)
        {
            string cacheKey = CoreCacheKeys.GetUserGuidsByDepartment(departmentFullPath, isIncludeSubDepartment);
            return CacheHelper.Access<string, bool, List<Guid>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUserGuidsByDepartment, departmentFullPath, isIncludeSubDepartment);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentFullPath">部门全路径</param>
        /// <param name="isIncludeSubDepartment">是否包含子部门数据</param>
        /// <returns></returns>
        public static List<BusinessUser> GetUsersByDepartment(string departmentFullPath, bool isIncludeSubDepartment)
        {
            string cacheKey = CoreCacheKeys.GetUsersByDepartment(departmentFullPath, isIncludeSubDepartment);
            return CacheHelper.Access<String, bool, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUsersByDepartment, departmentFullPath, isIncludeSubDepartment);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentCode">部门编码</param>
        /// <returns></returns>
        public static List<BusinessUser> GetUsersByDepartment(string departmentCode)
        {
            string cacheKey = CoreCacheKeys.GetUsersByDepartment(departmentCode);
            return CacheHelper.Access<String, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUsersByDepartment, departmentCode);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public static List<BusinessUser> GetUsersByDepartment(int departmentID)
        {
            string cacheKey = CoreCacheKeys.GetUsersByDepartment(departmentID);
            return CacheHelper.Access<int, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUsersByDepartment, departmentID);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentGuid">部门GUID</param>
        /// <returns></returns>
        public static List<BusinessUser> GetUsersByDepartment(Guid departmentGuid)
        {
            string cacheKey = CoreCacheKeys.GetUsersByDepartment(departmentGuid);
            return CacheHelper.Access<Guid, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUsersByDepartment, departmentGuid);
        }
        #endregion

        #region 角色相关信息
        /// <summary>
        /// 获取用户所拥有的角色
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static List<BusinessRole> GetUserRoles(Guid userGuid)
        {
            string cacheKey = CoreCacheKeys.GetUserRolesByGuidKey(userGuid);
            return CacheHelper.Access<Guid, List<BusinessRole>>(cacheKey, CacheHelper.AFewTime, DALSave.GetRoles, userGuid);
        }

        /// <summary>
        /// 更新用户所属的角色
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="roleGuidList"></param>
        public static void UpdateUserRoles(Guid userGuid, List<Guid> roleGuidList)
        {
            DALSave.UpdateUserRoles(userGuid, roleGuidList);
            BusinessUser entity = Get(userGuid);
            CleanUpCache(entity);
        }
        #endregion

        #region 帮助方法
        /// <summary>
        /// 清理某些已经改变的缓存
        /// </summary>
        /// <param name="entity"></param>
        private static void CleanUpCache(IBusinessUser entity)
        {
            CacheHelper.Remove(CoreCacheKeys.GetUserByAccountKey(entity.UserName));
            CacheHelper.Remove(CoreCacheKeys.GetUserByAccountKey(entity.UserEmail));
            CacheHelper.Remove(CoreCacheKeys.GetUserByEMailKey(entity.UserEmail));
            CacheHelper.Remove(CoreCacheKeys.GetUserByGuidKey(entity.UserGuid));
            CacheHelper.Remove(CoreCacheKeys.GetUserByNameKey(entity.UserName));

            CacheHelper.Remove(CoreCacheKeys.GetUserRolesByGuidKey(entity.UserGuid));

            CacheHelper.RemoveByPattern(CoreCacheKeys.GetUserListPrefixKey());
        }

        /// <summary>
        /// 根据实体构建缓存
        /// </summary>
        /// <param name="entity"></param>
        private static void BuildUpCache(IBusinessUser entity)
        {
            CacheHelper.Set(CoreCacheKeys.GetUserByAccountKey(entity.UserName), entity, CacheHelper.AFewTime);
            CacheHelper.Set(CoreCacheKeys.GetUserByAccountKey(entity.UserEmail), entity, CacheHelper.AFewTime);

            CacheHelper.Set(CoreCacheKeys.GetUserByEMailKey(entity.UserEmail), entity, CacheHelper.AFewTime);
            CacheHelper.Set(CoreCacheKeys.GetUserByGuidKey(entity.UserGuid), entity, CacheHelper.AFewTime);
            CacheHelper.Set(CoreCacheKeys.GetUserByNameKey(entity.UserName), entity, CacheHelper.AFewTime);
        }
        #endregion
    }
}
