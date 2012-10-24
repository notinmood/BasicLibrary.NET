using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class EnterpriseDAL : EnterpriseCommonDAL< SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {
        //        #region 基本信息
        //        /// <summary>
        //        /// 实体对应主表的名称
        //        /// </summary>
        //        protected override string TableName
        //        {
        //            get { return "GeneralEnterprise"; }
        //        }

        //        /// <summary>
        //        /// 主键名称
        //        /// </summary>
        //        protected override string[] KeyNames
        //        {
        //            get { return new string[] { "EnterpriseGuid" }; }
        //        }

        //        /// <summary>
        //        /// 分页存储过程的名字
        //        /// </summary>
        //        protected override string PagingSPName
        //        {
        //            get { return "usp_General_Enterprise_SelectPaging"; }
        //        }
        //        #endregion

        //        #region 逻辑操作
        //        public override bool Create(EnterpriseEntity entity)
        //        {
        //            string commandText = @"Insert Into [GeneralEnterprise] (
        //			        [EnterpriseGuid],
        //			        [CompanyName],
        //			        [BusinessType],
        //			        [TradingName],
        //			        [Industry],
        //			        [EnterpriseCode],
        //			        [TaxCode],
        //			        [PrincipleAddress],
        //			        [PostCode],
        //			        [Telephone],
        //			        [Fax],
        //			        [Email],
        //			        [EstablishedYears],
        //			        [EstablishedTime],
        //			        [GrossIncome],
        //			        [Profit],
        //			        [AssociatedEnterpriseGuid],
        //			        [ContactPerson],
        //			        [AreaCode],
        //			        [CompanyNameShort],
        //			        [CanUsable],
        //			        [Longitude],
        //			        [Lantitude],
        //                    [BrokerKey],
        //			        [PropertyNames],
        //			        [PropertyValues]
        //                ) 
        //                Values (
        //			        @EnterpriseGuid,
        //			        @CompanyName,
        //			        @BusinessType,
        //			        @TradingName,
        //			        @Industry,
        //			        @EnterpriseCode,
        //			        @TaxCode,
        //			        @PrincipleAddress,
        //			        @PostCode,
        //			        @Telephone,
        //			        @Fax,
        //			        @Email,
        //			        @EstablishedYears,
        //			        @EstablishedTime,
        //			        @GrossIncome,
        //			        @Profit,
        //			        @AssociatedEnterpriseGuid,
        //			        @ContactPerson,
        //			        @AreaCode,
        //			        @CompanyNameShort,
        //			        @CanUsable,
        //			        @Longitude,
        //			        @Lantitude,
        //                    @BrokerKey,
        //			        @PropertyNames,
        //			        @PropertyValues
        //                )";

        //            SqlParameter[] sqlParas = PrepareParasAll(entity);

        //            bool isSuccessful = SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
        //            return isSuccessful;
        //        }

        //        public override bool Update(EnterpriseEntity entity)
        //        {
        //            string commandText = @"Update [GeneralEnterprise] Set   
        //					[EnterpriseGuid] = @EnterpriseGuid,
        //					[CompanyName] = @CompanyName,
        //					[BusinessType] = @BusinessType,
        //					[TradingName] = @TradingName,
        //					[Industry] = @Industry,
        //					[EnterpriseCode] = @EnterpriseCode,
        //					[TaxCode] = @TaxCode,
        //					[PrincipleAddress] = @PrincipleAddress,
        //					[PostCode] = @PostCode,
        //					[Telephone] = @Telephone,
        //					[Fax] = @Fax,
        //					[Email] = @Email,
        //					[EstablishedYears] = @EstablishedYears,
        //					[EstablishedTime] = @EstablishedTime,
        //					[GrossIncome] = @GrossIncome,
        //					[Profit] = @Profit,
        //					[AssociatedEnterpriseGuid] = @AssociatedEnterpriseGuid,
        //					[ContactPerson] = @ContactPerson,
        //					[AreaCode] = @AreaCode,
        //					[CompanyNameShort] = @CompanyNameShort,
        //					[CanUsable] = @CanUsable,
        //					[Longitude] = @Longitude,
        //					[Lantitude] = @Lantitude,
        //                    [BrokerKey]= @BrokerKey,
        //					[PropertyNames] = @PropertyNames,
        //					[PropertyValues] = @PropertyValues
        //             Where [EnterpriseGuid] = @EnterpriseGuid";

        //            SqlParameter[] sqlParas = PrepareParasAll(entity);

        //            bool isSuccessful = SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
        //            return isSuccessful;
        //        }
        //        #endregion

        //        #region 辅助方法
        //        protected override void InnerPrepareParasAll(EnterpriseEntity entity, ref List<SqlParameter> paraList)
        //        {
        //            List<SqlParameter> list = new List<SqlParameter>()
        //            {
        //                new SqlParameter("@EnterpriseGuid",entity.EnterpriseGuid== Guid.Empty?Guid.NewGuid():entity.EnterpriseGuid),
        //                new SqlParameter("@CompanyName",entity.CompanyName??string.Empty),
        //                new SqlParameter("@BusinessType",(int)entity.BusinessType),
        //                new SqlParameter("@TradingName",entity.TradingName??string.Empty),
        //                new SqlParameter("@Industry",entity.Industry??string.Empty),
        //                new SqlParameter("@EnterpriseCode",entity.EnterpriseCode??string.Empty),
        //                new SqlParameter("@TaxCode",entity.TaxCode??string.Empty),
        //                new SqlParameter("@PrincipleAddress",entity.PrincipleAddress??string.Empty),
        //                new SqlParameter("@PostCode",entity.PostCode??string.Empty),
        //                new SqlParameter("@Telephone",entity.Telephone??string.Empty),
        //                new SqlParameter("@Fax",entity.Fax??string.Empty),
        //                new SqlParameter("@Email",entity.Email??string.Empty),
        //                new SqlParameter("@EstablishedYears",entity.EstablishedYears),
        //                new SqlParameter("@EstablishedTime",entity.EstablishedTime),
        //                new SqlParameter("@GrossIncome",entity.GrossIncome),
        //                new SqlParameter("@Profit",entity.Profit),
        //                new SqlParameter("@AssociatedEnterpriseGuid",entity.AssociatedEnterpriseGuid),
        //                new SqlParameter("@ContactPerson",entity.ContactPerson??string.Empty),
        //                new SqlParameter("@AreaCode",entity.AreaCode??string.Empty),
        //                new SqlParameter("@CompanyNameShort",entity.CompanyNameShort??string.Empty),
        //                new SqlParameter("@CanUsable",(int)entity.CanUsable),
        //                new SqlParameter("@Longitude",entity.Longitude),
        //                new SqlParameter("@Lantitude",entity.Lantitude),
        //                new SqlParameter("@BrokerKey",entity.BrokerKey??string.Empty)
        //            };

        //            paraList.AddRange(list);
        //        }

        //        protected override EnterpriseEntity Load(IDataReader reader)
        //        {
        //            EnterpriseEntity entity = new EnterpriseEntity();
        //            if (reader != null && reader.IsClosed == false)
        //            {
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseID"))
        //                {
        //                    entity.EnterpriseID = reader.GetInt32(reader.GetOrdinal("EnterpriseID"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseGuid"))
        //                {
        //                    entity.EnterpriseGuid = reader.GetGuid(reader.GetOrdinal("EnterpriseGuid"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CompanyName"))
        //                {
        //                    entity.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "BusinessType"))
        //                {
        //                    entity.BusinessType = (EnterpriseTypes)reader.GetInt32(reader.GetOrdinal("BusinessType"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TradingName"))
        //                {
        //                    entity.TradingName = reader.GetString(reader.GetOrdinal("TradingName"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Industry"))
        //                {
        //                    entity.Industry = reader.GetString(reader.GetOrdinal("Industry"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EnterpriseCode"))
        //                {
        //                    entity.EnterpriseCode = reader.GetString(reader.GetOrdinal("EnterpriseCode"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "TaxCode"))
        //                {
        //                    entity.TaxCode = reader.GetString(reader.GetOrdinal("TaxCode"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PrincipleAddress"))
        //                {
        //                    entity.PrincipleAddress = reader.GetString(reader.GetOrdinal("PrincipleAddress"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PostCode"))
        //                {
        //                    entity.PostCode = reader.GetString(reader.GetOrdinal("PostCode"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Telephone"))
        //                {
        //                    entity.Telephone = reader.GetString(reader.GetOrdinal("Telephone"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Fax"))
        //                {
        //                    entity.Fax = reader.GetString(reader.GetOrdinal("Fax"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Email"))
        //                {
        //                    entity.Email = reader.GetString(reader.GetOrdinal("Email"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EstablishedYears"))
        //                {
        //                    entity.EstablishedYears = reader.GetInt32(reader.GetOrdinal("EstablishedYears"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "EstablishedTime"))
        //                {
        //                    entity.EstablishedTime = reader.GetDateTime(reader.GetOrdinal("EstablishedTime"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "GrossIncome"))
        //                {
        //                    entity.GrossIncome = reader.GetDecimal(reader.GetOrdinal("GrossIncome"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Profit"))
        //                {
        //                    entity.Profit = reader.GetDecimal(reader.GetOrdinal("Profit"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AssociatedEnterpriseGuid"))
        //                {
        //                    entity.AssociatedEnterpriseGuid = reader.GetGuid(reader.GetOrdinal("AssociatedEnterpriseGuid"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ContactPerson"))
        //                {
        //                    entity.ContactPerson = reader.GetString(reader.GetOrdinal("ContactPerson"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "AreaCode"))
        //                {
        //                    entity.AreaCode = reader.GetString(reader.GetOrdinal("AreaCode"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CompanyNameShort"))
        //                {
        //                    entity.CompanyNameShort = reader.GetString(reader.GetOrdinal("CompanyNameShort"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
        //                {
        //                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Longitude"))
        //                {
        //                    entity.Longitude = reader.GetDecimal(reader.GetOrdinal("Longitude"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "Lantitude"))
        //                {
        //                    entity.Lantitude = reader.GetDecimal(reader.GetOrdinal("Lantitude"));
        //                }
        //                if (DataReaderHelper.IsExistField(reader, "BrokerKey") && Convert.IsDBNull(reader["BrokerKey"]) == false)
        //                {
        //                    entity.BrokerKey = reader.GetString(reader.GetOrdinal("BrokerKey"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PropertyNames"))
        //                {
        //                    entity.PropertyNames = reader.GetString(reader.GetOrdinal("PropertyNames"));
        //                }
        //                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PropertyValues"))
        //                {
        //                    entity.PropertyValues = reader.GetString(reader.GetOrdinal("PropertyValues"));
        //                }
        //            }
        //            return entity;
        //        }
        //        #endregion
    }
}
