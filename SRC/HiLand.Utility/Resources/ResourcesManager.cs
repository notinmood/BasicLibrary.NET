using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Xml;
using HiLand.Utility.Setting;

namespace HiLand.Utility.Resources
{
    /// <summary>
    /// 语言资源管理器
    /// </summary>
    /// <remarks>
    /// 其可以管理两种类型的资源:
    /// 1.系统的核心语言资源(其已经嵌入到dll的中)
    /// 2.项目级别(插件级别)的资源文件,其目录必须在web.config中用section"resourcePaths"指定,其格式为:
    ///     <section name="resourcePaths" type="System.Configuration.NameValueSectionHandler"/>
    /// 
    ///     <resourcePaths>
    ///         <add key="key1" value="~\Resources\resourceTest.xml"></add>
    ///         <add key="key2" value="~\Resources\resourceTest2.xml"></add>
    ///         ...
    ///     </resourcePaths>
    /// </remarks>
    public class ResourcesManager
    {
        static ResourcesManager()
        {
            resource = GetResource();
        }

        private static Dictionary<string, string> GetResource()
        {
            resource = new Dictionary<string, string>();


            //1.载入核心资源文件
            string cultureInfoString = Config.GetAppSetting("languageCulture");
            if (string.IsNullOrEmpty(cultureInfoString))
            {
                cultureInfoString = Thread.CurrentThread.CurrentCulture.Name;
            }
            //载入指定的语言文化信息的资源
            LoadCoreResource(cultureInfoString);
            //载入默认的资源
            string defaultCultureInfoString = "zh-CN";
            if (cultureInfoString != defaultCultureInfoString)
            {
                LoadCoreResource(defaultCultureInfoString);
            }

            //2.载入项目级别(插件级别)的资源文件
            NameValueCollection nvc = (NameValueCollection)ConfigurationManager.GetSection("resourcePaths");
            for (int i = 0; i < nvc.Count; i++)
            {
                string currentValue = nvc.Get(i);
                if (string.IsNullOrEmpty(currentValue)==false)
                {
                    LoadAppResource(currentValue);
                }
            }

            return resource;
        }

        private static Dictionary<string, string> resource = null;
        /// <summary>
        /// 所有的语言资源信息
        /// </summary>
        public static Dictionary<string, string> Resource
        {
            get
            {
                if (resource == null)
                {
                    resource = GetResource();
                }

                return resource;
            }
        }

        /// <summary>
        /// 载入系统核心的语言资源(其在dll的嵌入资源中)
        /// </summary>
        /// <param name="cultureInfoString"></param>
        private static void LoadCoreResource(string cultureInfoString)
        {
            Type type = MethodBase.GetCurrentMethod().DeclaringType;
            string fileNameSpace = type.Namespace;
            Assembly fileAssembly = Assembly.GetExecutingAssembly();
            string resourceName = string.Format("{0}.Resource_{1}.xml", fileNameSpace, cultureInfoString);
            Stream stream = fileAssembly.GetManifestResourceStream(resourceName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);
            LoadResourceItemFromXmlDoc(xmlDoc);
        }

        /// <summary>
        /// 载入系统(插件)级的语言资源(其通过配置文件指定了存放的文件夹位置)
        /// </summary>
        /// <param name="resourceFile"></param>
        private static void LoadAppResource(string resourceFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string phyFilePath = HttpContext.Current.Server.MapPath(resourceFile);
            if (File.Exists(phyFilePath) == true)
            {
                xmlDoc.Load(phyFilePath);
                LoadResourceItemFromXmlDoc(xmlDoc);
            }
        }

        private static void LoadResourceItemFromXmlDoc(XmlDocument xmlDoc)
        {
            XmlNodeList nodes = xmlDoc.SelectNodes("data/item");

            foreach (XmlNode node in nodes)
            {
                string nodeKey = node.Attributes["key"].Value;
                string nodeValue = node.InnerXml;
                if (resource.ContainsKey(nodeKey) == false)
                {
                    resource.Add(nodeKey, nodeValue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (Resource.ContainsKey(key))
            {
                return Resource[key];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取嵌入资源的web路径
        /// </summary>
        /// <param name="resourceFileName"></param>
        /// <returns>
        /// 注:这个资源必须在命名空间上使用 assembly: WebResource() 进行注册(或者在 AssemblyInfo.cs 文件中配置 assembly:WebResource 程序集属性),才可以使用
        /// </returns>
        public static string GetResourceWebUrl(string resourceFileName)
        {
            string result = string.Empty;
            Page currentPage =HttpContext.Current.CurrentHandler as Page;
            if (currentPage != null)
            {
                string resourceFileFullName = string.Format("HiLand.Utility.Resources.{0}",resourceFileName);
                result = currentPage.ClientScript.GetWebResourceUrl(typeof(ResourcesManager), resourceFileFullName);
            }
            return result;
        }
    }
}
