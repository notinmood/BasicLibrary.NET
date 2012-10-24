using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Data;

namespace HiLand.General.DALCommon
{
    public class SimpleProductCategoryCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<SimpleProductCategoryEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "SimpleProductCategory"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "ProductCategoryGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ProductCategoryGuid"; }
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
        public override bool Create(SimpleProductCategoryEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ProductCategoryGuid == Guid.Empty)
            {
                entity.ProductCategoryGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [SimpleProductCategory] (
			[ProductCategoryGuid],
			[ProductCategoryCode],
			[ProductCategoryName],
			[ProductCategoryStatus],
            [CanUsable],
			[ProductCategoryMemo],
			[ProductCategoryOrder],
			[ProductCount],
			[PropertyNames],
			[PropertyValues]
        ) 
        Values (
			@ProductCategoryGuid,
			@ProductCategoryCode,
			@ProductCategoryName,
			@ProductCategoryStatus,
            @CanUsable,
			@ProductCategoryMemo,
			@ProductCategoryOrder,
			@ProductCount,
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
        public override bool Update(SimpleProductCategoryEntity entity)
        {
            string commandText = @"Update [SimpleProductCategory] Set   
					[ProductCategoryGuid] = @ProductCategoryGuid,
					[ProductCategoryCode] = @ProductCategoryCode,
					[ProductCategoryName] = @ProductCategoryName,
					[ProductCategoryStatus] = @ProductCategoryStatus,
                    [CanUsable]= @CanUsable,
					[ProductCategoryMemo] = @ProductCategoryMemo,
					[ProductCategoryOrder] = @ProductCategoryOrder,
					[ProductCount] = @ProductCount,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [ProductCategoryGuid] = @ProductCategoryGuid";

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
        protected override void InnerPrepareParasAll(SimpleProductCategoryEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("ProductCategoryGuid",entity.ProductCategoryGuid==Guid.Empty?GuidHelper.NewGuid():entity.ProductCategoryGuid),
			    GenerateParameter("ProductCategoryCode",entity.ProductCategoryCode??string.Empty),
			    GenerateParameter("ProductCategoryName",entity.ProductCategoryName??string.Empty),
			    GenerateParameter("ProductCategoryStatus",(int)entity.ProductCategoryStatus),
                GenerateParameter("CanUsable",(int)entity.CanUsable),
			    GenerateParameter("ProductCategoryMemo",entity.ProductCategoryMemo??string.Empty),
			    GenerateParameter("ProductCategoryOrder",entity.ProductCategoryOrder),
			    GenerateParameter("ProductCount",entity.ProductCount),
			    GenerateParameter("PropertyNames",entity.PropertyNames),
			    GenerateParameter("PropertyValues",entity.PropertyValues)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref SimpleProductCategoryEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryID"))
                {
                    entity.ProductCategoryID = reader.GetInt32(reader.GetOrdinal("ProductCategoryID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryGuid"))
                {
                    entity.ProductCategoryGuid = reader.GetGuid(reader.GetOrdinal("ProductCategoryGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryCode"))
                {
                    entity.ProductCategoryCode = reader.GetString(reader.GetOrdinal("ProductCategoryCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryName"))
                {
                    entity.ProductCategoryName = reader.GetString(reader.GetOrdinal("ProductCategoryName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryStatus"))
                {
                    entity.ProductCategoryStatus = (Logics)reader.GetInt32(reader.GetOrdinal("ProductCategoryStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryMemo"))
                {
                    entity.ProductCategoryMemo = reader.GetString(reader.GetOrdinal("ProductCategoryMemo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryOrder"))
                {
                    entity.ProductCategoryOrder = reader.GetInt32(reader.GetOrdinal("ProductCategoryOrder"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCount"))
                {
                    entity.ProductCount = reader.GetInt32(reader.GetOrdinal("ProductCount"));
                }
            }
        }
        #endregion
    }
}
