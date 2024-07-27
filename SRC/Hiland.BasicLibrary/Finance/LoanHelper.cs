using System;
using System.Collections.Generic;
using Hiland.BasicLibrary.Event;
using Microsoft.VisualBasic;

namespace Hiland.BasicLibrary.Finance
{
    public static class LoanHelper
    {
        /// <summary>
        /// 根据当前还款时间和还款周期，计算下一个还款时间
        /// </summary>
        /// <param name="thisPaymentDate">当前还款时间</param>
        /// <param name="paymentCircle">还款周期</param>
        /// <returns>一个还款时间</returns>
        public static DateTime GetNextPaymentDate(DateTime thisPaymentDate, PaymentTermTypes paymentCircle)
        {
            DateTime nextPaymentDate = DateTime.MinValue;
            switch (paymentCircle)
            {
                case PaymentTermTypes.Daily:
                    nextPaymentDate = thisPaymentDate.AddDays(1);
                    break;
                case PaymentTermTypes.Weekly:
                    nextPaymentDate = thisPaymentDate.AddDays(7);
                    break;
                case PaymentTermTypes.DoubleWeekly:
                    nextPaymentDate = thisPaymentDate.AddDays(14);
                    break;
                case PaymentTermTypes.DoubleMonthly:
                    nextPaymentDate = thisPaymentDate.AddMonths(2);
                    break;
                case PaymentTermTypes.Quarterly:
                    nextPaymentDate = thisPaymentDate.AddMonths(3);
                    break;
                case PaymentTermTypes.SemiYearly:
                    nextPaymentDate = thisPaymentDate.AddMonths(6);
                    break;
                case PaymentTermTypes.Annual:
                    nextPaymentDate = thisPaymentDate.AddYears(1);
                    break;
                case PaymentTermTypes.Monthly:
                default:
                    nextPaymentDate = thisPaymentDate.AddMonths(1);
                    break;
            }

            //贷款还款日期的时间精度仅仅控制到日，不对时分秒控制
            return nextPaymentDate.Date;
        }

        /// <summary>
        /// 获取还款日程信息
        /// </summary>
        /// <param name="paymentNumber">指定要计算的还款档期序号</param>
        /// <param name="rate">利率</param>
        /// <param name="paymentCount">还款总期数</param>
        /// <param name="totalAmount">贷款总费用</param>
        /// <param name="dueDate">还款在每期的开始还是结尾时间，缺省为每期的结尾时间（大部分商业都是用结尾时间）</param>
        /// <param name="getPaymentFunc">生成每期贷款的方法</param>
        /// <returns>每期还款的额度</returns>
        /// <remarks>参数rate和参数paymentCount是呼应的。即如果paymentCount按照月计数，那么rate就必须指定为月利率；
        ///    如果paymentCount按照年计数，那么rate就必须指定为年利率。</remarks>
        public static List<Payment> GetPaymentSchedule(double rate, double totalAmount, int paymentCount, PaymentTermTypes paymentCircle, DateTime loanStartDate, DueDate dueDate, Funcs<double, double, int, int, DueDate, Payment> getPaymentFunc)
        {
            //贷款还款日期的时间精度仅仅控制到日，不对时分秒控制
            loanStartDate = loanStartDate.Date;

            List<Payment> paymentSchedule = new List<Payment>();
            double totalNeedPayPrinclepal = 0;
            DateTime lastPaymentDate = DateTime.MinValue;
            for (int i = 0; i < paymentCount; i++)
            {
                int paymentNumber = i + 1;
                Payment payment = getPaymentFunc(rate, totalAmount, paymentCount, paymentNumber, dueDate);
                totalNeedPayPrinclepal += payment.Principal;
                payment.TotalPrincipal = totalNeedPayPrinclepal;
                payment.PrincipalBalance = totalAmount - totalNeedPayPrinclepal;
                if (lastPaymentDate == DateTime.MinValue)
                {
                    if (dueDate == DueDate.BegOfPeriod)
                    {
                        lastPaymentDate = loanStartDate;
                    }
                    else
                    {
                        lastPaymentDate = LoanHelper.GetNextPaymentDate(loanStartDate, paymentCircle);
                    }
                }
                else
                {
                    lastPaymentDate = LoanHelper.GetNextPaymentDate(lastPaymentDate, paymentCircle);
                }

                payment.PaymentDate = lastPaymentDate;

                paymentSchedule.Add(payment);
            }

            return paymentSchedule;
        }
    }
}
