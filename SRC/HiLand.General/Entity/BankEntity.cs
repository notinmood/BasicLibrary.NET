using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class BankEntity : BaseModel<BankEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "BankGuid" }; }
        }

        #region 实体信息
        private int bankID;
        public int BankID
        {
            get { return bankID; }
            set { bankID = value; }
        }

        private Guid bankGuid = Guid.Empty;
        [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid BankGuid
        {
            get { return bankGuid; }
            set { bankGuid = value; }
        }

        private Guid userGuid = Guid.Empty;
        public Guid UserGuid
        {
            get { return userGuid; }
            set { userGuid = value; }
        }

        private int bankNo;
        public int BankNo
        {
            get { return bankNo; }
            set { bankNo = value; }
        }

        private Logics isPrimary = Logics.True;
        public Logics IsPrimary
        {
            get { return isPrimary; }
            set { isPrimary = value; }
        }

        private Logics canUsable = Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private string bankName = String.Empty;
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        private string branch = String.Empty;
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        private string bankCode = String.Empty;
        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        private string accountName = String.Empty;
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        private string accountNumber = String.Empty;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        private int accountStatus;
        public int AccountStatus
        {
            get { return accountStatus; }
            set { accountStatus = value; }
        }

        private string bankAddress = String.Empty;
        public string BankAddress
        {
            get { return bankAddress; }
            set { bankAddress = value; }
        }

        private string areaCode = String.Empty;
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
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
        #endregion
    }
}
