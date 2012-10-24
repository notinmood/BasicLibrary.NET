using System;
using HiLand.Utility.Event;

namespace HiLand.Utility.AOP
{
    /// <summary>
    /// 代码包装工具类
    /// </summary>
    public static class CodePacking
    {
        /// <summary>
        /// 将代码包装进Try语句块中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="action"></param>
        public static void Try<T>(T t, Action<T> action)
        {
            try
            {
                action(t);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将代码包装进Try/Finally语句块中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="tryAction"></param>
        /// <param name="finallyAction"></param>
        public static void Try<T>(T t, Action<T> tryAction,Action<T> finallyAction)
        {
            try
            {
                tryAction(t);
            }
            catch
            {

            }
            finally
            {
                finallyAction(t);
            }
        }

        /// <summary>
        /// 将代码包装进Try语句块中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TResult Try<T, TResult>(T t, Funcs<T, TResult> action)
        {
            TResult r = default(TResult);
            try
            {
                r = action(t);
            }
            catch
            {

            }

            return r;
        }

        /// <summary>
        /// 将代码包装进Try语句块中
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TResult Try<T1, T2, TResult>(T1 t1, T2 t2, Funcs<T1, T2, TResult> action)
        {
            TResult r = default(TResult);
            try
            {
                r = action(t1,t2);
            }
            catch
            {

            }

            return r;
        }

        /// <summary>
        /// 将代码包装进Try/Finally语句块中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="tryAction"></param>
        /// <param name="finallyAction"></param>
        /// <returns></returns>
        public static TResult Try<T, TResult>(T t, Funcs<T, TResult> tryAction,Funcs<T,TResult> finallyAction)
        {
            TResult r = default(TResult);
            try
            {
                r = tryAction(t);
            }
            catch
            {

            }
            finally
            {
                finallyAction(t);
            }

            return r;
        }

        /// <summary>
        /// 将代码包装进Using语句块中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void Using<T>(Action<T> action) where T : IDisposable, new()
        { 
            using(T t=new T())
            {
                action(t);
            }
        }
    }
}
