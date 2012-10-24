using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Framework.BusinessCore
{
    public class PluginEventArgs:EventArgs
    {
        private object tag = null;
        public object Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }
    }

    public class PluginEventArgs<T> : PluginEventArgs
    {
        private T tag = default(T);
        public new T Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }
    }
}
