using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// （产品等）预定数据实体
    /// </summary>
    public class ForeOrderEntity : BaseModel<ForeOrderEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ForeOrderGuid" }; }
        }

        #region 基本信息

        private int foreOrderID;
        public int ForeOrderID
        {
            get { return foreOrderID; }
            set { foreOrderID = value; }
        }

        private Guid foreOrderGuid = Guid.Empty;
        public Guid ForeOrderGuid
        {
            get { return foreOrderGuid; }
            set { foreOrderGuid = value; }
        }

        private int foreOrderType;
        public int ForeOrderType
        {
            get { return foreOrderType; }
            set { foreOrderType = value; }
        }

        private string foreOrderCategory = String.Empty;
        /// <summary>
        /// 预定信息的类别（产品还是某个服务）
        /// </summary>
        public string ForeOrderCategory
        {
            get { return foreOrderCategory; }
            set { foreOrderCategory = value; }
        }

        private ForeOrderStatuses foreOrderStatus= ForeOrderStatuses.Fore;
        /// <summary>
        /// 预定产品或者服务的履行状态
        /// </summary>
        public ForeOrderStatuses ForeOrderStatus
        {
            get { return foreOrderStatus; }
            set { foreOrderStatus = value; }
        }

        private DateTime foreOrderDate = DateTimeHelper.Min;
        /// <summary>
        /// 预定产品或者服务的履行日期
        /// </summary>
        public DateTime ForeOrderDate
        {
            get { return foreOrderDate; }
            set { foreOrderDate = value; }
        }

        private string foreOrderTitle = String.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public string ForeOrderTitle
        {
            get { return foreOrderTitle; }
            set { foreOrderTitle = value; }
        }

        private string foreOrderDesc = String.Empty;
        public string ForeOrderDesc
        {
            get { return foreOrderDesc; }
            set { foreOrderDesc = value; }
        }

        private string ownerKey = String.Empty;
        /// <summary>
        /// 预定人Key
        /// </summary>
        public string OwnerKey
        {
            get { return ownerKey; }
            set { ownerKey = value; }
        }

        private string ownerName = String.Empty;
        /// <summary>
        /// 预定人名称
        /// </summary>
        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        private string ownerOtherInfo = String.Empty;
        /// <summary>
        /// 预定人其他信息
        /// </summary>
        public string OwnerOtherInfo
        {
            get { return ownerOtherInfo; }
            set { ownerOtherInfo = value; }
        }

        private string relativeKey = String.Empty;
        /// <summary>
        /// 具体某产品或者服务的Key
        /// </summary>
        public string RelativeKey
        {
            get { return relativeKey; }
            set { relativeKey = value; }
        }

        private string relativeName = String.Empty;
        /// <summary>
        /// 具体某产品或者服务的名称
        /// </summary>
        public string RelativeName
        {
            get { return relativeName; }
            set { relativeName = value; }
        }

        private string relativeNameOther = String.Empty;
        /// <summary>
        /// 具体某产品或者服务的其他信息
        /// </summary>
        public string RelativeNameOther
        {
            get { return relativeNameOther; }
            set { relativeNameOther = value; }
        }

        private decimal foreOrderAmount;
        /// <summary>
        /// 预定数量
        /// </summary>
        public decimal ForeOrderAmount
        {
            get { return foreOrderAmount; }
            set { foreOrderAmount = value; }
        }

        private string foreOrderUnitName = String.Empty;
        /// <summary>
        /// 产品或者服务履行的计量单位（斤，个，顿等）
        /// </summary>
        public string ForeOrderUnitName
        {
            get { return foreOrderUnitName; }
            set { foreOrderUnitName = value; }
        }

        private decimal foreOrderUnitFee;
        /// <summary>
        /// 单价
        /// </summary>
        public decimal ForeOrderUnitFee
        {
            get { return foreOrderUnitFee; }
            set { foreOrderUnitFee = value; }
        }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal ForeOrderTotalFee
        {
            get { return foreOrderUnitFee * foreOrderAmount; }
        }

        private Logics foreOrderPaid;
        /// <summary>
        /// 费用是否已付
        /// </summary>
        public Logics ForeOrderPaid
        {
            get { return foreOrderPaid; }
            set { foreOrderPaid = value; }
        }

        private string foreOrderMemo1 = String.Empty;
        public string ForeOrderMemo1
        {
            get { return foreOrderMemo1; }
            set { foreOrderMemo1 = value; }
        }

        private string foreOrderMemo2 = String.Empty;
        public string ForeOrderMemo2
        {
            get { return foreOrderMemo2; }
            set { foreOrderMemo2 = value; }
        }

        private int canUsable;
        public int CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
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
