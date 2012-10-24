using System;
using System.Configuration;
using System.Xml;

namespace HiLand.Utility.Setting.SectionHandler
{
    public class GeneralValidateSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            GeneralValidateConfig config = new GeneralValidateConfig();

            foreach (XmlNode applicationNode in section.ChildNodes)
            {
                GeneralValidateApplication application = new GeneralValidateApplication();
                application.Name = applicationNode.Attributes["name"].Value;
                application.Guid = new Guid(applicationNode.Attributes["guid"].Value);

                foreach (XmlNode moduleNode in applicationNode.ChildNodes)
                {
                    GeneralValidateModule module = new GeneralValidateModule();
                    module.Name = moduleNode.Attributes["name"].Value;
                    module.Guid = new Guid(moduleNode.Attributes["guid"].Value);

                    foreach (XmlNode subModuleNode in moduleNode.ChildNodes)
                    {
                        GeneralValidateSubModule subModule = new GeneralValidateSubModule();
                        subModule.Name = subModuleNode.Attributes["name"].Value;
                        subModule.Guid = new Guid(subModuleNode.Attributes["guid"].Value);

                        XmlNodeList pageNodeList = subModuleNode.SelectNodes("pages/page");
                        foreach (XmlNode pageNode in pageNodeList)
                        {
                            subModule.Pages.Add(pageNode.Attributes["name"].Value, pageNode.Attributes["file"].Value.ToLower());
                        }

                        XmlNodeList actionNodeList = subModuleNode.SelectNodes("actions/action");
                        foreach (XmlNode actionNode in actionNodeList)
                        {
                            int actionValue = 0;
                            bool isOkValue = int.TryParse(actionNode.Attributes["value"].Value, out actionValue);
                            if (isOkValue == true)
                            {
                                string actionName = actionNode.Attributes["name"].Value;
                                string actionText = actionName;
                                XmlAttribute actionTextAttribute = actionNode.Attributes["text"];
                                if (actionTextAttribute != null)
                                {
                                    actionText = actionTextAttribute.Value;
                                }

                                GeneralValidateOperation action = new GeneralValidateOperation(actionName, actionText, actionValue);
                                subModule.Operations.Add(actionName, action);
                            }
                        }

                        module.SubModules.Add(subModule.Guid, subModule);
                    }

                    application.Modules.Add(module.Guid, module);
                }

                config.Applications.Add(application.Guid, application);
            }

            GeneralValidateConfig.OptimizeStrcture(config);

            return config;
        }
    }
}
