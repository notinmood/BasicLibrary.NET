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
    public class BankCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<BankEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>, IBankDAL
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
            get { return "GeneralBank"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "BankGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "BankGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Bank_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(BankEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.BankGuid == Guid.Empty)
            {
                entity.BankGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralBank] (
			    [BankGuid],
			    [UserGuid],
			    [BankNo],
			    [IsPrimary],
			    [CanUsable],
			    [BankName],
			    [Branch],
			    [BankCode],
			    [AccountName],
			    [AccountNumber],
			    [AccountStatus],
			    [BankAddress],
			    [AreaCode],
			    [Telephone],
			    [Fax],
			    [Email],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    @BankGuid,
			    @UserGuid,
			    @BankNo,
			    @IsPrimary,
			    @CanUsable,
			    @BankName,
			    @Branch,
			    @BankCode,
			    @AccountName,
			    @AccountNumber,
			    @AccountStatus,
			    @BankAddress,
			    @AreaCode,
			    @Telephone,
			    @Fax,
			    @Email,
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
        public override bool Update(BankEntity entity)
        {
            string commandText = @"Update [GeneralBank] Set   
					[BankGuid] = @BankGuid,
					[UserGuid] = @UserGuid,
					[BankNo] = @BankNo,
					[IsPrimary] = @IsPrimary,
					[CanUsable] = @CanUsable,
					[BankName] = @BankName,
					[Branch] = @Branch,
					[BankCode] = @BankCode,
					[AccountName] = @AccountName,
					[AccountNumber] = @AccountNumber,
					[AccountStatus] = @AccountStatus,
					[BankAddress] = @BankAddress,
					[AreaCode] = @AreaCode,
					[Telephone] = @Telephone,
					[Fax] = @Fax,
					[Email] = @Email,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [BankGuid] = @BankGuid";

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 移除某用户银行账户的Primary状态
        /// </summary>
        /// <param name="userGuid">人员Guid</param>
        /// <param name="bankGuidExclude">取消当前状态时，需要排除在外的银行Guid</param>
        /// <returns></returns>
        public virtual void RemovePrimaryStatusOfUser(Guid userGuid, Guid bankGuidExclude)
        {
            string commandText = string.Format(@"Update [GeneralBank] Set   
                    [IsPrimary]={0}IsPrimary
             Where 
                    [UserGuid] = {0}UserGuid AND 
                    [BankGuid] != {0}BankGuid", ParameterNamePrefix);

            TParameter[] sqlParas = new TParameter[]{
                GenerateParameter("IsPrimary",Logics.False),
                GenerateParameter("UserGuid",userGuid),
                GenerateParameter("BankGuid",bankGuidExclude)
            };

            HelperExInstance.ExecuteNonQuery(commandText, sqlParas);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 内部准备（为实体准备数据访问的参数）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="paraList"></param>
        protected override void InnerPrepareParasAll(BankEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("BankGuid",entity.BankGuid== Guid.Empty?Guid.NewGuid():entity.BankGuid),
			    GenerateParameter("UserGuid",entity.UserGuid),
			    GenerateParameter("BankNo",entity.BankNo),
			    GenerateParameter("IsPrimary",entity.IsPrimary),
			    GenerateParameter("CanUsable",(int)entity.CanUsable),
			    GenerateParameter("BankName",entity.BankName??string.Empty),
			    GenerateParameter("Branch",entity.Branch??string.Empty),
			    GenerateParameter("BankCode",entity.BankCode??string.Empty),
			    GenerateParameter("AccountName",entity.AccountName??string.Empty),
			    GenerateParameter("AccountNumber",entity.AccountNumber??string.Empty),
			    GenerateParameter("AccountStatus",entity.AccountStatus),
			    GenerateParameter("BankAddress",entity.BankAddress??string.Empty),
			    GenerateParameter("AreaCode",entity.AreaCode??string.Empty),
			    GenerateParameter("Telephone",entity.Telephone??string.Empty),
			    GenerateParameter("Fax",entity.Fax??string.Empty),
			    GenerateParameter("Email",entity.Email??string.Empty)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref BankEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BankID"))
                {
                    entity.BankID = reader.GetInt32(reader.GetOrdinal("BankID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BankGuid"))
                {
                    entity.BankGuid = reader.GetGuid(reader.GetOrdinal("BankGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserGuid"))
                {
                    entity.UserGuid = reader.GetGuid(reader.GetOrdinal("UserGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BankNo"))
                {
                    entity.BankNo = reader.GetInt32(reader.GetOrdinal("BankNo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsPrimary"))
                {
                    entity.IsPrimary = (Logics)reader.GetInt32(reader.GetOrdinal("IsPrimary"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BankName"))
                {
                    entity.BankName = reader.GetString(reader.GetOrdinal("BankName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Branch"))
                {
                    entity.Branch = reader.GetString(reader.GetOrdinal("Branch"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BankCode"))
                {
                    entity.BankCode = reader.GetString(reader.GetOrdinal("BankCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AccountName"))
                {
                    entity.AccountName = reader.GetString(reader.GetOrdinal("AccountName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AccountNumber"))
                {
                    entity.AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AccountStatus"))
                {
                    entity.AccountStatus = reader.GetInt32(reader.GetOrdinal("AccountStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BankAddress"))
                {
                    entity.BankAddress = reader.GetString(reader.GetOrdinal("BankAddress"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaCode"))
                {
                    entity.AreaCode = reader.GetString(reader.GetOrdinal("AreaCode"));
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
            }
        }
        #endregion
    }
}
