using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Handler.Ajax
{
    class QueryStringAttribute : ParameterAttribute
    {

        public QueryStringAttribute() { }

        public QueryStringAttribute(string name) : base(name, null) { }

        public QueryStringAttribute(string name, object defaultValue) : base(name, defaultValue) { }

        public override object GetParameterValue(System.Web.HttpContext context)
        {
            return context.Request.QueryString[Name];
        }
    }
}