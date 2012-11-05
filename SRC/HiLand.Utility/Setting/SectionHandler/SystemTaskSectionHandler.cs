using System;
using System.Configuration;
using System.Xml;
using HiLand.Utility.Data;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 系统任务配置节获取器
    /// </summary>
    public class SystemTaskSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// 获取config节点，创建配置信息实体
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            SystemTaskSectionConfig sectionConfig = new SystemTaskSectionConfig();
            foreach (XmlNode taskTypeNode in section.ChildNodes)
            {
                switch (taskTypeNode.Name.ToLower())
                {
                    case "dailytasks":
                        {
                            foreach (XmlNode taskNode in taskTypeNode.ChildNodes)
                            {
                                if (taskNode.NodeType == XmlNodeType.Comment)
                                {
                                    continue;
                                }

                                SystemTaskOfDailyExcutorEntity entity = new SystemTaskOfDailyExcutorEntity();
                                entity.AddonInfo = XmlHelper.GetNodeValue(taskNode, string.Empty, "addonInfo");
                                entity.AddonDetails = XmlHelper.GetNodeValue(taskNode, string.Empty, "addonDetails");
                                entity.Name = XmlHelper.GetNodeValue(taskNode, string.Empty, "name");
                                entity.ExcuteHour = XmlHelper.GetNodeValue(taskNode, string.Empty, "excuteHour", Config.GetAppSetting("taskExcuteHour", 0));
                                entity.ExcuteMinute = XmlHelper.GetNodeValue(taskNode, string.Empty, "excuteMinute", Config.GetAppSetting("taskExcuteMinute", 0));
                                string typeString = XmlHelper.GetNodeValue(taskNode, string.Empty, "type");
                                if (string.IsNullOrEmpty(typeString) == false)
                                {
                                    entity.Type = Type.GetType(typeString, true);
                                }

                                sectionConfig.SystemTaskOfDailyExcutorList.Add(entity);
                            }
                        }
                        break;
                    default:
                        //TODO:xieran20120926 需要添加其他各种任务执行周期类型（每小时，每周，每月等）
                        break;
                }
            }

            return sectionConfig;
        }
    }
}
