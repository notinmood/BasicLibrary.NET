using System;
using System.Collections.Generic;
using System.Web;
using HiLand.Framework.Plugins;
using HiLand.Utility.Misc;

namespace HiLand.Framework.PluginsWebSample
{
    public class FirstWebApplication:Application
    {
        public override Guid ApplicationGuid
        {
            get
            {
                return new Guid("99C48F41-033B-4338-8028-E165836D062A");
            }
        }

        public override string ApplicationName
        {
            get
            {
                return "web页面应用";
            }
        }

        public override MenuInfo MainMenu
        {
            get
            {
                return new MenuInfo("Foo功能", "~/InformationFoo/Default.aspx");
            }
        }
    }
}