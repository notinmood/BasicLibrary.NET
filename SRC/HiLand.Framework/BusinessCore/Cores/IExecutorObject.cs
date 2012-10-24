using System;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    ///  主体、客体行为对象的接口
    /// </summary>
    /// <remarks>
    /// 行为主体：行为的主动提出对象、使用对象（如某人执行某事的时候，某人即为行为主体）
    /// 行为客体：行为指派的被动对象（如给某人赋权限的时候，某人即为行为的客体）
    /// </remarks>
    public interface IExecutorObject
    {
        /// <summary>
        /// 主体、客体行为对象的Guid
        /// </summary>
        Guid ExecutorGuid { get; }

        /// <summary>
        /// 主体、客体行为对象的名称
        /// </summary>
        string ExecutorName { get; }

        /// <summary>
        /// 主体、客体行为对象的类型
        /// </summary>
        ExecuterTypes ExecutorType { get; }
    }
}
