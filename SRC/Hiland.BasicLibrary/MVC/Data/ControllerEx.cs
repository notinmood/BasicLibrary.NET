//using System.Web.Mvc;
//using HiLand.Utility.Data;

//namespace HiLand.Utility4.MVC.Data
//{
//    /// <summary>
//    /// Controller扩展器
//    /// </summary>
//    public static class ControllerEx
//    {
//        /// <summary>
//        /// 获取传递给Controller的参数
//        /// </summary>
//        /// <param name="controller"></param>
//        /// <param name="paramName"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static string GetParam(this Controller controller, string paramName,string defaultValue= StringHelper.Empty)
//        {
//            return GetParam<string>(controller, paramName, defaultValue);
//        }

//        /// <summary>
//        /// 获取传递给Controller的参数
//        /// </summary>
//        /// <param name="controller"></param>
//        /// <param name="paramName"></param>
//        /// <param name="defaultValue"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T GetParam<T>(this Controller controller, string paramName,T defaultValue= default(T))
//        {
//            return MVCHelper.GetParam<T>(paramName, defaultValue);
//            ////1.首先从请求中获取信息
//            //T paramValue = RequestHelper.GetValue<T>(paramName);

//            ////2.如果请求中没有此数据，则从controller的路由数据中获取
//            //RouteValueDictionary rvc = controller.RouteData.Values;
//            //if ((paramValue == null || paramValue.Equals(default(T))) && rvc.ContainsKey(paramName))
//            //{
//            //    paramValue = Converter.ChangeType<T>(rvc[paramName]);
//            //}

//            //return paramValue;
//        }

//        /// <summary>
//        /// 从Controller向View传递数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="controller"></param>
//        /// <param name="paramName"></param>
//        /// <param name="paramValue"></param>
//        public static void PassParam<T>(this Controller controller, string paramName, T paramValue)
//        {
//            controller.ViewData[paramName] = paramValue;
//        }

//        /// <summary>
//        /// 从Controller向View传递数据
//        /// </summary>
//        /// <param name="controller"></param>
//        /// <param name="paramName"></param>
//        /// <param name="paramValue"></param>
//        public static void PassParam(this Controller controller, string paramName, object paramValue)
//        {
//            controller.ViewData[paramName] = paramValue;
//        }

//        /// <summary>
//        /// 在Controller中接受参数并传递到View中
//        /// </summary>
//        public static void BrokeParam(this Controller controller, string paramName)
//        {
//            BrokeParam<string>(controller, paramName);
//        }

//        /// <summary>
//        /// 在Controller中接受参数并传递到View中
//        /// </summary>
//        public static void BrokeParam<T>(this Controller controller, string paramName)
//        {
//            T paramValue = GetParam<T>(controller, paramName);
//            PassParam(controller, paramName, paramValue);
//        }
//    }
//}
