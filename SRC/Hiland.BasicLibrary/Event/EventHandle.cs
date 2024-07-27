using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Event
{
    /// <summary>
    /// 事件委托（arg 参数有格式的要求）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <remarks>
    /// dotNET4下，可以直接使用以下框架内置的泛型委托
    /// public delegate void EventHandler<TEventArgs>(Object sender,TEventArgs e) where TEventArgs : EventArgs
    ///</remarks>
    public delegate void FormatEventHandle<T>(object sender, EventArgs<T> args) where T : new();

    /// <summary>
    /// 事件委托（arg 参数无格式的要求）
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void CommonEventHandle<TEventArgs>(object sender, TEventArgs args);

    /// <summary>
    /// 普通委托
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="args"></param>
    public delegate void CommonHandle<TEventArgs>(TEventArgs args);
}
