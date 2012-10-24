using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class BasicSettingEntity : BaseModel<BasicSettingEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "SettingKey" }; }
        }

        #region 实体信息


        private int settingID;
        public int SettingID
        {
            get { return settingID; }
            set { settingID = value; }
        }

        private string settingKey = String.Empty;
         [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public string SettingKey
        {
            get { return settingKey; }
            set { settingKey = value; }
        }

        private string settingValue = String.Empty;
        public string SettingValue
        {
            get { return settingValue; }
            set { settingValue = value; }
        }

        private string settingDesc = String.Empty;
        public string SettingDesc
        {
            get { return settingDesc; }
            set { settingDesc = value; }
        }

        private string settingCategory = String.Empty;
        public string SettingCategory
        {
            get { return settingCategory; }
            set { settingCategory = value; }
        }
        

        private string displayName = String.Empty;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private int orderNumber;
        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        private Logics canUsable = Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private Logics isInnerSetting;
        /// <summary>
        /// 是否为系统内部配置项（系统内部配置项不会向业务管理员开放）
        /// </summary>
        public Logics IsInnerSetting
        {
            get { return isInnerSetting; }
            set { isInnerSetting = value; }
        }
        #endregion
    }
}
