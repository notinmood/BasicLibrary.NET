using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Logging
{
    /// <summary>
    /// 日志实体接口
    /// </summary>
    public interface ILogEntity
    {
        int LogID { get; set; }

        Guid LogGuid { get; set; }

        string LogCategory
        { get; set; }

        Logics LogStatus
        { get; set; }

        string LogLevel
        { get; set; }

        string Logger
        { get; set; }

        string LogMessage
        { get; set; }

        string LogThread
        { get; set; }

        string LogException
        { get; set; }

        DateTime LogDate
        { get; set; }
    }
}
