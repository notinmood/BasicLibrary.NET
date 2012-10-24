using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.Membership;
using HiLand.Framework.Permission;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting.SectionHandler;
using HiLand.Utility4.MVC;

namespace HiLand.Framework4.Permission
{
    /// <summary>
    /// 数据权限帮助器
    /// </summary>
    public static class PermissionDataHelper
    {
        /// <summary>
        ///  获取MVC的数据权限
        /// </summary>
        /// <returns></returns>
        public static PermissionDataTypes GetDataPermission()
        {
            string areaName = MVCHelper.GetCurrentAreaName();
            string controllerName = MVCHelper.GetCurrentControllerName();

            return GetDataPermission(controllerName, areaName);
        }


        /// <summary>
        ///  获取MVC的数据权限
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <returns></returns>
        public static PermissionDataTypes GetDataPermission(string controllerName, string areaName = "")
        {
            PermissionDataConfig config = PermissionDataConfig.GetConfig();
            if (config == null)
            {
                return PermissionDataTypes.All;
            }

            Guid? settingItemGuid = GetSettingItemGuid(config, areaName, controllerName);

            //未配置的资源类型不需要控制权限
            if (settingItemGuid == null)
            {
                return PermissionDataTypes.All;
            }
            else
            {
                bool isCookieSuccessful = PermissionValidation.ReadCookie();
                if (isCookieSuccessful == false)
                {
                    return PermissionDataTypes.None;
                }

                IUser currentUser = BusinessUserBLL.CurrentUser;

                //对超级管理员类型的用户不做权限限制
                if (currentUser.UserType == UserTypes.SuperAdmin)
                {
                    return PermissionDataTypes.All;
                }

                foreach (KeyValuePair<Guid, PermissionItem> kvp in currentUser.PermissionItems)
                {
                    PermissionItem currentPermission = kvp.Value;
                    if (currentPermission.PermissionItemGuid == settingItemGuid.Value)
                    {
                        return (PermissionDataTypes)currentPermission.PermissionItemValue;
                    }
                }

                return PermissionDataTypes.None;
            }
        }

        /// <summary>
        /// 获取ACA对应的资源Guid
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 通过areaName, controllerName获取其对应的子模块配置项是否存在
        /// </remarks>
        private static Guid? GetSettingItemGuid(PermissionDataConfig config, string areaName, string controllerName)
        {
            Dictionary<Guid, PermissionDataSubModule> allSubModelDic = config.AllSubModules;
            foreach (KeyValuePair<Guid, PermissionDataSubModule> subModuleKVP in allSubModelDic)
            {
                if (subModuleKVP.Value.Area == areaName && subModuleKVP.Value.Controller == controllerName)
                {
                    return subModuleKVP.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取数据权限的过滤条件
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetFilterCondition(string fieldName)
        {
            string result = String.Empty;
            PermissionDataTypes permissionDataType = GetDataPermission();
            switch (permissionDataType)
            {
                case PermissionDataTypes.DepartmentWithoutSub:
                    {
                        List<Guid> guidList = BusinessUserBLL.GetUserGuidsByDepartment(BusinessUserBLL.CurrentUser.Department.DepartmentFullPath, false);
                        string guidsString = CollectionHelper.Concat<Guid>(",", "'", "'", guidList);

                        result = string.Format(" [{0}] in ({1}) ", fieldName, guidsString);
                    }
                    break;
                case PermissionDataTypes.DepatmentWithSub:
                    {
                        List<Guid> guidList = BusinessUserBLL.GetUserGuidsByDepartment(BusinessUserBLL.CurrentUser.Department.DepartmentFullPath, true);
                        string guidsString = CollectionHelper.Concat<Guid>(",", "'", "'", guidList);

                        result = string.Format(" [{0}] in ({1}) ", fieldName, guidsString);
                    }
                    break;
                case PermissionDataTypes.None:
                    result = " 1=2 ";
                    break;
                case PermissionDataTypes.Self:
                    result = string.Format(" [{0}] = '{1}' ", fieldName, BusinessUserBLL.CurrentUser.UserGuid);
                    break;
                case PermissionDataTypes.All:
                default:
                    result = " 1=1 ";
                    break;
            }

            return result;
        }
    }
}
