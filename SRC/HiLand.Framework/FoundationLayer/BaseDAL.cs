using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Paging;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.FoundationLayer
{
    //TODO:xieran20121025 生成参数、载入数据等方法缺省可以考虑使用ORM自动实现，
    //当然各子类中对方法可以通过手工操作的方式对方法进行重写以提高程序性能

    /// <summary>
    /// 数据访问的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class BaseDAL<TModel, TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseComputerDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>, IDAL<TModel>
        where TModel : IModel, new()
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        #region 数据库基本信息
        /// <summary>
        /// 实体对应主表的名称
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected abstract string[] KeyNames { get; }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected abstract string GuidKeyName { get; }

        /// <summary>
        /// 分页存储过程的名字
        /// </summary>
        protected abstract string PagingSPName { get; }

        /// <summary>
        /// GetList获取数据集合时的排序条件
        /// </summary>
        /// <remarks>
        /// 如果GetList的重载方法设置了参数OrderByClause，则此属性会被覆盖
        /// </remarks>
        protected virtual string OrderByCondition
        {
            get { return string.Empty; }
        }
        #endregion

        #region 数据库基本操作
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        public abstract bool Create(TModel model);

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        public abstract bool Update(TModel model);

        /// <summary>
        /// 添加或者更新实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        public bool CreateOrUpdate(TModel model)
        {
            bool isSuccessful = false;
            isSuccessful = Update(model);
            if (isSuccessful == false)
            {
                isSuccessful = Create(model);
            }
            return isSuccessful;
        }

        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(TModel entity)
        {
            string commandText = string.Format("Delete From [{0}] Where {1}", TableName, GetKeysWhereClause());

            //TODO:删除时暂时先使用全部的参数
            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 批量删除实体信息
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public virtual bool DeleteList(string whereClause)
        {
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            string commandText = string.Format("Delete From [{0}] Where {1}", TableName, whereClause);

            int resultCount = HelperExInstance.ExecuteNonQuery(commandText);

            if (resultCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public virtual TModel Get(string modelID)
        {
            string commandText = string.Format("SELECT * FROM [{0}]  WHERE  {1}", TableName, GetKeysWhereClause());
            TParameter[] sqlParas = GetKeyParameters(modelID);
            return CommonGeneralInstance.GetEntity<TModel>(commandText, sqlParas, Load);
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="modelGuiD"></param>
        /// <returns></returns>
        public virtual TModel Get(Guid modelGuiD)
        {
            string commandText = string.Format("SELECT * FROM [{0}]  WHERE  {1}", TableName, GetKeysWhereClause());
            TParameter[] sqlParas = GetGuidKeyParameters(modelGuiD);
            return CommonGeneralInstance.GetEntity<TModel>(commandText, sqlParas, Load);
        }

        /// <summary>
        /// 获取总的条目
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public virtual int GetTotalCount(string whereClause)
        {
            return CommonGeneralInstance.GetTotalCount(TableName, whereClause);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public virtual List<TModel> GetList(string whereClause, params IDbDataParameter[] paras)
        {
            return GetList(Logics.False, whereClause, 0, string.Empty, paras);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="onlyDisplayUsable"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public virtual List<TModel> GetList(Logics onlyDisplayUsable, string whereClause, int topCount, string orderByClause, params IDbDataParameter[] paras)
        {
            List<TModel> collection = new List<TModel>();
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            if (onlyDisplayUsable == Logics.True)
            {
                whereClause += string.Format(" AND CanUsable={0} ", (int)onlyDisplayUsable);
            }

            string commandText = string.Empty;
            if (topCount > 0)
            {
                commandText = string.Format("SELECT TOP {0} * FROM [{1}] ", topCount, TableName);
            }
            else
            {
                commandText = string.Format("SELECT * FROM [{0}] ", TableName);
            }

            commandText += " WHERE " + whereClause;

            if (string.IsNullOrEmpty(orderByClause) == false)
            {
                commandText += " Order By " + orderByClause;
            }
            else
            {
                if (string.IsNullOrEmpty(this.OrderByCondition) == false)
                {
                    commandText += " Order By " + this.OrderByCondition;
                }
            }

            TParameter[] sqlParas = paras as TParameter[];
            collection = CommonGeneralInstance.GetEntityList<TModel>(commandText, sqlParas, Load);

            return collection;
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <returns></returns>
        /// <remarks>直接传递sql语句获取实体集合</remarks>
        public virtual List<TModel> GetList(string sqlClause)
        {
            List<TModel> collection = CommonGeneralInstance.GetEntityList<TModel>(sqlClause, null, Load);
            return collection;
        }

        /// <summary>
        /// 获取分页的实体信息
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <returns></returns>
        public virtual PagedEntityCollection<TModel> GetPagedCollection(int startIndex, int pageSize, string whereClause, string orderClause)
        {
            return CommonGeneralInstance.GetPagedCollection<TModel>(PagingSPName, whereClause, orderClause, startIndex, pageSize, Load);
        }
        #endregion

        #region  
        /// <summary>
        /// 获取单条执行结果
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public virtual object GetScalar(string sqlClause, params IDbDataParameter[] paras)
        {
            return HelperExInstance.ExecuteScalar(sqlClause, paras as TParameter[]);
        }

        /// <summary>
        /// 非查询的方式执行语句
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <param name="paras"></param>
        public virtual int ExcuteNonQuery(string sqlClause, params IDbDataParameter[] paras)
        {
            return HelperExInstance.ExecuteNonQuery(sqlClause, paras as TParameter[]);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取主键形成的过滤条件
        /// </summary>
        /// <returns></returns>
        protected string GetKeysWhereClause()
        {
            string result = " 1=1 ";
            if (KeyNames != null)
            {
                foreach (string currentKeyName in KeyNames)
                {
                    result += string.Format(" AND [{0}]= {1}{0} ", currentKeyName, ParameterNamePrefix);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取主键形成过滤条件是的参数集合
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        protected TParameter[] GetKeyParameters(params string[] keyValues)
        {
            List<TParameter> result = new List<TParameter>();
            if (KeyNames != null && KeyNames.Length == keyValues.Length)
            {
                for (int i = 0; i < KeyNames.Length; i++)
                {
                    string currentKeyName = KeyNames[i];
                    TParameter tp = GenerateParameter(currentKeyName, keyValues[i]);
                    result.Add(tp);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取主键形成过滤条件是的参数集合
        /// </summary>
        /// <param name="guidKeyValue"></param>
        /// <returns></returns>
        protected TParameter[] GetGuidKeyParameters(Guid guidKeyValue)
        {
            List<TParameter> result = new List<TParameter>();

            TParameter tp = GenerateParameter(GuidKeyName, guidKeyValue);
            result.Add(tp);

            return result.ToArray();
        }

        /// <summary>
        /// 将实体信息的各属性值转换为数据库参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 在派生类中如果给实体的参数赋属性值，应该重写InnerPrepareParasAll。
        /// </remarks>
        protected virtual TParameter[] PrepareParasAll(TModel entity)
        {
            List<TParameter> list = new List<TParameter>();

            TParameter tpPropertyNames = GenerateParameter("PropertyNames", entity.PropertyNames ?? string.Empty);
            list.Add(tpPropertyNames);

            TParameter tpPropertyValues = GenerateParameter("PropertyValues", entity.PropertyValues ?? string.Empty);
            list.Add(tpPropertyValues);


            InnerPrepareParasAll(entity, ref list);

            if (entity is IModelExtensible)
            {
                TParameter paraPropertyNames = list.Find(paramater => paramater.ParameterName == string.Format("{0}PropertyNames", ParameterNamePrefix));
                TParameter paraPropertyValues = list.Find(paramater => paramater.ParameterName == string.Format("{0}PropertyValues", ParameterNamePrefix));

                if (paraPropertyNames != null && paraPropertyValues != null)
                {
                    SerializerData serializerData = ((IModelExtensible)entity).ExtensiableRepository.GetSerializerData();
                    paraPropertyNames.Value = serializerData.Keys ?? string.Empty;
                    paraPropertyValues.Value = serializerData.Values ?? string.Empty;
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 内部准备（为实体准备数据访问的参数）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="paraList"></param>
        protected virtual void InnerPrepareParasAll(TModel entity, ref List<TParameter> paraList)
        {
            // 本方法需要在派生类中重写
        }

        /// <summary>
        /// 将IDataReader中的数据装载如实体中
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual TModel Load(IDataReader reader)
        {
            TModel entity = new TModel();
            if (reader != null && reader.IsClosed == false)
            {
                //1.加载实体表的扩展属性（如果本实体对应的表有扩展字段）
                if (DataReaderHelper.IsExistField(reader, "PropertyNames") && Convert.IsDBNull(reader["PropertyNames"]) == false)
                {
                    entity.PropertyNames = reader.GetString(reader.GetOrdinal("PropertyNames"));
                }

                if (DataReaderHelper.IsExistField(reader, "PropertyValues") && Convert.IsDBNull(reader["PropertyValues"]) == false)
                {
                    entity.PropertyValues = reader.GetString(reader.GetOrdinal("PropertyValues"));
                }

                //2.加载扩展表的扩展属性（如果本实体对应有扩展表）

                //3.加载本实体具体的属性
                InnerLoad(reader, ref entity);

            }

            return entity;
        }

        /// <summary>
        /// 内部载入（将IDataReader中的数据装载如实体中）
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <remarks>除了对PropertyNames和PropertyValues的载入除外，以及对通过上述两个字段进行扩展的属性除外</remarks>
        protected virtual void InnerLoad(IDataReader reader, ref TModel entity)
        {
            //本方法需要在派生类中重写
        }
        #endregion
    }
}
