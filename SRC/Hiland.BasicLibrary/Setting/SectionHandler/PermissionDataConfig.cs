using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 数据权限配置项信息
    /// </summary>
    public class PermissionDataConfig
    {
        private static PermissionDataConfig config = null;
        
        /// <summary>
        /// 从配置文件读取配置
        /// </summary>
        /// <returns></returns>
        public static PermissionDataConfig GetConfig()
        {
            return GetConfig(string.Empty);
        }

        /// <summary>
        /// 从配置文件读取配置
        /// </summary>
        /// <param name="sectionNodePath">配置节点的路径</param>
        /// <returns></returns>
        public static PermissionDataConfig GetConfig(string sectionNodePath)
        {
            if (config != null)
            {
                return config;
            }
            else
            {
                if (string.IsNullOrEmpty(sectionNodePath) == false)
                {
                    config = (PermissionDataConfig)ConfigurationManager.GetSection(sectionNodePath);
                }
                else
                {
                    config = (PermissionDataConfig)ConfigurationManager.GetSection("permissionData");
                }

                return config;
            }
        }

        private Dictionary<Guid, PermissionDataApplication> applications = new Dictionary<Guid, PermissionDataApplication>();
        /// <summary>
        /// 所有的应用
        /// </summary>
        public Dictionary<Guid, PermissionDataApplication> Applications
        {
            get { return applications; }
            set { applications = value; }
        }

        private Dictionary<Guid, PermissionDataSubModule> allSubModules = new Dictionary<Guid, PermissionDataSubModule>();
        /// <summary>
        /// 所有的子模块
        /// </summary>
        public Dictionary<Guid, PermissionDataSubModule> AllSubModules
        {
            get { return allSubModules; }
            set { allSubModules = value; }
        }
    }
}
