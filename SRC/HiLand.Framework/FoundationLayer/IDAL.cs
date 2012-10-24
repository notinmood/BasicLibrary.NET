using System;
using System.Collections.Generic;
using HiLand.Utility.Paging;
using HiLand.Utility.Enums;
using System.Data;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 数据访问层接口（提供CRUD等基本的几个数据访问逻辑）
    /// </summary>
    /// <typeparam name="TModel">数据实体类型</typeparam>
    public interface IDAL<TModel> //where TModel  :IModel
    {
        /// <summary>
        /// 创建实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        bool Create(TModel model);
        
        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        bool Update(TModel model);

        /// <summary>
        /// 创建或者更新实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        bool CreateOrUpdate(TModel model);
        
        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        bool Delete(TModel model);

        /// <summary>
        /// 批量删除实体信息
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        bool DeleteList(string whereClause);

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="modelID">主键信息</param>
        /// <returns></returns>
        TModel Get(string modelID);

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="modelGuiD">主键信息</param>
        /// <returns></returns>
        TModel Get(Guid modelGuiD);

        /// <summary>
        /// 获取总的条目
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        int GetTotalCount(string whereClause);

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="topCount"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        List<TModel> GetList(Logics onlyDisplayUsable, string whereClause, int topCount, string orderByClause,params IDbDataParameter[] paras);

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="sqlClause"></param>
        /// <returns></returns>
        /// <remarks>直接传递sql语句获取实体集合</remarks>
        List<TModel> GetList(string sqlClause);

        /// <summary>
        /// 获取分页的实体集合
        /// </summary>
        /// <param name="startIndex">记录的开始索引数值</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="whereClause">过滤条件语句</param>
        /// <param name="orderClause">排序条件语句</param>
        /// <returns></returns>
        PagedEntityCollection<TModel> GetPagedCollection(int startIndex, int pageSize, string whereClause, string orderClause);
    }
}
