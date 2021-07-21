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
    /// 地区的数据访问类
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public class AreaCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<AreaEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
        where TConnection : class, IDbConnection, new()
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
            get { return "GeneralArea"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "AreaID" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return ""; }
        }

        /// <summary>
        /// 分页存储过程的名字(尚未实现此SP)
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Area_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(AreaEntity entity)
        {
            string commandText = string.Format(@"Insert Into [GeneralArea] (
			        [AreaCode],
			        [AreaName],
			        [AreaLevel],
			        [TelephoneCode],
			        [ZipCode],
			        [CanUsable],
			        [IsDisplay],
			        [AreaGroup],
			        [Nation],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        {0}AreaCode,
			        {0}AreaName,
			        {0}AreaLevel,
			        {0}TelephoneCode,
			        {0}ZipCode,
			        {0}CanUsable,
			        {0}IsDisplay,
			        {0}AreaGroup,
			        {0}Nation,
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
        public override bool Update(AreaEntity entity)
        {
            string commandText = string.Format(@"Update [GeneralArea] Set   
				    [AreaCode] = {0}AreaCode,
				    [AreaName] = {0}AreaName,
				    [AreaLevel] = {0}AreaLevel,
				    [TelephoneCode] = {0}TelephoneCode,
				    [ZipCode] = {0}ZipCode,
				    [CanUsable] = {0}CanUsable,
				    [IsDisplay] = {0}IsDisplay,
				    [AreaGroup] = {0}AreaGroup,
				    [Nation] = {0}Nation,
				    [PropertyNames] = {0}PropertyNames,
				    [PropertyValues] = {0}PropertyValues
            Where [AreaID] = {0}AreaID", ParameterNamePrefix);

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
        protected override void InnerPrepareParasAll(AreaEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("AreaID",entity.AreaID),
                GenerateParameter("AreaCode",entity.AreaCode?? String.Empty),
                GenerateParameter("AreaName",entity.AreaName?? String.Empty),
                GenerateParameter("AreaLevel",entity.AreaLevel),
                GenerateParameter("TelephoneCode",entity.TelephoneCode?? String.Empty),
                GenerateParameter("ZipCode",entity.ZipCode?? String.Empty),
                GenerateParameter("CanUsable",entity.CanUsable),
                GenerateParameter("IsDisplay",entity.IsDisplay),
                GenerateParameter("AreaGroup",entity.AreaGroup?? String.Empty),
                GenerateParameter("Nation",entity.Nation?? String.Empty)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref AreaEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaID"))
                {
                    entity.AreaID = reader.GetInt32(reader.GetOrdinal("AreaID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaCode"))
                {
                    entity.AreaCode = reader.GetString(reader.GetOrdinal("AreaCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaName"))
                {
                    entity.AreaName = reader.GetString(reader.GetOrdinal("AreaName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaLevel"))
                {
                    entity.AreaLevel = reader.GetInt32(reader.GetOrdinal("AreaLevel"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TelephoneCode"))
                {
                    entity.TelephoneCode = reader.GetString(reader.GetOrdinal("TelephoneCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ZipCode"))
                {
                    entity.ZipCode = reader.GetString(reader.GetOrdinal("ZipCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsDisplay"))
                {
                    entity.IsDisplay = reader.GetInt32(reader.GetOrdinal("IsDisplay"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaGroup"))
                {
                    entity.AreaGroup = reader.GetString(reader.GetOrdinal("AreaGroup"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Nation"))
                {
                    entity.Nation = reader.GetString(reader.GetOrdinal("Nation"));
                }
            }
        }
        #endregion
    }
}
