using System;
using System.Configuration;
using System.Web;
using HiLand.Utility.Data;

namespace HiLand.Utility.Module
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 1.使用此模块验证SQL注入，需要在web.config中配置节点sqlErrorPage,表示出错后的跳转页面
    /// 2.使用此模块验证SQL注入，需要在web.config中配置节点FilterSqlString,表示要过滤哪些sql关键字
    ///    其格式如下: ‘|[|]|--|declare|exec 即多个关键字之间用|分割
    /// </remarks>
    public class AntiSQLInjectionModule : IHttpModule
    {
        public void Dispose()
        {
            //
        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
        }

        private void context_AcquireRequestState(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = (application).Context;

            try
            {
                string getkeys = string.Empty;
                string sqlErrorPage = string.Empty;//转向的错误提示页面 
                sqlErrorPage = ConfigurationManager.AppSettings["sqlErrorPage"];
                if (string.IsNullOrEmpty(sqlErrorPage))
                {
                    sqlErrorPage = "~/SqlErrorPage.aspx";
                }
                string keyvalue = string.Empty;

                string requestUrl = context.Request.Path.ToString();
                //url提交数据
                if (context.Request.QueryString != null)
                {
                    for (int i = 0; i < context.Request.QueryString.Count; i++)
                    {
                        getkeys = context.Request.QueryString.Keys[i];
                        keyvalue = context.Server.UrlDecode(context.Request.QueryString[getkeys]);

                        if (SQLInjectionHelper.IsSQLInjectionSafe(keyvalue) == false)
                        {
                            application.CompleteRequest();
                            context.Response.Redirect(sqlErrorPage);
                            context.Response.End();
                            break;
                        }
                    }
                }
                //表单提交数据
                if (context.Request.Form != null)
                {
                    for (int i = 0; i < context.Request.Form.Count; i++)
                    {
                        getkeys = context.Request.Form.Keys[i];
                        keyvalue = context.Server.HtmlDecode(context.Request.Form[i]);
                        if (getkeys == "__VIEWSTATE") continue;
                        if (SQLInjectionHelper.IsSQLInjectionSafe(keyvalue) == false)
                        {
                            application.CompleteRequest();
                            context.Response.Redirect(sqlErrorPage);
                            context.Response.End();
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
