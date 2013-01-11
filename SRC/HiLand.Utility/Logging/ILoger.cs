using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Logging
{
    /// <summary>
    /// 日志记录器接口
    /// </summary>
    public interface ILoger
    {
        void Log(ILogEntity log);
        void Log(string log);
    }
}
