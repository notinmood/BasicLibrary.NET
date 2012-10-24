using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;

namespace HiLand.General.DALCommon
{
    public class BasicSettingCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter> : BaseDAL<BasicSettingEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralBasicSetting"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "SettingID" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return ""; }
        }

        //TODO:此存储过程尚未完成
        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_BasicSetting_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(BasicSettingEntity entity)
        {
            string commandText = string.Format(@"Insert Into [GeneralBasicSetting] (
			        [SettingKey],
			        [SettingValue],
			        [SettingDesc],
                    [SettingCategory],
			        [DisplayName],
			        [OrderNumber],
			        [CanUsable],
			        [IsInnerSetting]
                ) 
                Values (
			        {0}SettingKey,
			        {0}SettingValue,
			        {0}SettingDesc,
                    {0}SettingCategory,
			        {0}DisplayName,
			        {0}OrderNumber,
			        {0}CanUsable,
			        {0}IsInnerSetting
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
        public override bool Update(BasicSettingEntity entity)
        {
            string commandText = string.Format(@"Update [GeneralBasicSetting] Set   
					[SettingKey] = {0}SettingKey,
					[SettingValue] = {0}SettingValue,
					[SettingDesc] = {0}SettingDesc,
                    [SettingCategory]={0}SettingCategory,
					[DisplayName] = {0}DisplayName,
					[OrderNumber] = {0}OrderNumber,
					[CanUsable] = {0}CanUsable,
					[IsInnerSetting] = {0}IsInnerSetting
             Where [SettingID] = {0}SettingID", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 根据配置键名称获取实体信息
        /// </summary>
        /// <param name="settingKey">配置键名称</param>
        /// <returns></returns>
        public BasicSettingEntity GetBySettingKey(string settingKey)
        {
            string commandText = string.Format("SELECT * FROM [GeneralBasicSetting] WHERE  [SettingKey] = {0}SettingKey",ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("SettingKey", settingKey) };
            return CommonGeneralInstance.GetEntity<BasicSettingEntity>(commandText, sqlParas, Load);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 内部准备（为实体准备数据访问的参数）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="paraList"></param>
        protected override void InnerPrepareParasAll(BasicSettingEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("SettingID",entity.SettingID),
                GenerateParameter("SettingKey",entity.SettingKey??string.Empty),
                GenerateParameter("SettingValue",entity.SettingValue??string.Empty),
                GenerateParameter("SettingDesc",entity.SettingDesc??string.Empty),
                GenerateParameter("SettingCategory",entity.SettingCategory??string.Empty),
                GenerateParameter("DisplayName",entity.DisplayName??string.Empty),
                GenerateParameter("OrderNumber",entity.OrderNumber),
                GenerateParameter("CanUsable",(int)entity.CanUsable),
                GenerateParameter("IsInnerSetting",(int)entity.IsInnerSetting)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override BasicSettingEntity Load(IDataReader reader)
        {
            BasicSettingEntity entity = new BasicSettingEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SettingID"))
                {
                    entity.SettingID = reader.GetInt32(reader.GetOrdinal("SettingID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SettingKey"))
                {
                    entity.SettingKey = reader.GetString(reader.GetOrdinal("SettingKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SettingValue"))
                {
                    entity.SettingValue = reader.GetString(reader.GetOrdinal("SettingValue"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SettingDesc"))
                {
                    entity.SettingDesc = reader.GetString(reader.GetOrdinal("SettingDesc"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SettingCategory"))
                {
                    entity.SettingCategory = reader.GetString(reader.GetOrdinal("SettingCategory"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DisplayName"))
                {
                    entity.DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OrderNumber"))
                {
                    entity.OrderNumber = reader.GetInt32(reader.GetOrdinal("OrderNumber"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsInnerSetting"))
                {
                    entity.IsInnerSetting = (Logics)reader.GetInt32(reader.GetOrdinal("IsInnerSetting"));
                }
            }
            return entity;
        }
        #endregion
    }
}
