using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.Utility.Enums;

namespace HiLand.General.DALCommon
{
    public interface ILogDAL : IDAL<LogEntity>
    {
        /// <summary>
        /// 根据日志的名称和日期获取其状态（如果此记录不存在亦返回false）
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logDate"></param>
        /// <param name="isOnlyExacteDay">在数据库内获取数据的时候是否仅精确到日，忽略后面的时分秒</param>
        /// <returns></returns>
        Logics GetLogStatus(string logger, DateTime logDate, bool isOnlyExacteDay);

        /// <summary>
        /// 根据日志的名称和日期获取其状态（如果此记录不存在亦返回false）
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logDate"></param>
        /// <returns></returns>
        Logics GetLogStatus(string logger, DateTime logDate);
    }
}
