using System.Collections.Generic;
using HiLand.Utility.Web;

namespace HiLand.Utility.Setting.SectionHandler
{
    public class SSLSwitchConfig
    {
        private List<string> sslList = new List<string>();
        /// <summary>
        /// 需要使用SSL的页面信息列表
        /// </summary>
        public List<string> SSLList
        {
            get
            {
                return this.sslList;
            }
            set
            {
                this.sslList = value;
            }
        }

        private ControlModes controlMode = ControlModes.AllowOther;
        public ControlModes ControlMode
        {
            get
            {
                return this.controlMode;
            }
            set
            {
                this.controlMode = value;
            }
        }

        private DeployModes deployMode = DeployModes.On;
        public DeployModes DeployMode
        {
            get
            {
                return this.deployMode;
            }
            set
            {
                this.deployMode = value;
            }
        }

        public bool IsContaint(string virtualPathToValidate)
        {
            bool isContaint = false;

            virtualPathToValidate = WebHelper.GetRelativeVirtualPath(virtualPathToValidate).ToLower();

            for (int i = 0; i < sslList.Count; i++)
            {
                string currentItem = sslList[i];
                currentItem = WebHelper.GetRelativeVirtualPath(currentItem).ToLower();

                if (virtualPathToValidate.StartsWith(currentItem) == true)
                {
                    isContaint = true;
                    break;
                }
            }

            return isContaint;
        }
    }
}
