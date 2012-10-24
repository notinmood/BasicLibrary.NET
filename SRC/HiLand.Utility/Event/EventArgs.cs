using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Event
{
    public class EventArgs<T> : EventArgs
        where T:new()
    {
        private T data;

        public EventArgs()
        {
            this.data = new T();
        }

        public EventArgs(T data)
        {
            this.data = data;
        }

        public T Data
        {
            get { return this.data; }
        }
    }
}
