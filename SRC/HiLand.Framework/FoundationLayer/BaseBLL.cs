using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using HiLand.Utility.Cache;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Paging;
using HiLand.Utility.Pattern;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 业务逻辑的基类
    /// </summary>
    /// <typeparam name="TBLL">具体的业务逻辑类</typeparam>
    /// <typeparam name="TModel">业务实体</typeparam>
    /// <typeparam name="TDAL">数据访问类型</typeparam>
    public class BaseBLL<TBLL, TModel, TDAL> : BaseBLL<TBLL, TModel, TDAL, IDAL<TModel>>
        where TBLL : BaseBLL<TBLL, TModel, TDAL>, new()
        where TDAL : IDAL<TModel>, new()
        where TModel : IModel, new()
    {

    }

    /// <summary>
    /// 业务逻辑的基类
    /// </summary>
    /// <typeparam name="TBLL">具体的业务逻辑类</typeparam>
    /// <typeparam name="TModel">业务实体</typeparam>
    /// <typeparam name="TDAL">数据访问类型</typeparam>
    /// <typeparam name="TIDALExtend"></typeparam>
    public class BaseBLL<TBLL, TModel, TDAL, TIDALExtend>
        where TBLL : BaseBLL<TBLL, TModel, TDAL, TIDALExtend>, new()
        where TDAL : TIDALExtend, new()
        where TIDALExtend : IDAL<TModel>
        where TModel : IModel, new()
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static TBLL Instance
        {
            get
            {
                return Singleton<TBLL>.Instance;
            }
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <param name="cacheMintues"></param>
        /// <returns></returns>
        public static TBLL GetInstance(int cacheMintues)
        {
            TBLL bll = Singleton<TBLL>.Instance;
            bll.CacheMintues = cacheMintues;
            return bll;
        }

        private int cacheMintes = 0;
        /// <summary>
        /// 缓存的时间
        /// </summary>
        protected int CacheMintues
        {
            get
            {
                if (cacheMintes == 0)
                {
                    cacheMintes = CacheHelper.AFewTime;
                }
                return cacheMintes;
            }
            set
            {
                cacheMintes = value * 60;
            }
        }


        private TIDALExtend loadDAL = default(TIDALExtend);
        /// <summary>
        /// 从存储介质载入数据时使用的DAL
        /// </summary>
        protected TIDALExtend LoadDAL
        {
            get
            {
                if (loadDAL == null)
                {
                    string dllName = ConfigurationManager.AppSettings["HiGeneralDALDLLName"];
                    string nameSpace = ConfigurationManager.AppSettings["HiGeneralDALNameSpace"];

                    if (string.IsNullOrEmpty(dllName) || string.IsNullOrEmpty(nameSpace))
                    {
                        loadDAL = Singleton<TDAL>.Instance;
                    }
                    else
                    {
                        string dalClassShortName = typeof(TDAL).Name;
                        string dalClassFullName = string.Format("{0}.{1},{2}", nameSpace, dalClassShortName, dllName);
                        Type dalClassType = Type.GetType(dalClassFullName);
                        loadDAL = (TIDALExtend)Activator.CreateInstance(dalClassType);
                    }
                }

                return loadDAL;
            }
        }

        private TIDALExtend saveDAL = default(TIDALExtend);
        /// <summary>
        /// 向存储介质写入数据时使用的DAL
        /// </summary>
        protected TIDALExtend SaveDAL
        {
            get
            {
                if (saveDAL == null)
                {
                    saveDAL = LoadDAL;
                }

                return saveDAL;
            }
        }

        /// <summary>
        /// 创建实体对象
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public virtual bool Create(TModel model)
        {
            bool isSuccessful = SaveDAL.Create(model);
            if (isSuccessful == true && CacheHelper.IsUseCache == true)
            {
                CleanUpCache(model);
                BuildUpCache(model);
            }
            return isSuccessful;
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public virtual bool Update(TModel model)
        {
            bool isSuccessful = SaveDAL.Update(model);
            if (isSuccessful == true && CacheHelper.IsUseCache == true)
            {
                if (model is IModelExtensible)
                {
                    model.PropertyNames = ((IModelExtensible)model).ExtensiableRepository.GetSerializerData().Keys;
                    model.PropertyValues = ((IModelExtensible)model).ExtensiableRepository.GetSerializerData().Values;
                }

                CleanUpCache(model);
                BuildUpCache(model);
            }
            return isSuccessful;
        }

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateOrUpdate(TModel entity)
        {
            bool isSuccessfule = Update(entity);
            if (isSuccessfule == false)
            {
                isSuccessfule = Create(entity);
            }

            return isSuccessfule;
        }

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual bool Delete(string modelID)
        {
            TModel model = Get(modelID);
            return Delete(model);
        }

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual bool Delete(Guid modelID)
        {
            string modelIDString = modelID.ToString();
            return Delete(modelIDString);
        }

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual bool Delete(int modelID)
        {
            string modelIDString = modelID.ToString();
            return Delete(modelIDString);
        }

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Delete(TModel model)
        {
            bool isSuccessful = SaveDAL.Delete(model);

            if (isSuccessful == true && CacheHelper.IsUseCache == true)
            {
                CleanUpCache(model);
            }

            return isSuccessful;
        }

        /// <summary>
        /// 按照条件删除实体对象
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public virtual bool DeleteList(string whereClause)
        {
            bool isSuccessful = SaveDAL.DeleteList(whereClause);
            if (isSuccessful == true)
            {
                CleanUpAllCache();
            }

            return isSuccessful;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual TModel Get(Guid modelID)
        {
            return Get(modelID, false);
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public virtual TModel Get(Guid modelID, bool isForceUseNoCache)
        {
            TModel result = default(TModel);
            if (isForceUseNoCache == true)
            {
                result = SaveDAL.Get(modelID);
            }
            else
            {
                string cacheKey = GeneralCacheKeys<TModel>.GetEntityGuidKey(modelID);
                result = CacheHelper.Access<Guid, TModel>(cacheKey, CacheMintues, SaveDAL.Get, modelID);
            }

            if (result == null)
            {
                result = new TModel();
                result.ForceSetEmpty();
            }
            return result;
        }

        /// <summary>
        ///  获取实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual TModel Get(int modelID)
        {
            return Get(modelID, false);
        }

        /// <summary>
        ///  获取实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public virtual TModel Get(int modelID, bool isForceUseNoCache)
        {
            string modelIDString = modelID.ToString();
            return Get(modelIDString, isForceUseNoCache);
        }

        /// <summary>
        ///  获取实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual TModel Get(string modelID)
        {
            return Get(modelID, false);
        }

        /// <summary>
        ///  获取实体对象
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        /// <returns></returns>
        public virtual TModel Get(string modelID, bool isForceUseNoCache)
        {
            TModel result = default(TModel);

            if (isForceUseNoCache == true)
            {
                result = SaveDAL.Get(modelID);
            }
            else
            {
                string cacheKey = GeneralCacheKeys<TModel>.GetEntityKey(modelID);
                result = CacheHelper.Access<string, TModel>(cacheKey, CacheMintues, SaveDAL.Get, modelID);
            }

            if (result == null)
            {
                result = new TModel();
                result.ForceSetEmpty();
            }
            return result;
        }

        ///// <summary>
        /////  获取实体对象
        ///// </summary>
        ///// <param name="fieldValue"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="isForceUseNoCache">是否强制不使用缓存</param>
        ///// <returns></returns>
        //public virtual TModel Get<T>(T fieldValue,string fieldName="", bool isForceUseNoCache=false)
        //{
        //    TModel result = default(TModel);
        //    var fieldValueString = fieldValue.ToString();

        //    if (isForceUseNoCache == true)
        //    {
        //        result = SaveDAL.Get(fieldValueString);
        //    }
        //    else
        //    {
        //        string cacheKey = GeneralCacheKeys<TModel>.GetEntityKey(fieldValueString);
        //        result = CacheHelper.Access<string, TModel>(cacheKey, CacheMintues, SaveDAL.Get, fieldValueString);
        //    }

        //    if (result == null)
        //    {
        //        result = new TModel();
        //        result.ForceSetEmpty();
        //    }
        //    return result;
        //}

        /// <summary>
        /// 获取总的条目
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public virtual int GetTotalCount(string whereClause)
        {
            string cacheKey = GeneralCacheKeys<TModel>.GetEntityCountKey(whereClause);
            return CacheHelper.Access<String, int>(cacheKey, CacheMintues, SaveDAL.GetTotalCount, whereClause);
        }

        /// <summary>
        ///  获取实体对象列表
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public virtual List<TModel> GetList(string whereClause, params IDbDataParameter[] paras)
        {
            return GetList(Logics.False, whereClause, 0, string.Empty, paras);
        }

        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public virtual List<TModel> GetList(string whereClause, string orderByClause, params IDbDataParameter[] paras)
        {
            return GetList(Logics.False, whereClause, 0, orderByClause, paras);
        }

        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="onlyDisplayUsable"></param>
        /// <param name="whereClauseModel"></param>
        /// <param name="topCount"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public virtual List<TModel> GetList(Logics onlyDisplayUsable, ClauseModel<IDbDataParameter> whereClauseModel, int topCount, string orderByClause)
        {
            return GetList(onlyDisplayUsable, whereClauseModel.CluaseString, topCount, orderByClause, whereClauseModel.ParameterList.ToArray());
        }

        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="onlyDisplayUsable"></param>
        /// <param name="whereClause"></param>
        /// <param name="topCount"></param>
        /// <param name="orderByClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        /// <remarks>
        /// 二次开发TIP：获取类别的原始方法，其他各个GetList重载最后均调用此方法。即如果要开派生类中重写的时候，请重写此方法即可。
        /// </remarks>
        public virtual List<TModel> GetList(Logics onlyDisplayUsable, string whereClause, int topCount, string orderByClause, params IDbDataParameter[] paras)
        {
            string whereClauseString = GetRealWhereClauseString(whereClause, paras);
            string cacheKey = GeneralCacheKeys<TModel>.GetEntityListKey(onlyDisplayUsable, whereClauseString, topCount, orderByClause);
            return CacheHelper.Access<Logics, string, int, string, IDbDataParameter[], List<TModel>>(cacheKey, CacheMintues, SaveDAL.GetList, onlyDisplayUsable, whereClause, topCount, orderByClause, paras);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <returns></returns>
        /// <remarks>直接传递sql语句获取实体集合</remarks>
        public virtual List<TModel> GetListBySQL(string sqlClause)
        {
            string cacheKey = GeneralCacheKeys<TModel>.GetEntityListKey(sqlClause);
            return CacheHelper.Access<string, List<TModel>>(cacheKey, CacheMintues, SaveDAL.GetList, sqlClause);
        }

        /// <summary>
        /// 获取单条执行结果
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        /// <remarks>
        /// 虽然本方法可以执行任何sql语句，但是为了缓存保存的条理性，建议仅执行跟本实体有关的sql语句
        /// </remarks>
        public virtual object GetScalar(string sqlClause, params IDbDataParameter[] paras)
        {
            string sqlClauseString = GetRealWhereClauseString(sqlClause, paras);
            string cacheKey = GeneralCacheKeys<TModel>.GetScalarKey(sqlClause);
            return CacheHelper.Access<string, IDbDataParameter[], object>(cacheKey, CacheMintues, SaveDAL.GetScalar, sqlClause,paras);
        }

        /// <summary>
        /// 非查询的方式执行语句
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        /// <remarks>
        /// 虽然本方法可以执行任何sql语句，但是为了缓存清除的准确性，请务必仅执行跟本实体有关的sql语句
        /// </remarks>
        public virtual int ExcuteNonQuery(string sqlClause, params IDbDataParameter[] paras)
        {
            int result= SaveDAL.ExcuteNonQuery(sqlClause,paras);
            if (result > 0)
            { 
                //清空本实体模型对应的所有缓存
                CleanUpAllCache();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClauseWithPara"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private string GetRealWhereClauseString(string whereClauseWithPara, IDbDataParameter[] paras)
        {
            if (paras != null)
            {
                foreach (IDbDataParameter currentPara in paras)
                {
                    //TODO:xieran20111010 这个地方至少应该加入SQL注入的处理
                    whereClauseWithPara = whereClauseWithPara.Replace(currentPara.ParameterName, currentPara.Value.ToString());
                }
            }
            return whereClauseWithPara;
        }

        /// <summary>
        /// 获取分页显示的实体对象列表
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <returns></returns>
        public virtual PagedEntityCollection<TModel> GetPagedCollection(int startIndex, int pageSize, string whereClause, string orderClause)
        {
            string cacheKey = GeneralCacheKeys<TModel>.GetPagedEntityCollectionKey(startIndex, pageSize, whereClause, orderClause);
            return CacheHelper.Access<int, int, string, string, PagedEntityCollection<TModel>>(cacheKey, CacheMintues, SaveDAL.GetPagedCollection, startIndex, pageSize, whereClause, orderClause);
        }

        #region 帮助方法
        /// <summary>
        /// 清空关于当前业务的所有的缓存
        /// </summary>
        protected void CleanUpAllCache()
        {
            CacheHelper.RemoveByPattern(GeneralCacheKeys<TModel>.GetEntityPrefixKey());
        }

        /// <summary>
        /// 清理某些已经改变的缓存
        /// </summary>
        /// <param name="entity"></param>
        protected void CleanUpCache(TModel entity)
        {
            CacheHelper.Remove(GeneralCacheKeys<TModel>.GetEntityKey(entity.BusinessKeyValues));
            CacheHelper.RemoveByPattern(GeneralCacheKeys<TModel>.GetEntityListPrefixKey());
        }

        /// <summary>
        /// 清空列表的缓存
        /// </summary>
        public void CleanUpListCache()
        {
            CacheHelper.RemoveByPattern(GeneralCacheKeys<TModel>.GetEntityListPrefixKey());
        }

        /// <summary>
        /// 根据实体构建缓存
        /// </summary>
        /// <param name="entity"></param>
        protected void BuildUpCache(TModel entity)
        {
            CacheHelper.Set(GeneralCacheKeys<TModel>.GetEntityKey(entity.BusinessKeyValues), entity, CacheMintues);
        }
        #endregion
    }
}
