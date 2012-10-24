using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Paging;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 数据访问的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class BaseSqlDAL<TModel> : BaseDAL<TModel, SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>, IDAL<TModel> where TModel : IModel, new()
    {
        
    }
}
