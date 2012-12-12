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
    public class OperateLogCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<OperateLogEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralOperateLog"; }
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
            get { return "usp_General_OperateLog_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(OperateLogEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.LogGuid == Guid.Empty)
            {
                entity.LogGuid = GuidHelper.NewGuid();
            }

            string commandText = string.Format(@"Insert Into [GeneralOperateLog] (
			    [LogGuid],
			    [LogTitle],
			    [LogType],
			    [LogCategory],
			    [LogMessage],
			    [LogOperateName],
			    [LogStatus],
			    [CanUsable],
			    [RelativeKey],
			    [RelativeName],
			    [RelativeOther],
			    [LogUserKey],
			    [LogUserName],
			    [LogUserOther],
			    [LogDate],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    {0}LogGuid,
			    {0}LogTitle,
			    {0}LogType,
			    {0}LogCategory,
			    {0}LogMessage,
			    {0}LogOperateName,
			    {0}LogStatus,
			    {0}CanUsable,
			    {0}RelativeKey,
			    {0}RelativeName,
			    {0}RelativeOther,
			    {0}LogUserKey,
			    {0}LogUserName,
			    {0}LogUserOther,
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
        public override bool Update(OperateLogEntity entity)
        {
            string commandText = string.Format(@"Update [GeneralOperateLog] Set   
				[LogGuid] = {0}LogGuid,
				[LogTitle] = {0}LogTitle,
				[LogType] = {0}LogType,
				[LogCategory] = {0}LogCategory,
				[LogMessage] = {0}LogMessage,
				[LogOperateName] = {0}LogOperateName,
				[LogStatus] = {0}LogStatus,
				[CanUsable] = {0}CanUsable,
				[RelativeKey] = {0}RelativeKey,
				[RelativeName] = {0}RelativeName,
				[RelativeOther] = {0}RelativeOther,
				[LogUserKey] = {0}LogUserKey,
				[LogUserName] = {0}LogUserName,
				[LogUserOther] = {0}LogUserOther,
				[LogDate] = {0}LogDate,
				[PropertyNames] = {0}PropertyNames,
				[PropertyValues] = {0}PropertyValues
        Where [LogID] = {0}LogID", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 内部准备（为实体准备数据访问的参数）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="paraList"></param>
        protected override void InnerPrepareParasAll(OperateLogEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("LogID",entity.LogID),
			    GenerateParameter("LogGuid",entity.LogGuid),
			    GenerateParameter("LogTitle",entity.LogTitle?? String.Empty),
			    GenerateParameter("LogType",entity.LogType),
			    GenerateParameter("LogCategory",entity.LogCategory?? String.Empty),
			    GenerateParameter("LogMessage",entity.LogMessage?? String.Empty),
			    GenerateParameter("LogOperateName",entity.LogOperateName?? String.Empty),
			    GenerateParameter("LogStatus",entity.LogStatus),
			    GenerateParameter("CanUsable",entity.CanUsable),
			    GenerateParameter("RelativeKey",entity.RelativeKey?? String.Empty),
			    GenerateParameter("RelativeName",entity.RelativeName?? String.Empty),
			    GenerateParameter("RelativeOther",entity.RelativeOther?? String.Empty),
			    GenerateParameter("LogUserKey",entity.LogUserKey?? String.Empty),
			    GenerateParameter("LogUserName",entity.LogUserName?? String.Empty),
			    GenerateParameter("LogUserOther",entity.LogUserOther?? String.Empty),
			    GenerateParameter("LogDate",entity.LogDate)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref OperateLogEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogID"))
                {
                    entity.LogID = reader.GetInt32(reader.GetOrdinal("LogID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogGuid"))
                {
                    entity.LogGuid = reader.GetGuid(reader.GetOrdinal("LogGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogTitle"))
                {
                    entity.LogTitle = reader.GetString(reader.GetOrdinal("LogTitle"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogType"))
                {
                    entity.LogType = reader.GetInt32(reader.GetOrdinal("LogType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogCategory"))
                {
                    entity.LogCategory = reader.GetString(reader.GetOrdinal("LogCategory"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogMessage"))
                {
                    entity.LogMessage = reader.GetString(reader.GetOrdinal("LogMessage"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogOperateName"))
                {
                    entity.LogOperateName = reader.GetString(reader.GetOrdinal("LogOperateName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogStatus"))
                {
                    entity.LogStatus = reader.GetInt32(reader.GetOrdinal("LogStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeKey"))
                {
                    entity.RelativeKey = reader.GetString(reader.GetOrdinal("RelativeKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeName"))
                {
                    entity.RelativeName = reader.GetString(reader.GetOrdinal("RelativeName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeOther"))
                {
                    entity.RelativeOther = reader.GetString(reader.GetOrdinal("RelativeOther"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogUserKey"))
                {
                    entity.LogUserKey = reader.GetString(reader.GetOrdinal("LogUserKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogUserName"))
                {
                    entity.LogUserName = reader.GetString(reader.GetOrdinal("LogUserName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogUserOther"))
                {
                    entity.LogUserOther = reader.GetString(reader.GetOrdinal("LogUserOther"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LogDate"))
                {
                    entity.LogDate = reader.GetDateTime(reader.GetOrdinal("LogDate"));
                }
            }
        }
        #endregion
    }
}
