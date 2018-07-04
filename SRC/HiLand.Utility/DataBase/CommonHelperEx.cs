using System.Data;
using HiLand.Utility.Pattern;
using HiLand.Utility.Setting;

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// 数据库执行逻辑包装器
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <remarks>
    /// 目前仅将“连接”包装了
    /// </remarks>
    public class CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter
    {
        /// <summary>
        /// 数据库访问辅助器单例
        /// </summary>
        private static CommonHelper<TTransaction, TConnection, TCommand, TDataReader, TParameter> HelperInstance
        {
            get
            {
                return Singleton<CommonHelper<TTransaction, TConnection, TCommand, TDataReader, TParameter>>.Instance;
            }
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter> Instance
        {
            get
            {
                return Singleton<CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter>>.Instance;
            }
        }



        private string connectionString = string.Empty;
        /// <summary>
        /// 数据库连接信息
        /// </summary>
        public virtual string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    string connectionStringName = Config.GetAppSetting("connectionStringName");
                    if (string.IsNullOrEmpty(connectionStringName))
                    {
                        connectionString = HelperInstance.ConnectionString;
                    }
                    else
                    {
                        connectionString = HelperInstance.GetConnectionString(connectionStringName);
                    }
                }

                return connectionString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string commandText, params TParameter[] commandParameters)
        {
            return HelperInstance.ExecuteNonQuery(ConnectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual TDataReader ExecuteReader(string commandText, params TParameter[] commandParameters)
        {
            return HelperInstance.ExecuteReader(ConnectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 通过存储过程执行Reader
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual TDataReader ExecuteReaderBySP(string spName, params TParameter[] commandParameters)
        {
            return HelperInstance.ExecuteReader(ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string commandText, params TParameter[] commandParameters)
        {
            return HelperInstance.ExecuteScalar(ConnectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandTextForLineCount"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual bool IsExist(string commandTextForLineCount, params TParameter[] commandParameters)
        {
            return HelperInstance.IsExist(ConnectionString, CommandType.Text, commandTextForLineCount, commandParameters);
        }

        /// <summary>
        /// 执行影响数据库单行的sql语句,并返回执行成功与否的标识(安全模式下，即不抛出异常)
        /// </summary>
        /// <param name="singleRowCommandText">影响数据库单行的sql语句</param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual bool ExecuteSingleRowNonQuery(string singleRowCommandText, params TParameter[] commandParameters)
        {
            bool isSuccessful = false;
            try
            {
                isSuccessful = HelperInstance.ExecuteSingleRowNonQuery(ConnectionString, CommandType.Text, singleRowCommandText, commandParameters);
            }
            catch
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }
    }
}
