using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility.Cache;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 通用业务的缓冲键
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class GeneralCacheKeys<TModel> //where TModel : IModel
    {
        /// <summary>
        /// 获取应用程序的名称
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationName()
        {
            return "General";
        }

        /// <summary>
        /// 获取当前实体的名称
        /// </summary>
        /// <returns></returns>
        public static string GetModelName()
        {
            string modelName = typeof(TModel).Name;
            return modelName;
        }

        /// <summary>
        /// 通过实体的主键信息获取实体的缓存键名称
        /// </summary>
        /// <param name="modelIDs"></param>
        /// <returns></returns>
        public static string GetEntityKey(params string[] modelIDs)
        {
            return string.Format(CacheKeys.EntityKeyFormat, GetApplicationName(), GetModelName(), CollectionHelper.Concat(string.Empty, modelIDs));
        }

        /// <summary>
        /// 通过实体的Guid主键信息获取实体的缓存键名称
        /// </summary>
        /// <param name="modelGuid"></param>
        /// <returns></returns>
        public static string GetEntityGuidKey(Guid modelGuid)
        {
            return string.Format(CacheKeys.EntityKeyFormat, GetApplicationName(), GetModelName(), modelGuid.ToString());
        }

        /// <summary>
        /// 通过开发人员自定义的实体信息获取实体的缓存键名称
        /// </summary>
        /// <param name="modelGuid"></param>
        /// <returns></returns>
        public static string GetEntityCustomKey(string customName, string customValue)
        {
            return string.Format(CacheKeys.EntityKeyFormat, GetApplicationName(), GetModelName(), customName + "||" + customValue);
        }

        /// <summary>
        /// 通过实体的其他业务信息获取实体的缓存键名称
        /// </summary>
        /// <param name="businessValues">业务的值（s）</param>
        /// <param name="businessName">业务的名称</param>
        /// <returns></returns>
        /// <remarks>比如HiLand.General\BLL\BasicSettingBLL.cs中通过配置键的名称获取信息就会用到这个方法</remarks>
        public static string GetEntityBusinessKey(string businessName, params string[] businessValues)
        {
            return string.Format(CacheKeys.EntityKeyFormat, GetApplicationName(), string.Format("{0}-{1}", GetModelName(), businessName), CollectionHelper.Concat(string.Empty, businessValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public static string GetEntityCountKey(string whereClause)
        {

            return string.Format(CacheKeys.EntityCountFormat, GetApplicationName(), GetModelName(), whereClause);
        }

        /// <summary>
        /// 获取按照GetList的各个参数生成的Key
        /// </summary>
        /// <param name="onlyDisplayUsable"></param>
        /// <param name="whereClause"></param>
        /// <param name="topCount"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public static string GetEntityListKey(Logics onlyDisplayUsable, string whereClause, int topCount, string orderByClause)
        {
            return string.Format(CacheKeys.EntityListFormat,
                GetApplicationName(), GetModelName(), onlyDisplayUsable.ToString(), whereClause, topCount.ToString(), orderByClause);
        }

        /// <summary>
        /// 获取按照GetList的各个参数生成的Key
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <returns></returns>
        public static string GetEntityListKey(string sqlClause)
        {
            return string.Format(CacheKeys.EntityListFormat,
                GetApplicationName(), GetModelName(), true.ToString(), sqlClause, true.ToString(), string.Empty);
        }

        /// <summary>
        /// 获取按照GetScalar的各个参数生成的Key
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <returns></returns>
        public static string GetScalarKey(string sqlClause)
        {
            return string.Format(CacheKeys.EntityScalarFormat, GetApplicationName(), GetModelName(), sqlClause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public static string GetPagedEntityCollectionKey(int startIndex, int pageSize, string whereClause, string orderByClause)
        {
            return string.Format(CacheKeys.EntityPageCollectionFormat,
                GetApplicationName(), GetModelName(), startIndex.ToString(), pageSize, whereClause.ToString(), orderByClause);
        }

        /// <summary>
        /// 通过实体的名称此前缀信息获取当前业务的缓存键名称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetEntityPrefixKey()
        {
            return string.Format(CacheKeys.EntityPrefixFormat, GetApplicationName(), GetModelName());
        }

        /// <summary>
        /// 通过实体的名称此前缀信息获取实体集合的缓存键名称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetEntityListPrefixKey()
        {
            return string.Format(CacheKeys.EntityListPrefixFormat, GetApplicationName(), GetModelName());
        }
    }
}
