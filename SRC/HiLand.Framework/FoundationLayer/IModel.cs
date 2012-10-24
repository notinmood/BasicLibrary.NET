using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// 当前实例是否为空对象
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 将当前实例强制设置为空对象（二次开发中请勿直接使用）
        /// </summary>
        void ForceSetEmpty();

        /// <summary>
        /// 实体的名称
        /// </summary>
        string ModelName { get; }

        /// <summary>
        /// 实体的业务主键（区别于数据库的物理主键）
        /// </summary>
        string[] BusinessKeyNames { get; }

        /// <summary>
        /// 实体的业务主键的值（区别于数据库的物理主键）
        /// </summary>
        string[] BusinessKeyValues { get; }

        /// <summary>
        /// 扩展属性的名字
        /// </summary>
        string PropertyNames { get; set; }

        /// <summary>
        /// 扩展属性的值
        /// </summary>
        string PropertyValues { get; set; }
    }
}
