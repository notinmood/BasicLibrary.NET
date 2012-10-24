using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.Plugins;
using HiLand.Framework.BusinessCore;

namespace HiLand.Framework.PluginsSample
{
    public class FirstApplication : Application, IApplication
    {
        private Guid appGuid = new Guid("43075BE5-141F-4EF7-BA9B-786C91BA08D4");
        public override Guid ApplicationGuid
        {
            get { return this.appGuid; }
        }

        private string appName = "我的第1个插件";
        public override string ApplicationName
        {
            get
            {
                return this.appName;
            }
        }
    }
}