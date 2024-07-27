using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 数据（被）使用的方式
    /// </summary>
    /// <remarks>即某信息是在前台被终端用户使用，还是在后台被管理员使用</remarks>
    public enum DataUsingModes
    {
        /// <summary>
        /// 信息在前台被终端用户使用
        /// </summary>
        EndUserMode=1,
        /// <summary>
        /// 信息在后台被管理员使用
        /// </summary>
        AdminManagerMode=2,
    }
}
