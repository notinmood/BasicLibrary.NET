using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore.DALCommon
{
    public class BusinessPermissionCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<BusinessPermission, TTransaction, TConnection, TCommand, TDataReader, TParameter>, IBusinessPermissionDAL
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        #region 基本信息
        protected override string TableName
        {
            get { return "CorePermission"; }
        }

        protected override string[] KeyNames
        {
            get { return new string[] { "PermissionKey" }; }
        }

        protected override string GuidKeyName
        {
            get { return "PermissionKey"; }
        }

        //TODO:此存储过程未实行
        protected override string PagingSPName
        {
            get { return "usp_Core_Permission_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        public override bool Create(BusinessPermission entity)
        {
            string commandText = string.Format(@"Insert Into [CorePermission] (
			        [PermissionKey],
                    [OwnerKey],
			        [OwnerType],
			        [PermissionItemGuid],
			        [PermissionItemValue],
			        [PermissionMode],
                    [PermissionKind],
			        [CreateUserGuid],
			        [CreateUserType],
			        [IsFreeAwayCreator]
                ) 
                Values (
			        {0}PermissionKey,
                    {0}OwnerKey,
			        {0}OwnerType,
			        {0}PermissionItemGuid,
			        {0}PermissionItemValue,
			        {0}PermissionMode,
                    {0}PermissionKind,
			        {0}CreateUserGuid,
			        {0}CreateUserType,
			        {0}IsFreeAwayCreator
                )", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        public override bool Update(BusinessPermission entity)
        {
            string commandText = string.Format(@"Update [CorePermission] Set   
					[OwnerKey] = {0}OwnerKey,
					[OwnerType] = {0}OwnerType,
					[PermissionItemGuid] = {0}PermissionItemGuid,
					[PermissionItemValue] = {0}PermissionItemValue,
					[PermissionMode] = {0}PermissionMode,
                    [PermissionKind]= {0}PermissionKind,
					[CreateUserGuid] = {0}CreateUserGuid,
					[CreateUserType] = {0}CreateUserType,
					[IsFreeAwayCreator] = {0}IsFreeAwayCreator
             Where [PermissionItemGuid] = {0}PermissionItemGuid and [OwnerKey]={0}OwnerKey and [PermissionMode]={0}PermissionMode", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }
        #endregion

        #region 辅助方法
        protected override void InnerPrepareParasAll(BusinessPermission entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("PermissionKey",entity.PermissionKey== Guid.Empty?Guid.NewGuid():entity.PermissionKey),
			    GenerateParameter("OwnerKey",entity.OwnerKey?? String.Empty),
			    GenerateParameter("OwnerType",(int)entity.OwnerType),
			    GenerateParameter("PermissionItemGuid",entity.PermissionItemGuid),
			    GenerateParameter("PermissionItemValue",entity.PermissionItemValue),
			    GenerateParameter("PermissionMode",(int)entity.PermissionMode),
                GenerateParameter("PermissionKind",(int)entity.PermissionKind),
			    GenerateParameter("CreateUserGuid",entity.CreateUserGuid),
			    GenerateParameter("CreateUserType",entity.CreateUserType),
			    GenerateParameter("IsFreeAwayCreator",entity.IsFreeAwayCreator)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        protected override void InnerLoad(IDataReader reader, ref BusinessPermission entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PermissionKey"))
                {
                    entity.PermissionKey = reader.GetGuid(reader.GetOrdinal("PermissionKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerKey"))
                {
                    entity.OwnerKey = reader.GetString(reader.GetOrdinal("OwnerKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerType"))
                {
                    entity.OwnerType = (ExecuterTypes)reader.GetInt32(reader.GetOrdinal("OwnerType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PermissionItemGuid"))
                {
                    entity.PermissionItemGuid = reader.GetGuid(reader.GetOrdinal("PermissionItemGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PermissionItemValue"))
                {
                    entity.PermissionItemValue = reader.GetInt32(reader.GetOrdinal("PermissionItemValue"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PermissionMode"))
                {
                    entity.PermissionMode = (PermissionModes)reader.GetInt32(reader.GetOrdinal("PermissionMode"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PermissionKind"))
                {
                    entity.PermissionKind = (PermissionKinds)reader.GetInt32(reader.GetOrdinal("PermissionKind"));
                }
                
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateUserGuid"))
                {
                    entity.CreateUserGuid = reader.GetGuid(reader.GetOrdinal("CreateUserGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateUserType"))
                {
                    entity.CreateUserType = (UserTypes)reader.GetInt32(reader.GetOrdinal("CreateUserType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsFreeAwayCreator"))
                {
                    entity.IsFreeAwayCreator = (Logics)reader.GetInt32(reader.GetOrdinal("IsFreeAwayCreator"));
                }
            }
        }
        #endregion
    }
}
