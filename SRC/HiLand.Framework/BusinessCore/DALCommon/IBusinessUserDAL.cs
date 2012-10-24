using System;
using System.Collections.Generic;
using HiLand.Utility.Enums;
using HiLand.Utility.Paging;

namespace HiLand.Framework.BusinessCore.DAL
{
    public interface IBusinessUserDAL
    {
        /// <summary>
        /// 判断用户的用户名和EMail是否在系统内存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        bool IsExistUser(string userName, string userEMail, string userIDCard);

        /// <summary>
        /// 判断用户账号是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsExistUserName(string userName);

        /// <summary>
        /// 判断用户的EMail是否存在
        /// </summary>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        bool IsExistUserEMail(string userEMail);

        /// <summary>
        /// 判断用户的身份证是否存在
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <returns></returns>
        bool IsExistUserIDCard(string userIDCard);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BusinessUser CreateUser(IBusinessUser entity, out CreateUserRoleStatuses status);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateUser(IBusinessUser entity);

        /// <summary>
        /// 变更用户的全路径
        /// </summary>
        /// <param name="originalFullPath"></param>
        /// <param name="newFullpath"></param>
        bool ChangeFullPath(string originalFullPath, string newFullpath);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        bool DeleteUser(Guid userGuid);

        /// <summary>
        /// 改变用户的状态
        /// </summary>
        /// <param name="newUserStatus"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        bool SetUserStatus(Guid userGuid, UserStatuses newUserStatus);



        /// <summary>
        /// 更新用户的最后访问信息
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="lastIP"></param>
        /// <param name="lastTime"></param>
        void UpdateLastInfo(Guid userGuid, string lastIP, DateTime lastTime);

        /// <summary>
        /// 修改用户的口令
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="newPassword"></param>
        /// <param name="passwordEncrytType"></param>
        /// <param name="passwordEncrytSalt"></param>
        bool ChangePassword(Guid userGuid, string newPassword, int passwordEncrytType, string passwordEncrytSalt);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userAccount">其可以是用户的UserName,也可以是其EMail</param>
        /// <param name="password"></param>
        /// <returns></returns>
        BusinessUser Login(string userAccount, string password, out LoginStatuses status);

        /// <summary>
        /// 使用用户名称和口令登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        BusinessUser LoginWithUserName(string userName, string password, out LoginStatuses status);

        /// <summary>
        /// 使用Email和口令进行登录
        /// </summary>
        /// <param name="userEMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        BusinessUser LoginWithUserEMail(string userEMail, string password, out LoginStatuses status);

        /// <summary>
        /// 使用身份证和口令进行登录
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        BusinessUser LoginWithUserIDCard(string userIDCard, string password, out LoginStatuses status);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        BusinessUser Get(Guid userGuid);

        /// <summary>
        /// 获取用户(根据用户账号)
        /// </summary>
        /// <param name="userAccount">此处的账号可以是用户名或者用户EMail</param>
        /// <returns></returns>
        BusinessUser Get(string userAccount);

        /// <summary>
        /// 获取用户(根据用户名)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        BusinessUser GetByUserName(string userName);

        /// <summary>
        /// 获取用户(根据用户EMail)
        /// </summary>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        BusinessUser GetByUserEMail(string userEMail);

        /// <summary>
        /// 获取用户(根据用户IDCard)
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <returns></returns>
        BusinessUser GetByUserIDCard(string userIDCard);

        /// <summary>
        /// 根据部门FullPath获取用户Guid集合
        /// </summary>
        /// <param name="departmentFullPath">部门编码</param>
        /// <param name="isIncludeSubDepartment">是否包含子部门人员</param>
        /// <returns></returns>
        List<Guid> GetUserGuidsByDepartment(string departmentFullPath,bool isIncludeSubDepartment);

        /// <summary>
        /// 根据部门FullPath获取用户集合
        /// </summary>
        /// <param name="departmentFullPath">部门编码</param>
        /// <param name="isIncludeSubDepartment">是否包含子部门人员</param>
        /// <returns></returns>
        List<BusinessUser> GetUsersByDepartment(string departmentFullPath, bool isIncludeSubDepartment);

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentCode">部门编码</param>
        /// <returns></returns>
        List<BusinessUser> GetUsersByDepartment(string departmentCode);

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        List<BusinessUser> GetUsersByDepartment(int departmentID);

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentGuid">部门GUID</param>
        /// <returns></returns>
        List<BusinessUser> GetUsersByDepartment(Guid departmentGuid);


        /// <summary>
        /// 获取总的条目
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        int GetTotalCount(string whereClause);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        List<BusinessUser> GetList(string whereClause);

        /// <summary>
        /// 获取一批用户
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <returns></returns>
        PagedEntityCollection<BusinessUser> GetPagedCollection(int startIndex, int endIndex, string whereClause, string orderClause);

        //#region 权限
        ///// <summary>
        ///// 获取用户的允许权限
        ///// </summary>
        ///// <param name="userGuid"></param>
        ///// <returns></returns>
        //Dictionary<Guid, PermissionItem> GetPermissionItemsSelfAllow(Guid userGuid);

        ///// <summary>
        ///// 获取用户的拒绝权限
        ///// </summary>
        ///// <param name="userGuid"></param>
        ///// <returns></returns>
        // Dictionary<Guid, PermissionItem> GetPermissionItemsSelfDeny(Guid userGuid);

        ///// <summary>
        ///// 更新用户的允许权限
        ///// </summary>
        ///// <param name="userGuid"></param>
        ///// <param name="permissionItems"></param>
        // void UpdatePermissionSelfAllow(Guid userGuid, Dictionary<Guid, PermissionItem> permissionItems);

        ///// <summary>
        ///// 更新用户的拒绝权限
        ///// </summary>
        ///// <param name="userGuid"></param>
        ///// <param name="permissionItems"></param>
        // void UpdatePermissionSelfDeny(Guid userGuid, Dictionary<Guid, PermissionItem> permissionItems);
        //#endregion

        #region 角色
        /// <summary>
        /// 获取用户所拥有的角色
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
         List<BusinessRole> GetRoles(Guid userGuid);

        /// <summary>
        /// 更新用户所属的角色
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="roleGuidList"></param>
         void UpdateUserRoles(Guid userGuid, List<Guid> roleGuidList);
        #endregion

        #region 辅助方法
        /// <summary>
        /// 处理用户口令的加密逻辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public static IBusinessUser DealWithPassword(IBusinessUser entity);

        //public static IBusinessUser Load(SqlDataReader reader);
        #endregion
    }
}
