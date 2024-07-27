using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

//========================================================
// 在开源CPQuery的基础上进行了部分调整，原作者注释如下
// ------------------------------------------------------------------------
// CPQuery 是一个解决拼接SQL的新方法。
// CPQuery 可以让你采用拼接SQL的方式来编写参数化SQL 。
// 关于CPQuery的更多介绍请浏览：http://www.cnblogs.com/fish-li/archive/2012/09/10/CPQuery.html
// CPQuery 是一个开源的工具类，请在使用 CPQuery 时保留这段注释。
// 【 删除开源代码中的注释是可耻的行为！ 】
//========================================================

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// 
    /// </summary>
    public class CPQuery
    {
        /// <summary>
        /// 字符串参数的处理进度
        /// </summary>
        private enum SPStep
        {
            /// <summary>
            /// 没开始或者已完成一次字符串参数的拼接
            /// </summary>
            NotSet,

            /// <summary>
            /// 拼接时遇到一个单引号结束
            /// </summary>
            EndWith,

            /// <summary>
            ///  已跳过一次拼接
            /// </summary>
            Skip,
        }

        private int parameterCount;
        private StringBuilder commandTextContainer = new StringBuilder(1024);
        private Dictionary<string, QueryParameter> parameters = new Dictionary<string, QueryParameter>();

        /// <summary>
        /// 是否在传入的字符串中自动查找参数
        /// </summary>
        private bool isAutoDiscoverParameters;
        private SPStep step = SPStep.NotSet;

        /// <summary>
        /// 参数名称前缀
        /// </summary>
        /// <remarks>
        /// 在不同的数据库系统中，参数名称的前缀是不同的：SQLServer中为 “@”；SQLite中为“$”
        /// </remarks>
        private string parameterNamePrefix = "@";

        public CPQuery(string text, bool autoDiscoverParameters, string parameterNamePrefix)
        {
            commandTextContainer.Append(text);
            this.isAutoDiscoverParameters = autoDiscoverParameters;

            if (string.IsNullOrEmpty(parameterNamePrefix))
            {
                this.parameterNamePrefix = parameterNamePrefix;
            }
        }

        public static CPQuery New()
        {
            return new CPQuery(null, false, string.Empty);
        }

        public static CPQuery New(string text)
        {
            return new CPQuery(text, false, string.Empty);
        }

        public static CPQuery New(bool autoDiscoverParameters)
        {
            return new CPQuery(null, autoDiscoverParameters, string.Empty);
        }

        public static CPQuery New(string text, string parameterNamePrefix)
        {
            return new CPQuery(text, false, parameterNamePrefix);
        }

        public override string ToString()
        {
            return commandTextContainer.ToString();
        }

        /// <summary>
        /// 将拼接的字符串形成带参数的命令
        /// </summary>
        /// <param name="command">数据库访问命令</param>
        /// <remarks>
        /// 此方法执行完成后，命令内部携带参数
        /// </remarks>
        public void BindToCommand(ref IDbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            command.CommandText = commandTextContainer.ToString();
            command.Parameters.Clear();

            foreach (KeyValuePair<string, QueryParameter> kvp in parameters)
            {
                IDbDataParameter p = command.CreateParameter();
                p.ParameterName = kvp.Key;
                p.Value = kvp.Value.Value;
                command.Parameters.Add(p);
            }
        }


        private void AddSqlText(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }

            if (isAutoDiscoverParameters)
            {
                if (step == SPStep.NotSet)
                {
                    if (s[s.Length - 1] == '\'')
                    {	// 遇到一个单引号结束
                        commandTextContainer.Append(s.Substring(0, s.Length - 1));
                        step = SPStep.EndWith;
                    }
                    else
                    {
                        object val = TryGetValueFromString(s);
                        if (val == null)
                        {
                            commandTextContainer.Append(s);
                        }
                        else
                        {
                            this.AddParameter(CPQueryExtensions.GetQueryParameter(val));
                        }
                    }
                }
                else if (step == SPStep.EndWith)
                {
                    // 此时的s应该是字符串参数，不是SQL语句的一部分
                    // _step 在AddParameter方法中统一修改，防止中途拼接非字符串数据。
                    this.AddParameter(CPQueryExtensions.GetQueryParameter(s));
                }
                else
                {
                    if (s[0] != '\'')
                    {
                        throw new ArgumentException("正在等待以单引号开始的字符串，但参数不符合预期格式。");
                    }

                    // 找到单引号的闭合输入。
                    commandTextContainer.Append(s.Substring(1));
                    step = SPStep.NotSet;
                }
            }
            else
            {
                // 不检查单引号结尾的情况，此时认为一定是SQL语句的一部分。
                commandTextContainer.Append(s);
            }
        }

        private void AddParameter(QueryParameter p)
        {
            if (isAutoDiscoverParameters && step == SPStep.Skip)
            {
                throw new InvalidOperationException("正在等待以单引号开始的字符串，此时不允许再拼接其它参数。");
            }


            string name = "@hilandParameter" + (parameterCount++).ToString();
            commandTextContainer.Append(name);
            parameters.Add(name, p);


            if (isAutoDiscoverParameters && step == SPStep.EndWith)
            {
                step = SPStep.Skip;
            }
        }

        private object TryGetValueFromString(string s)
        {
            // 20，可以是byte, short, int, long, uint, ulong ...
            int number1 = 0;
            if (int.TryParse(s, out number1))
            {
                return number1;
            }

            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(s, out dt))
            {
                return dt;
            }

            // 23.45，可以是float, double, decimal
            decimal number5 = 0m;
            if (decimal.TryParse(s, out number5))
            {
                return number5;
            }

            // 其它类型全部放弃尝试。
            return null;
        }


        public static CPQuery operator +(CPQuery query, string s)
        {
            query.AddSqlText(s);
            return query;
        }
        public static CPQuery operator +(CPQuery query, QueryParameter p)
        {
            query.AddParameter(p);
            return query;
        }
    }

    public sealed class QueryParameter
    {
        private object value;

        public QueryParameter(object value)
        {
            this.value = value;
        }

        public object Value
        {
            get { return value; }
        }

        public static explicit operator QueryParameter(string a)
        {
            return new QueryParameter(a);
        }

        public static implicit operator QueryParameter(int a)
        {
            return new QueryParameter(a);
        }

        public static implicit operator QueryParameter(decimal a)
        {
            return new QueryParameter(a);
        }

        public static implicit operator QueryParameter(DateTime a)
        {
            return new QueryParameter(a);
        }
        // 其它需要支持的隐式类型转换操作符重载请自行添加。
    }

    public static class CPQueryExtensions
    {
        public static CPQuery GetCPQuery(string s)
        {
            return new CPQuery(s, false, string.Empty);
        }

        public static CPQuery GetCPQuery(string s, bool autoDiscoverParameters)
        {
            return new CPQuery(s, autoDiscoverParameters, string.Empty);
        }

        public static QueryParameter GetQueryParameter(object b)
        {
            return new QueryParameter(b);
        }
    }
}
