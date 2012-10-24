using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting;
using HiLand.Utility.Web;
using HiLand.Utility.Cache;
using HiLand.Utility.Resources;


namespace HiLand.Framework.Permission.Control
{
    public class PermissionHyperLink : HyperLink
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionTypes PermissionType
        {
            get
            {
                object obj = this.ViewState["PermissionHyperLink|PermissionType"];
                if (obj == null)
                {
                    return PermissionTypes.ALL;
                }
                else
                {
                    return (PermissionTypes)obj;
                }
            }
            set { this.ViewState["PermissionHyperLink|PermissionType"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            PermissionMisc.DealDisplayStatues(this, this.PermissionType);
            base.OnLoad(e);
        }
    }
}
