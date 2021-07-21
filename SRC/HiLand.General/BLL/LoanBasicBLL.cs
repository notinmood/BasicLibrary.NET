using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.DALCommon;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.Finance;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 贷款基本信息业务逻辑类
    /// </summary>
    public class LoanBasicBLL : BaseBLL<LoanBasicBLL, LoanBasicEntity, LoanBasicDAL, ILoanBasicDAL>
    {
        #region 演示方法
        /// <summary>
        /// 此方法仅仅为了演示，使用扩展的IDAL（即ILoanBasicDAL）可以实现除CURD外独特的数据访问功能
        /// </summary>
        /// <returns></returns>
        public int GetCountTest()
        {
            return SaveDAL.GetCountTest();
        }
        #endregion

        /// <summary>
        /// 生成还款日程
        /// </summary>
        /// <param name="loanBasicInfo">贷款基本信息</param>
        /// <param name="paymentStartCalculateDate">计算还款日程的开始时间</param>
        /// <param name="loanTermCount">贷款周期数</param>
        /// <param name="loanTermType">贷款周期类型</param>
        /// <returns></returns>
        public bool GeneralSchedules(LoanBasicEntity loanBasicInfo, DateTime? paymentStartCalculateDate = null, int? loanTermCount = null, PaymentTermTypes? loanTermType = null)
        {
            if (loanBasicInfo != null)
            {
                PaymentTermTypes monthly = PaymentTermTypes.Monthly;
                if (loanTermType.HasValue)
                {
                    monthly = loanTermType.Value;
                }
                double rate = this.GetRate(loanBasicInfo, monthly);
                int paymentCount = 0;
                if (loanTermCount.HasValue && (loanTermCount.Value > 0))
                {
                    paymentCount = loanTermCount.Value;
                }
                else
                {
                    paymentCount = loanBasicInfo.LoanTermCount;
                }
                PaymentTermTypes paymentCircle = PaymentTermTypes.Monthly;
                if (loanTermType.HasValue)
                {
                    paymentCircle = loanTermType.Value;
                }
                double loanAmount = (double)loanBasicInfo.LoanAmount;
                if (!paymentStartCalculateDate.HasValue || (paymentStartCalculateDate.Value == DateTimeHelper.Min))
                {
                    paymentStartCalculateDate = new DateTime?(loanBasicInfo.LoanDate);
                }
                List<Payment> list = CPMLoan.GetPaymentSchedule(rate, loanAmount, paymentCount, paymentCircle, paymentStartCalculateDate.Value);
                BaseBLL<LoanScheduleBLL, LoanScheduleEntity, LoanScheduleDAL, IDAL<LoanScheduleEntity>>.Instance.DeleteList(string.Format(" LoanGuid='{0}' ", loanBasicInfo.LoanGuid));
                for (int i = 0; i < list.Count; i++)
                {
                    Payment payment = list[i];
                    LoanScheduleEntity model = new LoanScheduleEntity
                    {
                        LoanGuid = loanBasicInfo.LoanGuid,
                        Interest = (decimal)payment.Interest,
                        ScheduleNo = i.ToString().PadLeft(3, '0'),
                        ScheduleGuid = Guid.NewGuid(),
                        Principal = (decimal)payment.Principal,
                        PrincipalBalance = (decimal)payment.PrincipalBalance,
                        PaymentDate = payment.PaymentDate,
                        ScheduleTimes = 1,
                        ScheduleParentGuid = Guid.Empty
                    };
                    BaseBLL<LoanScheduleBLL, LoanScheduleEntity, LoanScheduleDAL, IDAL<LoanScheduleEntity>>.Instance.Create(model);
                }
            }
            return true;
        }

        /// <summary>
        /// 获取利率
        /// </summary>
        /// <param name="loanBasicInfo"></param>
        /// <returns></returns>
        private double GetRate(LoanBasicEntity loanBasicInfo, PaymentTermTypes loanTermType)
        {
            double annualRate = 0.48;
            if (loanBasicInfo.LoanInterest > 0)
            {
                annualRate = (double)loanBasicInfo.LoanInterest;
            }

            RateConverter rateConverter = new SimpleRateConverter(annualRate, PaymentTermTypes.Annual);
            double rate = rateConverter.GetRate(loanTermType);

            return rate;
        }

        /// <summary>
        /// 更新读取人信息
        /// </summary>
        /// <param name="readUserID"></param>
        /// <param name="readDate"></param>
        /// <param name="loanGuid"></param>
        public bool UpdataReadInfo(Guid loanGuid, Guid readUserID, DateTime readDate)
        {
            bool isSuccessful = SaveDAL.UpdataReadInfo(loanGuid, readUserID, readDate);
            if (isSuccessful == true)
            {
                CleanUpAllCache();
            }

            return isSuccessful;
        }

        /// <summary>
        /// 更新读取人信息
        /// </summary>
        /// <param name="loanGuid"></param>
        public bool UpdataReadInfo(Guid loanGuid)
        {
            Guid readUserID = Guid.Empty;
            IBusinessUser currentUser = BusinessUserBLL.CurrentUser;
            if (currentUser != null)
            {
                readUserID = currentUser.UserGuid;
            }
            DateTime readDate = DateTimeHelper.RunningLocalNow;// DateTime.Now;
            return UpdataReadInfo(loanGuid, readUserID, readDate);
        }
    }
}