using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Finance;

namespace HiLand.General.Entity
{
    public class LoanBasicEntity : BaseModel<LoanBasicEntity>
    {
        #region 实体信息
        private int loanID;
        public int LoanID
        {
            get { return loanID; }
            set { loanID = value; }
        }

        private Guid loanGuid = Guid.Empty;
        [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid LoanGuid
        {
            get { return loanGuid; }
            set { loanGuid = value; }
        }

        private LoanTypes loanType;
        public LoanTypes LoanType
        {
            get { return loanType; }
            set { loanType = value; }
        }

        private decimal loanAmount;
        public decimal LoanAmount
        {
            get { return loanAmount; }
            set { loanAmount = value; }
        }

        private PaymentTermTypes loanTermType;
        public PaymentTermTypes LoanTermType
        {
            get { return loanTermType; }
            set { loanTermType = value; }
        }

        private decimal loanInterest;
        public decimal LoanInterest
        {
            get { return loanInterest; }
            set { loanInterest = value; }
        }

        private int loanTermCount;
        public int LoanTermCount
        {
            get { return loanTermCount; }
            set { loanTermCount = value; }
        }

        private string loanPurpose = String.Empty;
        public string LoanPurpose
        {
            get { return loanPurpose; }
            set { loanPurpose = value; }
        }

        private Guid loanUserID = Guid.Empty;
        public Guid LoanUserID
        {
            get { return loanUserID; }
            set { loanUserID = value; }
        }

        private DateTime loanDate = DateTimeHelper.Min;
        public DateTime LoanDate
        {
            get { return loanDate; }
            set { loanDate = value; }
        }

        private LoanStatuses loanStatus;
        public LoanStatuses LoanStatus
        {
            get { return loanStatus; }
            set { loanStatus = value; }
        }

        private Guid checkUserID = Guid.Empty;
        public Guid CheckUserID
        {
            get { return checkUserID; }
            set { checkUserID = value; }
        }

        private DateTime checkDate = DateTimeHelper.Min;
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        private Guid readUserID = Guid.Empty;
        public Guid ReadUserID
        {
            get { return readUserID; }
            set { readUserID = value; }
        }

        private DateTime readDate = DateTimeHelper.Min;
        public DateTime ReadDate
        {
            get { return readDate; }
            set { readDate = value; }
        }

        #region 扩展属性
        private string loanUserRealName = string.Empty;
        public string LoanUserRealName
        {
            get { return loanUserRealName; }
            set { loanUserRealName = value; }
        }

        private Guid loanUserRealID = Guid.Empty;
        public Guid LoanUserRealID
        {
            get { return loanUserRealID; }
            set { loanUserRealID = value; }
        }
        #endregion

        #endregion
    }
}