//using System.Windows.Forms;
//using HiLand.Utility.Event;
//using HiLand.Utility.Native;

//namespace HiLand.Utility4.Native
//{
//    /// <summary>
//    /// 控件操作扩展器
//    /// </summary>
//    public static class ControlEx
//    {
//        /// <summary>
//        /// 对控件的跨线程调用
//        /// </summary>
//        /// <typeparam name="T">委托参数的类型</typeparam>
//        /// <param name="control">目标控件</param>
//        /// <param name="action">无返回值的委托</param>
//        /// <param name="arg">委托的参数</param>
//        public static void CrossThreadInvoke<T>(this Control control, Actions<T> action, T arg)
//        {
//            ControlHelper.CrossThreadInvoke(control, action,arg);
//        }

//        /// <summary>
//        /// 对控件的跨线程调用
//        /// </summary>
//        /// <typeparam name="T">委托参数的类型</typeparam>
//        /// <typeparam name="TResult">方法返回值的类型</typeparam>
//        /// <param name="control">目标控件</param>
//        /// <param name="func">有回值的委托</param>
//        /// <param name="arg">委托的参数</param>
//        public static TResult CrossThreadInvoke<T, TResult>(this Control control, Funcs<T, TResult> func, T arg)
//        {
//            return ControlHelper.CrossThreadInvoke(control, func, arg);
//        }
//    }
//}
