using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 主体、客体行为对象的类型
    /// </summary>
    /// <remarks>
    /// 行为主体：行为的主动提出对象、使用对象（如某人执行某事的时候，某人即为行为主体）
    /// 行为客体：行为指派的被动对象（如给某人赋权限的时候，某人即为行为的客体）
    /// </remarks>
    public enum ExecuterTypes
    {
        /// <summary>
        /// 部门
        /// </summary>
        Department = 1,

        /// <summary>
        /// 组
        /// </summary>
        Group = 2,

        /// <summary>
        /// 角色
        /// </summary>
        Role= 3,

        /// <summary>
        /// 用户
        /// </summary>
        User = 4,
    }
}