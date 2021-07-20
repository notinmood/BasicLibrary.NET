using HiLand.Utility.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleConsoleMvcLast.Models
{
    public class MyCookie:CookieInfo
    {
        public int Age { get; set; }

        public string Name { get; set; }

    }
}