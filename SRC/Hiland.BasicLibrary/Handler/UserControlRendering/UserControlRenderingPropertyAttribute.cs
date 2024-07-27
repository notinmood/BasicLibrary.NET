using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Handler
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UserControlRenderingPropertyAttribute : Attribute
    {
        public string Key { get; set; }

        public UserControlRenderingPropertySource Source { get; set; }
    }
}
