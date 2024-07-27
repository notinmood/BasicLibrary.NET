//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Hiland.BasicLibrary.Web;

//namespace Hiland.BasicLibrary4.MVC.Controls
//{
//    /// <summary>
//    /// 控件使用中的辅助类工具
//    /// </summary>
//    public static class ControlHelper
//    {
//        /// <summary>
//        /// 获取某控件实际选择的值（通常是一个ID，Guid等信息，因为某些控件显示的值通常为一个对终端用户友好的文本值）
//        /// </summary>
//        /// <typeparam name="T">待获取数据的类型</typeparam>
//        /// <param name="controlName">控件的名称</param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 目前可以获取选择项的实际值的控件有：树控件
//        /// </remarks>
//        public static T GetRealValue<T>(string controlName)
//        {
//            return GetRealValue<T>(controlName, default(T));
//        }

//        /// <summary>
//        /// 获取某控件实际选择的值（通常是一个ID，Guid等信息，因为某些控件显示的值通常为一个对终端用户友好的文本值）
//        /// </summary>
//        /// <typeparam name="T">待获取数据的类型</typeparam>
//        /// <param name="controlName">控件的名称</param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 目前可以获取选择项的实际值的控件有：树控件
//        /// </remarks>
//        public static T GetRealValue<T>(string controlName, T defaultValue)
//        {
//            return RequestHelper.GetValue<T>(string.Format("{0}_Value", controlName, defaultValue));
//        }

//        /// <summary>
//        /// 获取某控件选择项显示的值
//        /// </summary>
//        /// <typeparam name="T">待获取数据的类型</typeparam>
//        /// <param name="controlName">控件的名称</param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 目前可以获取选择项的实际值的控件有：下拉控件
//        /// </remarks>
//        public static T GetDisplayText<T>(string controlName)
//        {
//            return GetDisplayText<T>(controlName, default(T));
//        }

//        /// <summary>
//        /// 获取某控件选择项显示的值
//        /// </summary>
//        /// <typeparam name="T">待获取数据的类型</typeparam>
//        /// <param name="controlName">控件的名称</param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 目前可以获取选择项的实际值的控件有：下拉控件
//        /// </remarks>
//        public static T GetDisplayText<T>(string controlName, T defaultValue)
//        {
//            return RequestHelper.GetValue<T>(string.Format("{0}_Text", controlName, defaultValue));
//        }
//    }
//}
