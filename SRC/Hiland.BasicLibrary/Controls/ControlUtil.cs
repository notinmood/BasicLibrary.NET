using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public static class ControlUtil
    {
        /// <summary>
        /// 将内容截取前N个字符，并用带title提示的span标签显示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="remainCharCount"></param>
        /// <param name="postFixAdded"></param>
        /// <returns></returns>
        public static string TitledAndTrimedSpan(string text, int remainCharCount, string postFixAdded)
        { 
            string displayText= StringHelper.SubString(text,remainCharCount,postFixAdded);
            return string.Format("<span title=\"{0}\">{1}</span>", text, displayText);
        }

        /// <summary>
        /// 将内容截取前N个字符，并用带title提示的span标签显示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="remainCharCount"></param>
        /// <returns></returns>
        public static string TitledAndTrimedSpan(string text, int remainCharCount)
        {
            return TitledAndTrimedSpan(text, remainCharCount, "...");
        }
    }
}
