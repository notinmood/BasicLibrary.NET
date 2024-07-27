using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Handler
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UserControlRenderingPropertyAttribute : Attribute
    {
        public string Key { get; set; }

        public UserControlRenderingPropertySource Source { get; set; }
    }
}
