using System;
using System.Collections.Generic;
using System.Text;


namespace HiLand.Utility.Event
{
    //由于微软的Func泛型委托在.Net2.0下不能使用，因此此处自定义此委托
    public delegate void Funcs();
    public delegate TResult Funcs<TResult>();
    public delegate TResult Funcs<T, TResult>(T t);
    public delegate TResult Funcs<T1, T2, TResult>(T1 t1, T2 t2);
    public delegate TResult Funcs<T1, T2, T3, TResult>(T1 t1, T2 t2, T3 t3);
    public delegate TResult Funcs<T1, T2, T3, T4, TResult>(T1 t1, T2 t2, T3 t3, T4 t4);
    public delegate TResult Funcs<T1, T2, T3, T4, T5, TResult>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
}
