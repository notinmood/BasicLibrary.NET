//using System;
//using System.Collections;
//using System.Configuration;
//using System.Data;
//using Hiland.BasicLibrary.Setting;
//using Hiland.BasicLibrary.Pattern;

//namespace Hiland.BasicLibrary.DataBase
//{
//    /// <summary>
//    /// 通用的数据访问类（兼容各个数据库类型）
//    /// </summary>
//    /// <typeparam name="TTransaction"></typeparam>
//    /// <typeparam name="TConnection"></typeparam>
//    /// <typeparam name="TCommand"></typeparam>
//    /// <typeparam name="TDataReader"></typeparam>
//    /// <typeparam name="TParameter"></typeparam>
//    public class CommonHelper<TTransaction, TConnection, TCommand, TDataReader, TParameter>
//        where TConnection : class,IDbConnection, new()
//        where TCommand : IDbCommand, new()
//        where TTransaction : IDbTransaction
//        where TDataReader : class, IDataReader
//        where TParameter : IDataParameter, IDbDataParameter
//    {
//        /// <summary>
//        /// 单例
//        /// </summary>
//        public static CommonHelper<TTransaction, TConnection, TCommand, TDataReader, TParameter> Instance
//        {
//            get
//            {
//                return Singleton<CommonHelper<TTransaction, TConnection, TCommand, TDataReader, TParameter>>.Instance;
//            }
//        }

//        /// <summary>
//        /// 获取缺省的数据库连接字符串
//        /// </summary>
//        public virtual string ConnectionString
//        {
//            get
//            {
//                return Config.DefaultConnectionString;
//            }
//        }

//        /// <summary>
//        /// 获取某个指定名称的数据库连接字符串
//        /// </summary>
//        /// <param name="connectionStringName"></param>
//        /// <returns></returns>
//        public virtual string GetConnectionString(string connectionStringName)
//        {
//            return Config.GetConnectionString(connectionStringName);
//        }

//        // Hashtable to store cached parameters
//        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

//        /// <summary>
//        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>an int representing the number of rows affected by the command</returns>
//        public virtual int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            using (TConnection connection = new TConnection())
//            {
//                connection.ConnectionString = connectionString;
//                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
//            }
//        }

//        /// <summary>
//        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="connection">an existing database connection</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>an int representing the number of rows affected by the command</returns>
//        public virtual int ExecuteNonQuery(TConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            TTransaction trans = default(TTransaction);
//            return ExecuteNonQuery(trans, connection, commandType, commandText, commandParameters);
//        }

//        /// <summary>
//        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="trans">an existing sql transaction</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>an int representing the number of rows affected by the command</returns>
//        public virtual int ExecuteNonQuery(TTransaction trans, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            return ExecuteNonQuery(trans, null, commandType, commandText, commandParameters);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="trans"></param>
//        /// <param name="connection"></param>
//        /// <param name="commandType"></param>
//        /// <param name="commandText"></param>
//        /// <param name="commandParameters"></param>
//        /// <returns></returns>
//        protected virtual int ExecuteNonQuery(TTransaction trans, TConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            if (connection == null)
//            {
//                connection = trans.Connection as TConnection;
//            }

//            using (TCommand cmd = new TCommand())
//            {
//                PrepareCommand(cmd, connection, trans, commandType, commandText, commandParameters);
//                int value = cmd.ExecuteNonQuery();
//                cmd.Parameters.Clear();
//                return value;
//            }
//        }

//        /// <summary>
//        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>A SqlDataReader containing the results</returns>
//        public virtual TDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            TConnection connection = new TConnection();
//            connection.ConnectionString = connectionString;
//            return ExecuteReader(connection, commandType, commandText, commandParameters);
//        }

//        /// <summary>
//        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="connection">a valid connection string for a SqlConnection</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>A SqlDataReader containing the results</returns>
//        public virtual TDataReader ExecuteReader(TConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            TCommand cmd = new TCommand();

//            // we use a try/catch here because if the method throws an exception we want to 
//            // close the connection throw code, because no datareader will exist, hence the 
//            // commandBehaviour.CloseConnection will not work
//            try
//            {
//                TTransaction trans = default(TTransaction);
//                PrepareCommand(cmd, connection, trans, commandType, commandText, commandParameters);
//                TDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection) as TDataReader;
//                cmd.Parameters.Clear();
//                return rdr;
//            }
//            catch(Exception ex)
//            {
//                connection.Close();
//                throw ex;
//            }
//        }

//        /// <summary>
//        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
//        public virtual object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            using (TConnection connection = new TConnection())
//            {
//                connection.ConnectionString = connectionString;
//                return ExecuteScalar(connection, commandType, commandText, commandParameters);
//            }
//        }

//        /// <summary>
//        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
//        /// using the provided parameters.
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        /// </remarks>
//        /// <param name="connection">an existing database connection</param>
//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
//        /// <param name="commandText">the stored procedure name or T-SQL command</param>
//        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
//        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
//        public virtual object ExecuteScalar(TConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters)
//        {
//            using (TCommand cmd = new TCommand())
//            {
//                TTransaction trans = default(TTransaction);
//                PrepareCommand(cmd, connection, trans, commandType, commandText, commandParameters);
//                object value = cmd.ExecuteScalar();
//                cmd.Parameters.Clear();
//                return value;
//            }
//        }

