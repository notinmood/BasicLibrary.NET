using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// 多选框控件
    /// </summary>
    public class CheckBoxControl : StatusSelectControl<CheckBoxControl>
    {
        /// <summary>
        /// 控件类型的名称（checkbox还是radio）
        /// </summary>
        protected override string InputTypeName
        {
            get { return "checkbox"; }
        }

        /// <summary>
        /// CSS类的名称
        /// </summary>
        protected override string cssClassName
        {
            get { return "checkbox"; }
        }
    }
}
