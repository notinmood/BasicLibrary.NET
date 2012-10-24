using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 部门实体的接口
    /// </summary>
    public interface IBusinessDepartment : IExecutorObject
    {
        #region 实体信息
        int DepartmentID
        {
            get;
            set;
        }

        Guid DepartmentGuid
        {
            get;
            set;
        }

        string DepartmentName
        {
            get;
            set;
        }

        string DepartmentNameShort
        {
            get;
            set;
        }

        string DepartmentDescription
        {
            get;
            set;
        }

        /// <summary>
        /// 部门的全路径信息
        /// </summary>
        /// <remarks>
        /// 全路径信息的结构类似如下：根部门名称/子部门名称/子子部门名称/.../当前部门名称
        /// </remarks>
        string DepartmentFullPath
        {
            get;
            set;
        }

        string DepartmentCode
        {
            get;
            set;
        }

        Guid DepartmentParentGuid
        {
            get;
            set;
        }

        DepartmentTypes DepartmentType
        {
            get;
            set;
        }

        Logics DepartmentIsSpecial
        {
            get;
            set;
        }

        Logics CanUsable
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 角色的权限集合
        /// </summary>
        Dictionary<Guid, PermissionItem> PermissionItems
        {
            get;
        }
    }
}
