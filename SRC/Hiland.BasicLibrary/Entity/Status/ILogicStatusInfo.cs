using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Entity
{
    /// <summary>
    /// 状态的表述接口
    /// </summary>
    public interface ILogicStatusInfo
    {
        /// <summary>
        /// 成功还是失败的状态
        /// </summary>
        bool IsSuccessful { get; set; }

        /// <summary>
        /// 成功还是失败的信息表述
        /// </summary>
        string Message { get; set; }
    }
}
