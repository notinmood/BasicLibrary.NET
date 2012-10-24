using System;
using HiLand.Framework.FoundationLayer;
using HiLand.General.BLL;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class SimpleProductEntity : BaseModel<SimpleProductEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ProductGuid" }; }
        }


        #region 实体信息
        private int productID;
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private Guid productGuid = Guid.Empty;
        public Guid ProductGuid
        {
            get { return productGuid; }
            set { productGuid = value; }
        }

        private string productCode = String.Empty;
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private string productName = String.Empty;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        /// <summary>
        /// 品牌
        /// </summary>
        private string productBrand = String.Empty;
        public string ProductBrand
        {
            get { return productBrand; }
            set { productBrand = value; }
        }

        private string productCategoryCode = String.Empty;
        public string ProductCategoryCode
        {
            get { return productCategoryCode; }
            set { productCategoryCode = value; }
        }

        private string productCategoryName = String.Empty;
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ProductCategoryName
        {
            get { return productCategoryName; }
            internal set { productCategoryName = value; }
        }


        private decimal productPriceNormal;
        public decimal ProductPriceNormal
        {
            get { return productPriceNormal; }
            set { productPriceNormal = value; }
        }

        private decimal productPricePromotion;
        public decimal ProductPricePromotion
        {
            get { return productPricePromotion; }
            set { productPricePromotion = value; }
        }

        private decimal productPriceReference;
        public decimal ProductPriceReference
        {
            get { return productPriceReference; }
            set { productPriceReference = value; }
        }

        private string productAddress = String.Empty;
        public string ProductAddress
        {
            get { return productAddress; }
            set { productAddress = value; }
        }

        private string productPackegUnit = String.Empty;
        public string ProductPackegUnit
        {
            get { return productPackegUnit; }
            set { productPackegUnit = value; }
        }

        private string productMaterial = String.Empty;
        public string ProductMaterial
        {
            get { return productMaterial; }
            set { productMaterial = value; }
        }

        private int productCountRepository;
        public int ProductCountRepository
        {
            get { return productCountRepository; }
            set { productCountRepository = value; }
        }

        private int productCountSaled;
        public int ProductCountSaled
        {
            get { return productCountSaled; }
            set { productCountSaled = value; }
        }

        private int productHasInvoice;
        public int ProductHasInvoice
        {
            get { return productHasInvoice; }
            set { productHasInvoice = value; }
        }

        private Logics productIsHot=  Logics.False;
        public Logics ProductIsHot
        {
            get { return productIsHot; }
            set { productIsHot = value; }
        }

        private Logics productIsTop = Logics.False;
        public Logics ProductIsTop
        {
            get { return productIsTop; }
            set { productIsTop = value; }
        }

        private Logics productStatus = Logics.True;
        public Logics ProductStatus
        {
            get { return productStatus; }
            set { productStatus = value; }
        }

        private Logics canUsable = Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private string productSpecification = String.Empty;
        public string ProductSpecification
        {
            get { return productSpecification; }
            set { productSpecification = value; }
        }

        private string productMemo = String.Empty;
        public string ProductMemo
        {
            get { return productMemo; }
            set { productMemo = value; }
        }
        #endregion

        #region 扩展属性
        private ImageEntity productMainImage = null;
        /// <summary>
        /// 产品的主图片
        /// </summary>
        public ImageEntity ProductMainImage
        {
            get
            {
                if (productMainImage == null)
                {
                    productMainImage = ImageBLL.Instance.GetMainImage(Guid.Empty, this.ProductGuid);
                }

                return productMainImage;
            }
        }
        #endregion
    }
}
