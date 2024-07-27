using System;

namespace Hiland.BasicLibrary.Entity
{
    [Flags]
    public enum ValidateTypes
    {
        /// <summary>
        /// 字段或属性是否为空字串
        /// </summary>
        IsEmpty = 1,
        /// <summary>
        /// 字段或属性的最小长度
        /// </summary>
        MinLength = 2,
        /// <summary>
        /// 字段或属性的最大长度
        /// </summary>
        MaxLength = 4,
        /// <summary>
        /// 字段或属性的值是否为数值型
        /// </summary>
        IsNumber = 8,
        /// <summary>
        /// 字段或属性的值是否为时间类型
        /// </summary>
        IsDateTime = 16,
        /// <summary>
        /// 字段或属性的值是否为正确的浮点类型
        /// </summary>
        IsDecimal = 32,

        /// <summary>
        /// 字段或属性的值是否为固定电话号码格式
        /// </summary>
        IsTelphone = 64,
        /// <summary>
        /// 字段或属性的值是否为手机号码格式
        /// </summary>
        IsMobile = 128,
        /// <summary>
        /// 字段或属性的值是否为电子邮件格式
        /// </summary>
        IsEmail=256,

        /// <summary>
        /// 字段或属性的值是否为IP格式
        /// </summary>
        IsIP= 512,
    }
}
