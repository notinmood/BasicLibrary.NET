using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
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

        private string industry = String.Empty;
        public string Industry
        {
            get { return industry; }
            set { industry = value; }
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

        //private string propertyNames = String.Empty;
        //public string PropertyNames
        //{
        //    get { return propertyNames; }
        //    set { propertyNames = value; }
        //}

        //private string propertyValues = String.Empty;
        //public string PropertyValues
        //{
        //    get { return propertyValues; }
        //    set { propertyValues = value; }
        //}
        #endregion
    }
}
