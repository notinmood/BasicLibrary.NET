//using System.Xml;
//using System.Xml.Linq;
//using HiLand.Utility.Data;

//namespace HiLand.Utility4.Data
//{
//    /// <summary>
//    /// XML信息扩展类
//    /// </summary>
//    public static class XmlEx
//    {
//        #region XmlNode
//        /// <summary>
//        /// 获取某一节点的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <returns></returns>
//        public static string GetNodeValue(this XmlNode xmlNode)
//        {
//            return GetNodeValue(xmlNode, string.Empty, string.Empty);
//        }

//        /// <summary>
//        /// 获取某一节点的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <param name="xpath">XPath查找路径</param>
//        /// <returns></returns>
//        public static string GetNodeValue(this XmlNode xmlNode, string xpath)
//        {
//            return GetNodeValue(xmlNode, xpath, string.Empty);
//        }

//        /// <summary>
//        /// 获取某一个节点中属性的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <param name="xpath">XPath查找路径</param>
//        /// <param name="attributeName">属性名称</param>
//        /// <returns></returns>
//        public static string GetNodeValue(this XmlNode xmlNode, string xpath, string attributeName)
//        {
//            return XmlHelper.GetNodeValue(xmlNode, xpath, attributeName);
//        }

//        /// <summary>
//        /// 获取某一个节点中属性的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <param name="xpath">XPath查找路径</param>
//        /// <returns></returns>
//        public static T GetNodeValue<T>(this XmlNode xmlNode, string xpath)
//        {
//            return GetNodeValue<T>(xmlNode, xpath, string.Empty, default(T));
//        }

//        /// <summary>
//        /// 获取某一个节点中属性的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <param name="xpath">XPath查找路径</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static T GetNodeValue<T>(this XmlNode xmlNode, string xpath, T defaultValue)
//        {
//            return GetNodeValue<T>(xmlNode, xpath, string.Empty, defaultValue);
//        }

//        /// <summary>
//        /// 获取某一个节点中属性的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <param name="xpath">XPath查找路径</param>
//        /// <param name="attributeName">属性名称</param>
//        /// <returns></returns>
//        public static T GetNodeValue<T>(this XmlNode xmlNode, string xpath, string attributeName)
//        {
//            return GetNodeValue<T>(xmlNode, xpath, attributeName, default(T));
//        }

//        /// <summary>
//        /// 获取某一个节点中属性的值
//        /// </summary>
//        /// <param name="xmlNode">Xml节点</param>
//        /// <param name="xpath">XPath查找路径</param>
//        /// <param name="attributeName">属性名称</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static T GetNodeValue<T>(this XmlNode xmlNode, string xpath, string attributeName, T defaultValue)
//        {
//            string nodeValueString = GetNodeValue(xmlNode, xpath, attributeName);
//            return Converter.ChangeType<T>(nodeValueString, defaultValue);
//        }
//        #endregion

//        #region XElement
//        /// <summary>
//        /// 获取某子节点的值
//        /// </summary>
//        /// <param name="parentXElement"></param>
//        /// <param name="xElementName"></param>
//        /// <returns></returns>
//        public static string GetXElementValue(this XElement parentXElement, string xElementName)
//        {
//            return GetXElementValue<string>(parentXElement, xElementName);
//        }

//        /// <summary>
//        /// 获取某子节点的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="parentXElement"></param>
//        /// <param name="xElementName"></param>
//        /// <returns></returns>
//        public static T GetXElementValue<T>(this XElement parentXElement, string xElementName)
//        {
//            T result = default(T);
//            if (parentXElement != null)
//            {
//                XElement xElement = parentXElement.Element(xElementName);

//                if (xElement != null)
//                {
//                    result = Converter.ChangeType<T>(xElement.Value);
//                }
//            }

//            return result;
//        }

//        /// <summary>
//        /// 获取某节点属性的值
//        /// </summary>
//        /// <param name="xElement"></param>
//        /// <param name="xAttributeName"></param>
//        /// <returns></returns>
//        public static string GetXElementAttribute(this XElement xElement, string xAttributeName)
//        {
//            return GetXElementAttribute<string>(xElement, xAttributeName);
//        }

//        /// <summary>
//        /// 获取某节点属性的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="xElement"></param>
//        /// <param name="xAttributeName"></param>
//        /// <returns></returns>
//        public static T GetXElementAttribute<T>(this XElement xElement, string xAttributeName)
//        {
//            T result = default(T);

//            if (xElement != null)
//            {
//                XAttribute xAttribute= xElement.Attribute(xAttributeName);

//                if (xAttribute != null)
//                {
//                    result = Converter.ChangeType<T>(xAttribute.Value);
//                }
//            }

//            return result;
//        }
//        #endregion
//    }
//}
