using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace HiLand.Utility.Web
{
    public static class WebHelper
    {
        /// <summary>
        /// 当前请求对应的Server信息
        /// </summary>
        public static HttpServerUtility Server
        {
            get 
            { 
                return HttpContext.Current.Server;
            }
        }

        /// <summary>
        /// 将普通的虚拟目录转化成应用程序的虚拟目录（以“~/”开头格式的目录）
        /// </summary>
        /// <param name="virtualPath">普通文件的虚拟路径</param>
        /// <returns></returns>
        public static string GetRelativeVirtualPath(string virtualPath)
        {
            if (virtualPath.StartsWith("~/") == false)
            {
                if (virtualPath.StartsWith("/") == false)
                {
                    virtualPath = "/" + virtualPath;
                }

                if (virtualPath.StartsWith("~") == false)
                {
                    virtualPath = "~" + virtualPath;
                }
            }

            return virtualPath;
        }

        /// <summary>
        /// 获取绝对目录
        /// </summary>
        /// <param name="virtualPath">文件的虚拟路径</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string virtualPath)
        {
            string result = virtualPath;
            result = VirtualPathUtility.ToAbsolute(virtualPath);

            return result;
        }

        /// <summary>
        /// 获取虚拟路径对应的物理路径
        /// </summary>
        /// <param name="virtualPath">文件的虚拟路径</param>
        /// <returns></returns>
        public static string GetPhysicalPath(string virtualPath)
        {
            return RequestHelper.CurrentRequest.MapPath(virtualPath);
        }

        /// <summary>
        /// 获取虚拟路径对应的物理路径
        /// </summary>
        /// <param name="virtualPath">文件的虚拟路径</param>
        /// <returns></returns>
        public static string MapPath(string virtualPath)
        { 
            return GetPhysicalPath(virtualPath);
        }

        /// <summary>
        /// 是否为本机服务器
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     当前请求的服务是否位于本机上（主要针对开发人员的授权时使用）
        /// </remarks>
        public static bool IsSelfServer
        {
            get
            {
                bool isSelf = false;

                string hostAddress = RequestHelper.CurrentRequest.UserHostAddress.ToLower();
                if (hostAddress == "127.0.0.1" || hostAddress == "localhost")
                {
                    isSelf = true;
                }

                return isSelf;
            }
        }

        /// <summary>
        /// 是否为本地服务器
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     本机服务器，局域网内的服务器均为本地服务器
        ///     局域网可用的ip地址范围为:
        ///         A类地址10.0.0.0 - 10.255.255.255
        ///         b类网172.16.0.0 - 172.31.255.255
        ///         c类网192.168.0.0 -192.168.255.255
        /// </remarks>
        public static bool IsLocalServer
        {
            get
            {
                bool isLocal = false;

                //判断本机
                isLocal = IsSelfServer;
                if (isLocal == true)
                {
                    return true;
                }

                //判断局域网
                string hostAddress = RequestHelper.CurrentRequest.UserHostAddress.ToLower();
                if (hostAddress.StartsWith("10.") || hostAddress.StartsWith("172.") || hostAddress.StartsWith("192.168."))
                {
                    isLocal = true;
                }

                return isLocal;
            }
        }

        /// <summary>
        /// 是否为远程服务器
        /// </summary>
        /// <returns></returns>
        public static bool IsRemoteServer
        {
            get
            {
                bool isLocal = IsLocalServer;

                if (isLocal == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetClientIP()
        {
            return ClientBrowser.GetClientIP();
        }
    }
}
