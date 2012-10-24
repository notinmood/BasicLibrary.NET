using System;
using System.Collections.Generic;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Utility.Finance;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 还款日程逻辑类
    /// </summary>
    public class LoanScheduleBLL : BaseBLL<LoanScheduleBLL, LoanScheduleEntity, LoanScheduleDAL>
    {
        /// <summary>
        /// 对某一期剩余费用继续分期处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="loanStartDate">贷款开始计算日期</param>
        /// <param name="balanceToReinstall">需要分期的费用，如果未传入该值，将会对Schedule实体中的费用进行分期</param>
        /// <returns></returns>
        public bool ReInstall(LoanScheduleEntity entity, int loanTermCount, PaymentTermTypes paymentTermType, decimal balanceToReinstall = 0, DateTime? loanStartDate = null)
        {
            bool result = true;
            //1.继续分期
            double remainBalance = 0;
            if (balanceToReinstall == 0)
            {
                remainBalance = (double)(entity.Amount - entity.AmountPaid);
            }
            else
            {
                remainBalance = (double)balanceToReinstall;
            }

            DateTime loanStartDateTemp = entity.PaymentDate;
            if (loanStartDate.HasValue)
            {
                loanStartDateTemp = loanStartDate.Value;
            }

            List<Payment> paymentList = CPMLoan.GetPaymentSchedule(0, remainBalance, loanTermCount, paymentTermType, loanStartDateTemp);
            for (int i = 0; i < paymentList.Count; i++)
            {
                Payment payment = paymentList[i];
                LoanScheduleEntity scheduleEntity = new LoanScheduleEntity();

                scheduleEntity.LoanGuid = entity.LoanGuid;
                scheduleEntity.Interest = (decimal)payment.Interest;
                scheduleEntity.ScheduleNo =entity.ScheduleNo+  i.ToString().PadLeft(3, '0');
                scheduleEntity.ScheduleGuid = Guid.NewGuid();
                scheduleEntity.Principal = (decimal)payment.Principal;
                scheduleEntity.PaymentDate = payment.PaymentDate;
                scheduleEntity.ScheduleTimes = entity.ScheduleTimes + 1;
                scheduleEntity.ScheduleParentGuid = entity.ScheduleGuid;
                scheduleEntity.ScheduleStatus = ScheduleStatuses.Normal;

                Create(scheduleEntity);
            }


            //2.设置当前期不可用
            entity.ScheduleStatus = ScheduleStatuses.ReInstalled;
            Update(entity);
            return result;
        }
    }
}