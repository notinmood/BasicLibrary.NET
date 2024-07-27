//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Text;
//using System.Xml;
//using Hiland.BasicLibrary.Data;

//namespace Hiland.BasicLibrary.Setting.SectionHandler
//{
//    /// <summary>
//    /// 数据权限配置项读取器
//    /// </summary>
//    public class PermissionDataSectionHandler : IConfigurationSectionHandler
//    {
//        /// <summary>
//        /// 获取config节点，创建配置信息实体
//        /// </summary>
//        /// <param name="parent"></param>
//        /// <param name="configContext"></param>
//        /// <param name="section"></param>
//        /// <returns></returns>
//        public object Create(object parent, object configContext, System.Xml.XmlNode section)
//        {
//            PermissionDataConfig config = new PermissionDataConfig();

//            foreach (XmlNode applicationNode in section.ChildNodes)
//            {
//                PermissionDataApplication application = new PermissionDataApplication();
//                application.Name = applicationNode.Attributes["name"].Value;
//                application.Guid = new Guid(applicationNode.Attributes["guid"].Value);

//                foreach (XmlNode moduleNode in applicationNode.ChildNodes)
//                {
//                    PermissionDataModule module = new PermissionDataModule();
//                    module.Name = moduleNode.Attributes["name"].Value;
//                    module.Guid = new Guid(moduleNode.Attributes["guid"].Value);

//                    foreach (XmlNode subModuleNode in moduleNode.ChildNodes)
//                    {
//                        PermissionDataSubModule subModule = new PermissionDataSubModule();
//                        subModule.Name = subModuleNode.Attributes["name"].Value;
//                        subModule.Guid = new Guid(subModuleNode.Attributes["guid"].Value);
//                        subModule.Area = XmlHelper.GetNodeValue(subModuleNode, string.Empty, "area");
//                        subModule.Controller = XmlHelper.GetNodeValue(subModuleNode, string.Empty, "controller");

//                        module.SubModules.Add(subModule.Guid, subModule);
//                        config.AllSubModules.Add(subModule.Guid, subModule);
//                    }

//                    application.Modules.Add(module.Guid, module);
//                }

//                config.Applications.Add(application.Guid, application);
//            }

//            return config;
//        }
//    }
//}
