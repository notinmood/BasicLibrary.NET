using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;

namespace HiLand.General.DALCommon
{
    /// <summary>
    /// 银行账户接口
    /// </summary>
    public interface IBankDAL : IDAL<BankEntity>
    {
        /// <summary>
        /// 移除某用户银行账户的Primary状态
        /// </summary>
        /// <param name="userGuid">人员Guid</param>
        /// <param name="bankGuidExclude">取消当前状态时，需要排除在外的银行Guid</param>
        /// <returns></returns>
        void RemovePrimaryStatusOfUser(Guid userGuid, Guid bankGuidExclude);
    }
}
