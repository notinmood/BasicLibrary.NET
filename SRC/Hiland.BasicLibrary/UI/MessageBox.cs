//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web;

//namespace Hiland.BasicLibrary.UI
//{
//    public class MessageBox
//    {
//        /// <summary>
//        /// 显示提示信息
//        /// </summary>
//        /// <param name="message">显示提示信息内容</param>
//        public static void Show(string message)
//        {
//            Page page = HttpContext.Current.Handler as Page;
//            if (page != null)
//            {
//                Show(page,message);
//            }
//        }

//        /// <summary>
//        /// 显示提示信息
//        /// </summary>
//        /// <param name="page">调用页面</param>
//        /// <param name="message">显示提示信息内容</param>
//        public static void Show(Page page, string message)
//        {
//            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + message.ToString() + "');</script>");
//        }

//        /// <summary>
//        /// 显示提示信息并且跳转页面
//        /// </summary>
//        /// <param name="message">显示提示信息内容</param>
//        /// <param name="url">跳转的页面地址</param>
//        public static void ShowAndRedirect(string message, string url)
//        {
//            Page page = HttpContext.Current.Handler as Page;
//            if (page != null)
//            {
//                ShowAndRedirect(page, message,url);
//            }
//        }

//        /// <summary>
//        /// 显示提示信息并且跳转页面
//        /// </summary>
//        /// <param name="page">调用页面</param>
//        /// <param name="message">显示提示信息内容</param>
//        /// <param name="url">跳转的页面地址</param>
//        public static void ShowAndRedirect(Page page, string message, string url)
//        {
//            if (url.StartsWith("~/"))
//            {
//                url = page.ResolveClientUrl(url);
//            }

//            StringBuilder Builder = new StringBuilder();
//            Builder.Append("<script language='javascript' defer>");
//            Builder.AppendFormat("alert('{0}');", message);
//            Builder.AppendFormat("self.location.href='{0}'", url);
//            Builder.Append("</script>");
//            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

//        }
//        /// <summary>
//        /// 控件点击 消息确认提示框
//        /// </summary>
//        /// <param name="page">当前页面指针，一般为this</param>
//        /// <param name="message">提示信息</param>
//        public static void ShowConfirm(WebControl Control, string  message)
//        {
//            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
//            Control.Attributes.Add("onclick", "return confirm('" + message + "');");
//        }
//    }
//}
