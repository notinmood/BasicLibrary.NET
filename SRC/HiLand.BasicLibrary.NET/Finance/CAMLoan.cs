using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace HiLand.Utility.Finance
{
    /// <summary>
    /// Constant Amoritization Mortgage 等额本金还款（固定摊薄（本金）的抵押贷款）
    /// </summary>
    public class CAMLoan
    {
        /// <summary>
        ///获取某个指定还款档期的还款信息
        /// </summary>
        /// <param name="paymentNumber">指定要计算的还款档期序号</param>
        /// <param name="rate">利率</param>
        /// <param name="paymentCount">还款总期数</param>
        /// <param name="totalAmount">贷款总费用</param>
        /// <param name="dueDate">还款在每期的开始还是结尾时间，缺省为每期的结尾时间（大部分商业都是用结尾时间）</param>
        /// <returns>每期还款的额度</returns>
        /// <remarks>参数rate和参数paymentCount是呼应的。即如果paymentCount按照月计数，那么rate就必须指定为月利率；
        ///    如果paymentCount按照年计数，那么rate就必须指定为年利率。</remarks>
        public static Payment GetPayment(double rate, double totalAmount, int paymentCount, int paymentNumber, DueDate dueDate = DueDate.EndOfPeriod)
        {
            double principal = totalAmount / paymentCount;
            double remainAmountPercent = 1 - ((double)paymentNumber - 1) / (double)paymentCount;
            double interest = totalAmount * remainAmountPercent * rate;

            Payment payment = new Payment();
            payment.Number = paymentNumber;
            payment.Interest = interest;
            payment.Principal = principal;

            return payment;
        }

        /// <summary>
        /// 获取还款日程信息
        /// </summary>
        /// <param name="paymentNumber">指定要计算的还款档期序号</param>
        /// <param name="rate">利率</param>
        /// <param name="paymentCount">还款总期数</param>
        /// <param name="totalAmount">贷款总费用</param>
        /// <returns>每期还款的额度</returns>
        /// <remarks>参数rate和参数paymentCount是呼应的。即如果paymentCount按照月计数，那么rate就必须指定为月利率；
        ///    如果paymentCount按照年计数，那么rate就必须指定为年利率。</remarks>
        public static List<Payment> GetPaymentSchedule(double rate, double totalAmount, int paymentCount, PaymentTermTypes paymentCircle, DateTime loanStartDate)
        {
            return GetPaymentSchedule(rate, totalAmount, paymentCount, paymentCircle, loanStartDate, DueDate.EndOfPeriod);
        }

        /// <summary>
        /// 获取还款日程信息
        /// </summary>
        /// <param name="paymentNumber">指定要计算的还款档期序号</param>
        /// <param name="rate">利率</param>
        /// <param name="paymentCount">还款总期数</param>
        /// <param name="totalAmount">贷款总费用</param>
        /// <param name="dueDate">还款在每期的开始还是结尾时间，缺省为每期的结尾时间（大部分商业都是用结尾时间）</param>
        /// <returns>每期还款的额度</returns>
        /// <remarks>参数rate和参数paymentCount是呼应的。即如果paymentCount按照月计数，那么rate就必须指定为月利率；
        ///    如果paymentCount按照年计数，那么rate就必须指定为年利率。</remarks>
        public static List<Payment> GetPaymentSchedule(double rate, double totalAmount, int paymentCount, PaymentTermTypes paymentCircle, DateTime loanStartDate, DueDate dueDate = DueDate.EndOfPeriod)
        {
            return LoanHelper.GetPaymentSchedule(rate, totalAmount, paymentCount, paymentCircle, loanStartDate, dueDate, GetPayment);
        }
    }
}
