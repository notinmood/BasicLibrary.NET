using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.DAL;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore.BLL
{
    /// <summary>
    /// 权限业务逻辑类
    /// </summary>
    public class BusinessPermissionBLL : BaseBLL<BusinessPermissionBLL, BusinessPermission, BusinessPermissionDAL>
    {
        /// <summary>
        /// 判断某一个所有者是否对某项目拥有某种模式（允许/拒绝）的权限
        /// </summary>
        /// <param name="ownerKey">所有者的标识(通常是Guid)</param>
        /// <param name="permissionItemGuid">待验证的项目的Guid(通常是子模块的Guid)</param>
        /// <param name="permissionItemValue">待验证的项目的值</param>
        /// <param name="permissionMode">权限模式（允许权限还是拒绝权限）</param>
        /// <returns></returns>
        public bool HasPermission(string ownerKey,PermissionModes permissionMode, Guid permissionItemGuid, int permissionItemValue)
        {
            bool isAllow = false;
            List<BusinessPermission> permissions = GetPermissions(ownerKey, permissionMode);

            if (permissions != null)
            {
                foreach (var item in permissions)
                {
                    if (item.PermissionItemGuid == permissionItemGuid && (item.PermissionItemValue & permissionItemValue) == permissionItemValue)
                    {
                        isAllow = true;
                    }
                }
            }
            return isAllow;
        }

        /// <summary>
        /// 获取某所有者拥有的所有权限的集合
        /// </summary>
        /// <param name="ownerKey">所有者标识</param>
        /// <param name="permissionMode">权限模式（允许权限还是拒绝权限）</param>
        public List<BusinessPermission> GetPermissions(string ownerKey,PermissionModes permissionMode)
        {
            string whereClause = string.Format("OwnerKey='{0}' AND PermissionMode={1} ", ownerKey,(int)permissionMode);
            return this.LoadDAL.GetList(Logics.False, whereClause,0,string.Empty);
        }
    }
}
