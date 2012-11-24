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
        /// 当前资源是否被Owner保护（被保护的数据，仅能所有者修改，其他人仅能查看）
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
        /// 是否拥有此资源（计算属性，计算当前人的数据权限是否可以拥有此资源；因为并发只有此资源的直接所有人才可以拥有资源，部门领导也可以拥有下属的资源）
        /// </summary>
        bool IsOwning { get; }

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
