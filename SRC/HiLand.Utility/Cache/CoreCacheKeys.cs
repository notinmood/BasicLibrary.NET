using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Cache
{
    /// <summary>
    /// 缓存在系统中以字典的形式保存
    /// </summary>
    /// <remarks>
    /// 缓存键的规则为{0}-{1}-{2}-{3}...
    /// 其中0级为最高级，其他序号级别依次向下
    /// </remarks>
    public class CoreCacheKeys
    {
        #region 缓存键的最高级别
        /// <summary>
        /// 核心功能
        /// </summary>
        public static string GetCoreKey()
        {
            return "Core";
        }

        /// <summary>
        /// 插件功能
        /// </summary>
        public static string GetApplicationKey(Guid appGuid)
        {
            return string.Format("Application:{0}", appGuid);
        }

        /// <summary>
        /// 插件功能
        /// </summary>
        public static string GetApplicationKey(string appName)
        {
            return string.Format("Application:{0}", appName);
        }
        #endregion

        #region 权限功能
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPermissionKey()
        {
            return string.Format("{0}-Permission", GetCoreKey());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetNoPermissionControlDisplayModeConfigKey()
        {
            return string.Format("{0}-NoPermissionControlDisplayMode", GetPermissionKey());
        }
        #endregion

        #region 用户功能
        /// <summary>
        /// 用户的缓存前缀（包括用户实体，用户列表等）
        /// </summary>
        /// <returns></returns>
        public static string GetUserPrefixKey()
        {
            return string.Format("{0}-User", GetCoreKey());
        }

        /// <summary>
        /// 得到用户列表的缓存前缀
        /// </summary>
        /// <returns></returns>
        public static string GetUserListPrefixKey()
        {
            return string.Format("{0}s-", GetUserPrefixKey());
        }

        public static string GetUserClassTypeKey()
        {
            return string.Format("{0}ClassType", GetUserPrefixKey());
        }

        public static string GetUserByGuidKey(Guid userGuid)
        {
            return string.Format("{0}-ByGuid{1}", GetUserPrefixKey(), userGuid);
        }

        public static string GetUserByNameKey(string userName)
        {
            return string.Format("{0}-ByName{1}", GetUserPrefixKey(), userName);
        }

        public static string GetUserByEMailKey(string userEMail)
        {
            return string.Format("{0}-ByEMail{1}", GetUserPrefixKey(), userEMail);
        }

        public static string GetByUserIDCardKey(string userIDCard)
        {
            return string.Format("{0}-ByIDCard{1}", GetUserPrefixKey(), userIDCard);
        }

        public static string GetUserByAccountKey(string userAccount)
        {
            return string.Format("{0}-ByAccount{1}", GetUserPrefixKey(), userAccount);
        }

        /// <summary>
        /// 用户所属角色的缓存键
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static string GetUserRolesByGuidKey(Guid userGuid)
        {
            return string.Format("{0}-Roles-ByGuid{1}", GetUserPrefixKey(), userGuid);
        }

        public static string GetUsersByDepartment(string departmentFullPath, bool isIncludeSubDepartment)
        {
            return string.Format("{0}ByDepartmentFullPath{1}-isIncludeSubDepartment{2}", GetUserListPrefixKey(), departmentFullPath, isIncludeSubDepartment);
        }

        public static string GetUserGuidsByDepartment(string departmentFullPath, bool isIncludeSubDepartment)
        {
            return string.Format("{0}GuidsByDepartmentFullPath{1}-isIncludeSubDepartment{2}", GetUserListPrefixKey(), departmentFullPath, isIncludeSubDepartment);
        }

        public static string GetUsersByDepartment(string departmentCode)
        {
            return string.Format("{0}ByDepartmentCode{1}", GetUserListPrefixKey(), departmentCode);
        }

        public static string GetUsersByDepartment(int departmentID)
        {
            return string.Format("{0}ByDepartmentID{1}", GetUserListPrefixKey(), departmentID);
        }

        public static string GetUsersByDepartment(Guid departmentGuid)
        {
            return string.Format("{0}ByDepartmentGuid{1}", GetUserListPrefixKey(), departmentGuid);
        }

        public static string GetUserCountKey(string whereClause)
        {
            return string.Format("{0}Count{1}", GetUserListPrefixKey(), whereClause);
        }

        public static string GetUserListKey(string whereClause)
        {
            return string.Format("{0}where{1}", GetUserListPrefixKey(), whereClause);
        }

        public static string GetUserPagedKey(int startIndex, int endIndex, string whereClause, string orderClause)
        {
            return string.Format("{0}startIndex{1}_endIndex{2}_whereClause{3}_orderClause{4}",
                GetUserListPrefixKey(), startIndex, endIndex, whereClause, orderClause);
        }

        /// <summary>
        /// 获取用户浏览历史的key
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetUserBrowseHistoryKey(string userName)
        {
            return string.Format("{0}-UserBrowseHistory{1}", GetCoreKey(), userName);
        }
        #endregion

        #region 角色功能
        public static string GetRoleListPrefixKey()
        {
            return string.Format("{0}-Roles-", GetCoreKey());
        }

        public static string GetRoleByNameKey(string roleName)
        {
            return string.Format("{0}-Role-ByName{1}", GetCoreKey(), roleName);
        }

        public static string GetRoleByGuidKey(Guid roleGuid)
        {
            return string.Format("{0}-Role-ByGuid{1}", GetCoreKey(), roleGuid);
        }

        /// <summary>
        /// 角色内的用户的缓存键
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static string GetRoleUsersByGuidKey(Guid roleGuid)
        {
            return string.Format("{0}-Role-Users-ByGuid{1}", GetCoreKey(), roleGuid);
        }

        /// <summary>
        /// 角色内的用户的缓存键
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static string GetRoleUsersByNameKey(string roleName)
        {
            return string.Format("{0}-Role-Users-ByName{1}", GetCoreKey(), roleName);
        }

        public static string GetRoleListKey(Logics onlyDisplayUsable, string whereClause)
        {
            return string.Format("{0}onlyDisplayUsable:{1}-whereClause:{2}", GetRoleListPrefixKey(), onlyDisplayUsable, whereClause);
        }
        #endregion

        #region 地区功能
        public static string GetAreaListPrefixKey()
        {
            return string.Format("{0}-Areas-", GetCoreKey());
        }

        public static string GetAreaByCodeKey(string areaCode)
        {
            return string.Format("{0}-Area-ByCode{1}", GetCoreKey(), areaCode);
        }

        public static string GetAreaCountKey(string whereClause)
        {
            return string.Format("{0}Count{1}", GetAreaListPrefixKey(), whereClause);
        }

        public static string GetAreaListKey(string parentAreaCode, Logics onlyDisplayUsable)
        {
            return string.Format("{0}ParentAreaCode{1}_CanUsable{2}", GetAreaListPrefixKey(), parentAreaCode, (int)onlyDisplayUsable);
        }

        public static string GetAreaPagedKey(int startIndex, int endIndex, string whereClause, string orderClause)
        {
            return string.Format("{0}startIndex{1}_endIndex{2}_whereClause{3}_orderClause{4}",
                GetAreaListPrefixKey(), startIndex, endIndex, whereClause, orderClause);
        }
        #endregion

        #region 附件功能
        public static string GetAttachmentListPrefixKey()
        {
            return string.Format("{0}-Attachments-", GetCoreKey());
        }

        public static string GetAttachmentByGuidKey(Guid attachmentGuid)
        {
            return string.Format("{0}-Attachment-ByGuid{1}", GetCoreKey(), attachmentGuid);
        }

        public static string GetAttachmentCountKey(string whereClause)
        {
            return string.Format("{0}Count{1}", GetAttachmentListPrefixKey(), whereClause);
        }

        public static string GetAttachmentListKey(string whereClause)
        {
            return string.Format("{0}Where{1}", GetAreaListPrefixKey(), whereClause);
        }

        public static string GetAttachmentPagedKey(int startIndex, int endIndex, string whereClause, string orderClause)
        {
            return string.Format("{0}startIndex{1}_endIndex{2}_whereClause{3}_orderClause{4}",
                GetAttachmentListPrefixKey(), startIndex, endIndex, whereClause, orderClause);
        }
        #endregion
    }
}
