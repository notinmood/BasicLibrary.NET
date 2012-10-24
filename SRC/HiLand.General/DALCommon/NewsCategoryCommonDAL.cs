using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;

namespace HiLand.General.DALCommon
{
    public class NewsCategoryCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<NewsCategoryEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralNewsCategory"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "NewsCategoryCode" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return ""; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_NewsCategory_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(NewsCategoryEntity entity)
        {
            string commandText = @"Insert Into [GeneralNewsCategory] (
			        [NewsCategoryCode],
			        [NewsCategoryName],
			        [CanUsable]
                ) 
                Values (
			        @NewsCategoryCode,
			        @NewsCategoryName,
			        @CanUsable
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
        public override bool Update(NewsCategoryEntity entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 内部准备（为实体准备数据访问的参数）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="paraList"></param>
        protected override void InnerPrepareParasAll(NewsCategoryEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>() 
            {
			    GenerateParameter("NewsCategoryID",entity.NewsCategoryID),
			    GenerateParameter("NewsCategoryName",entity.NewsCategoryName??string.Empty),
			    GenerateParameter("CanUsable",(int)entity.CanUsable)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override NewsCategoryEntity Load(IDataReader reader)
        {
            NewsCategoryEntity entity = new NewsCategoryEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsCategoryID"))
                {
                    entity.NewsCategoryID = reader.GetInt32(reader.GetOrdinal("NewsCategoryID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsCategoryCode"))
                {
                    entity.NewsCategoryCode = reader.GetString(reader.GetOrdinal("NewsCategoryCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsCategoryName"))
                {
                    entity.NewsCategoryName = reader.GetString(reader.GetOrdinal("NewsCategoryName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
            }
            return entity;
        }
        #endregion
    }
}
