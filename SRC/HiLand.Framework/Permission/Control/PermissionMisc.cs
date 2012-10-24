using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;
using HiLand.Utility.Cache;
using HiLand.Utility.Resources;
using HiLand.Utility.Web;
using HiLand.Utility.Setting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HiLand.Framework.Permission.Control
{
    public static class PermissionMisc
    {
        /// <summary>
        /// 根据权限处理按钮的状态
        /// </summary>
        public static void DealDisplayStatues(WebControl control, PermissionTypes permissionType)
        {
            bool hasPermission = PermissionValidation.GeneralValidate(permissionType);
            if (hasPermission == false)
            {
                NoPermissionControlDisplayModes displayModes = GetNoPermissionControlDisplayMode();
                switch (displayModes)
                {
                    case NoPermissionControlDisplayModes.Disable:
                        control.Enabled = false;
                        control.ToolTip = ResourcesManager.GetValue("NoPermissionDisplayInfo");
                        break;
                    case NoPermissionControlDisplayModes.Hidden:
                    default:
                        control.Visible = false;
                        break;
                }
            }
        }

        private static NoPermissionControlDisplayModes GetNoPermissionControlDisplayMode()
        {
            return CacheHelper.Access<NoPermissionControlDisplayModes>(CoreCacheKeys.GetNoPermissionControlDisplayModeConfigKey(), CacheHelper.MaxMintues, GetNoPermissionControlDisplayModeDetails);
        }

        private static NoPermissionControlDisplayModes GetNoPermissionControlDisplayModeDetails()
        {
            NoPermissionControlDisplayModes displayModes = NoPermissionControlDisplayModes.Disable;
            string noPermissionControlDisplayModeString = Config.GetAppSetting("NoPermissionControlDisplayMode");
            if (string.IsNullOrEmpty(noPermissionControlDisplayModeString) == false)
            {
                noPermissionControlDisplayModeString = noPermissionControlDisplayModeString.ToLower();
                switch (noPermissionControlDisplayModeString)
                {
                    case "hidden":
                        displayModes = NoPermissionControlDisplayModes.Hidden;
                        break;
                    case "disable":
                    default:
                        displayModes = NoPermissionControlDisplayModes.Disable;
                        break;
                }
            }

            return displayModes;
        }
    }
}
