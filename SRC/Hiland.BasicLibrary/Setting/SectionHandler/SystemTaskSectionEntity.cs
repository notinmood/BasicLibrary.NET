//using System;
//using System.Collections.Generic;
//using System.Text;
//using HiLand.Utility.Data;

//namespace HiLand.Utility.Setting.SectionHandler
//{
//    /// <summary>
//    /// 每日执行的系统任务实体
//    /// </summary>
//    public class SystemTaskOfDailyExcutorEntity
//    {
//        private string name = string.Empty;
//        /// <summary>
//        /// 任务名称
//        /// </summary>
//        public string Name
//        {
//            get { return this.name; }
//            set { this.name = value; }
//        }

//        private int excuteHour = 0;
//        /// <summary>
//        /// 执行的小时点值
//        /// </summary>
//        public int ExcuteHour
//        {
//            get { return this.excuteHour; }
//            set { this.excuteHour = value; }
//        }

//        private int excuteMinute = 0;
//        /// <summary>
//        /// 执行的分钟值
//        /// </summary>
//        public int ExcuteMinute
//        {
//            get { return this.excuteMinute; }
//            set { this.excuteMinute = value; }
//        }

//        private bool isUse = false;
//        /// <summary>
//        /// 是否启用此任务
//        /// </summary>
//        public bool IsUse
//        {
//            get { return this.isUse; }
//            set { this.isUse = value; }
//        }

//        private string addon = string.Empty;
//        /// <summary>
//        /// 任务的附属信息
//        /// </summary>
//        /// <remarks>
//        /// 格式类似如下 key1:value1||key2:value2
//        /// </remarks>
//        public string Addon
//        {
//            get { return this.addon; }
//            set { this.addon = value; }
//        }

//        private Type type = null;
//        /// <summary>
//        /// 任务对应类的类型
//        /// </summary>
//        public Type Type
//        {
//            get { return this.type; }
//            set { this.type = value; }
//        }


//        #region 获取Addon内部里面设置项的值
//        private Dictionary<string, string> addonDic = null;

//        private Dictionary<string, string> AddonDic
//        {
//            get
//            {
//                if (addonDic == null)
//                {
//                    addonDic = StringHelper.SplitToDictionary(addon, ":", "||");
//                }

//                return addonDic;
//            }
//        }

//        /// <summary>
//        /// 获取Addon内部里面设置项的值
//        /// </summary>
//        /// <param name="addonKey"></param>
//        /// <returns></returns>
//        public string GetAddonItemValue(string addonKey)
//        {
//            string result = string.Empty;
//            if (AddonDic.ContainsKey(addonKey))
//            {
//                result = AddonDic[addonKey];
//            }

//            return result;
//        }

//        /// <summary>
//        /// 获取Addon内部里面设置项的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="addonKey"></param>
//        /// <returns></returns>
//        public T GetAddonItemValue<T>(string addonKey)
//        {
//            return Converter.ChangeType<T>(GetAddonItemValue(addonKey));
//        }
//        #endregion
//    }
//}
