//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;
//using HiLand.Utility.Event;

//namespace HiLand.Utility.Native
//{
//    //TODO:目前仅实行了带一个参数的委托（有返回值、无返回值），如果需要传递多个参数请重载CrossThreadInvoke方法

//    /// <summary>
//    /// 控件操作帮助器
//    /// </summary>
//    public static class ControlHelper
//    {
//        /// <summary>
//        /// 对控件的跨线程调用
//        /// </summary>
//        /// <typeparam name="T">委托参数的类型</typeparam>
//        /// <param name="control">目标控件</param>
//        /// <param name="action">无返回值的委托</param>
//        /// <param name="args">委托的参数</param>
//        public static void CrossThreadInvoke<T>(Control control, Actions<T> action, T args)
//        {
//            if (control.InvokeRequired == true)
//            {
//                control.Invoke(action, args);
//            }
//            else
//            {
//                action(args);
//            }
//        }

//        /// <summary>
//        /// 对控件的跨线程调用
//        /// </summary>
//        /// <typeparam name="T">委托参数的类型</typeparam>
//        /// <typeparam name="TResult">方法返回值的类型</typeparam>
//        /// <param name="control">目标控件</param>
//        /// <param name="func">有回值的委托</param>
//        /// <param name="args">委托的参数</param>
//        public static TResult CrossThreadInvoke<T, TResult>(Control control, Funcs<T, TResult> func, T args)
//        {
//            if (control.InvokeRequired == true)
//            {
//                return (TResult)control.Invoke(func, args);
//            }
//            else
//            {
//                return func(args);
//            }
//        }
//    }
//}
