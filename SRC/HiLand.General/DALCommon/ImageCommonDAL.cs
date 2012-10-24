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
    public class ImageCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<ImageEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralImage"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "ImageGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "ImageGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_Image_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(ImageEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.ImageGuid == Guid.Empty)
            {
                entity.ImageGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralImage] (
			        [ImageGuid],
			        [ImageName],
			        [RelativeGuid],
			        [ImageCategoryCode],
			        [ImageKind],
			        [OwnerGuid],
			        [ImageRelativePath],
			        [ImageSize],
			        [ImageWidth],
			        [ImageHeight],
			        [ImageStatus],
			        [ImageOrder],
			        [ImageIsMain],
			        [CanUsable],
			        [ImageType],
			        [CreateTime],
			        [ImageDescription],
			        [ImageDownCount],
			        [ImageDisplayCount],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        @ImageGuid,
			        @ImageName,
			        @RelativeGuid,
			        @ImageCategoryCode,
			        @ImageKind,
			        @OwnerGuid,
			        @ImageRelativePath,
			        @ImageSize,
			        @ImageWidth,
			        @ImageHeight,
			        @ImageStatus,
			        @ImageOrder,
			        @ImageIsMain,
			        @CanUsable,
			        @ImageType,
			        @CreateTime,
			        @ImageDescription,
			        @ImageDownCount,
			        @ImageDisplayCount,
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
        public override bool Update(ImageEntity entity)
        {
            string commandText = @"Update [GeneralImage] Set   
					[ImageGuid] = @ImageGuid,
					[ImageName] = @ImageName,
					[RelativeGuid] = @RelativeGuid,
					[ImageCategoryCode] = @ImageCategoryCode,
					[ImageKind] = @ImageKind,
					[OwnerGuid] = @OwnerGuid,
					[ImageRelativePath] = @ImageRelativePath,
					[ImageSize] = @ImageSize,
					[ImageWidth] = @ImageWidth,
					[ImageHeight] = @ImageHeight,
					[ImageStatus] = @ImageStatus,
					[ImageOrder] = @ImageOrder,
					[ImageIsMain] = @ImageIsMain,
					[CanUsable] = @CanUsable,
					[ImageType] = @ImageType,
					[CreateTime] = @CreateTime,
					[ImageDescription] = @ImageDescription,
					[ImageDownCount] = @ImageDownCount,
					[ImageDisplayCount] = @ImageDisplayCount,
					[PropertyNames] = @PropertyNames,
					[PropertyValues] = @PropertyValues
             Where [ImageGuid] = @ImageGuid";

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
        protected override void InnerPrepareParasAll(ImageEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("ImageGuid",entity.ImageGuid==Guid.Empty?GuidHelper.NewGuid():entity.ImageGuid),
			    GenerateParameter("ImageName",entity.ImageName??string.Empty),
			    GenerateParameter("RelativeGuid",entity.RelativeGuid),
			    GenerateParameter("ImageCategoryCode",entity.ImageCategoryCode??string.Empty),
			    GenerateParameter("ImageKind",entity.ImageKind??string.Empty),
			    GenerateParameter("OwnerGuid",entity.OwnerGuid),
			    GenerateParameter("ImageRelativePath",entity.ImageRelativePath??string.Empty),
			    GenerateParameter("ImageSize",entity.ImageSize),
			    GenerateParameter("ImageWidth",entity.ImageWidth),
			    GenerateParameter("ImageHeight",entity.ImageHeight),
			    GenerateParameter("ImageStatus",(Logics)entity.ImageStatus),
			    GenerateParameter("ImageOrder",entity.ImageOrder),
			    GenerateParameter("ImageIsMain",(Logics)entity.ImageIsMain),
			    GenerateParameter("CanUsable",(Logics)entity.CanUsable),
			    GenerateParameter("ImageType",entity.ImageType??string.Empty),
			    GenerateParameter("CreateTime",entity.CreateTime),
			    GenerateParameter("ImageDescription",entity.ImageDescription??string.Empty),
			    GenerateParameter("ImageDownCount",entity.ImageDownCount),
			    GenerateParameter("ImageDisplayCount",entity.ImageDisplayCount)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected override void InnerLoad(IDataReader reader, ref ImageEntity entity)
        {
            if (reader != null && reader.IsClosed == false && entity != null)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageID"))
                {
                    entity.ImageID = reader.GetInt32(reader.GetOrdinal("ImageID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageGuid"))
                {
                    entity.ImageGuid = reader.GetGuid(reader.GetOrdinal("ImageGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageName"))
                {
                    entity.ImageName = reader.GetString(reader.GetOrdinal("ImageName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "RelativeGuid"))
                {
                    entity.RelativeGuid = reader.GetGuid(reader.GetOrdinal("RelativeGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageCategoryCode"))
                {
                    entity.ImageCategoryCode = reader.GetString(reader.GetOrdinal("ImageCategoryCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageKind"))
                {
                    entity.ImageKind = reader.GetString(reader.GetOrdinal("ImageKind"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "OwnerGuid"))
                {
                    entity.OwnerGuid = reader.GetGuid(reader.GetOrdinal("OwnerGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageRelativePath"))
                {
                    entity.ImageRelativePath = reader.GetString(reader.GetOrdinal("ImageRelativePath"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageSize"))
                {
                    entity.ImageSize = reader.GetInt32(reader.GetOrdinal("ImageSize"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageWidth"))
                {
                    entity.ImageWidth = reader.GetInt32(reader.GetOrdinal("ImageWidth"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageHeight"))
                {
                    entity.ImageHeight = reader.GetInt32(reader.GetOrdinal("ImageHeight"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageStatus"))
                {
                    entity.ImageStatus = (Logics)reader.GetInt32(reader.GetOrdinal("ImageStatus"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageOrder"))
                {
                    entity.ImageOrder = reader.GetInt32(reader.GetOrdinal("ImageOrder"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageIsMain"))
                {
                    entity.ImageIsMain = (Logics)reader.GetInt32(reader.GetOrdinal("ImageIsMain"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageType"))
                {
                    entity.ImageType = reader.GetString(reader.GetOrdinal("ImageType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CreateTime"))
                {
                    entity.CreateTime = reader.GetDateTime(reader.GetOrdinal("CreateTime"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageDescription"))
                {
                    entity.ImageDescription = reader.GetString(reader.GetOrdinal("ImageDescription"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageDownCount"))
                {
                    entity.ImageDownCount = reader.GetInt32(reader.GetOrdinal("ImageDownCount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "ImageDisplayCount"))
                {
                    entity.ImageDisplayCount = reader.GetInt32(reader.GetOrdinal("ImageDisplayCount"));
                }
            }
        }
        #endregion
    }
}
