using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Handler.Ajax
{
    public abstract class ParameterAttribute:Attribute
    {
        public object DefaultValue { get; set; }

        public string Name { get; set; }

        public ParameterAttribute() { }

        public ParameterAttribute(string name, object defaultValue)
        {
            Name = name;
            DefaultValue = defaultValue;
        }

        public virtual object GetValue(System.Web.HttpContext context, Type parameterType)
        {
            object value = GetParameterValue(context) ?? DefaultValue;
            if (parameterType.FullName == "System.String")
            {
                return value;
            }
            if (value.GetType().FullName == "System.String")
            {
                if (string.IsNullOrEmpty((string)value))
                {
                    value = DefaultValue;
                }
            }
            return Convert.ChangeType(value, parameterType);
        }

        public abstract object GetParameterValue(System.Web.HttpContext context);
    }
}
