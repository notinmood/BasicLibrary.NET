using System;

namespace HiLand.Utility.Finance
{
    /// <summary>
    /// 还款日程中，某一次还款的信息
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// 还款的序号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 本次还款包含的本金
        /// </summary>
        public double Principal { get; set; }

        /// <summary>
        /// 本次还款包含的利息
        /// </summary>
        public double Interest { get; set; }

        /// <summary>
        /// 罚款
        /// </summary>
        public double Penalty { get; set; }

        /// <summary>
        /// 逾期费
        /// </summary>
        public double LateCharge { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        public double OtherFee { get; set; }

        /// <summary>
        /// 正常情况下本次还款的总额度（仅包括本金和利息）
        /// </summary>
        public virtual double PureAmount
        {
            get { return Principal + Interest; }
        }

        /// <summary>
        /// 本次还款总费用
        /// </summary>
        public virtual double Amount 
        {
            get { return Principal + Interest+ Penalty+ LateCharge+ OtherFee; }
        }

        /// <summary>
        /// 本次还款后，剩余的的本金(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double PrincipalBalance { get; set; }

        private DateTime paymentDate = DateTime.MinValue;
        /// <summary>
        /// 本次还款的日期(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public DateTime PaymentDate 
        {
            get { return paymentDate;}
            set { paymentDate = value; ;}
        }


        /// <summary>
        /// 本次还款后，总的已还款总费用(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// 本次还款后，总的已还款本金(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double TotalPrincipal { get; set; }

        /// <summary>
        /// 本次还款后，总的已还款利息(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double TotalInterest { get; set; }

        /// <summary>
        /// 本次还款后，总的已还款罚款(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double TotalPenalty { get; set; }

        /// <summary>
        /// 本次还款后，总的已还款逾期费用(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double TotalLateCharge { get; set; }

        /// <summary>
        /// 本次还款后，总的已还款其他费用(在还款日程的某个还款信息中使用；在计算某个单期还款信息的时候，此数据无效)
        /// </summary>
        public double TotalOtherFee { get; set; }
    }
}
