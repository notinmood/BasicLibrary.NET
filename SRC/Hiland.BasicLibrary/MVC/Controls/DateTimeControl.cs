//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Resources;

//namespace Hiland.BasicLibrary4.MVC.Controls
//{
//   /// <summary>
//   /// 日期显示控件
//   /// </summary>
//    public class DateTimeControl : TextBoxControl<DateTimeControl>
//    {
//        /// <summary>
//        /// CSS类的名称
//        /// </summary>
//        protected override string cssClassName
//        {
//            get { return "datetime"; }
//        }

//        private string dateInputOptions = "{format: 'yyyy/mm/dd'}";
//        /// <summary>
//        /// 日期输入框的可选项
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public DateTimeControl DateInputOptions(string data)
//        {
//            this.dateInputOptions = data;
//            return this;
//        }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            string result= base.WriteCoreHtml();
//            result += WriteDateTimeScript();

//            return result;
//        }

//        /// <summary>
//        /// 绘制日期使用的脚本
//        /// </summary>
//        /// <returns></returns>
//        private string WriteDateTimeScript()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append("<script type=\"text/javascript\">");
//            sb.Append(" jQuery(document).ready(function () {");
//            sb.AppendFormat("$(\".{0}\").dateinput({1});",MvcControlCssPrefix+cssClassName,dateInputOptions);
//            sb.Append("});</script>");

//            return sb.ToString();
//        }
//    }
//}
