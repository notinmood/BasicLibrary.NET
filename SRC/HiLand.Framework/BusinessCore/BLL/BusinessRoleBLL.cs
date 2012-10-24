using System;
using System.Collections.Generic;
using System.Configuration;
using HiLand.Framework.BusinessCore.DAL;
using HiLand.Utility.Cache;
using HiLand.Utility.Enums;
using HiLand.Utility.Pattern;

namespace HiLand.Framework.BusinessCore.BLL
{
    /// <summary>
    /// 角色业务逻辑类
    /// </summary>
    public static class BusinessRoleBLL
    {
        private static IBusinessRoleDAL dalSave = null;
        private static IBusinessRoleDAL DALSave
        {
            get
            {
                if (dalSave == null)
                {
                    //ProxyGenerator proxy = new ProxyGenerator();
                    //dalSave = proxy.CreateClassProxy<RoleDAL>(new SQLInjectionSaveBeforeInterceptor());
                    dalSave = DAL;
                }

                return dalSave;
            }
        }

        private static IBusinessRoleDAL dal = null;
        private static IBusinessRoleDAL DAL
        {
            get
            {
                if (dal == null)
                {
                    string dllName = ConfigurationManager.AppSettings["CoreDALDLLName"];
                    string nameSpace = ConfigurationManager.AppSettings["CoreDALNameSpace"];

                    if (string.IsNullOrEmpty(dllName) || string.IsNullOrEmpty(nameSpace))
                    {
                        dal = Singleton<BusinessRoleDAL>.Instance;
                    }
                    else
                    {
                        string dalClassString = string.Format("{0}.{1},{2}", nameSpace, "BusinessRoleDAL", dllName);
                        Type dalClassType = Type.GetType(dalClassString);
                        dal = Activator.CreateInstance(dalClassType) as IBusinessRoleDAL;
                    }
                }

                return dal;
            }
        }

        /// <summary>
        /// 判断是否存在某个名称的角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool IsExistRole(string roleName)
        {
            return DALSave.IsExistRole(roleName);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static BusinessRole CreateRole(IBusinessRole entity, out CreateUserRoleStatuses status)
        {
            BusinessRole entityCreated = DALSave.CreateRole(entity, out status);
            if (status == CreateUserRoleStatuses.Successful)
            {
                CleanUpCache(entity);
                BuildUpCache(entityCreated);
            }
            return entityCreated;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool UpdateRole(IBusinessRole entity)
        {
            bool isSuccessful = DALSave.UpdateRole(entity);
            if (isSuccessful == true)
            {
                CleanUpCache(entity);
                BuildUpCache(entity);
            }
            return isSuccessful;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static BusinessRole Get(Guid roleGuid)
        {
            return Get(roleGuid, false);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessRole Get(Guid roleGuid, bool isForceUseNoCache)
        {
            BusinessRole result = BusinessRole.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.Get(roleGuid);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetRoleByGuidKey(roleGuid);
                result = CacheHelper.Access<Guid, BusinessRole>(cacheKey, CacheHelper.AFewTime, DALSave.Get, roleGuid);
            }

            return result;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static BusinessRole Get(string roleName)
        {
            return Get(roleName, false);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public static BusinessRole Get(string roleName, bool isForceUseNoCache)
        {
            BusinessRole result = BusinessRole.Empty;
            if (isForceUseNoCache == true)
            {
                result = DALSave.Get(roleName);
            }
            else
            {
                string cacheKey = CoreCacheKeys.GetRoleByNameKey(roleName);
                result = CacheHelper.Access<string, BusinessRole>(cacheKey, CacheHelper.AFewTime, DALSave.Get, roleName);
            }

            return result;
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        public static List<BusinessRole> GetList(Logics onlyDisplayUsable, string whereClause)
        {
            string cacheKey = CoreCacheKeys.GetRoleListKey(onlyDisplayUsable, whereClause);
            return CacheHelper.Access<Logics, string, List<BusinessRole>>(cacheKey, CacheHelper.AFewTime, DALSave.GetList, onlyDisplayUsable, whereClause);
        }

        /// <summary>
        /// 获取角色内的用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<BusinessUser> GetUsers(string roleName)
        {
            string cacheKey = CoreCacheKeys.GetRoleUsersByNameKey(roleName);
            return CacheHelper.Access<string, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUsers, roleName);
        }

        /// <summary>
        /// 获取角色内的用户
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static List<BusinessUser> GetUsers(Guid roleGuid)
        {
            string cacheKey = CoreCacheKeys.GetRoleUsersByGuidKey(roleGuid);
            return CacheHelper.Access<Guid, List<BusinessUser>>(cacheKey, CacheHelper.AFewTime, DALSave.GetUsers, roleGuid);
        }

        #region 帮助方法
        /// <summary>
        /// 清理某些已经改变的缓存
        /// </summary>
        /// <param name="entity"></param>
        private static void CleanUpCache(IBusinessRole entity)
        {
            CacheHelper.Remove(CoreCacheKeys.GetRoleByNameKey(entity.RoleName));
            CacheHelper.Remove(CoreCacheKeys.GetRoleByGuidKey(entity.RoleGuid));

            CacheHelper.RemoveByPattern(CoreCacheKeys.GetRoleListPrefixKey());
        }

        /// <summary>
        /// 根据实体构建缓存
        /// </summary>
        /// <param name="entity"></param>
        private static void BuildUpCache(IBusinessRole entity)
        {
            CacheHelper.Set(CoreCacheKeys.GetRoleByNameKey(entity.RoleName), entity, CacheHelper.AFewTime);
            CacheHelper.Set(CoreCacheKeys.GetRoleByGuidKey(entity.RoleGuid), entity, CacheHelper.AFewTime);
        }
        #endregion
    }
}
