using System;
using System.Collections.Generic;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 企业实体
    /// </summary>
    public class EnterpriseEntity : BaseModel<EnterpriseEntity>, IResource
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

        private DateTime establishedTime = DateTimeHelper.Min;
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

        private string areaOther = String.Empty;
        /// <summary>
        /// 地区的其他信息
        /// </summary>
        public string AreaOther
        {
            get { return areaOther; }
            set { areaOther = value; }
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

        public Guid ResourceGuid
        {
            get { return this.EnterpriseGuid; }
        }

        public string ResourceName
        {
            get { return this.CompanyName; }
        }

        private string createUserKey = String.Empty;
        /// <summary>
        /// 资源创建人Key
        /// </summary>
        public string CreateUserKey
        {
            get { return createUserKey; }
            set { createUserKey = value; }
        }

        private string createUserName = String.Empty;
        /// <summary>
        /// 资源创建人名称
        /// </summary>
        public string CreateUserName
        {
            get { return createUserName; }
            set { createUserName = value; }
        }

        private DateTime createDate = DateTimeHelper.Min;
        /// <summary>
        /// 资源创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private string lastUpdateUserKey = String.Empty;
        /// <summary>
        /// 资源最后更新人Key
        /// </summary>
        public string LastUpdateUserKey
        {
            get { return lastUpdateUserKey; }
            set { lastUpdateUserKey = value; }
        }

        private string lastUpdateUserName = String.Empty;
        /// <summary>
        /// 资源最后更新人名称
        /// </summary>
        public string LastUpdateUserName
        {
            get { return lastUpdateUserName; }
            set { lastUpdateUserName = value; }
        }

        private DateTime lastUpdateDate = DateTimeHelper.Min;
        /// <summary>
        /// 资源最后更新时间
        /// </summary>
        public DateTime LastUpdateDate
        {
            get { return lastUpdateDate; }
            set { lastUpdateDate = value; }
        }

        private Logics isProtectedByOwner = Logics.True;
        /// <summary>
        /// 当前资源是否被保护（被保护的数据，仅能所有者修改，其他人仅能查看）
        /// </summary>
        public Logics IsProtectedByOwner
        {
            get { return this.isProtectedByOwner; }
            set { this.isProtectedByOwner = value; }
        }

        private int cooperateStatus = 0;
        /// <summary>
        /// 当前企业是否已合作
        /// </summary>
        public int CooperateStatus
        {
            get { return this.cooperateStatus; }
            set { this.cooperateStatus = value; }
        }

        private List<string> ownerKeys = new List<string>();
        public List<string> OwnerKeys
        {
            get
            {
                if (ownerKeys.Count == 0)
                {
                    ownerKeys.Add(CreateUserKey);
                    ownerKeys.Add(LastUpdateUserKey);
                }

                return ownerKeys;
            }
        }

        //HACK:xieran20121126 此处只是为了实现接口，不要直接使用IsOwning这个属性
        /// <summary>
        /// 当前用户是否拥有资源的控制权(此处仅是实现接口IResource,不要直接使用.)
        /// </summary>
        public bool IsOwning
        {
            get { return true; }
        }
        #endregion
    }
}
