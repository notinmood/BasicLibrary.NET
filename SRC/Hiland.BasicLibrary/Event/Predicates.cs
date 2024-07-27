using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Event
{
    //由于微软的某些Predicate泛型委托在.Net2.0下不能使用，因此此处自定义此委托
    public delegate bool Predicates();
    public delegate bool Predicates<T>(T t);
    public delegate bool Predicates<T1, T2>(T1 t1, T2 t2);
    public delegate bool Predicates<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
    public delegate bool Predicates<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
    public delegate bool Predicates<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
}