//        /// <summary>
//        /// 判断某信息在数据库内是否存在
//        /// </summary>
//        /// <param name="connection"></param>
//        /// <param name="commandType"></param>
//        /// <param name="commandTextForLineCount">命令内容,其应该是这样的sql语句
//        ///     select count(1) from [CoreUser] where [UserEmail]=@UserEmail
//        ///     即内部是要返回数据的行数的sql语句
//        /// </param>
//        /// <param name="commandParameters"></param>
//        /// <returns></returns>
//        public virtual bool IsExist(TConnection connection, CommandType commandType, string commandTextForLineCount, params TParameter[] commandParameters)
//        {
//            bool isExist = true;

//            object returnValue = ExecuteScalar(connection, commandType, commandTextForLineCount, commandParameters);

//            int returnCount = 0;

//            try
//            {
//                returnCount = Convert.ToInt32(returnValue);
//            }
//            catch { }

//            if (returnCount == 0)
//            {
//                isExist = false;
//            }
//            else
//            {
//                isExist = true;
//            }

//            return isExist;
//        }

//        /// <summary>
//        /// 判断某信息在数据库内是否存在
//        /// </summary>
//        /// <param name="connectionString"></param>
//        /// <param name="commandType"></param>
//        /// <param name="commandTextForLineCount">命令内容,其应该是这样的sql语句
//        ///     select count(1) from [CoreUser] where [UserEmail]=@UserEmail
//        ///     即内部是要返回数据的行数的sql语句
//        /// </param>
//        /// <param name="commandParameters"></param>
//        /// <returns></returns>
//        public virtual bool IsExist(string connectionString, CommandType commandType, string commandTextForLineCount, params TParameter[] commandParameters)
//        {
//            using (TConnection conn = new TConnection())
//            {
//                conn.ConnectionString = connectionString;
//                return IsExist(conn, commandType, commandTextForLineCount, commandParameters);
//            }
//        }

//        /// <summary>
//        /// 执行影响数据库单行的sql语句,并返回执行成功与否的标识
//        /// </summary>
//        /// <param name="singleRowCommandText">影响数据库单行的sql语句</param>
//        /// <param name="commandParameters"></param>
//        /// <param name="commandType"></param>
//        /// <param name="connection"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteSingleRowNonQuery(TConnection connection, CommandType commandType, string singleRowCommandText, params TParameter[] commandParameters)
//        {
//            bool isSuccessful = true;
//            int returnValue = ExecuteNonQuery(connection, commandType, singleRowCommandText, commandParameters);

//            if (returnValue >= 1)
//            {
//                isSuccessful = true;
//            }
//            else
//            {
//                isSuccessful = false;
//            }

//            return isSuccessful;
//        }

//        /// <summary>
//        /// 执行影响数据库单行的sql语句,并返回执行成功与否的标识
//        /// </summary>
//        /// <param name="singleRowCommandText">影响数据库单行的sql语句</param>
//        /// <param name="commandParameters"></param>
//        /// <param name="commandType"></param>
//        /// <param name="connectionString"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteSingleRowNonQuery(string connectionString, CommandType commandType, string singleRowCommandText, params TParameter[] commandParameters)
//        {
//            using (TConnection conn = new TConnection())
//            {
//                conn.ConnectionString = connectionString;
//                return ExecuteSingleRowNonQuery(conn, commandType, singleRowCommandText, commandParameters);
//            }
//        }

//        /// <summary>
//        /// add parameter array to the cache
//        /// </summary>
//        /// <param name="cacheKey">Key to the parameter cache</param>
//        /// <param name="commandParameters">an array of SqlParamters to be cached</param>
//        public void CacheParameters(string cacheKey, params TParameter[] commandParameters)
//        {
//            parmCache[cacheKey] = commandParameters;
//        }

//        /// <summary>
//        /// Retrieve cached parameters
//        /// </summary>
//        /// <param name="cacheKey">key used to lookup parameters</param>
//        /// <returns>Cached SqlParamters array</returns>
//        public virtual TParameter[] GetCachedParameters(string cacheKey)
//        {
//            TParameter[] cachedParms = (TParameter[])parmCache[cacheKey];

//            if (cachedParms == null)
//            {
//                return null;
//            }

//            TParameter[] clonedParms = new TParameter[cachedParms.Length];

//            for (int i = 0, j = cachedParms.Length; i < j; i++)
//            {
//                clonedParms[i] = (TParameter)((ICloneable)cachedParms[i]).Clone();
//            }

//            return clonedParms;
//        }

//        /// <summary>
//        /// Prepare a command for execution
//        /// </summary>
//        /// <param name="command">SqlCommand object</param>
//        /// <param name="connection">SqlConnection object</param>
//        /// <param name="trans">SqlTransaction object</param>
//        /// <param name="commandType">Cmd type e.g. stored procedure or text</param>
//        /// <param name="commandText">Command text, e.g. Select * from Products</param>
//        /// <param name="commandParams">SqlParameters to use in the command</param>
//        protected virtual void PrepareCommand(TCommand command, TConnection connection, TTransaction trans, CommandType commandType, string commandText, TParameter[] commandParams)
//        {
//            if (connection.State != ConnectionState.Open)
//            {
//                connection.Open();
//            }

//            command.Connection = connection;
//            command.CommandText = commandText;

//            if (trans != null)
//            {
//                command.Transaction = trans;
//            }

//            command.CommandType = commandType;

//            if (commandParams != null)
//            {
//                foreach (TParameter parm in commandParams)
//                {
//                    command.Parameters.Add(parm);
//                }
//            }
//        }
//    }
//}
