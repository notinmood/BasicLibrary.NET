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
    public class EnterpriseCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<EnterpriseEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralEnterprise"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "EnterpriseGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "EnterpriseGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Enterprise_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(EnterpriseEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.EnterpriseGuid == Guid.Empty)
            {
                entity.EnterpriseGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralEnterprise] (
			        [EnterpriseGuid],
			        [CompanyName],
			        [BusinessType],
			        [TradingName],
			        [Industry],
			        [EnterpriseCode],
			        [TaxCode],
			        [PrincipleAddress],
			        [PostCode],
			        [Telephone],
			        [Fax],
			        [Email],
			        [EstablishedYears],
			        [EstablishedTime],
			        [GrossIncome],
			        [Profit],
			        [AssociatedEnterpriseGuid],
			        [ContactPerson],
			        [AreaCode],
			        [CompanyNameShort],
			        [CanUsable],
			        [Longitude],
			        [Lantitude],
                    [BrokerKey],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        @EnterpriseGuid,
			        @CompanyName,
			        @BusinessType,
			        @TradingName,
			        @Industry,
			        @EnterpriseCode,
			        @TaxCode,
			        @PrincipleAddress,
			        @PostCode,
			        @Telephone,
			        @Fax,
			        @Email,
			        @EstablishedYears,
			        @EstablishedTime,
			        @GrossIncome,
			        @Profit,
			        @AssociatedEnterpriseGuid,
			        @ContactPerson,
			        @AreaCode,
			        @CompanyNameShort,
			        @CanUsable,
			        @Longitude,
			        @Lantitude,
                    @BrokerKey,
			        @PropertyNames,
			        @PropertyValues
                )";

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Update(EnterpriseEntity entity)
        {
            string commandText = @"Update [GeneralEnterprise] Set   
					[EnterpriseGuid] = @EnterpriseGuid,
					[CompanyName] = @CompanyName,
					[BusinessType] = @BusinessType,
					[TradingName] = @TradingName,
					[Industry] = @Industry,
					[EnterpriseCode] = @EnterpriseCode,
					[TaxCode] = @TaxCode,
					[PrincipleAddress] = @PrincipleAddress,
					[PostCode] = @PostCode,
					[Telephone] = @Telephone,
					[Fax] = @Fax,
					[Email] = @Email,
					[EstablishedYears] = @EstablishedYears,
					[EstablishedTime] = @EstablishedTime,
					[GrossIncome] = @GrossIncome,
					[Profit] = @Profit,
					[AssociatedEnterpriseGuid] = @AssociatedEnterpriseGuid,
					[ContactPerson] = @ContactPerson,
					[AreaCode] = @AreaCode,
					[CompanyNameShort] = @CompanyNameShort,
					[CanUsable] = @CanUsable,
					[Longitude] = @Longitude,
					[Lantitude] = @Lantitude,
                    [BrokerKey]= @BrokerKey,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [EnterpriseGuid] = @EnterpriseGuid";

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
        protected override void InnerPrepareParasAll(EnterpriseEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
			    GenerateParameter("EnterpriseGuid",entity.EnterpriseGuid== Guid.Empty?Guid.NewGuid():entity.EnterpriseGuid),
			    GenerateParameter("CompanyName",entity.CompanyName??string.Empty),
			    GenerateParameter("BusinessType",(int)entity.BusinessType),
			    GenerateParameter("TradingName",entity.TradingName??string.Empty),
			    GenerateParameter("Industry",entity.Industry??string.Empty),
			    GenerateParameter("EnterpriseCode",entity.EnterpriseCode??string.Empty),
			    GenerateParameter("TaxCode",entity.TaxCode??string.Empty),
			    GenerateParameter("PrincipleAddress",entity.PrincipleAddress??string.Empty),
			    GenerateParameter("PostCode",entity.PostCode??string.Empty),
			    GenerateParameter("Telephone",entity.Telephone??string.Empty),
			    GenerateParameter("Fax",entity.Fax??string.Empty),
			    GenerateParameter("Email",entity.Email??string.Empty),
			    GenerateParameter("EstablishedYears",entity.EstablishedYears),
			    GenerateParameter("EstablishedTime",entity.EstablishedTime),
			    GenerateParameter("GrossIncome",entity.GrossIncome),
			    GenerateParameter("Profit",entity.Profit),
			    GenerateParameter("AssociatedEnterpriseGuid",entity.AssociatedEnterpriseGuid),
			    GenerateParameter("ContactPerson",entity.ContactPerson??string.Empty),
			    GenerateParameter("AreaCode",entity.AreaCode??string.Empty),
			    GenerateParameter("CompanyNameShort",entity.CompanyNameShort??string.Empty),
			    GenerateParameter("CanUsable",(int)entity.CanUsable),
			    GenerateParameter("Longitude",entity.Longitude),
			    GenerateParameter("Lantitude",entity.Lantitude),
                GenerateParameter("BrokerKey",entity.BrokerKey??string.Empty)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override EnterpriseEntity Load(IDataReader reader)
        {
            EnterpriseEntity entity = new EnterpriseEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseID"))
                {
                    entity.EnterpriseID = reader.GetInt32(reader.GetOrdinal("EnterpriseID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseGuid"))
                {
                    entity.EnterpriseGuid = reader.GetGuid(reader.GetOrdinal("EnterpriseGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CompanyName"))
                {
                    entity.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BusinessType"))
                {
                    entity.BusinessType = (EnterpriseTypes)reader.GetInt32(reader.GetOrdinal("BusinessType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TradingName"))
                {
                    entity.TradingName = reader.GetString(reader.GetOrdinal("TradingName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Industry"))
                {
                    entity.Industry = reader.GetString(reader.GetOrdinal("Industry"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseCode"))
                {
                    entity.EnterpriseCode = reader.GetString(reader.GetOrdinal("EnterpriseCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TaxCode"))
                {
                    entity.TaxCode = reader.GetString(reader.GetOrdinal("TaxCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PrincipleAddress"))
                {
                    entity.PrincipleAddress = reader.GetString(reader.GetOrdinal("PrincipleAddress"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PostCode"))
                {
                    entity.PostCode = reader.GetString(reader.GetOrdinal("PostCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Telephone"))
                {
                    entity.Telephone = reader.GetString(reader.GetOrdinal("Telephone"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Fax"))
                {
                    entity.Fax = reader.GetString(reader.GetOrdinal("Fax"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Email"))
                {
                    entity.Email = reader.GetString(reader.GetOrdinal("Email"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EstablishedYears"))
                {
                    entity.EstablishedYears = reader.GetInt32(reader.GetOrdinal("EstablishedYears"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EstablishedTime"))
                {
                    entity.EstablishedTime = reader.GetDateTime(reader.GetOrdinal("EstablishedTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "GrossIncome"))
                {
                    entity.GrossIncome = reader.GetDecimal(reader.GetOrdinal("GrossIncome"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Profit"))
                {
                    entity.Profit = reader.GetDecimal(reader.GetOrdinal("Profit"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AssociatedEnterpriseGuid"))
                {
                    entity.AssociatedEnterpriseGuid = reader.GetGuid(reader.GetOrdinal("AssociatedEnterpriseGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ContactPerson"))
                {
                    entity.ContactPerson = reader.GetString(reader.GetOrdinal("ContactPerson"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaCode"))
                {
                    entity.AreaCode = reader.GetString(reader.GetOrdinal("AreaCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CompanyNameShort"))
                {
                    entity.CompanyNameShort = reader.GetString(reader.GetOrdinal("CompanyNameShort"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Longitude"))
                {
                    entity.Longitude = reader.GetDecimal(reader.GetOrdinal("Longitude"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Lantitude"))
                {
                    entity.Lantitude = reader.GetDecimal(reader.GetOrdinal("Lantitude"));
                }
                if (DataReaderHelper.IsExistField(reader, "BrokerKey") && Convert.IsDBNull(reader["BrokerKey"]) == false)
                {
                    entity.BrokerKey = reader.GetString(reader.GetOrdinal("BrokerKey"));
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
    }
}
