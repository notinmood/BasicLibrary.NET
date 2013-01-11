using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Logging
{
    /// <summary>
    /// 日志实体信息
    /// </summary>
    public class LogEntity : ILogEntity
    {
        private int logID;
        public int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        private Guid logGuid = Guid.Empty;
        public Guid LogGuid
        {
            get { return logGuid; }
            set { logGuid = value; }
        }

        private string logCategory = String.Empty;
        public string LogCategory
        {
            get { return logCategory; }
            set { logCategory = value; }
        }

        private Logics logStatus = Logics.True;
        public Logics LogStatus
        {
            get { return logStatus; }
            set { logStatus = value; }
        }

        private string logLevel = String.Empty;
        public string LogLevel
        {
            get { return logLevel; }
            set { logLevel = value; }
        }

        private string logger = String.Empty;
        public string Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        private string logMessage = String.Empty;
        public string LogMessage
        {
            get { return logMessage; }
            set { logMessage = value; }
        }

        private string logThread = String.Empty;
        public string LogThread
        {
            get { return logThread; }
            set { logThread = value; }
        }

        private string logException = String.Empty;
        public string LogException
        {
            get { return logException; }
            set { logException = value; }
        }

        private DateTime logDate = DateTimeHelper.Min;
        public DateTime LogDate
        {
            get { return logDate; }
            set { logDate = value; }
        }

        public override string ToString()
        {
            string result = string.Format("{0}-{1}-{2}\r\n--------\r\n",this.LogDate.ToString(),this.LogCategory,this.LogMessage);
            return base.ToString();
        }
    }
}
