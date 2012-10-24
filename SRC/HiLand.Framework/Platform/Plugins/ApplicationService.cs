using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Reflection;
using HiLand.Utility.IO;
using System.Web;
using HiLand.Utility.Setting;

namespace HiLand.Framework.BusinessCore
{
    public class ApplicationService
    {
        private static Dictionary<Guid, IApplication> plugins = new Dictionary<Guid, IApplication>();
        /// <summary>
        /// 插件列表
        /// </summary>
        public static Dictionary<Guid, IApplication> Plugins
        {
            get
            {
                return plugins;
            }
        }

        public static BusinessKernal core = null;
        public static BusinessKernal Core
        {
            set { core = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static void LoadPlugins()
        {
            //动态加载插件
            string pluginPathInSetting = Config.GetAppSetting("pluginPath");
            if (pluginPathInSetting == null)
            {
                pluginPathInSetting = string.Empty;
            }
            string pluginDirPath = string.Empty;



            //以下代码综合考虑web和win应用
            if (HttpContext.Current != null)
            {
                pluginDirPath = HttpContext.Current.Server.MapPath(pluginPathInSetting);
            }
            else
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (basePath.EndsWith("\\") == false)
                {
                    basePath += Path.DirectorySeparatorChar;
                }
                pluginDirPath = basePath + pluginPathInSetting;
            }

            DirectoryInfo dir = new DirectoryInfo(pluginDirPath);
            if (dir.Exists == true)
            {
                string pluginExtensions = Config.GetAppSetting("pluginExtensions");
                if (string.IsNullOrEmpty(pluginExtensions))
                {
                    FileInfo[] files = dir.GetFiles();
                    for (int i = 0; i < files.Length; i++)
                    {
                        LoadPlugin(files[i].FullName);
                    }
                }
                else
                {
                    string[] plugExtensionArray = pluginExtensions.Split(new string[]{";"},StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < plugExtensionArray.Length; j++)
                    {
                        FileInfo[] files = dir.GetFiles(plugExtensionArray[j]);
                        for (int i = 0; i < files.Length; i++)
                        {
                            LoadPlugin(files[i].FullName);
                        }
                    }
                }
            }
        }

        public static void LoadPlugin(string pluginFilePath)
        {
            //为了能够载入的插件能够在系统服务运行中能动态替换，加载时先将插件拷贝到内存中，然后加载
            //(在内存中复制插件Dll，然后加载内存中的Dll。这样，硬盘上的Dll就可以随意地被覆盖或删除了)
            if (File.Exists(pluginFilePath) == false)
            {
                return;
            }

            // IApplication result = null;
            Assembly assembly = null;

            //方式1：(直接加载)
            //Assembly assembly = Assembly.LoadFile(pluginFilePath);

            //方式2：(内存加载)
            //先将插件拷贝到内存缓冲
            byte[] addinStream = null;
            addinStream = FileHelper.ReadFileBytes(pluginFilePath);
            assembly = Assembly.Load(addinStream); //加载内存中的Dll

            //得到Assembly中的所有类型
            Type[] types = assembly.GetTypes();

            //遍历所有的类型，找到插件类型，并创建插件实例并加载
            foreach (Type type in types)
            {
                if (type.GetInterface("IApplication") != null)//判断类型是否派生自IPlugin接口
                {
                    IApplication plugin = (IApplication)Activator.CreateInstance(type);//创建插件实例
                    plugin.Core = core;
                    plugin.Load();
                    if (plugins.ContainsKey(plugin.ApplicationGuid))
                    {
                        plugins[plugin.ApplicationGuid] = plugin;
                    }
                    else
                    {
                        plugins.Add(plugin.ApplicationGuid, plugin);
                    }
                }
            }
        }

        /// <summary>
        /// 根据插件的名称获取插件
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public IApplication GetApplication(string applicationName)
        {
            IApplication result = null;
            foreach (KeyValuePair<Guid, IApplication> kvp in plugins)
            {
                if (kvp.Value.ApplicationName == applicationName)
                {
                    result = kvp.Value;
                    break;
                }
            }

            return result;
        }


        /// <summary>
        /// 根据插件的GUID获取插件
        /// </summary>
        /// <param name="applicationGuid"></param>
        /// <returns></returns>
        public IApplication GetApplication(Guid applicationGuid)
        {
            IApplication result = null;
            if (plugins.ContainsKey(applicationGuid))
            {
                result = plugins[applicationGuid];
            }

            return result;
        }
    }
}
