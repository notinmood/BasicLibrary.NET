using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.Plugins;

namespace HiLand.Framework.PluginsSample
{
    public class SecondApplication:Application
    {
        private Guid appGuid = new Guid("A85953DD-B85A-44B7-A94F-1B836D42F761");
        public override Guid ApplicationGuid
        {
            get { return this.appGuid; }
        }

        private string appName = "第二个插件";
        public override string ApplicationName
        {
            get
            {
                return this.appName;
            }
        }
    }
}
