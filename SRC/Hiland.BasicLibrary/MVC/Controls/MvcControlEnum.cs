using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiland.BasicLibrary4.MVC.Controls
{
    /// <summary>
    /// 控件的使用模式（是用于显示内容，还是用于编辑输入）
    /// </summary>
    public enum MvcControlUsingModes
    {
        /// <summary>
        /// 显示模式
        /// </summary>
        Display = 1,

        /// <summary>
        /// 编辑模式
        /// </summary>
        Editable = 2,
    }
}
