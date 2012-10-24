using System;
using System.Web;

namespace HiLand.Utility.Module
{
    //TODO:
    /// <summary>
    /// 修正模板页中客户端资源路径的Module
    /// </summary>
    /// <remarks>
    ///      模版页中难免要引用CSS、脚本、图片等，这些文件的路径如果简单的使用相对路径，那么如果引用模版的目录一发生变化，这些路径就会出错；
    ///      如果使用绝对路径，又不够灵活，如果应用程序目录发生变化，可能会导致要大量修改。asp.net支持一种相对于应用程序的路径，
    ///      以波浪线开头的，形如"~/"，使用它即可解决，例如：
    ///     <link rel="stylesheet" media="screen" type="text/css" href="<%=ResolveClientUrl("~/css/global.css") %>" />
    ///     当然如果你觉得每个路径都要写成动态的不爽，是可以换一种方式--使用本module：
    ///     接着在所有的路径直接书写为相对于应用程序目录的路径，形如：
    ///     <link rel="stylesheet" media="screen" type="text/css" href="~/css/global.css" />
    /// </remarks>
    public class MasterClientResourcePathModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestContent += new EventHandler(context_PreSendRequestContent);
        }

        void context_PreSendRequestContent(object sender, EventArgs e)
        {
            //HttpApplication context = sender as HttpApplication;
            //TextWriter textWriter= context.Context.Response.Output;

            //Page currentPage = context.Context.CurrentHandler as Page;
            //if (currentPage != null)
            //{

            //    StringWriter stringWriter = new StringWriter();
            //    HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            //    string html = stringWriter.ToString();
            //    #region 转换相对路径
            //    MatchCollection collection = Regex.Matches(html, "<(a|link|img|script|input|form).[^>]*(href|src|action)=(\\\"|'|)(.[^\\\"']*)(\\\"|'|)[^>]*>", RegexOptions.IgnoreCase);
            //    foreach (Match match in collection)
            //    {
            //        if (match.Groups[match.Groups.Count - 2].Value.IndexOf("~") != -1)
            //        {
            //            string url = currentPage.ResolveUrl(match.Groups[match.Groups.Count - 2].Value);
            //            html = html.Replace(match.Groups[match.Groups.Count - 2].Value, url);
            //        }
            //    }
            //    #endregion
            //    //context.Context.Response.OutputStream = new  MemoryStream();// HtmlTextWriter.;
            //    context.Context.Response.Filter = new MemoryStream();
            //}
        }
    }
}
