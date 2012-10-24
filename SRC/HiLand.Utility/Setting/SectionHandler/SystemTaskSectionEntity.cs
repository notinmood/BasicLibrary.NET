using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 每日执行的系统任务实体
    /// </summary>
    public class SystemTaskOfDailyExcutorEntity
    {
        private string name = string.Empty;
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private int excuteHour = 0;
        /// <summary>
        /// 执行的小时点值
        /// </summary>
        public int ExcuteHour
        {
            get { return this.excuteHour; }
            set { this.excuteHour = value; }
        }

        private int excuteMinute = 0;
        /// <summary>
        /// 执行的分钟值
        /// </summary>
        public int ExcuteMinute
        {
            get { return this.excuteMinute; }
            set { this.excuteMinute = value; }
        }

        private string addonInfo = string.Empty;
        /// <summary>
        /// 任务的附属信息
        /// </summary>
        public string AddonInfo
        {
            get { return this.addonInfo; }
            set { this.addonInfo = value; }
        }

        private string addonDetails = string.Empty;
        /// <summary>
        /// 任务的其他附属信息
        /// </summary>
        public string AddonDetails
        {
            get { return this.addonDetails; }
            set { this.addonDetails = value; }
        }
        

        private Type type = null;
        /// <summary>
        /// 任务对应类的类型
        /// </summary>
        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}
