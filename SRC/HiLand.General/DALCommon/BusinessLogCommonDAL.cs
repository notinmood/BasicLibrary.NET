using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;

namespace HiLand.General.DALCommon
{
    public class BusinessLogCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<BusinessLogEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>, IBusinessLogDAL
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        #region 基本信息
        /// <summary>
        /// 实体对应主表的名称
        /// </summary>
        protected override string TableName
        {
            get { return "GeneralBusinessLog"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "LogGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "LogGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_BusinessLog_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(BusinessLogEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.LogGuid == Guid.Empty)
            {
                entity.LogGuid = GuidHelper.NewGuid();
            }

            string commandText = string.Format(@"Insert Into [GeneralBusinessLog] (
			    [LogGuid],
			    [LogCategory],
			    [LogStatus],
			    [LogLevel],
			    [Logger],
			    [LogMessage],
			    [LogThread],
			    [LogException],
			    [LogDate],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    {0}LogGuid,
			    {0}LogCategory,
			    {0}LogStatus,
			    {0}LogLevel,
			    {0}Logger,
			    {0}LogMessage,
			    {0}LogThread,
			    {0}LogException,
			    {0}LogDate,
			    {0}PropertyNames,
			    {0}PropertyValues
            )", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Update(BusinessLogEntity entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 内部准备（为实体准备数据访问的参数）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="paraList"></param>
        protected override void InnerPrepareParasAll(BusinessLogEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>() 
            {
                GenerateParameter("LogID",entity.LogID),
			    GenerateParameter("LogGuid",entity.LogGuid),
			    GenerateParameter("LogCategory",entity.LogCategory?? String.Empty),
			    GenerateParameter("LogStatus",entity.LogStatus),
			    GenerateParameter("LogLevel",entity.LogLevel?? String.Empty),
			    GenerateParameter("Logger",entity.Logger?? String.Empty),
			    GenerateParameter("LogMessage",entity.LogMessage?? String.Empty),
			    GenerateParameter("LogThread",entity.LogThread?? String.Empty),
			    GenerateParameter("LogException",entity.LogException?? String.Empty),
			    GenerateParameter("LogDate",entity.LogDate)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override BusinessLogEntity Load(IDataReader reader)
        {
            BusinessLogEntity entity = new BusinessLogEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogID"))
                {
                    entity.LogID = reader.GetInt32(reader.GetOrdinal("LogID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogGuid"))
                {
                    entity.LogGuid = reader.GetGuid(reader.GetOrdinal("LogGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogCategory"))
                {
                    entity.LogCategory = reader.GetString(reader.GetOrdinal("LogCategory"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogStatus"))
                {
                    entity.LogStatus = (Logics)reader.GetInt32(reader.GetOrdinal("LogStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogLevel"))
                {
                    entity.LogLevel = reader.GetString(reader.GetOrdinal("LogLevel"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Logger"))
                {
                    entity.Logger = reader.GetString(reader.GetOrdinal("Logger"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogMessage"))
                {
                    entity.LogMessage = reader.GetString(reader.GetOrdinal("LogMessage"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogThread"))
                {
                    entity.LogThread = reader.GetString(reader.GetOrdinal("LogThread"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogException"))
                {
                    entity.LogException = reader.GetString(reader.GetOrdinal("LogException"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogDate"))
                {
                    entity.LogDate = reader.GetDateTime(reader.GetOrdinal("LogDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PropertyNames"))
                {
                    entity.PropertyNames = reader.GetString(reader.GetOrdinal("PropertyNames"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PropertyValues"))
                {
                    entity.PropertyValues = reader.GetString(reader.GetOrdinal("PropertyValues"));
                }
            }
            return entity;
        }
        #endregion
        /// <summary>
        /// 根据日志的名称和日期获取其状态（如果此记录不存在亦返回false）
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logDate"></param>
        /// <returns></returns>
        public Logics GetLogStatus(string logger, DateTime logDate)
        {
            return GetLogStatus(logger, logDate, true);
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
            string sqlClause = string.Format("SELECT [LogStatus]  FROM [GeneralBusinessLog]  WHERE [Logger]= '{0}' AND ", logger);
            if (isOnlyExacteDay == true)
            {
                DateTime todayWithZeroHour = logDate.Date;
                sqlClause += string.Format(" [LogDate]>= '{0}' AND [LogDate]<='{1}' ", logDate.Date, logDate.Date.AddDays(1));
            }
            else
            {
                sqlClause += string.Format(" [LogDate]='{0}'", logDate);
            }

            object objResult = HelperExInstance.ExecuteScalar(sqlClause);
            int intResult = Converter.ChangeType(objResult, 0);
            return (Logics)intResult;
        }
    }
}
