using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.General.Enums
{
    /// <summary>
    /// 贷款人类别
    /// </summary>
    public enum LoanOwnerTypes
    {
        /// <summary>
        /// 个人（其基本信息需要关联CoreUser）
        /// </summary>
        Person=1,
        
        /// <summary>
        /// 企业（其基本信息需要关联GeneralEnterprise）
        /// </summary>
        Enterprise=10,
    }
}
