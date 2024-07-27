//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using Hiland.BasicLibrary.Data;

//namespace Hiland.BasicLibrary.Setting
//{
//    /// <summary>
//    /// config配置文件读取工具
//    /// </summary>
//    public static class Config
//    {
//        #region 数据库连接字符串
//        private static string defaultConnectionString = null;
//        /// <summary>
//        /// 数据库连接字符串
//        /// </summary>
//        public static string DefaultConnectionString
//        {
//            get
//            {
//                if (defaultConnectionString == null)
//                {
//                    defaultConnectionString = GetConnectionString();
//                }
//                return defaultConnectionString;

//                //TODO:需要加入缓存的判断和使用
//                //if (CacheConnectionStrings == true)
//                //{
//                //    if (connectionString == null)
//                //    {
//                //        connectionString = GetConnectionString();
//                //    }
//                //    return connectionString;
//                //}
//                //else
//                //{
//                //    if (connectionString == null)
//                //    {
//                //        connectionString = GetConnectionString();
//                //    }
//                //    return connectionString;
//                //}
//            }
//        }

//        private static string defaultConnectionStringName = string.Empty;
//        /// <summary>
//        /// 系统缺省的连接字符串名称
//        /// </summary>
//        public static string DefaultConnectionStringName
//        {
//            get
//            {
//                //1、先从web.config配置内读取
//                if (string.IsNullOrEmpty(defaultConnectionStringName))
//                {
//                    defaultConnectionStringName = Config.GetAppSetting<string>("defaultConnectionStringName");
//                }

//                //2、如果配置文件内没有设置，则使用缺省的连接名称
//                if (string.IsNullOrEmpty(defaultConnectionStringName))
//                {
//                    defaultConnectionStringName = "mainConnection";
//                }

//                return defaultConnectionStringName;
//            }
//        }

//        private static Nullable<bool> cacheConnectionStrings = null;
//        /// <summary>
//        /// 是否要缓存数据库连接
//        /// </summary>
//        private static bool CacheConnectionStrings
//        {
//            get
//            {
//                if (cacheConnectionStrings == null)
//                {
//                    string temp = GetAppSetting("cacheConnectionStrings");
//                    if (temp == null)
//                    {
//                        cacheConnectionStrings = true;
//                    }
//                    else
//                    {
//                        bool cache = false;
//                        Boolean.TryParse(temp, out cache);
//                        cacheConnectionStrings = cache;
//                    }
//                }

//                return cacheConnectionStrings.Value;
//            }
//        }

//        /// <summary>
//        /// 获取系统缺省的连接字符串
//        /// </summary>
//        /// <returns></returns>
//        public static string GetConnectionString()
//        {
//            return GetConnectionString(DefaultConnectionStringName);
//        }

//        /// <summary>
//        /// 获取某个指定名称的数据库连接字符串
//        /// </summary>
//        /// <param name="connectionStringName">连接字符串名称</param>
//        /// <returns></returns>
//        public static string GetConnectionString(string connectionStringName)
//        {
//            var conn = ConfigurationManager.ConnectionStrings[connectionStringName];

//            if (conn == null)
//            {
//                return string.Empty;
//            }
//            else
//            {
//                return conn.ConnectionString;
//            }
//        }
//        #endregion

//        #region 基础应用程序配置节点读取
//        private static Nullable<bool> cacheAppSettings = null;
//        /// <summary>
//        /// 是否要缓存配置项
//        /// </summary>
//        private static bool CacheAppSettings
//        {
//            get
//            {
//                if (cacheAppSettings == null)
//                {
//                    string temp = ConfigurationManager.AppSettings["cacheAppSettings"];
//                    if (temp == null)
//                    {
//                        cacheAppSettings = true;
//                    }
//                    else
//                    {
//                        bool cache = false;
//                        Boolean.TryParse(temp, out cache);
//                        cacheAppSettings = cache;
//                    }
//                }

//                return cacheAppSettings.Value;
//            }
//        }

