using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 字符类别序列
    /// </summary>
    public enum CharCategories
    {
        /// <summary>
        /// 只有数字
        /// </summary>
        Number,
        
        /// <summary>
        /// 包含数字和大小写字符
        /// </summary>
        NumberAndChar,
        
        /// <summary>
        /// 包含数字和大写字符
        /// </summary>
        NumberAndCharIgnoreCase,
    }
}
