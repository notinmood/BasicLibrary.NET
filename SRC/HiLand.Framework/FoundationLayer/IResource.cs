using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 资源数据接口
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// 资源的Guid
        /// </summary>
        Guid ResourceGuid{get;}

        /// <summary>
        /// 资源的名称
        /// </summary>
        string ResourceName { get; }

        /// <summary>
        /// 资源是否被所有人保护中
        /// </summary>
        Logics IsProtectedByOwner 
        { 
            get; 
            set;
        }

        /// <summary>
        /// 资源所有人列表
        /// </summary>
        List<String> OwnerKeys { get; }

        /// <summary>
        /// 资源创建人Key
        /// </summary>
        string CreateUserKey { get; set; }

        /// <summary>
        /// 资源创建人名称
        /// </summary>
        string CreateUserName { get; set; }

        /// <summary>
        /// 资源创建时间
        /// </summary>
        DateTime CreateDate { get; set; }
    }
}
