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
    /// <summary>
    /// 回访、跟踪数据访问类
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public class TrackerCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<TrackerEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralTracker"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "TrackerGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "TrackerGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Tracker_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(TrackerEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.TrackerGuid == Guid.Empty)
            {
                entity.TrackerGuid = GuidHelper.NewGuid();
            }

            string commandText = string.Format(@"Insert Into [GeneralTracker] (
			    [TrackerGuid],
			    [RelativeKey],
			    [CanUsable],
			    [TrackerTitle],
			    [TrackerDesc],
			    [TrackerCategory],
			    [TrackerType],
			    [TrackerTime],
			    [TrackerUserKey],
			    [CreateTime],
			    [CreateUserKey],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    {0}TrackerGuid,
			    {0}RelativeKey,
			    {0}CanUsable,
			    {0}TrackerTitle,
			    {0}TrackerDesc,
			    {0}TrackerCategory,
			    {0}TrackerType,
			    {0}TrackerTime,
			    {0}TrackerUserKey,
			    {0}CreateTime,
			    {0}CreateUserKey,
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
        public override bool Update(TrackerEntity entity)
        {
            string commandText = string.Format(@"Update [GeneralTracker] Set   
				    [TrackerGuid] = {0}TrackerGuid,
				    [RelativeKey] = {0}RelativeKey,
				    [CanUsable] = {0}CanUsable,
				    [TrackerTitle] = {0}TrackerTitle,
				    [TrackerDesc] = {0}TrackerDesc,
				    [TrackerCategory] = {0}TrackerCategory,
				    [TrackerType] = {0}TrackerType,
				    [TrackerTime] = {0}TrackerTime,
				    [TrackerUserKey] = {0}TrackerUserKey,
				    [CreateTime] = {0}CreateTime,
				    [CreateUserKey] = {0}CreateUserKey,
				    [PropertyNames] = {0}PropertyNames,
				    [PropertyValues] = {0}PropertyValues
            Where [TrackerID] = {0}TrackerID", ParameterNamePrefix);

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
        protected override void InnerPrepareParasAll(TrackerEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("TrackerID",entity.TrackerID),
			    GenerateParameter("TrackerGuid",entity.TrackerGuid),
			    GenerateParameter("RelativeKey",entity.RelativeKey?? String.Empty),
			    GenerateParameter("CanUsable",entity.CanUsable),
			    GenerateParameter("TrackerTitle",entity.TrackerTitle?? String.Empty),
			    GenerateParameter("TrackerDesc",entity.TrackerDesc?? String.Empty),
			    GenerateParameter("TrackerCategory",entity.TrackerCategory?? String.Empty),
			    GenerateParameter("TrackerType",entity.TrackerType),
			    GenerateParameter("TrackerTime",entity.TrackerTime),
			    GenerateParameter("TrackerUserKey",entity.TrackerUserKey?? String.Empty),
			    GenerateParameter("CreateTime",entity.CreateTime),
			    GenerateParameter("CreateUserKey",entity.CreateUserKey?? String.Empty)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref TrackerEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerID"))
                {
                    entity.TrackerID = reader.GetInt32(reader.GetOrdinal("TrackerID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerGuid"))
                {
                    entity.TrackerGuid = reader.GetGuid(reader.GetOrdinal("TrackerGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeKey"))
                {
                    entity.RelativeKey = reader.GetString(reader.GetOrdinal("RelativeKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerTitle"))
                {
                    entity.TrackerTitle = reader.GetString(reader.GetOrdinal("TrackerTitle"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerDesc"))
                {
                    entity.TrackerDesc = reader.GetString(reader.GetOrdinal("TrackerDesc"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerCategory"))
                {
                    entity.TrackerCategory = reader.GetString(reader.GetOrdinal("TrackerCategory"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerType"))
                {
                    entity.TrackerType = reader.GetInt32(reader.GetOrdinal("TrackerType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerTime"))
                {
                    entity.TrackerTime = reader.GetDateTime(reader.GetOrdinal("TrackerTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TrackerUserKey"))
                {
                    entity.TrackerUserKey = reader.GetString(reader.GetOrdinal("TrackerUserKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateTime"))
                {
                    entity.CreateTime = reader.GetDateTime(reader.GetOrdinal("CreateTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateUserKey"))
                {
                    entity.CreateUserKey = reader.GetString(reader.GetOrdinal("CreateUserKey"));
                }
            }
        }
        #endregion
    }
}
