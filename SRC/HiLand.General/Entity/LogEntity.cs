using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class LogEntity : BaseModel<LogEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "LogGuid" }; }
        }

        #region 实体信息
        private int logID;
        [DBFieldAttribute(IsBusinessPrimaryKey = true)]
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

        private Logics logStatus= Logics.True;
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
        #endregion
    }
}
