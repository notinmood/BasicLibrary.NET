using System;
using System.Collections.Generic;
using System.Text;


namespace Hiland.BasicLibrary.Event
{
    //由于微软的Func泛型委托在.Net2.0下不能使用，因此此处自定义此委托
    /// <summary>
    /// 
    /// </summary>
    public delegate void Funcs();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public delegate TResult Funcs<TResult>();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public delegate TResult Funcs<T, TResult>(T t);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public delegate TResult Funcs<T1, T2, TResult>(T1 t1, T2 t2);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    /// <returns></returns>
    public delegate TResult Funcs<T1, T2, T3, TResult>(T1 t1, T2 t2, T3 t3);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    /// <param name="t4"></param>
    /// <returns></returns>
    public delegate TResult Funcs<T1, T2, T3, T4, TResult>(T1 t1, T2 t2, T3 t3, T4 t4);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    /// <param name="t4"></param>
    /// <param name="t5"></param>
    /// <returns></returns>
    public delegate TResult Funcs<T1, T2, T3, T4, T5, TResult>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
}
