using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Framework.Membership
{
    public interface IUser
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        string UserName
        {
            set;
            get;
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        UserTypes UserType
        {
            set;
            get;
        }

        /// <summary>
        /// 用户的权限集合(包括操作权限，包括数据权限)
        /// </summary>
        Dictionary<Guid, PermissionItem> PermissionItems
        {
            get;
        }
    }
}
