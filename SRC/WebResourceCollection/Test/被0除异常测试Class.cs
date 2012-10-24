using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebResourceCollection.Test
{
    public class 被0除异常测试Class
    {
        public static int Foo()
        {
            int m = 9;
            int n = 0;
            int result = m / n;
            return result;
        }
    }
}