using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// 单选框控件
    /// </summary>
    public class RadioButtonControl : StatusSelectControl<RadioButtonControl>
    {
        /// <summary>
        /// 控件类型的名称（checkbox还是radio）
        /// </summary>
        protected override string InputTypeName
        {
            get { return "radio"; }
        }

        /// <summary>
        /// CSS类的名称
        /// </summary>
        protected override string cssClassName
        {
            get { return "radio"; }
        }
    }
}
