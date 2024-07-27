using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Entity.Status
{
    /// <summary>
    /// 系统级别的状态接口
    /// </summary>
    public interface ISystemStatusInfo
    {
        /// <summary>
        /// 状态的类型
        /// </summary>
        SystemStatuses SystemStatus { get; set; }

        /// <summary>
        /// 具体要显示的信息
        /// </summary>
        string Message { get; set; }
    }
}
