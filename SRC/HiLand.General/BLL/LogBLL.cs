using System;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.DALCommon;
using HiLand.General.Entity;
using HiLand.Utility.Cache;
using HiLand.Utility.Enums;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 日志业务逻辑类
    /// </summary>
    public class LogBLL : BaseBLL<LogBLL, LogEntity, LogDAL, ILogDAL>
    {
        /// <summary>
        /// 根据日志的名称和日期获取其状态（如果此记录不存在亦返回false）
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logDate"></param>
        /// <returns></returns>
        public Logics GetLogStatus(string logger, DateTime logDate)
        {
            string cacheKey = GeneralCacheKeys<LogEntity>.GetEntityBusinessKey("Logger&LogDate", logger, logDate.ToString());
            return CacheHelper.Access<string, DateTime, Logics>(cacheKey, CacheMintues, SaveDAL.GetLogStatus, logger, logDate);
        }

        /// <summary>
        /// 根据日志的名称和日期获取其状态（如果此记录不存在亦返回false）
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logDate"></param>
        /// <param name="isOnlyExacteDay">在数据库内获取数据的时候是否仅精确到日，忽略后面的时分秒</param>
        /// <returns></returns>
        public Logics GetLogStatus(string logger, DateTime logDate, bool isOnlyExacteDay)
        {
            string cacheKey = GeneralCacheKeys<LogEntity>.GetEntityBusinessKey("Logger&LogDate&isOnlyExacteDay", logger, logDate.ToString(), isOnlyExacteDay.ToString());
            return CacheHelper.Access<string, DateTime,bool, Logics>(cacheKey, CacheMintues, SaveDAL.GetLogStatus, logger, logDate,isOnlyExacteDay);
        }
    }
}
