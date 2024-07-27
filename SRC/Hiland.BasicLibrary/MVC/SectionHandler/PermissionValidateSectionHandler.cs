//using System;
//using System.Configuration;
//using System.Xml;
//using Hiland.BasicLibrary.Data;
//using Hiland.BasicLibrary4.Data;

//namespace Hiland.BasicLibrary4.MVC.SectionHandler
//{
//    /// <summary>
//    /// 权限控制配置节点读取工具
//    /// </summary>
//    /// <remarks>
//    /// 此类型需要配置在web.config等文件中，其会通过如下代码自动执行
//    /// (PermissionValidateSectionHandler)ConfigurationManager.GetSection("generalValidate");
//    /// </remarks>
//    public class PermissionValidateSectionHandler : IConfigurationSectionHandler
//    {
//        /// <summary>
//        ///  从配置文件自动构建权限配置对象
//        /// </summary>
//        /// <param name="parent"></param>
//        /// <param name="configContext"></param>
//        /// <param name="section"></param>
//        /// <returns></returns>
//        public object Create(object parent, object configContext, XmlNode section)
//        {
//            PermissionValidateConfig config = new PermissionValidateConfig();

//            foreach (XmlNode applicationNode in section.ChildNodes)
//            {
//                PermissionValidateApplication application = new PermissionValidateApplication();
//                application.Name = applicationNode.Attributes["name"].Value;
//                application.Guid = new Guid(applicationNode.Attributes["guid"].Value);
//                application.IsVisible = XmlHelper.GetNodeValue<bool>(applicationNode,string.Empty,"isVisible",true);

//                foreach (XmlNode moduleNode in applicationNode.ChildNodes)
//                {
//                    PermissionValidateModule module = new PermissionValidateModule();
//                    module.Name = moduleNode.Attributes["name"].Value;
//                    module.Guid = new Guid(moduleNode.Attributes["guid"].Value);
//                    module.IsVisible = XmlHelper.GetNodeValue<bool>(moduleNode, string.Empty, "isVisible", true);

//                    foreach (XmlNode subModuleNode in moduleNode.ChildNodes)
//                    {
//                        PermissionValidateSubModule subModule = new PermissionValidateSubModule();
//                        subModule.Name = subModuleNode.Attributes["name"].Value;
//                        subModule.Guid = new Guid(subModuleNode.Attributes["guid"].Value);
//                        subModule.IsVisible = XmlHelper.GetNodeValue<bool>(subModuleNode, string.Empty, "isVisible", true);

//                        XmlNodeList operationNodeList = subModuleNode.SelectNodes("operation");
//                        foreach (XmlNode operationNode in operationNodeList)
//                        {
//                            int actionValue = 0;
//                            bool isOkValue = int.TryParse(operationNode.Attributes["value"].Value, out actionValue);
//                            if (isOkValue == true)
//                            {
//                                string operationName = operationNode.Attributes["name"].Value;
//                                string operationText = operationName;
//                                XmlAttribute actionTextAttribute = operationNode.Attributes["text"];
//                                if (actionTextAttribute != null)
//                                {
//                                    operationText = actionTextAttribute.Value;
//                                }

//                                string operationArea = operationNode.GetNodeValue(string.Empty,"area");
//                                string operationController = operationNode.GetNodeValue(string.Empty, "controller");
//                                string operationAction = operationNode.GetNodeValue(string.Empty, "action");
//                                bool isVisible = operationNode.GetNodeValue<bool>(string.Empty, "isVisible", true);

//                                PermissionValidateOperation operation = new PermissionValidateOperation(operationName, operationText, actionValue, operationController, operationAction, operationArea, isVisible);
//                                subModule.Operations.Add(operationName, operation);
//                            }
//                        }

//                        module.SubModules.Add(subModule.Guid, subModule);
//                    }

//                    application.Modules.Add(module.Guid, module);
//                }

//                config.Applications.Add(application.Guid, application);
//            }

//            config.OptimizeStrcture();

//            return config;
//        }
//    }
//}
