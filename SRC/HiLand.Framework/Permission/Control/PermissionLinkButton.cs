using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using HiLand.Utility.Enums;

namespace HiLand.Framework.Permission.Control
{
    public class PermissionLinkButton : LinkButton
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionTypes PermissionType
        {
            get
            {
                object obj = this.ViewState["PermissionLinkButton|PermissionType"];
                if (obj == null)
                {
                    return PermissionTypes.ALL;
                }
                else
                {
                    return (PermissionTypes)obj;
                }
            }
            set { this.ViewState["PermissionLinkButton|PermissionType"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            PermissionMisc.DealDisplayStatues(this, this.PermissionType);
            base.OnLoad(e);
        }
    }
}
