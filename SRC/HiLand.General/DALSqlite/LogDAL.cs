using System.Data.SQLite;
using HiLand.General.DALCommon;

namespace HiLand.General.DALSqlite
{
    public class LogDAL : LogCommonDAL<SQLiteTransaction, SQLiteConnection, SQLiteCommand, SQLiteDataReader, SQLiteParameter>
    {
        #region 计算机逻辑基本信息
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
