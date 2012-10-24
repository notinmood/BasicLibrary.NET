using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;

namespace HiLand.General.Entity
{
    public class LoanScheduleEntity : BaseModel<LoanScheduleEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ScheduleGuid" }; }
        }

        #region 实体信息
        private int scheduleID;
        public int ScheduleID
        {
            get { return scheduleID; }
            set { scheduleID = value; }
        }

        private Guid scheduleGuid = Guid.Empty;
         [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid ScheduleGuid
        {
            get { return scheduleGuid; }
            set { scheduleGuid = value; }
        }

        private string scheduleNo = String.Empty;
        public string ScheduleNo
        {
            get { return scheduleNo; }
            set { scheduleNo = value; }
        }

        private int scheduleTimes = 1;
        /// <summary>
        /// 分期的代数（即当前记录是原始分期的基础上的第几次分期，原始分期的代数为1）
        /// </summary>
        public int ScheduleTimes
        {
            get { return scheduleTimes; }
            set { scheduleTimes = value; }
        }

        private Guid scheduleParentGuid = Guid.Empty;
        /// <summary>
        /// 当前分期的父分期Guid（即当前分期是在哪个分期记录上进行的第N+1次分期）
        /// </summary>
        public Guid ScheduleParentGuid
        {
            get { return scheduleParentGuid; }
            set { scheduleParentGuid = value; }
        }

        private ScheduleStatuses scheduleStatus = ScheduleStatuses.Normal;
        public ScheduleStatuses ScheduleStatus
        {
            get { return scheduleStatus; }
            set { scheduleStatus = value; }
        }


        private Guid loanGuid = Guid.Empty;
        public Guid LoanGuid
        {
            get { return loanGuid; }
            set { loanGuid = value; }
        }

        private decimal principal;
        public decimal Principal
        {
            get { return principal; }
            set { principal = value; }
        }

        private decimal interest;
        public decimal Interest
        {
            get { return interest; }
            set { interest = value; }
        }

        private decimal penalty;
        public decimal Penalty
        {
            get { return penalty; }
            set { penalty = value; }
        }

        private decimal lateCharge;
        public decimal LateCharge
        {
            get { return lateCharge; }
            set { lateCharge = value; }
        }

        private decimal otherFee;
        public decimal OtherFee
        {
            get { return otherFee; }
            set { otherFee = value; }
        }

        public decimal PureAmount
        {
            get { return this.principal + this.interest; }
        }

        public decimal Amount
        {
            get { return this.principal + this.interest + this.penalty + this.lateCharge + this.otherFee; }
        }

        private decimal principalPaid;
        public decimal PrincipalPaid
        {
            get { return principalPaid; }
            set { principalPaid = value; }
        }

        private decimal interestPaid;
        public decimal InterestPaid
        {
            get { return interestPaid; }
            set { interestPaid = value; }
        }

        private decimal penaltyPaid;
        public decimal PenaltyPaid
        {
            get { return penaltyPaid; }
            set { penaltyPaid = value; }
        }

        private decimal lateChargePaid;
        public decimal LateChargePaid
        {
            get { return lateChargePaid; }
            set { lateChargePaid = value; }
        }

        private decimal otherFeePaid;
        public decimal OtherFeePaid
        {
            get { return otherFeePaid; }
            set { otherFeePaid = value; }
        }

        public decimal AmountPaid
        {
            get { return this.principalPaid + this.interestPaid + this.penaltyPaid + this.lateChargePaid + this.otherFeePaid; }
        }

        private decimal principalBalance;
        /// <summary>
        /// 本次还款后，剩余的的本金
        /// </summary>
        public decimal PrincipalBalance
        {
            get { return principalBalance; }
            set { principalBalance = value; }
        }

        private decimal allBalance;
        /// <summary>
        /// 本次还款后剩余的总的费用（累计前面的各期未付费用）
        /// </summary>
        public decimal AllBalance
        {
            get { return this.allBalance; }
            set { this.allBalance = value; }
        }

        private decimal totalBalance;
        //TODO:这个应该是通过计算得到，不应该在数据库中保存
        /// <summary>
        /// 本次还款后剩余的总的费用
        /// </summary>
        /// <remarks>不推荐使用，其是在数据库内记录的值（如果尚未还款的总费用允许的过程中通过计算获取，请使用AllBalance属性）</remarks>
        public decimal TotalBalance
        {
            get { return totalBalance; }
            set { totalBalance = value; }
        }

        /// <summary>
        /// 当期的剩余的应付费用
        /// </summary>
        public decimal CurrentBalance
        {
            get
            {
                return Amount - AmountPaid;
            }
        }

        private DateTime paymentDate = DateTimeHelper.Min;
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }

        private DateTime paidDate = DateTimeHelper.Min;
        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }
        #endregion
    }
}
