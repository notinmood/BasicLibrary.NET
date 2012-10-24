using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.Membership;
using HiLand.Framework.Permission;
using HiLand.Framework4.Permission.Attributes;
using HiLand.Utility.Enums;
using HiLand.Utility4.MVC;
using HiLand.Utility4.MVC.SectionHandler;

namespace HiLand.Framework4.Permission
{
    /// <summary>
    /// 操作权限帮助器
    /// </summary>
    public static class PermissionValidationHelper
    {
        /// <summary>
        /// 通用的MVC操作权限验证
        /// </summary>
        /// <param name="permissionAuthorizeMode">验证模式类型</param>
        /// <returns></returns>
        public static bool GeneralValidate(PermissionAuthorizeModes permissionAuthorizeMode = PermissionAuthorizeModes.Normal)
        {
            string areaName = MVCHelper.GetCurrentAreaName();
            string controllerName = MVCHelper.GetCurrentControllerName();
            string actionName = MVCHelper.GetCurrentActionName();

            return GeneralValidate(actionName, controllerName, areaName, permissionAuthorizeMode);
        }


        /// <summary>
        /// 通用的MVC操作权限验证
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <param name="permissionAuthorizeMode">验证模式类型</param>
        /// <returns></returns>
        public static bool GeneralValidate(string actionName, string controllerName, string areaName = "", PermissionAuthorizeModes permissionAuthorizeMode = PermissionAuthorizeModes.Normal)
        {
            if (permissionAuthorizeMode == PermissionAuthorizeModes.None)
            {
                return true;
            }

            PermissionValidateConfig config = PermissionValidateConfig.GetConfig();
            if (config == null)
            {
                return true;
            }

            KeyValuePair<Guid,int>? pemissionInfoRequired = GetPermissionInfo(config, areaName, controllerName, actionName);

            //未配置的资源类型不需要控制权限
            if (pemissionInfoRequired == null)
            {
                return true;
            }
            else
            {
                bool isCookieSuccessful = PermissionValidation.ReadCookie();
                if (isCookieSuccessful == false)
                {
                    return false;
                }
                else
                {
                    if (permissionAuthorizeMode ==  PermissionAuthorizeModes.LoginedAsPass)
                    {
                        return true;
                    }
                }

                IUser currentUser = BusinessUserBLL.CurrentUser;

                //对超级管理员类型的用户不做权限限制
                if (currentUser.UserType == UserTypes.SuperAdmin)
                {
                    return true;
                }

                foreach (KeyValuePair<Guid, PermissionItem> kvp in currentUser.PermissionItems)
                {
                    PermissionItem currentPermission = kvp.Value;
                    if (currentPermission.PermissionItemGuid == pemissionInfoRequired.Value.Key)
                    {
                        int permissionValueRequied = pemissionInfoRequired.Value.Value;

                        if ((currentPermission.PermissionItemValue & permissionValueRequied) == permissionValueRequied)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 获取ACA对应的资源所需要的权限值
        /// </summary>
        /// <param name="config"></param>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 通过areaName, controllerName, actionName获取其对应的子模块guid以及当前权限项的值 KVP对，如果没有匹配到合适的值，返回null
        /// </remarks>
        private static KeyValuePair<Guid,int>? GetPermissionInfo(PermissionValidateConfig config, string areaName, string controllerName, string actionName)
        {
            Dictionary<Guid, PermissionValidateSubModule> allSubModelDic = config.AllSubModuleDic;
            foreach (KeyValuePair<Guid, PermissionValidateSubModule> subModule in allSubModelDic)
            {
                Dictionary<string, PermissionValidateOperation> operations = subModule.Value.Operations;
                foreach (KeyValuePair<string, PermissionValidateOperation> operateKVP in operations)
                {
                    PermissionValidateOperation operation = operateKVP.Value;

                    if (operation.Action.ToLower() == actionName.ToLower() &&
                        operation.Controller.ToLower() == controllerName.ToLower() &&
                        operation.Area.ToLower() == areaName.ToLower())
                    {
                        return new KeyValuePair<Guid, int>( subModule.Key,operation.Value);
                    }
                }
            }

            return null;
        }
    }
}
