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
    public class SimpleProductCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<SimpleProductEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "SimpleProduct"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "ProductGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ProductGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_Simple_Product_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        public override SimpleProductEntity Get(string modelID)
        {
            string commandText = string.Format("SELECT A.*,B.ProductCategoryName FROM [{0}] as A LEFT JOIN [SimpleProductCategory] as B ON A.productCategoryCode=B.productCategoryCode  WHERE  {1}", TableName, GetKeysWhereClause());
            TParameter[] sqlParas = GetKeyParameters(modelID);
            return CommonGeneralInstance.GetEntity<SimpleProductEntity>(commandText, sqlParas, Load);
        }

        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(SimpleProductEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ProductGuid == Guid.Empty)
            {
                entity.ProductGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [SimpleProduct] (
			    [ProductGuid],
			    [ProductCode],
			    [ProductName],
                [ProductBrand],
			    [ProductCategoryCode],
			    [ProductPriceNormal],
			    [ProductPricePromotion],
			    [ProductPriceReference],
			    [ProductAddress],
			    [ProductPackegUnit],
			    [ProductMaterial],
			    [ProductCountRepository],
			    [ProductCountSaled],
			    [ProductHasInvoice],
			    [ProductIsHot],
			    [ProductIsTop],
			    [ProductStatus],
                [CanUsable],
			    [ProductSpecification],
			    [ProductMemo],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    @ProductGuid,
			    @ProductCode,
			    @ProductName,
                @ProductBrand,
			    @ProductCategoryCode,
			    @ProductPriceNormal,
			    @ProductPricePromotion,
			    @ProductPriceReference,
			    @ProductAddress,
			    @ProductPackegUnit,
			    @ProductMaterial,
			    @ProductCountRepository,
			    @ProductCountSaled,
			    @ProductHasInvoice,
			    @ProductIsHot,
			    @ProductIsTop,
			    @ProductStatus,
                @CanUsable,
			    @ProductSpecification,
			    @ProductMemo,
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
        public override bool Update(SimpleProductEntity entity)
        {
            string commandText = @"Update [SimpleProduct] Set   
					[ProductGuid] = @ProductGuid,
					[ProductCode] = @ProductCode,
					[ProductName] = @ProductName,
                    [ProductBrand]= @ProductBrand,
					[ProductCategoryCode] = @ProductCategoryCode,
					[ProductPriceNormal] = @ProductPriceNormal,
					[ProductPricePromotion] = @ProductPricePromotion,
					[ProductPriceReference] = @ProductPriceReference,
					[ProductAddress] = @ProductAddress,
					[ProductPackegUnit] = @ProductPackegUnit,
					[ProductMaterial] = @ProductMaterial,
					[ProductCountRepository] = @ProductCountRepository,
					[ProductCountSaled] = @ProductCountSaled,
					[ProductHasInvoice] = @ProductHasInvoice,
					[ProductIsHot] = @ProductIsHot,
					[ProductIsTop] = @ProductIsTop,
					[ProductStatus] = @ProductStatus,
                    [CanUsable]= @CanUsable,
					[ProductSpecification] = @ProductSpecification,
					[ProductMemo] = @ProductMemo,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [ProductGuid] = @ProductGuid";

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
        protected override void InnerPrepareParasAll(SimpleProductEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("ProductGuid",entity.ProductGuid == Guid.Empty? GuidHelper.NewGuid():entity.ProductGuid),
			    GenerateParameter("ProductCode",entity.ProductCode??string.Empty),
			    GenerateParameter("ProductName",entity.ProductName??string.Empty),
                GenerateParameter("ProductBrand",entity.ProductBrand??string.Empty),
			    GenerateParameter("ProductCategoryCode",entity.ProductCategoryCode??string.Empty),
			    GenerateParameter("ProductPriceNormal",entity.ProductPriceNormal),
			    GenerateParameter("ProductPricePromotion",entity.ProductPricePromotion),
			    GenerateParameter("ProductPriceReference",entity.ProductPriceReference),
			    GenerateParameter("ProductAddress",entity.ProductAddress??string.Empty),
			    GenerateParameter("ProductPackegUnit",entity.ProductPackegUnit??string.Empty),
			    GenerateParameter("ProductMaterial",entity.ProductMaterial??string.Empty),
			    GenerateParameter("ProductCountRepository",entity.ProductCountRepository),
			    GenerateParameter("ProductCountSaled",entity.ProductCountSaled),
			    GenerateParameter("ProductHasInvoice",entity.ProductHasInvoice),
			    GenerateParameter("ProductIsHot",(int)entity.ProductIsHot),
			    GenerateParameter("ProductIsTop",(int)entity.ProductIsTop),
			    GenerateParameter("ProductStatus",(int)entity.ProductStatus),
                GenerateParameter("CanUsable",(int)entity.CanUsable),
			    GenerateParameter("ProductSpecification",entity.ProductSpecification??string.Empty),
			    GenerateParameter("ProductMemo",entity.ProductMemo??string.Empty)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref SimpleProductEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductID"))
                {
                    entity.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductGuid"))
                {
                    entity.ProductGuid = reader.GetGuid(reader.GetOrdinal("ProductGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCode"))
                {
                    entity.ProductCode = reader.GetString(reader.GetOrdinal("ProductCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductName"))
                {
                    entity.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductBrand"))
                {
                    entity.ProductBrand = reader.GetString(reader.GetOrdinal("ProductBrand"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryCode"))
                {
                    entity.ProductCategoryCode = reader.GetString(reader.GetOrdinal("ProductCategoryCode"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCategoryName"))
                {
                    entity.ProductCategoryName = reader.GetString(reader.GetOrdinal("ProductCategoryName"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductPriceNormal"))
                {
                    entity.ProductPriceNormal = reader.GetDecimal(reader.GetOrdinal("ProductPriceNormal"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductPricePromotion"))
                {
                    entity.ProductPricePromotion = reader.GetDecimal(reader.GetOrdinal("ProductPricePromotion"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductPriceReference"))
                {
                    entity.ProductPriceReference = reader.GetDecimal(reader.GetOrdinal("ProductPriceReference"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductAddress"))
                {
                    entity.ProductAddress = reader.GetString(reader.GetOrdinal("ProductAddress"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductPackegUnit"))
                {
                    entity.ProductPackegUnit = reader.GetString(reader.GetOrdinal("ProductPackegUnit"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductMaterial"))
                {
                    entity.ProductMaterial = reader.GetString(reader.GetOrdinal("ProductMaterial"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCountRepository"))
                {
                    entity.ProductCountRepository = reader.GetInt32(reader.GetOrdinal("ProductCountRepository"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductCountSaled"))
                {
                    entity.ProductCountSaled = reader.GetInt32(reader.GetOrdinal("ProductCountSaled"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductHasInvoice"))
                {
                    entity.ProductHasInvoice = reader.GetInt32(reader.GetOrdinal("ProductHasInvoice"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductIsHot"))
                {
                    entity.ProductIsHot = (Logics)reader.GetInt32(reader.GetOrdinal("ProductIsHot"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductIsTop"))
                {
                    entity.ProductIsTop = (Logics)reader.GetInt32(reader.GetOrdinal("ProductIsTop"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductStatus"))
                {
                    entity.ProductStatus = (Logics)reader.GetInt32(reader.GetOrdinal("ProductStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductSpecification"))
                {
                    entity.ProductSpecification = reader.GetString(reader.GetOrdinal("ProductSpecification"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ProductMemo"))
                {
                    entity.ProductMemo = reader.GetString(reader.GetOrdinal("ProductMemo"));
                }
            }
        }
        #endregion
    }
}
