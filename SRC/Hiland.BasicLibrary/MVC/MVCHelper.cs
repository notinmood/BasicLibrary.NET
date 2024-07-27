//using System.Web;
//using System.Web.Routing;
//using Hiland.BasicLibrary.Data;
//using Hiland.BasicLibrary.Web;

//namespace Hiland.BasicLibrary4.MVC
//{
//    /// <summary>
//    /// MVC辅助类
//    /// </summary>
//    public static class MVCHelper
//    {
//        /// <summary>
//        /// 当前请求信息中的路由数据
//        /// </summary>
//        public static RouteData CurrentRouteData
//        {
//            get
//            {
//                return HttpContext.Current.Request.RequestContext.RouteData;
//            }
//        }

//        /// <summary>
//        /// 获取当前请求对应的Controller名称
//        /// </summary>
//        /// <returns></returns>
//        public static string GetCurrentControllerName()
//        {
//            var controllerName = CurrentRouteData.Values["controller"].ToString();
//            return controllerName;
//        }

//        /// <summary>
//        /// 获取当前请求对应的Action名称
//        /// </summary>
//        /// <returns></returns>
//        public static string GetCurrentActionName()
//        {
//            var actionName = CurrentRouteData.Values["action"].ToString();
//            return actionName;
//        }

//        /// <summary>
//        /// 获取当前请求对应的命名空间
//        /// </summary>
//        /// <returns></returns>
//        public static string GetCurrentNamespace()
//        {
//            string namespaces = string.Empty;

//            RouteData routeData = CurrentRouteData;
//            if (routeData.DataTokens.ContainsKey("namespaces") == false || routeData.DataTokens["namespaces"] == null)
//            {
//                namespaces = string.Empty;
//            }
//            else
//            {
//                namespaces = (routeData.DataTokens["namespaces"] as string[])[0];
//            }

//            return namespaces;
//        }

//        /// <summary>
//        /// 获取当前请求对应的Area名称
//        /// </summary>
//        /// <returns></returns>
//        public static string GetCurrentAreaName()
//        {
//            RouteData routeData = CurrentRouteData;
//            string areaName = string.Empty;
//            if (routeData.DataTokens.ContainsKey("area") == false || routeData.DataTokens["area"] == null)
//            {
//                areaName = string.Empty;
//            }
//            else
//            {
//                areaName = routeData.DataTokens["area"].ToString();
//            }

//            return areaName;
//        }

//        /// <summary>
//        /// 设置用户自定义数据
//        /// </summary>
//        /// <param name="dataName"></param>
//        /// <param name="dataValue"></param>
//        public static void SetCustomData(string dataName, string dataValue)
//        {
//            SetCustomData<string>(dataName, dataValue);
//        }

//        /// <summary>
//        /// 设置用户自定义数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dataName"></param>
//        /// <param name="dataValue"></param>
//        public static void SetCustomData<T>(string dataName, T dataValue)
//        {
//            CurrentRouteData.Values[dataName] = dataValue;
//        }

//        /// <summary>
//        /// 获取用户自定义数据
//        /// </summary>
//        /// <param name="dataName">数据名称</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static string GetCustomData(string dataName, string defaultValue = StringHelper.Empty)
//        {
//            return GetCustomData<string>(dataName, defaultValue);
//        }

//        /// <summary>
//        /// 获取用户自定义数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dataName">数据名称</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static T GetCustomData<T>(string dataName, T defaultValue = default(T))
//        {
//            object resultObject = CurrentRouteData.Values[dataName];
//            if (resultObject == null)
//            {
//                return defaultValue;
//            }
//            else
//            {
//                return (T)(resultObject);
//            }
//        }

//        /// <summary>
//        /// 获取传递的参数
//        /// </summary>
//        /// <param name="paramName"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static string GetParam(string paramName, string defaultValue = StringHelper.Empty)
//        {
//            return GetParam<string>(paramName, defaultValue);
//        }

//        /// <summary>
//        /// 获取传递的参数
//        /// </summary>
//        /// <param name="defaultValue"></param>
//        /// <param name="paramName"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T GetParam<T>(string paramName, T defaultValue = default(T))
//        {
//            //1.首先从请求中获取信息
//            T paramValue = RequestHelper.GetValue<T>(paramName);

//            //2.如果请求中没有此数据，则从路由数据中获取
//            if ((paramValue == null || paramValue.Equals(default(T))))
//            {
//                paramValue = GetCustomData(paramName, defaultValue);
//            }

//            return paramValue;
//        }
//    }
//}
