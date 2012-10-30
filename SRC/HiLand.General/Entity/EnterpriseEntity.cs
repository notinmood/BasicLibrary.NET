using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 企业实体
    /// </summary>
    public class EnterpriseEntity : BaseModel<EnterpriseEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "EnterpriseGuid" }; }
        }

        #region 实体信息
        private int enterpriseID;
        public int EnterpriseID
        {
            get { return enterpriseID; }
            set { enterpriseID = value; }
        }

        private Guid enterpriseGuid = Guid.Empty;
         [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid EnterpriseGuid
        {
            get { return enterpriseGuid; }
            set { enterpriseGuid = value; }
        }

        private string companyName = String.Empty;
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        private EnterpriseTypes businessType;
        public EnterpriseTypes BusinessType
        {
            get { return businessType; }
            set { businessType = value; }
        }

        private string tradingName = String.Empty;
        public string TradingName
        {
            get { return tradingName; }
            set { tradingName = value; }
        }

        private string industryKey = String.Empty;
        /// <summary>
        /// 所属行业
        /// </summary>
        /// <remarks>
        /// 1、如果需要使用用户自定义的多级别的行业信息，可以用此属性记录关联Guid
        /// 2、如果是简单的行业信息，可以用此属性直接记录行业名称的文字
        /// </remarks>
        public string IndustryKey
        {
            get { return industryKey; }
            set { industryKey = value; }
        }

        private IndustryTypes industryType = IndustryTypes.NonSet;
        /// <summary>
        /// 行业类型
        /// </summary>
        public IndustryTypes IndustryType
        {
            get { return industryType; }
            set { industryType = value; }
        }

        private string enterpriseCode = String.Empty;
        public string EnterpriseCode
        {
            get { return enterpriseCode; }
            set { enterpriseCode = value; }
        }

        private string taxCode = String.Empty;
        public string TaxCode
        {
            get { return taxCode; }
            set { taxCode = value; }
        }

        private string principleAddress = String.Empty;
        public string PrincipleAddress
        {
            get { return principleAddress; }
            set { principleAddress = value; }
        }

        private string postCode = String.Empty;
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        private string telephone = String.Empty;
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        private string fax = String.Empty;
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string email = String.Empty;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private int establishedYears;
        public int EstablishedYears
        {
            get { return establishedYears; }
            set { establishedYears = value; }
        }

        private DateTime establishedTime= DateTimeHelper.Min;
        public DateTime EstablishedTime
        {
            get { return establishedTime; }
            set { establishedTime = value; }
        }

        private decimal grossIncome;
        public decimal GrossIncome
        {
            get { return grossIncome; }
            set { grossIncome = value; }
        }

        private decimal profit;
        public decimal Profit
        {
            get { return profit; }
            set { profit = value; }
        }

        private Guid associatedEnterpriseGuid = Guid.Empty;
        public Guid AssociatedEnterpriseGuid
        {
            get { return associatedEnterpriseGuid; }
            set { associatedEnterpriseGuid = value; }
        }

        private string contactPerson = String.Empty;
        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        private string areaCode = String.Empty;
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        private string companyNameShort = String.Empty;
        /// <summary>
        /// 企业简称（如果没有填写简称其会获取全称）
        /// </summary>
        public string CompanyNameShort
        {
            get 
            {
                if (string.IsNullOrEmpty(this.companyNameShort))
                {
                    this.companyNameShort = companyName;
                }
                return this.companyNameShort; 
            }
            set { companyNameShort = value; }
        }

        private Logics canUsable = Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private decimal longitude;
        public decimal Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private decimal lantitude;
        public decimal Lantitude
        {
            get { return lantitude; }
            set { lantitude = value; }
        }

        private string brokerKey = string.Empty;
        /// <summary>
        /// 介绍人信息
        /// </summary>
        public virtual string BrokerKey
        {
            get
            {
                return this.brokerKey;
            }
            set
            {
                this.brokerKey = value;
            }
        }

        private string enterpriseDescription = String.Empty;
        /// <summary>
        /// 企业描述
        /// </summary>
        public string EnterpriseDescription
        {
            get { return enterpriseDescription; }
            set { enterpriseDescription = value; }
        }

        private string enterpriseMemo = String.Empty;
        /// <summary>
        /// 企业备注
        /// </summary>
        public string EnterpriseMemo
        {
            get { return enterpriseMemo; }
            set { enterpriseMemo = value; }
        }

        private string enterpriseWWW = String.Empty;
        /// <summary>
        /// 企业网址
        /// </summary>
        public string EnterpriseWWW
        {
            get { return enterpriseWWW; }
            set { enterpriseWWW = value; }
        }

        private StaffScopes staffScope;
        /// <summary>
        /// 人员规模
        /// </summary>
        public StaffScopes StaffScope
        {
            get { return staffScope; }
            set { staffScope = value; }
        }

        private CommonLevels enterpriseLevel;
        /// <summary>
        /// 企业级别
        /// </summary>
        /// <remarks>
        /// 可以是合作意向的级别，企业自身的级别等
        /// </remarks>
        public CommonLevels EnterpriseLevel
        {
            get { return enterpriseLevel; }
            set { enterpriseLevel = value; }
        }

        private string enterpriseRank = String.Empty;
        /// <summary>
        /// 企业等级
        /// </summary>
        /// <remarks>可以根据具体场景，规定此字段的含义</remarks>
        public string EnterpriseRank
        {
            get { return enterpriseRank; }
            set { enterpriseRank = value; }
        }

        private string createUserKey = String.Empty;
        public string CreateUserKey
        {
            get { return createUserKey; }
            set { createUserKey = value; }
        }

        private DateTime createDate = DateTimeHelper.Min;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion
    }
}
