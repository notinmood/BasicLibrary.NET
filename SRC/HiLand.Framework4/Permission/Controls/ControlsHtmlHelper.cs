using System.Web.Mvc;

namespace HiLand.Framework4.Permission.Controls
{
    public static class ControlsHtmlHelper
    {
        /// <summary>
        /// 超连接控件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AControl HiA(string name = "")
        {
            return new AControl().Name(name);
        }

        /// <summary>
        /// 超连接控件
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AControl HiA(this HtmlHelper htmlHelper, string name = "")
        {
            return new AControl().Name(name);
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SubmitControl HiSubmit(string name = "")
        {
            return new SubmitControl().Name(name);
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SubmitControl HiSubmit(this HtmlHelper htmlHelper, string name = "")
        {
            return new SubmitControl().Name(name);
        }
    }
}
