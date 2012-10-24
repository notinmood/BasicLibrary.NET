using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Core
{
    /// <summary>
    /// 当前运行的系统是Web模式还是Windows模式
    /// </summary>
    public enum ApplicationModes
    {
        /// <summary>
        /// 本地应用
        /// </summary>
        NativeApp=0,

        /// <summary>
        /// Web应用
        /// </summary>
        WebApp=1,
    }
}
