using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 行政区划实体信息
    /// </summary>
    public class AreaEntity : BaseModel<BankEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "AreaCode" }; }
        }

        #region 基本信息

        private int areaID;
        public int AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        private string areaCode = String.Empty;
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        private string areaName = String.Empty;
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        private int areaLevel;
        public int AreaLevel
        {
            get { return areaLevel; }
            set { areaLevel = value; }
        }

        private string telephoneCode = String.Empty;
        public string TelephoneCode
        {
            get { return telephoneCode; }
            set { telephoneCode = value; }
        }

        private string zipCode = String.Empty;
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        private int canUsable;
        public int CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private int isDisplay;
        public int IsDisplay
        {
            get { return isDisplay; }
            set { isDisplay = value; }
        }

        private string areaGroup = String.Empty;
        public string AreaGroup
        {
            get { return areaGroup; }
            set { areaGroup = value; }
        }

        private string nation = String.Empty;
        public string Nation
        {
            get { return nation; }
            set { nation = value; }
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
