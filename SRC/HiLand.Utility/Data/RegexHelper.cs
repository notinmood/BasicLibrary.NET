using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 正则表达式操作辅助类
    /// </summary>
    public static class RegexHelper
    {
        #region 各种格式
        /// <summary>
        /// IP格式匹配字符串
        /// </summary>
        public static string IPFormat = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

        /// <summary>
        /// Email格式匹配字符串
        /// </summary>
        public static string EmailFormat = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        /// <summary>
        /// 匹配中文字符
        /// </summary>
        public static string ChineseCharFormat = @"[\u4e00-\u9fa5]";

        /// <summary>
        /// 匹配双字节字符(包括汉字在内)
        /// </summary>
        public static string DoubleByteCharFormat = @"[^\x00-\xff]";

        /// <summary>
        /// 匹配空白行
        /// </summary>
        public static string WhiteSpaceRowFormat = @"\n\s*\r";

        /// <summary>
        /// 匹配HTML标记
        /// </summary>
        /// <remarks>
        /// 网上流传的版本太糟糕，上面这个也仅仅能匹配部分，对于复杂的嵌套标记依旧无能为力
        /// </remarks>
        public static string HtmlTagFormat = @"<(\S*?)[^>]*>.*?</\1>|<.*? />";

        /// <summary>
        /// 匹配首尾空白字符(包括空格、制表符、换页符等等)
        /// </summary>
        public static string WhiteSpaceCharOnBeginAndEndFormat = @"^\s*|\s*$";

        /// <summary>
        /// 匹配网址URL
        /// </summary>
        /// <remarks>
        /// 网上流传的版本功能很有限，上面这个基本可以满足需求
        /// </remarks>
        public static string UrlFormat = @"[a-zA-z]+://[^\s]*";

        /// <summary>
        /// 匹配帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)
        /// </summary>
        /// <remarks>
        /// 使用时，请根据具体的场景需求，进行调整
        /// </remarks>
        public static string AccountFormat = @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$";

        /// <summary>
        /// 中国邮政编码
        /// </summary>
        public static string PostCodeFormat = @"[1-9]\d{5}(?!\d)";

        /// <summary>
        /// 中国居民身份证
        /// </summary>
        public static string IDCardFormat = @"^(\d{15}|\d{18}|\d{17}(\d|X|x))$";
        #endregion
        /* 
        匹配特定数字：
        ^[1-9]\d*$　 　 //匹配正整数
        ^-[1-9]\d*$ 　 //匹配负整数
        ^-?[1-9]\d*$　　 //匹配整数
        ^[1-9]\d*|0$　 //匹配非负整数（正整数 + 0）
        ^-[1-9]\d*|0$　　 //匹配非正整数（负整数 + 0）
        ^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$　　 //匹配正浮点数
        ^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$　 //匹配负浮点数
        ^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$　 //匹配浮点数
        ^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$　　 //匹配非负浮点数（正浮点数 + 0）
        ^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$　　//匹配非正浮点数（负浮点数 + 0）
        评注：处理大量数据时有用，具体应用时注意修正

        匹配特定字符串：
        ^[A-Za-z]+$　　//匹配由26个英文字母组成的字符串
        ^[A-Z]+$　　//匹配由26个英文字母的大写组成的字符串
        ^[a-z]+$　　//匹配由26个英文字母的小写组成的字符串
        ^[A-Za-z0-9]+$　　//匹配由数字和26个英文字母组成的字符串
        ^\w+$　　//匹配由数字、26个英文字母或者下划线组成的字符串
        评注：最基本也是最常用的一些表达式
         */
    }
}
