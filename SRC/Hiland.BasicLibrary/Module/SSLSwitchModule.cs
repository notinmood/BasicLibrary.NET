//using System;
//using System.Configuration;
//using System.Web;
//using Hiland.BasicLibrary.Setting.SectionHandler;
//using Hiland.BasicLibrary.Web;

//namespace Hiland.BasicLibrary.Module
//{
//    /// <summary>
//    /// 根据需求进行http和https之间进行切换的模块
//    /// </summary>
//    public class SSLSwitchModule : IHttpModule
//    {
//        private HttpApplication context;
//        public void Dispose()
//        {
//        }

//        public void Init(HttpApplication context)
//        {
//            this.context = context;
//            context.BeginRequest += new EventHandler(context_BeginRequest);
//        }

//        void context_BeginRequest(object sender, EventArgs e)
//        {
//            SSLSwitchConfig config = ConfigurationManager.GetSection("sslSwitchPaths") as SSLSwitchConfig;
//            if (config != null && config.DeployMode != DeployModes.Off)
//            {
//                string currentVirtualPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
//                bool isContaint = config.IsContaint(currentVirtualPath);

//                bool needJumpForSSLControl = isContaint;
//                bool needJumpForCommonControl = false;

//                if (config.ControlMode == ControlModes.OnlyThus && isContaint == false)
//                {
//                    needJumpForCommonControl = true;
//                }


//                bool needJumpForDeploy = false;
//                switch (config.DeployMode)
//                {
//                    case DeployModes.RemoteOnly:
//                        if (WebHelper.IsRemoteServer == true)
//                        {
//                            needJumpForDeploy = true;
//                        }
//                        break;
//                    case DeployModes.LocalOnly:
//                        if (WebHelper.IsLocalServer == true)
//                        {
//                            needJumpForDeploy = true;
//                        }
//                        break;
//                    case DeployModes.On:
//                    default:
//                        needJumpForDeploy = true;
//                        break;
//                }

//                string currentScheme = HttpContext.Current.Request.Url.Scheme.ToLower();

//                //根据需要将被控制的页面使用安全请求模式
//                if (needJumpForDeploy == true && needJumpForSSLControl == true)
//                {

//                    if (currentScheme == "http")
//                    {
//                        string newPath = "https://" + RequestHelper.GetOriginalUrlWithoutSchemeHeader();
//                        HttpContext.Current.Response.Redirect(newPath);
//                    }
//                }

//                //根据需要将未控制的页面使用普通请求模式
//                if (needJumpForCommonControl == true)
//                {
//                    if (currentScheme == "https")
//                    {
//                        string newPath = "http://" + RequestHelper.GetOriginalUrlWithoutSchemeHeader();
//                        HttpContext.Current.Response.Redirect(newPath);
//                    }
//                }
//            }
//        }
//    }
//}
