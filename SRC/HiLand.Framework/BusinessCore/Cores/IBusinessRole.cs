using System;
using System.Collections.Generic;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 角色实体的接口
    /// </summary>
    public interface IBusinessRole : IExecutorObject
    {
        int RoleID
        {
            get;
            set;
        }

        Guid RoleGuid
        {
            get;
            set;
        }

        string RoleName
        {
            get;
            set;
        }

        Logics CanUsable
        {
            get;
            set;
        }

        Logics IsInnerRole
        {
            get;
            set;
        }

        string RoleDescrition
        {
            get;
            set;
        }

        /// <summary>
        /// 角色的权限集合
        /// </summary>
        Dictionary<Guid, PermissionItem> PermissionItems
        {
            get;
        }
    }
}
