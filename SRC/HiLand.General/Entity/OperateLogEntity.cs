using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 操作日志信息
    /// </summary>
    public class OperateLogEntity : BaseModel<OperateLogEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "LogGuid" }; }
        }

        #region 基本信息

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

        private string logTitle = String.Empty;
        public string LogTitle
        {
            get { return logTitle; }
            set { logTitle = value; }
        }

        private int logType;
        public int LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        private string logCategory = String.Empty;
        public string LogCategory
        {
            get { return logCategory; }
            set { logCategory = value; }
        }

        private string logMessage = String.Empty;
        public string LogMessage
        {
            get { return logMessage; }
            set { logMessage = value; }
        }

        private string logOperateName = String.Empty;
        public string LogOperateName
        {
            get { return logOperateName; }
            set { logOperateName = value; }
        }

        private int logStatus;
        public int LogStatus
        {
            get { return logStatus; }
            set { logStatus = value; }
        }

        private Logics canUsable;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private string relativeKey = String.Empty;
        public string RelativeKey
        {
            get { return relativeKey; }
            set { relativeKey = value; }
        }

        private string relativeName = String.Empty;
        public string RelativeName
        {
            get { return relativeName; }
            set { relativeName = value; }
        }

        private string relativeOther = String.Empty;
        public string RelativeOther
        {
            get { return relativeOther; }
            set { relativeOther = value; }
        }

        private string logUserKey = String.Empty;
        public string LogUserKey
        {
            get { return logUserKey; }
            set { logUserKey = value; }
        }

        private string logUserName = String.Empty;
        public string LogUserName
        {
            get { return logUserName; }
            set { logUserName = value; }
        }

        private string logUserOther = String.Empty;
        public string LogUserOther
        {
            get { return logUserOther; }
            set { logUserOther = value; }
        }

        private DateTime logDate = DateTimeHelper.Min;
        public DateTime LogDate
        {
            get { return logDate; }
            set { logDate = value; }
        }

        private string propertyNames = String.Empty;
        public string PropertyNames
        {
            get { return propertyNames; }
            set { propertyNames = value; }
        }

        private string propertyValues = String.Empty;
        public string PropertyValues
        {
            get { return propertyValues; }
            set { propertyValues = value; }
        }

        #endregion
    }
}
