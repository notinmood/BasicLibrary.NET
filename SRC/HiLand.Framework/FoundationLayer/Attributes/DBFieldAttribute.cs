using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Framework.FoundationLayer.Attributes
{
    /// <summary>
    /// 实体成员（属性，字段等）的对应数据库字段信息
    /// </summary>
    public class DBFieldAttribute:Attribute
    {
        /// <summary>
        /// 数据库中字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 是否为业务主键
        /// </summary>
        public bool IsBusinessPrimaryKey { get; set; }

        /// <summary>
        /// 是否为数据库主键
        /// </summary>
        public bool IsDBPrimaryKey { get; set; }

        /// <summary>
        /// 是否为标识字段（即自增加的字段）
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否可以为null
        /// </summary>
        public bool IsNullalbe { get; set; }

        /// <summary>
        /// 字段的扩展模式
        /// </summary>
        public FieldExtendModes FieldExtendMode { get; set; }
    }
}
