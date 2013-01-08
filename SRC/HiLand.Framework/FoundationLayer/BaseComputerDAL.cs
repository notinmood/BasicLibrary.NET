using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.DataBase;
using System.Data;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 数据访问的基类(仅处理计算机数据逻辑部分，不处理业务数据逻辑部分)
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class BaseComputerDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter> 
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        #region 计算机逻辑基本信息
        /// <summary>
        /// 数据库访问通用操作单例
        /// </summary>
        protected CommonGeneral<TTransaction, TConnection, TCommand, TDataReader, TParameter> CommonGeneralInstance
        {
            get
            {
                return CommonGeneral<TTransaction, TConnection, TCommand, TDataReader, TParameter>.Instance;
            }
        }

        /// <summary>
        /// 数据库访问辅助器扩展单例
        /// </summary>
        public static CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter> HelperExInstance
        {
            get
            {
                return CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter>.Instance;
            }
        }

        /// <summary>
        /// 参数名称前缀
        /// </summary>
        /// <remarks>
        /// 在不同的数据库系统中，参数名称的前缀是不同的：SQLServer中为 “@”；SQLite中为“$”
        /// </remarks>
        protected virtual string ParameterNamePrefix
        {
            get
            {
                return "@";
            }
        }

        /// <summary>
        /// 通过名称和值构建数据查询参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        protected TParameter GenerateParameter<T>(string parameterName, T parameterValue)
        {
            return SqlMisc.GenerateParameter<TParameter, T>(ParameterNamePrefix, parameterName, parameterValue);
        }
        #endregion
    }
}
