using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.General.Enums;
using HiLand.Utility.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Finance;

namespace HiLand.General.DALCommon
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public class LoanBasicCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<LoanBasicEntity, TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralLoanBasic"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "LoanGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "LoanGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_LoanBasic_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(LoanBasicEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.LoanGuid == Guid.Empty)
            {
                entity.LoanGuid = GuidHelper.NewGuid();
            }
            string singleRowCommandText = string.Format(@"INSERT INTO [dbo].[GeneralLoanBasic]
           ([LoanGuid]
           ,[LoanType]
           ,[LoanAmount]
           ,[LoanTermType]
           ,[LoanInterest]
           ,[LoanTermCount]
           ,[LoanPurpose]
           ,[LoanOwnerKey]
           ,[LoanOwnerType]
           ,[LoanOwnerAddon]
           ,[LoanDate]
           ,[LoanStatus]
           ,[CheckUserID]
           ,[CheckDate]
           ,[ReadDate]
           ,[ReadUserID]
           ,[PropertyNames]
           ,[PropertyValues])
     VALUES
           (
            {0}LoanGuid
           ,{0}LoanType
           ,{0}LoanAmount
           ,{0}LoanTermType
           ,{0}LoanInterest
           ,{0}LoanTermCount
           ,{0}LoanPurpose
           ,{0}LoanOwnerKey
           ,{0}LoanOwnerType
           ,{0}LoanOwnerAddon
           ,{0}LoanDate
           ,{0}LoanStatus
           ,{0}CheckUserID
           ,{0}CheckDate
           ,{0}ReadDate
           ,{0}ReadUserID
           ,{0}PropertyNames
           ,{0}PropertyValues)", this.ParameterNamePrefix);
            TParameter[] commandParameters = this.PrepareParasAll(entity);
            return BaseComputerDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>.HelperExInstance.ExecuteSingleRowNonQuery(singleRowCommandText, commandParameters);
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Update(LoanBasicEntity entity)
        {
            string commandText = @"Update [GeneralLoanBasic] Set   
					[LoanGuid] = @LoanGuid,
					[LoanType] = @LoanType,
					[LoanAmount] = @LoanAmount,
					[LoanTermType] = @LoanTermType,
					[LoanInterest] = @LoanInterest,
					[LoanTermCount] = @LoanTermCount,
					[LoanPurpose] = @LoanPurpose,
					[LoanOwnerKey] = @LoanOwnerKey,
                    [LoanOwnerType] = @LoanOwnerType,
                    [LoanOwnerAddon] = @LoanOwnerAddon,
					[LoanDate] = @LoanDate,
					[LoanStatus] = @LoanStatus,
					[CheckUserID] = @CheckUserID,
					[CheckDate] = @CheckDate,
                    [ReadDate] = @ReadDate,
                    [ReadUserID] = @ReadUserID,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [LoanGuid] = @LoanGuid";

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 更新读取人信息
        /// </summary>
        /// <param name="readUserID"></param>
        /// <param name="readDate"></param>
        /// <param name="loanGuid"></param>
        public bool UpdataReadInfo(Guid loanGuid, Guid readUserID, DateTime readDate)
        {
            string commandText = @"Update [GeneralLoanBasic] Set   
					[ReadUserID] = @ReadUserID,
					[ReadDate] = @ReadDate
             Where [LoanGuid] = @LoanGuid";

            TParameter[] sqlParas = new TParameter[] {
                GenerateParameter("ReadUserID",readUserID),
                GenerateParameter("ReadDate",readDate),
                GenerateParameter("LoanGuid",loanGuid)
            };

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
        protected override void InnerPrepareParasAll(LoanBasicEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> collection = new List<TParameter> {
                base.GenerateParameter<int>("LoanID", entity.LoanID),
                base.GenerateParameter<Guid>("LoanGuid", entity.LoanGuid),
                base.GenerateParameter<LoanTypes>("LoanType", entity.LoanType),
                base.GenerateParameter<decimal>("LoanAmount", entity.LoanAmount),
                base.GenerateParameter<PaymentTermTypes>("LoanTermType", entity.LoanTermType),
                base.GenerateParameter<decimal>("LoanInterest", entity.LoanInterest),
                base.GenerateParameter<int>("LoanTermCount", entity.LoanTermCount),
                base.GenerateParameter<string>("LoanPurpose", entity.LoanPurpose ?? string.Empty),
                base.GenerateParameter<string>("LoanOwnerKey", entity.LoanOwnerKey ?? string.Empty),
                base.GenerateParameter<LoanOwnerTypes>("LoanOwnerType", entity.LoanOwnerType),
                base.GenerateParameter<string>("LoanOwnerAddon", entity.LoanOwnerAddon ?? string.Empty),
                base.GenerateParameter<DateTime>("LoanDate", entity.LoanDate),
                base.GenerateParameter<LoanStatuses>("LoanStatus", entity.LoanStatus),
                base.GenerateParameter<Guid>("CheckUserID", entity.CheckUserID),
                base.GenerateParameter<DateTime>("CheckDate", entity.CheckDate),
                base.GenerateParameter<DateTime>("ReadDate", entity.ReadDate),
                base.GenerateParameter<Guid>("ReadUserID", entity.ReadUserID)
            };
            paraList.AddRange(collection);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override LoanBasicEntity Load(IDataReader reader)
        {
            LoanBasicEntity entity = new LoanBasicEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanID"))
                {
                    entity.LoanID = reader.GetInt32(reader.GetOrdinal("LoanID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanGuid"))
                {
                    entity.LoanGuid = reader.GetGuid(reader.GetOrdinal("LoanGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanType"))
                {
                    entity.LoanType = (LoanTypes)reader.GetInt32(reader.GetOrdinal("LoanType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanAmount"))
                {
                    entity.LoanAmount = reader.GetDecimal(reader.GetOrdinal("LoanAmount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanTermType"))
                {
                    entity.LoanTermType = (PaymentTermTypes)reader.GetInt32(reader.GetOrdinal("LoanTermType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanInterest"))
                {
                    entity.LoanInterest = reader.GetDecimal(reader.GetOrdinal("LoanInterest"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanTermCount"))
                {
                    entity.LoanTermCount = reader.GetInt32(reader.GetOrdinal("LoanTermCount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanPurpose"))
                {
                    entity.LoanPurpose = reader.GetString(reader.GetOrdinal("LoanPurpose"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanUserID"))
                {
                    entity.LoanUserID = reader.GetInt32(reader.GetOrdinal("LoanUserID"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanOwnerKey"))
                {
                    entity.LoanOwnerKey = reader.GetString(reader.GetOrdinal("LoanOwnerKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanOwnerType"))
                {
                    entity.LoanOwnerType = (LoanOwnerTypes)reader.GetInt32(reader.GetOrdinal("LoanOwnerType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanOwnerAddon"))
                {
                    entity.LoanOwnerAddon = reader.GetString(reader.GetOrdinal("LoanOwnerAddon"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanDate"))
                {
                    entity.LoanDate = reader.GetDateTime(reader.GetOrdinal("LoanDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanStatus"))
                {
                    entity.LoanStatus = (LoanStatuses)reader.GetInt32(reader.GetOrdinal("LoanStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CheckUserID"))
                {
                    entity.CheckUserID = reader.GetGuid(reader.GetOrdinal("CheckUserID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CheckDate"))
                {
                    entity.CheckDate = reader.GetDateTime(reader.GetOrdinal("CheckDate"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanUserRealName"))
                {
                    entity.LoanUserRealName = reader.GetString(reader.GetOrdinal("LoanUserRealName"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanUserRealID"))
                {
                    entity.LoanUserRealID = reader.GetGuid(reader.GetOrdinal("LoanUserRealID"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ReadDate"))
                {
                    entity.ReadDate = reader.GetDateTime(reader.GetOrdinal("ReadDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ReadUserID"))
                {
                    entity.ReadUserID = reader.GetGuid(reader.GetOrdinal("ReadUserID"));
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

        #region 演示方法
        /// <summary>
        /// 此方法仅仅为了演示，使用扩展的IDAL（即ILoanBasicDAL）可以实现除CURD外独特的数据访问功能
        /// </summary>
        /// <returns></returns>
        public int GetCountTest()
        {
            return 99;
        }
        #endregion
    }
}
