using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// 多行多列的文本控件
    /// </summary>
    public class TextAreaControl : InputControl<TextAreaControl>
    {
        private int rows = 0;
        /// <summary>
        /// 文本框的行数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TextAreaControl Rows(int data)
        {
            this.rows = data;
            return this;
        }

        private int cols = 0;
        /// <summary>
        /// 文本框的列数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TextAreaControl Cols(int data)
        {
            this.cols = data;
            return this;
        }

        /// <summary>
        /// 绘制控件核心部分的Html代码
        /// </summary>
        /// <returns></returns>
        protected override string WriteCoreHtml()
        {
            string result = string.Empty;
            TagBuilder tagInput = CreateTag("textarea");

            if (this.usingMode == MvcControlUsingModes.Display)
            {
                tagInput.Attributes["disabled"] = "disabled";
            }

            if (rows > 0)
            {
                tagInput.Attributes["rows"] = rows.ToString();
            }

            if (cols > 0)
            {
                tagInput.Attributes["cols"] = cols.ToString();
            }
            tagInput.InnerHtml = value;
            result = tagInput.ToString();
            return result;
        }
    }
}
