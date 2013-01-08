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
    public class RemindCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<RemindEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralRemind"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "RemindGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "RemindGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Remind_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(RemindEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.RemindGuid == Guid.Empty)
            {
                entity.RemindGuid = GuidHelper.NewGuid();
            }

            string commandText = string.Format(@"Insert Into [GeneralRemind] (
			    [RemindGuid],
			    [SenderKey],
			    [SenderName],
			    [ReceiverKey],
			    [ReceiverName],
			    [Emergency],
			    [Importance],
			    [TopLevel],
			    [RemindTitle],
                [RemindUrl],
			    [RemindDescription],
			    [RemindCategory],
			    [RemindType],
			    [CreateDate],
			    [StartDate],
			    [ExpireDate],
			    [ReadDate],
    			[ReadStatus],
			    [ResourceKey],
			    [ProcessKey],
			    [ActivityKey],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    {0}RemindGuid,
			    {0}SenderKey,
			    {0}SenderName,
			    {0}ReceiverKey,
			    {0}ReceiverName,
			    {0}Emergency,
			    {0}Importance,
			    {0}TopLevel,
			    {0}RemindTitle,
                {0}RemindUrl,
			    {0}RemindDescription,
			    {0}RemindCategory,
			    {0}RemindType,
			    {0}CreateDate,
			    {0}StartDate,
			    {0}ExpireDate,
			    {0}ReadDate,
			    {0}ReadStatus,
			    {0}ResourceKey,
			    {0}ProcessKey,
			    {0}ActivityKey,
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
        public override bool Update(RemindEntity entity)
        {
            string commandText = string.Format(@"Update [GeneralRemind] Set   
					[RemindGuid] = {0}RemindGuid,
					[SenderKey] = {0}SenderKey,
					[SenderName] = {0}SenderName,
					[ReceiverKey] = {0}ReceiverKey,
					[ReceiverName] = {0}ReceiverName,
					[Emergency] = {0}Emergency,
					[Importance] = {0}Importance,
					[TopLevel] = {0}TopLevel,
					[RemindTitle] = {0}RemindTitle,
                    [RemindUrl] = {0}RemindUrl,
					[RemindDescription] = {0}RemindDescription,
					[RemindCategory] = {0}RemindCategory,
					[RemindType] = {0}RemindType,
					[CreateDate] = {0}CreateDate,
					[StartDate] = {0}StartDate,
					[ExpireDate] = {0}ExpireDate,
					[ReadDate] = {0}ReadDate,
				    [ReadStatus] = {0}ReadStatus,
					[ResourceKey] = {0}ResourceKey,
					[ProcessKey] = {0}ProcessKey,
					[ActivityKey] = {0}ActivityKey,
					[PropertyNames] = {0}PropertyNames,
					[PropertyValues] = {0}PropertyValues
             Where [RemindID] = {0}RemindID", ParameterNamePrefix);

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
        protected override void InnerPrepareParasAll(RemindEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("RemindID",entity.RemindID),
                GenerateParameter("RemindGuid",entity.RemindGuid),
			    GenerateParameter("SenderKey",entity.SenderKey?? String.Empty),
			    GenerateParameter("SenderName",entity.SenderName?? String.Empty),
			    GenerateParameter("ReceiverKey",entity.ReceiverKey?? String.Empty),
			    GenerateParameter("ReceiverName",entity.ReceiverName?? String.Empty),
			    GenerateParameter("Emergency",entity.Emergency),
			    GenerateParameter("Importance",entity.Importance),
			    GenerateParameter("TopLevel",entity.TopLevel),
			    GenerateParameter("RemindTitle",entity.RemindTitle?? String.Empty),
                GenerateParameter("RemindUrl",entity.RemindUrl?? String.Empty),
			    GenerateParameter("RemindDescription",entity.RemindDescription?? String.Empty),
			    GenerateParameter("RemindCategory",entity.RemindCategory),
			    GenerateParameter("RemindType",entity.RemindType),
			    GenerateParameter("CreateDate",entity.CreateDate),
			    GenerateParameter("StartDate",entity.StartDate),
			    GenerateParameter("ExpireDate",entity.ExpireDate),
			    GenerateParameter("ReadDate",entity.ReadDate),
                GenerateParameter("ReadStatus",entity.ReadStatus),
			    GenerateParameter("ResourceKey",entity.ResourceKey?? String.Empty),
			    GenerateParameter("ProcessKey",entity.ProcessKey?? String.Empty),
			    GenerateParameter("ActivityKey",entity.ActivityKey?? String.Empty)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref RemindEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindID"))
                {
                    entity.RemindID = reader.GetInt32(reader.GetOrdinal("RemindID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindGuid"))
                {
                    entity.RemindGuid = reader.GetGuid(reader.GetOrdinal("RemindGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SenderKey"))
                {
                    entity.SenderKey = reader.GetString(reader.GetOrdinal("SenderKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SenderName"))
                {
                    entity.SenderName = reader.GetString(reader.GetOrdinal("SenderName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ReceiverKey"))
                {
                    entity.ReceiverKey = reader.GetString(reader.GetOrdinal("ReceiverKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ReceiverName"))
                {
                    entity.ReceiverName = reader.GetString(reader.GetOrdinal("ReceiverName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Emergency"))
                {
                    entity.Emergency = (LevelTypes)reader.GetInt32(reader.GetOrdinal("Emergency"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Importance"))
                {
                    entity.Importance = (LevelTypes)reader.GetInt32(reader.GetOrdinal("Importance"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TopLevel"))
                {
                    entity.TopLevel = reader.GetInt32(reader.GetOrdinal("TopLevel"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindTitle"))
                {
                    entity.RemindTitle = reader.GetString(reader.GetOrdinal("RemindTitle"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindUrl"))
                {
                    entity.RemindUrl = reader.GetString(reader.GetOrdinal("RemindUrl"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindDescription"))
                {
                    entity.RemindDescription = reader.GetString(reader.GetOrdinal("RemindDescription"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindCategory"))
                {
                    entity.RemindCategory = (RemindCategories)reader.GetInt32(reader.GetOrdinal("RemindCategory"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RemindType"))
                {
                    entity.RemindType = reader.GetInt32(reader.GetOrdinal("RemindType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateDate"))
                {
                    entity.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "StartDate"))
                {
                    entity.StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ExpireDate"))
                {
                    entity.ExpireDate = reader.GetDateTime(reader.GetOrdinal("ExpireDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ReadDate"))
                {
                    entity.ReadDate = reader.GetDateTime(reader.GetOrdinal("ReadDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ReadStatus"))
                {
                    entity.ReadStatus = (Logics)reader.GetInt32(reader.GetOrdinal("ReadStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResourceKey"))
                {
                    entity.ResourceKey = reader.GetString(reader.GetOrdinal("ResourceKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProcessKey"))
                {
                    entity.ProcessKey = reader.GetString(reader.GetOrdinal("ProcessKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ActivityKey"))
                {
                    entity.ActivityKey = reader.GetString(reader.GetOrdinal("ActivityKey"));
                }
            }
        }
        #endregion
    }
}
