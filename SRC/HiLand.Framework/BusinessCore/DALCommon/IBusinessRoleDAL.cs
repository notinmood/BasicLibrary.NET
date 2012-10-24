using System;
using System.Collections.Generic;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore.DAL
{
    public interface IBusinessRoleDAL
    {
        /// <summary>
        /// 判断是否存在某个名称的角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsExistRole(string roleName);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BusinessRole CreateRole(IBusinessRole entity, out CreateUserRoleStatuses status);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateRole(IBusinessRole entity);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        bool DeleteRole(Guid roleGuid);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        BusinessRole Get(Guid roleGuid);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        BusinessRole Get(string roleName);


        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        List<BusinessRole> GetList(Logics onlyDisplayUsable, string whereClause);

        /// <summary>
        /// 获取角色内的用户
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        List<BusinessUser> GetUsers(Guid roleGuid);

        /// <summary>
        /// 获取角色内的用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        List<BusinessUser> GetUsers(string roleName);
    }
}
