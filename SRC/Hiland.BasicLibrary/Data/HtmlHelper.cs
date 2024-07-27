using System.Text.RegularExpressions;

namespace Hiland.BasicLibrary.Data
{
    /// <summary>
    /// HTML操作辅助类
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// 清除文本中的html标签
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static string StripHTML(string htmlText)
        {
            return StripHTML(htmlText, false);
        }

        /// <summary>
        /// 清除文本中的html标签
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="isRetainSpace">是否在原来标签的地方保留空格</param>
        /// <returns></returns>
        public static string StripHTML(string htmlText, bool isRetainSpace)
        { 
            return StripHTML( htmlText,  isRetainSpace, true);
        }

        /// <summary>
        /// 清除文本中的html标签
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="isRetainSpace">是否在原来标签的地方保留空格</param>
        /// <param name="isRemoveMisc">是否移除JS，Style等信息</param>
        /// <returns></returns>
        public static string StripHTML(string htmlText, bool isRetainSpace,bool isRemoveMisc)
        {
            string replacedString = string.Empty ;
            if (isRetainSpace)
            {
                replacedString = " ";
            }

            //去掉脚本，样式等信息
            if (isRemoveMisc == true)
            {
                htmlText = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(htmlText, replacedString);
                htmlText = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(htmlText, replacedString);
                htmlText = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(htmlText, replacedString);
                htmlText = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(htmlText, replacedString);
            }

            ////去掉空白字符
            //Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //input = objReg2.Replace(input, " ");

            Regex reg = new Regex("<[^>]*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.Replace(htmlText, replacedString);
        }

        /// <summary>
        /// 将文本文件中的换行替换为HTML中的换行（主要用于在网页上显示由textarea产生的内容）
        /// </summary>
        /// <param name="data">传入文本字符串</param>
        /// <returns></returns>
        public static string ConvertPlainTextToHtml(string data)
        {
            if (data.Trim().Length == 0)
            {
                return string.Empty;
            }

            data = data.Replace("\r\n", "<br/>");
            data = data.Replace("\n", "<br/>");
            data = data.Replace("\r", "<br/>");

            data = data.Replace("\t", "&nbsp;&nbsp;");
            data = data.Replace("  ", "&nbsp;&nbsp;");

            return data;
        }

        /// <summary>
        /// 将HTML中的换行替换为文本文件中的换行
        /// </summary>
        /// <param name="data">传入HTML字符串</param>
        /// <returns></returns>
        public static string ConvertHtmlToPlainText(string data)
        {
            if (data.Trim().Length == 0)
            {
                return string.Empty;
            }

            data = data.Replace("<br>", "\r\n");
            data = data.Replace("<br >", "\r\n");
            data = data.Replace("<br/>", "\r\n");
            data = data.Replace("<br />", "\r\n");

            data = data.Replace("&nbsp;&nbsp;", "  ");

            return data;
        }
    }
}
