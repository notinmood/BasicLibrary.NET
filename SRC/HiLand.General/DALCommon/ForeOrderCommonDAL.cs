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
    /// （产品等）预定数据实体数据访问类
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public class ForeOrderCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<ForeOrderEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return new string[] { "ForeOrderGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ForeOrderGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_ForeOrderGuid_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(ForeOrderEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ForeOrderGuid == Guid.Empty)
            {
                entity.ForeOrderGuid = GuidHelper.NewGuid();
            }

            string commandText = string.Format(@"Insert Into [GeneralForeOrder] (
			    [ForeOrderGuid],
			    [ForeOrderType],
			    [ForeOrderCategory],
			    [ForeOrderStatus],
			    [ForeOrderDate],
			    [ForeOrderTitle],
			    [ForeOrderDesc],
			    [OwnerKey],
			    [OwnerName],
			    [OwnerOtherInfo],
			    [RelativeKey],
			    [RelativeName],
			    [RelativeNameOther],
			    [ForeOrderAmount],
			    [ForeOrderUnitName],
			    [ForeOrderUnitFee],
			    [ForeOrderPaid],
			    [ForeOrderMemo1],
			    [ForeOrderMemo2],
			    [CanUsable],
			    [CreateTime],
			    [CreateUserKey],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    {0}ForeOrderGuid,
			    {0}ForeOrderType,
			    {0}ForeOrderCategory,
			    {0}ForeOrderStatus,
			    {0}ForeOrderDate,
			    {0}ForeOrderTitle,
			    {0}ForeOrderDesc,
			    {0}OwnerKey,
			    {0}OwnerName,
			    {0}OwnerOtherInfo,
			    {0}RelativeKey,
			    {0}RelativeName,
			    {0}RelativeNameOther,
			    {0}ForeOrderAmount,
			    {0}ForeOrderUnitName,
			    {0}ForeOrderUnitFee,
			    {0}ForeOrderPaid,
			    {0}ForeOrderMemo1,
			    {0}ForeOrderMemo2,
			    {0}CanUsable,
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
        public override bool Update(ForeOrderEntity entity)
        {
            string commandText = string.Format(@"Update [GeneralForeOrder] Set   
				[ForeOrderGuid] = {0}ForeOrderGuid,
				[ForeOrderType] = {0}ForeOrderType,
				[ForeOrderCategory] = {0}ForeOrderCategory,
				[ForeOrderStatus] = {0}ForeOrderStatus,
				[ForeOrderDate] = {0}ForeOrderDate,
				[ForeOrderTitle] = {0}ForeOrderTitle,
				[ForeOrderDesc] = {0}ForeOrderDesc,
				[OwnerKey] = {0}OwnerKey,
				[OwnerName] = {0}OwnerName,
				[OwnerOtherInfo] = {0}OwnerOtherInfo,
				[RelativeKey] = {0}RelativeKey,
				[RelativeName] = {0}RelativeName,
				[RelativeNameOther] = {0}RelativeNameOther,
				[ForeOrderAmount] = {0}ForeOrderAmount,
				[ForeOrderUnitName] = {0}ForeOrderUnitName,
				[ForeOrderUnitFee] = {0}ForeOrderUnitFee,
				[ForeOrderPaid] = {0}ForeOrderPaid,
				[ForeOrderMemo1] = {0}ForeOrderMemo1,
				[ForeOrderMemo2] = {0}ForeOrderMemo2,
				[CanUsable] = {0}CanUsable,
				[CreateTime] = {0}CreateTime,
				[CreateUserKey] = {0}CreateUserKey,
				[PropertyNames] = {0}PropertyNames,
				[PropertyValues] = {0}PropertyValues
        Where [ForeOrderID] = {0}ForeOrderID", ParameterNamePrefix);

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
        protected override void InnerPrepareParasAll(ForeOrderEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("ForeOrderID",entity.ForeOrderID),
			    GenerateParameter("ForeOrderGuid",entity.ForeOrderGuid),
			    GenerateParameter("ForeOrderType",entity.ForeOrderType),
			    GenerateParameter("ForeOrderCategory",entity.ForeOrderCategory?? String.Empty),
			    GenerateParameter("ForeOrderStatus",entity.ForeOrderStatus),
			    GenerateParameter("ForeOrderDate",entity.ForeOrderDate),
			    GenerateParameter("ForeOrderTitle",entity.ForeOrderTitle?? String.Empty),
			    GenerateParameter("ForeOrderDesc",entity.ForeOrderDesc?? String.Empty),
			    GenerateParameter("OwnerKey",entity.OwnerKey?? String.Empty),
			    GenerateParameter("OwnerName",entity.OwnerName?? String.Empty),
			    GenerateParameter("OwnerOtherInfo",entity.OwnerOtherInfo?? String.Empty),
			    GenerateParameter("RelativeKey",entity.RelativeKey?? String.Empty),
			    GenerateParameter("RelativeName",entity.RelativeName?? String.Empty),
			    GenerateParameter("RelativeNameOther",entity.RelativeNameOther?? String.Empty),
			    GenerateParameter("ForeOrderAmount",entity.ForeOrderAmount),
			    GenerateParameter("ForeOrderUnitName",entity.ForeOrderUnitName?? String.Empty),
			    GenerateParameter("ForeOrderUnitFee",entity.ForeOrderUnitFee),
			    GenerateParameter("ForeOrderPaid",entity.ForeOrderPaid),
			    GenerateParameter("ForeOrderMemo1",entity.ForeOrderMemo1?? String.Empty),
			    GenerateParameter("ForeOrderMemo2",entity.ForeOrderMemo2?? String.Empty),
			    GenerateParameter("CanUsable",entity.CanUsable),
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
        protected override void InnerLoad(IDataReader reader, ref ForeOrderEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderID"))
                {
                    entity.ForeOrderID = reader.GetInt32(reader.GetOrdinal("ForeOrderID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderGuid"))
                {
                    entity.ForeOrderGuid = reader.GetGuid(reader.GetOrdinal("ForeOrderGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderType"))
                {
                    entity.ForeOrderType = reader.GetInt32(reader.GetOrdinal("ForeOrderType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderCategory"))
                {
                    entity.ForeOrderCategory = reader.GetString(reader.GetOrdinal("ForeOrderCategory"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderStatus"))
                {
                    entity.ForeOrderStatus = (ForeOrderStatuses)reader.GetInt32(reader.GetOrdinal("ForeOrderStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderDate"))
                {
                    entity.ForeOrderDate = reader.GetDateTime(reader.GetOrdinal("ForeOrderDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderTitle"))
                {
                    entity.ForeOrderTitle = reader.GetString(reader.GetOrdinal("ForeOrderTitle"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderDesc"))
                {
                    entity.ForeOrderDesc = reader.GetString(reader.GetOrdinal("ForeOrderDesc"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerKey"))
                {
                    entity.OwnerKey = reader.GetString(reader.GetOrdinal("OwnerKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerName"))
                {
                    entity.OwnerName = reader.GetString(reader.GetOrdinal("OwnerName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerOtherInfo"))
                {
                    entity.OwnerOtherInfo = reader.GetString(reader.GetOrdinal("OwnerOtherInfo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeKey"))
                {
                    entity.RelativeKey = reader.GetString(reader.GetOrdinal("RelativeKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeName"))
                {
                    entity.RelativeName = reader.GetString(reader.GetOrdinal("RelativeName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeNameOther"))
                {
                    entity.RelativeNameOther = reader.GetString(reader.GetOrdinal("RelativeNameOther"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderAmount"))
                {
                    entity.ForeOrderAmount = reader.GetDecimal(reader.GetOrdinal("ForeOrderAmount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderUnitName"))
                {
                    entity.ForeOrderUnitName = reader.GetString(reader.GetOrdinal("ForeOrderUnitName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderUnitFee"))
                {
                    entity.ForeOrderUnitFee = reader.GetDecimal(reader.GetOrdinal("ForeOrderUnitFee"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderPaid"))
                {
                    entity.ForeOrderPaid = (Logics)reader.GetInt32(reader.GetOrdinal("ForeOrderPaid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderMemo1"))
                {
                    entity.ForeOrderMemo1 = reader.GetString(reader.GetOrdinal("ForeOrderMemo1"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ForeOrderMemo2"))
                {
                    entity.ForeOrderMemo2 = reader.GetString(reader.GetOrdinal("ForeOrderMemo2"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = reader.GetInt32(reader.GetOrdinal("CanUsable"));
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
