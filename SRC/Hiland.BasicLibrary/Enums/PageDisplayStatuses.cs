using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 页面展示实体信息的方式
    /// </summary>
    public enum PageDisplayModes
    {
        /// <summary>
        /// 单纯展示
        /// </summary>
        Display = 0,
        /// <summary>
        /// 添加实体
        /// </summary>
        Add=1,
        /// <summary>
        /// 展示并允许修改
        /// </summary>
        Edit=2,
    }
}
