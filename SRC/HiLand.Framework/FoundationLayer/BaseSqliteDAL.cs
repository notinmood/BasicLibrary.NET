using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using HiLand.Utility.Enums;
using HiLand.Utility.Paging;
using HiLand.Utility.Serialization;
using HiLand.Utility.DataBase;

namespace HiLand.Framework.FoundationLayer
{
    public abstract class BaseSqliteDAL<TModel> : BaseDAL<TModel, SQLiteTransaction, SQLiteConnection, SQLiteCommand, SQLiteDataReader, SQLiteParameter> 
        where TModel : IModel, new()
    {
        #region 参数化查询的信息
        /// <summary>
        /// 参数名称前缀
        /// </summary>
        /// <remarks>
        /// 在不同的数据库系统中，参数名称的前缀是不同的：SQLServer中为 “@”；SQLite中为“$”
        /// </remarks>
        protected override string ParameterNamePrefix
        {
            get
            {
                return "$";
            }
        }
        #endregion
    }
}
