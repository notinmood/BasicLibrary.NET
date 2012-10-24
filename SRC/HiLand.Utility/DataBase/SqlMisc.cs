using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// 数据库语句杂项
    /// </summary>
    public static class SqlMisc
    {
        /// <summary>
        /// 通过名称和值构建数据查询参数
        /// </summary>
        /// <typeparam name="TParameter">参数的类型</typeparam>
        /// <typeparam name="T">参数值的类型</typeparam>
        /// <param name="parameterName">参数名称</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns></returns>
        public static TParameter GenerateParameter<TParameter,T>(string parameterName,T parameterValue) where TParameter : IDataParameter, IDbDataParameter,new()
        {
            return GenerateParameter<TParameter, T>("@",parameterName,parameterValue);
        }

        /// <summary>
        /// 通过名称和值构建数据查询参数
        /// </summary>
        /// <typeparam name="TParameter">参数的类型</typeparam>
        /// <typeparam name="T">参数值的类型</typeparam>
        /// <param name="parameterNamePrefix">参数名称前缀</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns></returns>
        public static TParameter GenerateParameter<TParameter, T>(string parameterNamePrefix, string parameterName, T parameterValue) where TParameter : IDataParameter, IDbDataParameter, new()
        {
            TParameter parameter = new TParameter();
            parameter.ParameterName = string.Format("{0}{1}", parameterNamePrefix, parameterName);
            parameter.Value = parameterValue;

            return parameter;
        }
    }
}
