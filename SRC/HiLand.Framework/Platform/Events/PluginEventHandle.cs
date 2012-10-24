using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Framework.BusinessCore
{
    public delegate void PluginEventHandler(object sender,PluginEventArgs e);
    public delegate void PluginEventHandler<T>(object sender,T t) where T:PluginEventArgs;
    public delegate U PluginEventHandler<U,V>(object sender,V e) where V:PluginEventArgs;
}
