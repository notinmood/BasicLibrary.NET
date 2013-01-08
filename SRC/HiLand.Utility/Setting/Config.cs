using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using HiLand.Utility.Data;

namespace HiLand.Utility.Setting
{
    /// <summary>
    /// config配置文件读取工具
    /// </summary>
    public static class Config
    {
        #region 数据库连接字符串
        /// <summary>
        /// 获取缺省的数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string connectionString = string.Empty;
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnectionString"];
                if (settings != null)
                {
                    connectionString = settings.ConnectionString;
                }
                return connectionString;
            }
        }

        /// <summary>
        /// 获取某个指定名称的数据库连接字符串
        /// </summary>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
        #endregion


        #region 基础应用程序配置节点读取
        /// <summary>
        /// 读取配置节点
        /// </summary>
        /// <param name="settingName">配置节点名称</param>
        /// <returns></returns>
        public static string GetAppSetting(string settingName)
        {
            return GetAppSetting(settingName, string.Empty);
        }

        /// <summary>
        /// 读取配置节点
        /// </summary>
        /// <param name="settingName">配置节点名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static string GetAppSetting(string settingName, string defaultValue)
        {
            string result = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrEmpty(result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 读取Int类型的配置节点
        /// </summary>
        /// <param name="settingName">配置节点名称</param>
        /// <returns></returns>
        public static int GetAppSettingInt(string settingName)
        {
            return GetAppSettingInt(settingName, 0);
        }

        /// <summary>
        /// 读取Int类型的配置节点
        /// </summary>
        /// <param name="settingName">配置节点名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static int GetAppSettingInt(string settingName, int defaultValue)
        {
            string result = ConfigurationManager.AppSettings[settingName];
            int resultValue = 0;
            bool isSuccessful = int.TryParse(result, out resultValue);
            if (isSuccessful == false)
            {
                resultValue = defaultValue;
            }
            return resultValue;
        }

        /// <summary>
        ///  读取Bool类型的配置节点
        /// </summary>
        /// <param name="settingName">配置节点名称</param>
        /// <returns></returns>
        public static bool GetAppSettingBool(string settingName)
        {
            return GetAppSettingBool(settingName, false);
        }

        /// <summary>
        /// 读取Bool类型的配置节点
        /// </summary>
        /// <param name="settingName">配置节点名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static bool GetAppSettingBool(string settingName, bool defaultValue)
        {
            string result = ConfigurationManager.AppSettings[settingName];
            bool resultValue = false;
            bool isSuccessful = bool.TryParse(result, out resultValue);
            if (isSuccessful == false)
            {
                resultValue = defaultValue;
            }
            return resultValue;
        }

        /// <summary>
        /// 读取配置节点的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static T GetAppSetting<T>(string settingName)
        {
            return GetAppSetting<T>(settingName, default(T));
        }

        /// <summary>
        /// 读取配置节点的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetAppSetting<T>(string settingName, T defaultValue)
        {
            string resultString = ConfigurationManager.AppSettings[settingName];
            return Converter.ChangeType<T>(resultString, defaultValue);
        }
        #endregion

        #region 自定义应用程序配置项读取
        /// <summary>
        /// 获取配置节信息
        /// </summary>
        /// <typeparam name="T">配置节对应的数据类型</typeparam>
        /// <param name="sectionName">配置节的名称</param>
        /// <returns></returns>
        public static T GetSection<T>(string sectionName)
        {
            T result = default(T);

            object section= ConfigurationManager.GetSection(sectionName);
            if (section != null)
            { 
                result= (T)section;
            }

            return result;
        }
        #endregion

        #region 常用配置
        private static bool? isRecordOperateLog= null;
        /// <summary>
        /// 是否记录操作日志
        /// </summary>
        public static bool IsRecordOperateLog
        {
            get
            {
                if (isRecordOperateLog.HasValue == false)
                {
                    isRecordOperateLog= GetAppSetting<bool>("isRecordOperateLog");
                }

                return isRecordOperateLog.Value;
            }
        }
        #endregion
    }
}
