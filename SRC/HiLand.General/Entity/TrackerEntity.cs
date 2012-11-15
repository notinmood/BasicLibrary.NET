using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 回访、跟踪信息实体
    /// </summary>
    public class TrackerEntity : BaseModel<TrackerEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "TrackerGuid" }; }
        }

        #region 基本信息

        private int trackerID;
        public int TrackerID
        {
            get { return trackerID; }
            set { trackerID = value; }
        }

        private Guid trackerGuid = Guid.Empty;
        public Guid TrackerGuid
        {
            get { return trackerGuid; }
            set { trackerGuid = value; }
        }

        private string relativeKey = String.Empty;
        public string RelativeKey
        {
            get { return relativeKey; }
            set { relativeKey = value; }
        }

        private Logics canUsable= Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private string trackerTitle = String.Empty;
        public string TrackerTitle
        {
            get { return trackerTitle; }
            set { trackerTitle = value; }
        }

        private string trackerDesc = String.Empty;
        public string TrackerDesc
        {
            get { return trackerDesc; }
            set { trackerDesc = value; }
        }

        private string trackerCategory = String.Empty;
        public string TrackerCategory
        {
            get { return trackerCategory; }
            set { trackerCategory = value; }
        }

        private int trackerType;
        public int TrackerType
        {
            get { return trackerType; }
            set { trackerType = value; }
        }

        private DateTime trackerTime = DateTimeHelper.Min;
        public DateTime TrackerTime
        {
            get { return trackerTime; }
            set { trackerTime = value; }
        }

        private string trackerUserKey = String.Empty;
        public string TrackerUserKey
        {
            get { return trackerUserKey; }
            set { trackerUserKey = value; }
        }

        private DateTime createTime = DateTimeHelper.Min;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string createUserKey = String.Empty;
        public string CreateUserKey
        {
            get { return createUserKey; }
            set { createUserKey = value; }
        }
        #endregion
    }
}
