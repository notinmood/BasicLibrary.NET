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
    public class ResidentalCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<ResidentalEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralResidental"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "ResidentialGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ResidentialGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Residental_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(ResidentalEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ResidentialGuid == Guid.Empty)
            {
                entity.ResidentialGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralResidental] (
			    [ResidentialGuid],
			    [ResidentalUserGuid],
			    [ResidentialStatus],
			    [ResidentalNo],
			    [IsPrimary],
			    [State],
			    [City],
			    [Suburb],
			    [Street],
			    [ApartmentNo],
			    [PostCode],
			    [ContactPerson],
			    [Telephone],
			    [Fax],
			    [Mobile],
			    [ResidentalYears],
			    [ResidentalMonths],
			    [ResidentalBeginTime],
			    [ResidentaEndTime],
			    [CreateDate],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    @ResidentialGuid,
			    @ResidentalUserGuid,
			    @ResidentialStatus,
			    @ResidentalNo,
			    @IsPrimary,
			    @State,
			    @City,
			    @Suburb,
			    @Street,
			    @ApartmentNo,
			    @PostCode,
			    @ContactPerson,
			    @Telephone,
			    @Fax,
			    @Mobile,
			    @ResidentalYears,
			    @ResidentalMonths,
			    @ResidentalBeginTime,
			    @ResidentaEndTime,
			    @CreateDate,
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
        public override bool Update(ResidentalEntity entity)
        {
            string commandText = @"Update [GeneralResidental] Set   
					[ResidentialGuid] = @ResidentialGuid,
					[ResidentalUserGuid] = @ResidentalUserGuid,
					[ResidentialStatus] = @ResidentialStatus,
					[ResidentalNo] = @ResidentalNo,
					[IsPrimary] = @IsPrimary,
					[State] = @State,
					[City] = @City,
					[Suburb] = @Suburb,
					[Street] = @Street,
					[ApartmentNo] = @ApartmentNo,
					[PostCode] = @PostCode,
					[ContactPerson] = @ContactPerson,
					[Telephone] = @Telephone,
					[Fax] = @Fax,
					[Mobile] = @Mobile,
					[ResidentalYears] = @ResidentalYears,
					[ResidentalMonths] = @ResidentalMonths,
					[ResidentalBeginTime] = @ResidentalBeginTime,
					[ResidentaEndTime] = @ResidentaEndTime,
					[CreateDate] = @CreateDate,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [ResidentialGuid] = @ResidentialGuid";

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
        protected override void InnerPrepareParasAll(ResidentalEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
			    GenerateParameter("ResidentialGuid",entity.ResidentialGuid== Guid.Empty?Guid.NewGuid():entity.ResidentialGuid),
			    GenerateParameter("ResidentalUserGuid",entity.ResidentalUserGuid),
			    GenerateParameter("ResidentialStatus",(int)entity.ResidentialStatus),
			    GenerateParameter("ResidentalNo",entity.ResidentalNo),
			    GenerateParameter("IsPrimary",entity.IsPrimary),
			    GenerateParameter("State",entity.State??string.Empty),
			    GenerateParameter("City",entity.City??string.Empty),
			    GenerateParameter("Suburb",entity.Suburb??string.Empty),
			    GenerateParameter("Street",entity.Street??string.Empty),
			    GenerateParameter("ApartmentNo",entity.ApartmentNo??string.Empty),
			    GenerateParameter("PostCode",entity.PostCode??string.Empty),
			    GenerateParameter("ContactPerson",entity.ContactPerson??string.Empty),
			    GenerateParameter("Telephone",entity.Telephone??string.Empty),
			    GenerateParameter("Fax",entity.Fax??string.Empty),
			    GenerateParameter("Mobile",entity.Mobile??string.Empty),
			    GenerateParameter("ResidentalYears",entity.ResidentalYears),
			    GenerateParameter("ResidentalMonths",entity.ResidentalMonths),
			    GenerateParameter("ResidentalBeginTime",entity.ResidentalBeginTime),
			    GenerateParameter("ResidentaEndTime",entity.ResidentaEndTime),
			    GenerateParameter("CreateDate",entity.CreateDate)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override ResidentalEntity Load(IDataReader reader)
        {
            ResidentalEntity entity = new ResidentalEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentialID"))
                {
                    entity.ResidentialID = reader.GetInt32(reader.GetOrdinal("ResidentialID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentialGuid"))
                {
                    entity.ResidentialGuid = reader.GetGuid(reader.GetOrdinal("ResidentialGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentalUserGuid"))
                {
                    entity.ResidentalUserGuid = reader.GetGuid(reader.GetOrdinal("ResidentalUserGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentialStatus"))
                {
                    entity.ResidentialStatus = (ResidentalTypes)reader.GetInt32(reader.GetOrdinal("ResidentialStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentalNo"))
                {
                    entity.ResidentalNo = reader.GetInt32(reader.GetOrdinal("ResidentalNo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsPrimary"))
                {
                    entity.IsPrimary = reader.GetInt32(reader.GetOrdinal("IsPrimary"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "State"))
                {
                    entity.State = reader.GetString(reader.GetOrdinal("State"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "City"))
                {
                    entity.City = reader.GetString(reader.GetOrdinal("City"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Suburb"))
                {
                    entity.Suburb = reader.GetString(reader.GetOrdinal("Suburb"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Street"))
                {
                    entity.Street = reader.GetString(reader.GetOrdinal("Street"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ApartmentNo"))
                {
                    entity.ApartmentNo = reader.GetString(reader.GetOrdinal("ApartmentNo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PostCode"))
                {
                    entity.PostCode = reader.GetString(reader.GetOrdinal("PostCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ContactPerson"))
                {
                    entity.ContactPerson = reader.GetString(reader.GetOrdinal("ContactPerson"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Telephone"))
                {
                    entity.Telephone = reader.GetString(reader.GetOrdinal("Telephone"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Fax"))
                {
                    entity.Fax = reader.GetString(reader.GetOrdinal("Fax"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Mobile"))
                {
                    entity.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentalYears"))
                {
                    entity.ResidentalYears = reader.GetInt32(reader.GetOrdinal("ResidentalYears"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentalMonths"))
                {
                    entity.ResidentalMonths = reader.GetInt32(reader.GetOrdinal("ResidentalMonths"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentalBeginTime"))
                {
                    entity.ResidentalBeginTime = reader.GetDateTime(reader.GetOrdinal("ResidentalBeginTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ResidentaEndTime"))
                {
                    entity.ResidentaEndTime = reader.GetDateTime(reader.GetOrdinal("ResidentaEndTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateDate"))
                {
                    entity.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
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
