using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Hiland.BasicLibrary4.MVC.SectionHandler
{
    /// <summary>
    /// 权限验证的配置类
    /// </summary>
    public class PermissionValidateConfig
    {
        private static PermissionValidateConfig config = null;
        /// <summary>
        /// 从配置文件读取配置
        /// </summary>
        /// <param name="sectionNodePath">配置节点的路径</param>
        /// <returns></returns>
        public static PermissionValidateConfig GetConfig(string sectionNodePath = "")
        {
            if (config != null)
            {
                return config;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(sectionNodePath) == false)
                {
                    config = (PermissionValidateConfig)ConfigurationManager.GetSection(sectionNodePath);
                }
                else
                {
                    config = (PermissionValidateConfig)ConfigurationManager.GetSection("generalValidate");
                    if (config == null)
                    {
                        config = (PermissionValidateConfig)ConfigurationManager.GetSection("permissionValidate/generalValidate");
                    }
                }

                return config;
            }
        }
        //-----------------------------------------------------------------------------------


        private Dictionary<Guid, PermissionValidateApplication> applications = new Dictionary<Guid, PermissionValidateApplication>();
        /// <summary>
        /// 系统内所有的应用程序集合
        /// </summary>
        public Dictionary<Guid, PermissionValidateApplication> Applications
        {
            get { return applications; }
            set { applications = value; }
        }

        /// <summary>
        /// 系统内所有的子模块集合
        /// </summary>
        public Dictionary<Guid, PermissionValidateSubModule> AllSubModuleDic = null;

        /// <summary>
        /// 子模块与其操作权限对照字典
        /// </summary>
        public Dictionary<Guid, int> SubModelOperationDic = null;
        /// <summary>
        /// 优化GeneralValidateConfig存储结构
        /// </summary>
        /// <param name="config"></param>
        internal void OptimizeStrcture()
        {
            AllSubModuleDic = new Dictionary<Guid, PermissionValidateSubModule>();
            SubModelOperationDic = new Dictionary<Guid, int>();

            foreach (KeyValuePair<Guid, PermissionValidateApplication> application in this.Applications)
            {
                foreach (KeyValuePair<Guid, PermissionValidateModule> module in application.Value.Modules)
                {
                    foreach (KeyValuePair<Guid, PermissionValidateSubModule> subModule in module.Value.SubModules)
                    {
                        AllSubModuleDic.Add(subModule.Key, subModule.Value);

                        int allActionValues = 0;
                        foreach (KeyValuePair<string, PermissionValidateOperation> kvp in subModule.Value.Operations)
                        {
                            allActionValues += kvp.Value.Value;
                        }
                        SubModelOperationDic.Add(subModule.Key, allActionValues);
                    }
                }
            }
        }
    }
}