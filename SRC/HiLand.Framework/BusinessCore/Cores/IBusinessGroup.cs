using System;
using System.Collections.Generic;
using HiLand.Framework.Membership;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 组实体的接口
    /// </summary>
    public interface IBusinessGroup : IExecutorObject
    {
        int GroupID
        {
            get;
            set;
        }

        Guid GroupGuid
        {
            get;
            set;
        }

        string GroupName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户组的权限集合
        /// </summary>
        Dictionary<Guid, PermissionItem> PermissionItems
        {
            get;
            set;
        }
    }
}
