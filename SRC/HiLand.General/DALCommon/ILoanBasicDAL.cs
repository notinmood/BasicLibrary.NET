using System;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;

namespace HiLand.General.DALCommon
{
    public interface ILoanBasicDAL : IDAL<LoanBasicEntity>
    {
        /// <summary>
        /// 更新读取人信息
        /// </summary>
        /// <param name="readUserID"></param>
        /// <param name="readDate"></param>
        /// <param name="loanGuid"></param>
        bool UpdataReadInfo(Guid loanGuid, Guid readUserID, DateTime readDate);

        #region 演示方法
        /// <summary>
        /// 此方法仅仅为了演示，使用扩展的IDAL（即ILoanBasicDAL）可以实现除CURD外独特的数据访问功能
        /// </summary>
        /// <returns></returns>
        int GetCountTest();
        #endregion
    }
}
