using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class ShoppingCartEntity : BaseModel<ShoppingCartEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ShoppingItemGuid" }; }
        }

        #region entity information

        private int shoppingItemID;
        public int ShoppingItemID
        {
            get { return shoppingItemID; }
            set { shoppingItemID = value; }
        }

        private Guid shoppingItemGuid = Guid.Empty;
        public Guid ShoppingItemGuid
        {
            get { return shoppingItemGuid; }
            set { shoppingItemGuid = value; }
        }

        private string productKey = String.Empty;
        public string ProductKey
        {
            get { return productKey; }
            set { productKey = value; }
        }

        private string productName = String.Empty;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private decimal productPrice;
        public decimal ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }

        private int productQuantity;
        public int ProductQuantity
        {
            get { return productQuantity; }
            set { productQuantity = value; }
        }

        private string ownerKey = String.Empty;
        public string OwnerKey
        {
            get { return ownerKey; }
            set { ownerKey = value; }
        }

        private Logics isTempOwner= Logics.False;
        public Logics IsTempOwner
        {
            get { return isTempOwner; }
            set { isTempOwner = value; }
        }

        /// <summary>
        /// 是否为收藏产品（即不是本次购物的内容）
        /// </summary>
        private Logics isFavoriteItem = Logics.False;
        public Logics IsFavoriteItem
        {
            get { return isFavoriteItem; }
            set { isFavoriteItem = value; }
        }

        private string shoppingItemMemo = String.Empty;
        public string ShoppingItemMemo
        {
            get { return shoppingItemMemo; }
            set { shoppingItemMemo = value; }
        }

        private DateTime createTime = DateTimeHelper.Min;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private Logics canUsable= Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }
        #endregion
    }
}
