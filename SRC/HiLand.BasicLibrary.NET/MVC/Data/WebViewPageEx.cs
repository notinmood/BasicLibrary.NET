//using System.Web.Mvc;
//using HiLand.Utility.Data;

//namespace HiLand.Utility4.MVC.Data
//{
//    public static class WebViewPageEx
//    {
//        /// <summary>
//        /// 获取从Controller传递到View中的值
//        /// </summary>
//        /// <typeparam name="TModel"></typeparam>
//        /// <param name="page"></param>
//        /// <param name="paramName"></param>
//        /// <returns></returns>
//        public static string GetPassedParam<TModel>(this WebViewPage<TModel> page, string paramName)
//        {
//            return GetPassedParam<TModel, string>(page, paramName);
//        }

//        /// <summary>
//        /// 获取从Controller传递到View中的值
//        /// </summary>
//        /// <typeparam name="TModel"></typeparam>
//        /// <typeparam name="TParam"></typeparam>
//        /// <param name="page"></param>
//        /// <param name="paramName"></param>
//        /// <returns></returns>
//        public static TParam GetPassedParam<TModel, TParam>(this WebViewPage<TModel> page, string paramName)
//        {
//            object paramValue = page.ViewData[paramName];
//            return Converter.ChangeType<TParam>(paramValue);
//        }
//    }
//}
