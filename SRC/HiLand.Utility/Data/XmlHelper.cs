using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// XML操作辅助类
    /// </summary>
    public static class XmlHelper
    {
        #region 节点值(或属性值)的获取
        /// <summary>
        /// 获取某一节点的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlNode xmlNode)
        {
            return GetNodeValue(xmlNode, string.Empty, string.Empty);
        }

        /// <summary>
        /// 获取某一节点的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <param name="xpath">XPath查找路径</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlNode xmlNode, string xpath)
        {
            return GetNodeValue(xmlNode, xpath, string.Empty);
        }

        /// <summary>
        /// 获取某一个节点中属性的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <param name="xpath">XPath查找路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlNode xmlNode, string xpath, string attributeName)
        {
            XmlNode targetNode = xmlNode;
            if (string.IsNullOrEmpty(xpath)==false)
            {
                targetNode = xmlNode.SelectSingleNode(xpath);
            }

            string nodeValue = string.Empty;
            if (targetNode != null)
            {
                if (string.IsNullOrEmpty(attributeName))
                {
                    nodeValue = targetNode.InnerXml;
                }
                else
                {
                    XmlAttribute targetAttr = targetNode.Attributes[attributeName];
                    if (targetAttr != null)
                    {
                        nodeValue = targetAttr.Value;
                    }
                }
            }

            return nodeValue;
        }

        /// <summary>
        /// 获取某一个节点中属性的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <param name="xpath">XPath查找路径</param>
        /// <returns></returns>
        public static T GetNodeValue<T>(XmlNode xmlNode, string xpath)
        {
            return GetNodeValue<T>(xmlNode, xpath, string.Empty, default(T));
        }

        /// <summary>
        /// 获取某一个节点中属性的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <param name="xpath">XPath查找路径</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static T GetNodeValue<T>(XmlNode xmlNode, string xpath, T defaultValue)
        {
            return GetNodeValue<T>(xmlNode, xpath, string.Empty, defaultValue);
        }

        /// <summary>
        /// 获取某一个节点中属性的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <param name="xpath">XPath查找路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public static T GetNodeValue<T>(XmlNode xmlNode, string xpath, string attributeName)
        {
            return GetNodeValue<T>(xmlNode, xpath, attributeName, default(T));
        }

        /// <summary>
        /// 获取某一个节点中属性的值
        /// </summary>
        /// <param name="xmlNode">Xml节点</param>
        /// <param name="xpath">XPath查找路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static T GetNodeValue<T>(XmlNode xmlNode, string xpath, string attributeName, T defaultValue)
        {
            string nodeValueString = GetNodeValue(xmlNode, xpath, attributeName);
            return Converter.ChangeType<T>(nodeValueString, defaultValue);
        }

        #endregion 

        #region 序列化及反序列化
        /// <summary>
        /// 序列化实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string Serialize<T>(T entity)
        {
            return Serialize<T>(entity, string.Empty);
        }

        /// <summary>
        /// 序列化实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="defaultXmlNamespace">构建xml节点的缺省的命名空间</param>
        /// <returns></returns>
        public static string Serialize<T>(T entity,string defaultXmlNamespace)
        {
            string result = string.Empty;

            XmlSerializer serializer = null;
            if (string.IsNullOrEmpty(defaultXmlNamespace))
            {
                serializer= new XmlSerializer(typeof(T));
            }
            else
            {
                serializer = new XmlSerializer(typeof(T),defaultXmlNamespace);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms,entity);
                result= Encoding.UTF8.GetString(ms.ToArray());
            }

            return result;
        }

         /// <summary>
        /// 反序列化实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlValue"></param>
        /// <param name="defaultXmlNamespace">构建xml节点的缺省的命名空间</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string xmlValue)
        {
            return DeSerialize<T>( xmlValue, string.Empty);
        }

        /// <summary>
        /// 反序列化实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlValue"></param>
        /// <param name="defaultXmlNamespace">构建xml节点的缺省的命名空间</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string xmlValue, string defaultXmlNamespace)
        { 
            T entity= default(T);

            if (string.IsNullOrEmpty(xmlValue) == false)
            {
                XmlSerializer serializer = null;
                if (string.IsNullOrEmpty(defaultXmlNamespace))
                {
                    serializer = new XmlSerializer(typeof(T));
                }
                else
                {
                    serializer = new XmlSerializer(typeof(T), defaultXmlNamespace);
                }

                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlValue)))
                {
                    entity = (T)serializer.Deserialize(ms);
                }
            }

            return entity;
        }
        #endregion
    }
}