//        private static List<KeyValuePair<string, object>> appSettings = new List<KeyValuePair<string, object>>();

//        /// <summary>
//        /// 读取配置节点
//        /// </summary>
//        /// <param name="settingName">配置节点名称</param>
//        /// <returns></returns>
//        public static string GetAppSetting(string settingName)
//        {
//            return GetAppSetting(settingName, string.Empty);
//        }



//        /// <summary>
//        /// 读取配置节点
//        /// </summary>
//        /// <param name="settingName">配置节点名称</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static string GetAppSetting(string settingName, string defaultValue)
//        {
//            return GetAppSetting<string>(settingName, defaultValue);
//        }

//        /// <summary>
//        /// 读取Int类型的配置节点
//        /// </summary>
//        /// <param name="settingName">配置节点名称</param>
//        /// <returns></returns>
//        public static int GetAppSettingInt(string settingName)
//        {
//            return GetAppSettingInt(settingName, 0);
//        }

//        /// <summary>
//        /// 读取Int类型的配置节点
//        /// </summary>
//        /// <param name="settingName">配置节点名称</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static int GetAppSettingInt(string settingName, int defaultValue)
//        {
//            return GetAppSetting<int>(settingName, defaultValue);
//        }

//        /// <summary>
//        ///  读取Bool类型的配置节点
//        /// </summary>
//        /// <param name="settingName">配置节点名称</param>
//        /// <returns></returns>
//        public static bool GetAppSettingBool(string settingName)
//        {
//            return GetAppSettingBool(settingName, false);
//        }

//        /// <summary>
//        /// 读取Bool类型的配置节点
//        /// </summary>
//        /// <param name="settingName">配置节点名称</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static bool GetAppSettingBool(string settingName, bool defaultValue)
//        {
//            return GetAppSetting<bool>(settingName, defaultValue);
//        }

//        /// <summary>
//        /// 读取配置节点的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="settingName"></param>
//        /// <returns></returns>
//        public static T GetAppSetting<T>(string settingName)
//        {
//            return GetAppSetting<T>(settingName, default(T));
//        }


//        /// <summary>
//        /// 读取配置节点的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="settingName"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static T GetAppSetting<T>(string settingName, T defaultValue)
//        {
//            if (CacheAppSettings == true)
//            {
//                int count = appSettings.Count;
//                foreach (KeyValuePair<string, object> kvpItem in appSettings)
//                {
//                    if (kvpItem.Key == settingName)
//                    {
//                        return Converter.ChangeType<T>(kvpItem.Value);
//                    }
//                }
//            }

//            T result = default(T);
//            string resultString = ConfigurationManager.AppSettings[settingName];

//            if (string.IsNullOrEmpty(resultString))
//            {
//                result = defaultValue;
//            }
//            else
//            {
//                result = Converter.ChangeType<T>(resultString, defaultValue);
//            }

//            if (CacheAppSettings == true)
//            {
//                KeyValuePair<string, object> kvp = new KeyValuePair<string, object>(settingName, result);
//                appSettings.Add(kvp);
//            }

//            return result;
//        }
//        #endregion

//        #region 自定义应用程序配置项读取
//        /// <summary>
//        /// 获取配置节信息（因为同一个应用内调用的次数不多，暂时不做缓存处理）
//        /// </summary>
//        /// <typeparam name="T">配置节对应的数据类型</typeparam>
//        /// <param name="sectionName">配置节的名称</param>
//        /// <returns></returns>
//        public static T GetSection<T>(string sectionName)
//        {
//            T result = default(T);

//            object section = ConfigurationManager.GetSection(sectionName);
//            if (section != null)
//            {
//                result = (T)section;
//            }

//            return result;
//        }
//        #endregion

//        #region 常用配置
//        /// <summary>
//        /// 是否记录操作日志
//        /// </summary>
//        public static bool IsRecordOperateLog
//        {
//            get
//            {
//                return GetAppSetting<bool>("isRecordOperateLog");
//            }
//        }
//        #endregion
//    }
//}
