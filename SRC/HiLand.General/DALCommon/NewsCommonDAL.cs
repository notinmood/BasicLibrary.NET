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
    public class NewsCommonDAL< TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        : BaseDAL<NewsEntity,  TTransaction, TConnection, TCommand, TDataReader, TParameter>
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
            get { return "GeneralNews"; }
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected override string[] KeyNames
        {
            get { return new string[] { "NewsGuid" }; }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { return "NewsGuid"; }
        }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected override string PagingSPName
        {
            get { return "usp_General_News_SelectPaging"; }
        }
        #endregion

        #region 逻辑操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        public override bool Create(NewsEntity entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.NewsGuid == Guid.Empty)
            {
                entity.NewsGuid = GuidHelper.NewGuid();
            }

            string commandText = @"Insert Into [GeneralNews] (
			[NewsGuid],
			[NewsCategoryCode],
			[NewsTitle],
			[NewsBody],
			[NewsSEOUrl],
			[NewsDate],
			[NewsAuthor],
			[NewsReadCount],
			[NewsRating],
            [CanUsable],
            [NewsIsTop],
			[NewsIsRecommend],
			[NewsPlusCount],
			[NewsMinusCount],
			[NewsOriginalFrom],
			[NewsOriginalUrl],
			[NewsOriginalAuthor],
			[NewsOriginalDate]
        ) 
        Values (
			@NewsGuid,
			@NewsCategoryCode,
			@NewsTitle,
			@NewsBody,
			@NewsSEOUrl,
			@NewsDate,
			@NewsAuthor,
			@NewsReadCount,
			@NewsRating,
            @CanUsable,
            @NewsIsTop,
			@NewsIsRecommend,
			@NewsPlusCount,
			@NewsMinusCount,
			@NewsOriginalFrom,
			@NewsOriginalUrl,
			@NewsOriginalAuthor,
			@NewsOriginalDate
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
        public override bool Update(NewsEntity entity)
        {
            string commandText = @"Update [GeneralNews] Set   
					[NewsCategoryCode] = @NewsCategoryCode,
					[NewsTitle] = @NewsTitle,
					[NewsBody] = @NewsBody,
					[NewsSEOUrl] = @NewsSEOUrl,
					[NewsDate] = @NewsDate,
					[NewsAuthor] = @NewsAuthor,
					[NewsReadCount] = @NewsReadCount,
					[CanUsable] = @CanUsable,
					[NewsIsTop] = @NewsIsTop,
					[NewsIsRecommend] = @NewsIsRecommend,
					[NewsPlusCount] = @NewsPlusCount,
					[NewsMinusCount] = @NewsMinusCount,
					[NewsRating] = @NewsRating,
					[NewsOriginalFrom] = @NewsOriginalFrom,
					[NewsOriginalUrl] = @NewsOriginalUrl,
					[NewsOriginalAuthor] = @NewsOriginalAuthor,
					[NewsOriginalDate] = @NewsOriginalDate
             Where [NewsGuid] = @NewsGuid";

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
        protected override void InnerPrepareParasAll(NewsEntity entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>()
            {
			    GenerateParameter("NewsGuid",entity.NewsGuid== Guid.Empty?Guid.NewGuid():entity.NewsGuid),
			    GenerateParameter("NewsCategoryCode",entity.NewsCategoryCode??string.Empty),
			    GenerateParameter("NewsTitle",entity.NewsTitle??string.Empty),
			    GenerateParameter("NewsBody",entity.NewsBody??string.Empty),
			    GenerateParameter("NewsSEOUrl",entity.NewsSEOUrl??string.Empty),
			    GenerateParameter("NewsDate",entity.NewsDate),
			    GenerateParameter("NewsAuthor",entity.NewsAuthor??string.Empty),
			    GenerateParameter("NewsReadCount",entity.NewsReadCount),
			    GenerateParameter("NewsRating",entity.NewsRating),
			    GenerateParameter("CanUsable",(int)entity.CanUsable),
                GenerateParameter("NewsIsTop",entity.NewsIsTop),
			    GenerateParameter("NewsIsRecommend",entity.NewsIsRecommend),
			    GenerateParameter("NewsPlusCount",entity.NewsPlusCount),
			    GenerateParameter("NewsMinusCount",entity.NewsMinusCount),
                GenerateParameter("NewsOriginalFrom",entity.NewsOriginalFrom??string.Empty),
			    GenerateParameter("NewsOriginalUrl",entity.NewsOriginalUrl??string.Empty),
			    GenerateParameter("NewsOriginalAuthor",entity.NewsOriginalAuthor??string.Empty),
			    GenerateParameter("NewsOriginalDate",entity.NewsOriginalDate)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override NewsEntity Load(IDataReader reader)
        {
            NewsEntity entity = new NewsEntity();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsID"))
                {
                    entity.NewsID = reader.GetInt32(reader.GetOrdinal("NewsID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsGuid"))
                {
                    entity.NewsGuid = reader.GetGuid(reader.GetOrdinal("NewsGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsCategoryCode"))
                {
                    entity.NewsCategoryCode = reader.GetString(reader.GetOrdinal("NewsCategoryCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsCategoryName"))
                {
                    entity.NewsCategoryName = reader.GetString(reader.GetOrdinal("NewsCategoryName"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsTitle"))
                {
                    entity.NewsTitle = reader.GetString(reader.GetOrdinal("NewsTitle"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsBody"))
                {
                    entity.NewsBody = reader.GetString(reader.GetOrdinal("NewsBody"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsSEOUrl"))
                {
                    entity.NewsSEOUrl = reader.GetString(reader.GetOrdinal("NewsSEOUrl"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsDate"))
                {
                    entity.NewsDate = reader.GetDateTime(reader.GetOrdinal("NewsDate"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsAuthor"))
                {
                    entity.NewsAuthor = reader.GetString(reader.GetOrdinal("NewsAuthor"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsReadCount"))
                {
                    entity.NewsReadCount = reader.GetInt32(reader.GetOrdinal("NewsReadCount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsRating"))
                {
                    entity.NewsRating = reader.GetDecimal(reader.GetOrdinal("NewsRating"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsIsTop"))
                {
                    entity.NewsIsTop = reader.GetInt32(reader.GetOrdinal("NewsIsTop"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsIsRecommend"))
                {
                    entity.NewsIsRecommend = reader.GetInt32(reader.GetOrdinal("NewsIsRecommend"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsPlusCount"))
                {
                    entity.NewsPlusCount = reader.GetInt32(reader.GetOrdinal("NewsPlusCount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsMinusCount"))
                {
                    entity.NewsMinusCount = reader.GetInt32(reader.GetOrdinal("NewsMinusCount"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsOriginalFrom"))
                {
                    entity.NewsOriginalFrom = reader.GetString(reader.GetOrdinal("NewsOriginalFrom"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsOriginalFrom"))
                {
                    entity.NewsOriginalFrom = reader.GetString(reader.GetOrdinal("NewsOriginalFrom"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsOriginalUrl"))
                {
                    entity.NewsOriginalUrl = reader.GetString(reader.GetOrdinal("NewsOriginalUrl"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsOriginalAuthor"))
                {
                    entity.NewsOriginalAuthor = reader.GetString(reader.GetOrdinal("NewsOriginalAuthor"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "NewsOriginalDate"))
                {
                    entity.NewsOriginalDate = reader.GetDateTime(reader.GetOrdinal("NewsOriginalDate"));
                }
            }
            return entity;
        }
        #endregion
    }
}
