using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 提醒信息实体
    /// </summary>
    public class RemindEntity : BaseModel<RemindEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "RemindGuid" }; }
        }

        #region 基本信息

        private int remindID;
        public int RemindID
        {
            get { return remindID; }
            set { remindID = value; }
        }

        private Guid remindGuid = Guid.Empty;
        public Guid RemindGuid
        {
            get { return remindGuid; }
            set { remindGuid = value; }
        }

        private string senderKey = String.Empty;
        public string SenderKey
        {
            get { return senderKey; }
            set { senderKey = value; }
        }

        private string senderName = String.Empty;
        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }

        private string receiverKey = String.Empty;
        public string ReceiverKey
        {
            get { return receiverKey; }
            set { receiverKey = value; }
        }

        private string receiverName = String.Empty;
        public string ReceiverName
        {
            get { return receiverName; }
            set { receiverName = value; }
        }

        private LevelTypes emergency= LevelTypes.Normal;
        public LevelTypes Emergency
        {
            get { return emergency; }
            set { emergency = value; }
        }

        private LevelTypes importance= LevelTypes.Normal;
        public LevelTypes Importance
        {
            get { return importance; }
            set { importance = value; }
        }

        private int topLevel;
        public int TopLevel
        {
            get { return topLevel; }
            set { topLevel = value; }
        }

        private string remindTitle = String.Empty;
        public string RemindTitle
        {
            get { return remindTitle; }
            set { remindTitle = value; }
        }

        private string remindUrl = String.Empty;
        public string RemindUrl
        {
            get { return remindUrl; }
            set { remindUrl = value; }
        }

        private string remindDescription = String.Empty;
        public string RemindDescription
        {
            get { return remindDescription; }
            set { remindDescription = value; }
        }

        private RemindCategories remindCategory;
        public RemindCategories RemindCategory
        {
            get { return remindCategory; }
            set { remindCategory = value; }
        }

        private int remindType;
        public int RemindType
        {
            get { return remindType; }
            set { remindType = value; }
        }

        private DateTime createDate = DateTimeHelper.Min;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private DateTime startDate = DateTimeHelper.Min;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime expireDate = DateTimeHelper.Min;
        public DateTime ExpireDate
        {
            get { return expireDate; }
            set { expireDate = value; }
        }

        private DateTime readDate = DateTimeHelper.Min;
        public DateTime ReadDate
        {
            get { return readDate; }
            set { readDate = value; }
        }

        private Logics readStatus= Logics.False;
        /// <summary>
        /// 是否已读状态
        /// </summary>
        public Logics ReadStatus
        {
            get { return readStatus; }
            set { readStatus = value; }
        }

        private string resourceKey = String.Empty;
        public string ResourceKey
        {
            get { return resourceKey; }
            set { resourceKey = value; }
        }

        private string processKey = String.Empty;
        public string ProcessKey
        {
            get { return processKey; }
            set { processKey = value; }
        }

        private string activityKey = String.Empty;
        public string ActivityKey
        {
            get { return activityKey; }
            set { activityKey = value; }
        }

        #endregion
    }
}
