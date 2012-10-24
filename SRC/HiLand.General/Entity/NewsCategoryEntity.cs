using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class NewsCategoryEntity : BaseModel<NewsCategoryEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "NewsCategoryCode" }; }
        }

        #region 实体信息
        private int newsCategoryID;
        public int NewsCategoryID
        {
            get { return newsCategoryID; }
            set { newsCategoryID = value; }
        }

        private string newsCategoryCode = String.Empty;
         [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public string NewsCategoryCode
        {
            get { return newsCategoryCode; }
            set { newsCategoryCode = value; }
        }

        private string newsCategoryName = String.Empty;
        public string NewsCategoryName
        {
            get { return newsCategoryName; }
            set { newsCategoryName = value; }
        }

        private Logics canUsable = Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }
        #endregion
    }
}
