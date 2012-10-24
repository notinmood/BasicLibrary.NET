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
    public class LoanScheduleCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<LoanScheduleEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralLoanSchedule"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "ScheduleGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ScheduleGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_LoanSchedule_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(LoanScheduleEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ScheduleGuid == Guid.Empty)
            {
                entity.ScheduleGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralLoanSchedule] (
			        [ScheduleGuid],
			        [ScheduleNo],
			        [LoanGuid],
			        [Principal],
			        [Interest],
			        [Penalty],
			        [LateCharge],
                    [OtherFee],
			        [PrincipalPaid],
			        [InterestPaid],
			        [PenaltyPaid],
			        [LateChargePaid],
                    [OtherFeePaid],
                    [PrincipalBalance],
			        [TotalBalance],
			        [PaymentDate],
			        [PaidDate],
                    [ScheduleTimes],
                    [ScheduleParentGuid],
                    [ScheduleStatus],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        @ScheduleGuid,
			        @ScheduleNo,
			        @LoanGuid,
			        @Principal,
			        @Interest,
			        @Penalty,
			        @LateCharge,
                    @OtherFee,
			        @PrincipalPaid,
			        @InterestPaid,
			        @PenaltyPaid,
			        @LateChargePaid,
                    @OtherFeePaid,
                    @PrincipalBalance,
			        @TotalBalance,
			        @PaymentDate,
			        @PaidDate,
                    @ScheduleTimes,
                    @ScheduleParentGuid,
                    @ScheduleStatus,
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
        public override bool Update(LoanScheduleEntity entity)
        {
            string commandText = @"Update [GeneralLoanSchedule] Set   
					[ScheduleGuid] = @ScheduleGuid,
					[ScheduleNo] = @ScheduleNo,
					[LoanGuid] = @LoanGuid,
					[Principal] = @Principal,
					[Interest] = @Interest,
					[Penalty] = @Penalty,
					[LateCharge] = @LateCharge,
                    [OtherFee]=@OtherFee,
					[PrincipalPaid] = @PrincipalPaid,
					[InterestPaid] = @InterestPaid,
					[PenaltyPaid] = @PenaltyPaid,
					[LateChargePaid] = @LateChargePaid,
                    [OtherFeePaid]=@OtherFeePaid,
                    [PrincipalBalance]=@PrincipalBalance,
					[TotalBalance] = @TotalBalance,
					[PaymentDate] = @PaymentDate,
					[PaidDate] = @PaidDate,
                    [ScheduleTimes]=@ScheduleTimes,
                    [ScheduleParentGuid]=@ScheduleParentGuid,
                    [ScheduleStatus]=@ScheduleStatus,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [ScheduleGuid] = @ScheduleGuid";

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
        protected override void InnerPrepareParasAll(LoanScheduleEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>() 
            {
			    GenerateParameter("ScheduleGuid",entity.ScheduleGuid== Guid.Empty?Guid.NewGuid():entity.ScheduleGuid),
			    GenerateParameter("ScheduleNo",entity.ScheduleNo??string.Empty),
			    GenerateParameter("LoanGuid",entity.LoanGuid),
			    GenerateParameter("Principal",entity.Principal),
			    GenerateParameter("Interest",entity.Interest),
			    GenerateParameter("Penalty",entity.Penalty),
                GenerateParameter("OtherFee",entity.OtherFee),
			    GenerateParameter("LateCharge",entity.LateCharge),
			    GenerateParameter("PrincipalPaid",entity.PrincipalPaid),
			    GenerateParameter("InterestPaid",entity.InterestPaid),
			    GenerateParameter("PenaltyPaid",entity.PenaltyPaid),
                GenerateParameter("OtherFeePaid",entity.OtherFeePaid),
			    GenerateParameter("LateChargePaid",entity.LateChargePaid),
                GenerateParameter("PrincipalBalance",entity.PrincipalBalance),
			    GenerateParameter("TotalBalance",entity.TotalBalance),
			    GenerateParameter("PaymentDate",entity.PaymentDate),
			    GenerateParameter("PaidDate",entity.PaidDate),
                GenerateParameter("ScheduleTimes",entity.ScheduleTimes),
                GenerateParameter("ScheduleParentGuid",entity.ScheduleParentGuid),
                GenerateParameter("ScheduleStatus",(int)entity.ScheduleStatus)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override LoanScheduleEntity Load(IDataReader reader)
        {
            LoanScheduleEntity entity = new LoanScheduleEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ScheduleID"))
                {
                    entity.ScheduleID = reader.GetInt32(reader.GetOrdinal("ScheduleID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ScheduleGuid"))
                {
                    entity.ScheduleGuid = reader.GetGuid(reader.GetOrdinal("ScheduleGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ScheduleNo"))
                {
                    entity.ScheduleNo = reader.GetString(reader.GetOrdinal("ScheduleNo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LoanGuid"))
                {
                    entity.LoanGuid = reader.GetGuid(reader.GetOrdinal("LoanGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Principal"))
                {
                    entity.Principal = reader.GetDecimal(reader.GetOrdinal("Principal"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Interest"))
                {
                    entity.Interest = reader.GetDecimal(reader.GetOrdinal("Interest"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Penalty"))
                {
                    entity.Penalty = reader.GetDecimal(reader.GetOrdinal("Penalty"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LateCharge"))
                {
                    entity.LateCharge = reader.GetDecimal(reader.GetOrdinal("LateCharge"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OtherFee"))
                {
                    entity.OtherFee = reader.GetDecimal(reader.GetOrdinal("OtherFee"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PrincipalPaid"))
                {
                    entity.PrincipalPaid = reader.GetDecimal(reader.GetOrdinal("PrincipalPaid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "InterestPaid"))
                {
                    entity.InterestPaid = reader.GetDecimal(reader.GetOrdinal("InterestPaid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PenaltyPaid"))
                {
                    entity.PenaltyPaid = reader.GetDecimal(reader.GetOrdinal("PenaltyPaid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "LateChargePaid"))
                {
                    entity.LateChargePaid = reader.GetDecimal(reader.GetOrdinal("LateChargePaid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OtherFeePaid"))
                {
                    entity.OtherFeePaid = reader.GetDecimal(reader.GetOrdinal("OtherFeePaid"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PrincipalBalance"))
                {
                    entity.PrincipalBalance = reader.GetDecimal(reader.GetOrdinal("PrincipalBalance"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TotalBalance"))
                {
                    entity.TotalBalance = reader.GetDecimal(reader.GetOrdinal("TotalBalance"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PaymentDate"))
                {
                    entity.PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PaidDate"))
                {
                    entity.PaidDate = reader.GetDateTime(reader.GetOrdinal("PaidDate"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ScheduleTimes"))
                {
                    entity.ScheduleTimes = reader.GetInt32(reader.GetOrdinal("ScheduleTimes"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ScheduleParentGuid"))
                {
                    entity.ScheduleParentGuid = reader.GetGuid(reader.GetOrdinal("ScheduleParentGuid"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ScheduleStatus"))
                {
                    entity.ScheduleStatus = (ScheduleStatuses)reader.GetInt32(reader.GetOrdinal("ScheduleStatus"));
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
