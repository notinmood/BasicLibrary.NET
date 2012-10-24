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
    public class ShoppingCartCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<ShoppingCartEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralShoppingCart"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "ShoppingItemGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ShoppingItemGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_ShoppingCart_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(ShoppingCartEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ShoppingItemGuid == Guid.Empty)
            {
                entity.ShoppingItemGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralShoppingCart] (
			        [ShoppingItemGuid],
			        [ProductKey],
			        [ProductName],
			        [ProductPrice],
			        [ProductQuantity],
			        [OwnerKey],
			        [IsTempOwner],
                    [IsFavoriteItem],
			        [ShoppingItemMemo],
			        [CreateTime],
			        [CanUsable],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        @ShoppingItemGuid,
			        @ProductKey,
			        @ProductName,
			        @ProductPrice,
			        @ProductQuantity,
			        @OwnerKey,
			        @IsTempOwner,
                    @IsFavoriteItem,
			        @ShoppingItemMemo,
			        @CreateTime,
			        @CanUsable,
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
        public override bool Update(ShoppingCartEntity entity)
        {
            string commandText = @"Update [GeneralShoppingCart] Set   
					[ShoppingItemGuid] = @ShoppingItemGuid,
					[ProductKey] = @ProductKey,
					[ProductName] = @ProductName,
					[ProductPrice] = @ProductPrice,
					[ProductQuantity] = @ProductQuantity,
					[OwnerKey] = @OwnerKey,
					[IsTempOwner] = @IsTempOwner,
                    [IsFavoriteItem]= @IsFavoriteItem,
					[ShoppingItemMemo] = @ShoppingItemMemo,
					[CreateTime] = @CreateTime,
					[CanUsable] = @CanUsable,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [ShoppingItemGuid] = @ShoppingItemGuid";

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
        protected override void InnerPrepareParasAll(ShoppingCartEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
			    GenerateParameter("ShoppingItemGuid",entity.ShoppingItemGuid== Guid.Empty?Guid.NewGuid():entity.ShoppingItemGuid),
			    GenerateParameter("ProductKey",entity.ProductKey?? String.Empty),
			    GenerateParameter("ProductName",entity.ProductName?? String.Empty),
			    GenerateParameter("ProductPrice",entity.ProductPrice),
			    GenerateParameter("ProductQuantity",entity.ProductQuantity),
			    GenerateParameter("OwnerKey",entity.OwnerKey?? String.Empty),
			    GenerateParameter("IsTempOwner",(int)entity.IsTempOwner),
                GenerateParameter("IsFavoriteItem",(int)entity.IsFavoriteItem),
			    GenerateParameter("ShoppingItemMemo",entity.ShoppingItemMemo?? String.Empty),
			    GenerateParameter("CreateTime",entity.CreateTime),
			    GenerateParameter("CanUsable",(int)entity.CanUsable)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref ShoppingCartEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ShoppingItemID"))
                {
                    entity.ShoppingItemID = reader.GetInt32(reader.GetOrdinal("ShoppingItemID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ShoppingItemGuid"))
                {
                    entity.ShoppingItemGuid = reader.GetGuid(reader.GetOrdinal("ShoppingItemGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductKey"))
                {
                    entity.ProductKey = reader.GetString(reader.GetOrdinal("ProductKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductName"))
                {
                    entity.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductPrice"))
                {
                    entity.ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductQuantity"))
                {
                    entity.ProductQuantity = reader.GetInt32(reader.GetOrdinal("ProductQuantity"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerKey"))
                {
                    entity.OwnerKey = reader.GetString(reader.GetOrdinal("OwnerKey"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsTempOwner"))
                {
                    entity.IsTempOwner = (Logics)reader.GetInt32(reader.GetOrdinal("IsTempOwner"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "IsFavoriteItem"))
                {
                    entity.IsFavoriteItem = (Logics)reader.GetInt32(reader.GetOrdinal("IsFavoriteItem"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ShoppingItemMemo"))
                {
                    entity.ShoppingItemMemo = reader.GetString(reader.GetOrdinal("ShoppingItemMemo"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateTime"))
                {
                    entity.CreateTime = reader.GetDateTime(reader.GetOrdinal("CreateTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
            }
        }
        #endregion
    }
}
