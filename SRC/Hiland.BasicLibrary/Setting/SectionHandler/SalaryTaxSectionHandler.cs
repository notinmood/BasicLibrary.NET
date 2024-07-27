//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Text;
//using System.Xml;
//using Hiland.BasicLibrary.Data;

//namespace Hiland.BasicLibrary.Setting.SectionHandler
//{
//    /// <summary>
//    /// 工资税配置节获取器
//    /// </summary>
//    public class SalaryTaxSectionHandler : IConfigurationSectionHandler
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
//            SalaryTaxConfig config = new SalaryTaxConfig();
//            config.SalaryTax.TaxThreshold = XmlHelper.GetNodeValue<decimal>(section, "", "taxThreshold", 3500M);
//            foreach (XmlNode currentNode in section.ChildNodes)
//            {
//                SalaryTaxLevel salaryTaxLevel = new SalaryTaxLevel();
//                salaryTaxLevel.ExpressCalcValue = XmlHelper.GetNodeValue<decimal>(currentNode, "", "expressCalcValue");
//                salaryTaxLevel.Max = XmlHelper.GetNodeValue<decimal>(currentNode, "", "max");
//                salaryTaxLevel.Min = XmlHelper.GetNodeValue<decimal>(currentNode, "", "min");
//                salaryTaxLevel.Name = XmlHelper.GetNodeValue<string>(currentNode, "", "name");
//                salaryTaxLevel.Rate = XmlHelper.GetNodeValue<decimal>(currentNode, "", "rate");
//                config.SalaryTax.SalaryTaxLevels.Add(salaryTaxLevel);
//            }

//            return config;
//        }
//    }
//}
