using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Logging
{
    /// <summary>
    /// 日志记录器接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="data"></param>
        void Log(ILogEntity data);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="data"></param>
        void Log(string data);
    }
}
