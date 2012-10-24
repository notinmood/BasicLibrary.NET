using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class SimpleProductCategoryEntity : BaseModel<SimpleProductCategoryEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ProductCategoryGuid" }; }
        }


        #region 实体信息
        private int productCategoryID;
        public int ProductCategoryID
        {
            get { return productCategoryID; }
            set { productCategoryID = value; }
        }

        private Guid productCategoryGuid = Guid.Empty;
        public Guid ProductCategoryGuid
        {
            get { return productCategoryGuid; }
            set { productCategoryGuid = value; }
        }

        private string productCategoryCode = String.Empty;
        public string ProductCategoryCode
        {
            get { return productCategoryCode; }
            set { productCategoryCode = value; }
        }

        private string productCategoryName = String.Empty;
        public string ProductCategoryName
        {
            get { return productCategoryName; }
            set { productCategoryName = value; }
        }

        private Logics productCategoryStatus = Logics.True;
        public Logics ProductCategoryStatus
        {
            get { return productCategoryStatus; }
            set { productCategoryStatus = value; }
        }

        private Logics canUsable= Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private string productCategoryMemo = String.Empty;
        public string ProductCategoryMemo
        {
            get { return productCategoryMemo; }
            set { productCategoryMemo = value; }
        }

        private int productCategoryOrder;
        public int ProductCategoryOrder
        {
            get { return productCategoryOrder; }
            set { productCategoryOrder = value; }
        }

        private int productCount;
        public int ProductCount
        {
            get { return productCount; }
            set { productCount = value; }
        }
        #endregion
    }
}
