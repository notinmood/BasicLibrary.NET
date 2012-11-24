using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;

namespace HiLand.General.DALCommon
{
    public class EnterpriseCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<EnterpriseEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
        /// 分页存储过程名称
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

            if (entity.CreateDate == DateTimeHelper.Min)
            {
                entity.CreateDate = DateTime.Now;
            }

            if (string.IsNullOrEmpty(entity.CreateUserKey))
            {
                entity.CreateUserKey = BusinessUserBLL.CurrentUserGuid.ToString();
                entity.CreateUserName = BusinessUserBLL.CurrentUserName;
            }

            string commandText = string.Format(@"Insert Into [GeneralEnterprise] (
			        [EnterpriseGuid],
			        [CompanyName],
			        [CompanyNameShort],
			        [BusinessType],
			        [TradingName],
			        [IndustryKey],
                    [IndustryType],
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
                    [AreaOther],
			        [CanUsable],
			        [Longitude],
			        [Lantitude],
			        [BrokerKey],
			        [EnterpriseDescription],
			        [EnterpriseMemo],
			        [EnterpriseWWW],
			        [StaffScope],
			        [EnterpriseLevel],
			        [EnterpriseRank],
			        [CreateUserKey],
                    [CreateUserName],
			        [CreateDate],
                    [LastUpdateUserKey],
                    [LastUpdateUserName],
			        [LastUpdateDate],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        {0}EnterpriseGuid,
			        {0}CompanyName,
			        {0}CompanyNameShort,
			        {0}BusinessType,
			        {0}TradingName,
			        {0}IndustryKey,
                    {0}IndustryType,
			        {0}EnterpriseCode,
			        {0}TaxCode,
			        {0}PrincipleAddress,
			        {0}PostCode,
			        {0}Telephone,
			        {0}Fax,
			        {0}Email,
			        {0}EstablishedYears,
			        {0}EstablishedTime,
			        {0}GrossIncome,
			        {0}Profit,
			        {0}AssociatedEnterpriseGuid,
			        {0}ContactPerson,
			        {0}AreaCode,
                    {0}AreaOther,
			        {0}CanUsable,
			        {0}Longitude,
			        {0}Lantitude,
			        {0}BrokerKey,
			        {0}EnterpriseDescription,
			        {0}EnterpriseMemo,
			        {0}EnterpriseWWW,
			        {0}StaffScope,
			        {0}EnterpriseLevel,
			        {0}EnterpriseRank,
			        {0}CreateUserKey,
                    {0}CreateUserName,
			        {0}CreateDate,
                    {0}LastUpdateUserKey,
                    {0}LastUpdateUserName,
			        {0}LastUpdateDate,
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
        public override bool Update(EnterpriseEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            entity.LastUpdateUserKey = BusinessUserBLL.CurrentUserGuid.ToString();
            entity.LastUpdateUserName = BusinessUserBLL.CurrentUserName;

            string commandText = string.Format(@"Update [GeneralEnterprise] Set   
					[EnterpriseGuid] = {0}EnterpriseGuid,
				    [CompanyName] = {0}CompanyName,
				    [CompanyNameShort] = {0}CompanyNameShort,
				    [BusinessType] = {0}BusinessType,
				    [TradingName] = {0}TradingName,
				    [IndustryKey] = {0}IndustryKey,
                    [IndustryType] = {0}IndustryType,
				    [EnterpriseCode] = {0}EnterpriseCode,
				    [TaxCode] = {0}TaxCode,
				    [PrincipleAddress] = {0}PrincipleAddress,
				    [PostCode] = {0}PostCode,
				    [Telephone] = {0}Telephone,
				    [Fax] = {0}Fax,
				    [Email] = {0}Email,
				    [EstablishedYears] = {0}EstablishedYears,
				    [EstablishedTime] = {0}EstablishedTime,
				    [GrossIncome] = {0}GrossIncome,
				    [Profit] = {0}Profit,
				    [AssociatedEnterpriseGuid] = {0}AssociatedEnterpriseGuid,
				    [ContactPerson] = {0}ContactPerson,
				    [AreaCode] = {0}AreaCode,
                    [AreaOther] = {0}AreaOther,
				    [CanUsable] = {0}CanUsable,
				    [Longitude] = {0}Longitude,
				    [Lantitude] = {0}Lantitude,
				    [BrokerKey] = {0}BrokerKey,
				    [EnterpriseDescription] = {0}EnterpriseDescription,
				    [EnterpriseMemo] = {0}EnterpriseMemo,
				    [EnterpriseWWW] = {0}EnterpriseWWW,
				    [StaffScope] = {0}StaffScope,
				    [EnterpriseLevel] = {0}EnterpriseLevel,
				    [EnterpriseRank] = {0}EnterpriseRank,
				    [CreateUserKey] = {0}CreateUserKey,
                    [CreateUserName] = {0}CreateUserName,
				    [CreateDate] = {0}CreateDate,
                    [LastUpdateUserKey] = {0}LastUpdateUserKey,
                    [LastUpdateUserName] = {0}LastUpdateUserName,
				    [LastUpdateDate] = {0}LastUpdateDate,
				    [PropertyNames] = {0}PropertyNames,
				    [PropertyValues] = {0}PropertyValues
             Where [EnterpriseGuid] = @EnterpriseGuid", ParameterNamePrefix);

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
			    GenerateParameter("EnterpriseID",entity.EnterpriseID),
			    GenerateParameter("EnterpriseGuid",entity.EnterpriseGuid),
			    GenerateParameter("CompanyName",entity.CompanyName?? String.Empty),
			    GenerateParameter("CompanyNameShort",entity.CompanyNameShort?? String.Empty),
			    GenerateParameter("BusinessType",entity.BusinessType),
			    GenerateParameter("TradingName",entity.TradingName?? String.Empty),
			    GenerateParameter("IndustryKey",entity.IndustryKey?? String.Empty),
                GenerateParameter("IndustryType",entity.IndustryType),
			    GenerateParameter("EnterpriseCode",entity.EnterpriseCode?? String.Empty),
			    GenerateParameter("TaxCode",entity.TaxCode?? String.Empty),
			    GenerateParameter("PrincipleAddress",entity.PrincipleAddress?? String.Empty),
			    GenerateParameter("PostCode",entity.PostCode?? String.Empty),
			    GenerateParameter("Telephone",entity.Telephone?? String.Empty),
			    GenerateParameter("Fax",entity.Fax?? String.Empty),
			    GenerateParameter("Email",entity.Email?? String.Empty),
			    GenerateParameter("EstablishedYears",entity.EstablishedYears),
			    GenerateParameter("EstablishedTime",entity.EstablishedTime),
			    GenerateParameter("GrossIncome",entity.GrossIncome),
			    GenerateParameter("Profit",entity.Profit),
			    GenerateParameter("AssociatedEnterpriseGuid",entity.AssociatedEnterpriseGuid),
			    GenerateParameter("ContactPerson",entity.ContactPerson?? String.Empty),
			    GenerateParameter("AreaCode",entity.AreaCode?? String.Empty),
                GenerateParameter("AreaOther",entity.AreaOther?? String.Empty),
			    GenerateParameter("CanUsable",entity.CanUsable),
			    GenerateParameter("Longitude",entity.Longitude),
			    GenerateParameter("Lantitude",entity.Lantitude),
			    GenerateParameter("BrokerKey",entity.BrokerKey?? String.Empty),
			    GenerateParameter("EnterpriseDescription",entity.EnterpriseDescription?? String.Empty),
			    GenerateParameter("EnterpriseMemo",entity.EnterpriseMemo?? String.Empty),
			    GenerateParameter("EnterpriseWWW",entity.EnterpriseWWW?? String.Empty),
			    GenerateParameter("StaffScope",entity.StaffScope),
			    GenerateParameter("EnterpriseLevel",entity.EnterpriseLevel),
			    GenerateParameter("EnterpriseRank",entity.EnterpriseRank?? String.Empty),
			    GenerateParameter("CreateUserKey",entity.CreateUserKey?? String.Empty),
                GenerateParameter("CreateUserName",entity.CreateUserName?? String.Empty),
			    GenerateParameter("CreateDate",entity.CreateDate),
                GenerateParameter("LastUpdateUserKey",entity.LastUpdateUserKey?? String.Empty),
                GenerateParameter("LastUpdateUserName",entity.LastUpdateUserName?? String.Empty),
			    GenerateParameter("LastUpdateDate",entity.LastUpdateDate)
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
            return StaticLoad(reader);
        }

        /// <summary>
        /// 此方法存储为了向外暴漏
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static EnterpriseEntity StaticLoad(IDataReader reader)
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
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CompanyNameShort"))
                {
                    entity.CompanyNameShort = reader.GetString(reader.GetOrdinal("CompanyNameShort"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BusinessType"))
                {
                    entity.BusinessType = (EnterpriseTypes)reader.GetInt32(reader.GetOrdinal("BusinessType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TradingName"))
                {
                    entity.TradingName = reader.GetString(reader.GetOrdinal("TradingName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IndustryKey"))
                {
                    entity.IndustryKey = reader.GetString(reader.GetOrdinal("IndustryKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IndustryType"))
                {
                    entity.IndustryType = (IndustryTypes)reader.GetInt32(reader.GetOrdinal("IndustryType"));
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
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaOther"))
                {
                    entity.AreaOther = reader.GetString(reader.GetOrdinal("AreaOther"));
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
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BrokerKey"))
                {
                    entity.BrokerKey = reader.GetString(reader.GetOrdinal("BrokerKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseDescription"))
                {
                    entity.EnterpriseDescription = reader.GetString(reader.GetOrdinal("EnterpriseDescription"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseMemo"))
                {
                    entity.EnterpriseMemo = reader.GetString(reader.GetOrdinal("EnterpriseMemo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseWWW"))
                {
                    entity.EnterpriseWWW = reader.GetString(reader.GetOrdinal("EnterpriseWWW"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "StaffScope"))
                {
                    entity.StaffScope = (StaffScopes)reader.GetInt32(reader.GetOrdinal("StaffScope"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseLevel"))
                {
                    entity.EnterpriseLevel = (CommonLevels)reader.GetInt32(reader.GetOrdinal("EnterpriseLevel"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseRank"))
                {
                    entity.EnterpriseRank = reader.GetString(reader.GetOrdinal("EnterpriseRank"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateUserKey"))
                {
                    entity.CreateUserKey = reader.GetString(reader.GetOrdinal("CreateUserKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateUserName"))
                {
                    entity.CreateUserName = reader.GetString(reader.GetOrdinal("CreateUserName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateDate"))
                {
                    entity.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LastUpdateUserKey"))
                {
                    entity.LastUpdateUserKey = reader.GetString(reader.GetOrdinal("LastUpdateUserKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LastUpdateUserName"))
                {
                    entity.LastUpdateUserName = reader.GetString(reader.GetOrdinal("LastUpdateUserName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LastUpdateDate"))
                {
                    entity.LastUpdateDate = reader.GetDateTime(reader.GetOrdinal("LastUpdateDate"));
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
