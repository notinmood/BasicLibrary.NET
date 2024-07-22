using System;
using System.Configuration;
using System.Reflection;

namespace HiLand.Utility.Data
{
    //TODO:还需要另外考虑转义编码 %27等
    /// <summary>
    /// SQL注入操作辅助类
    /// </summary>
    public static class SQLInjectionHelper
    {
        private static string[] filterSqlKeyWordArray;
        private static string[] GetFilterSQLKeyWords()
        {
            if (filterSqlKeyWordArray == null || filterSqlKeyWordArray.Length == 0)
            {
                string filterSqlString = string.Empty;
                //一般将关键词组配置在webconfig中(其中如果有[ ]两个符号必须在字符串的最前面)
                filterSqlString = ConfigurationManager.AppSettings["FilterSqlString"];
                if (string.IsNullOrEmpty(filterSqlString))
                {
                    filterSqlString = "[|]|‘|--|declare|exec|varchar|cursor|begin|open|drop|creat|select|truncate";
                }

                filterSqlKeyWordArray = filterSqlString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return filterSqlKeyWordArray;
        }

        /// <summary>
        /// 判断待验证信息对SQL注入来说是否为安全的
        /// </summary>
        /// <param name="informationToValide">判断待验证信息</param>
        /// <returns></returns>
        public static bool IsSQLInjectionSafe(string informationToValide)
        {
            bool returnValue = true;
            try
            {
                if (informationToValide.Trim() != "")
                {
                    foreach (string filterSql in GetFilterSQLKeyWords())
                    {
                        if (informationToValide.ToLower().IndexOf(filterSql) >= 0)
                        {
                            returnValue = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }

        private static string replacedStringFormat = "[{0}]";

        /// <summary>
        /// 向SQL数据库保持数据前转化非法字符
        /// </summary>
        /// <param name="informationToConvert"></param>
        /// <returns></returns>
        public static string GetSafeSqlBeforeSave(string informationToConvert)
        {
            foreach (string filterSqlKeyWord in GetFilterSQLKeyWords())
            {
                informationToConvert = informationToConvert.Replace(filterSqlKeyWord, string.Format(replacedStringFormat, filterSqlKeyWord));
            }

            return informationToConvert;
        }

        /// <summary>
        /// 从SQL数据库获取数据后将非法信息还原（为了后续的页面显示）
        /// </summary>
        /// <param name="informationToRecover"></param>
        /// <returns></returns>
        public static string RecoverOriginalSqlAfterLoad(string informationToRecover)
        {
            foreach (string filterSqlKeyWord in GetFilterSQLKeyWords())
            {
                informationToRecover = informationToRecover.Replace(string.Format(replacedStringFormat, filterSqlKeyWord), filterSqlKeyWord);
            }

            return informationToRecover;
        }

        /// <summary>
        /// 向SQL数据库保持数据实体前转化非法字符
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>其仅能处理实体的一级属性</remarks>
        public static T GetSafeEntityBeforeSave<T>(T entity)
        {
            Type entityType = typeof(T);
            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite && propertyInfo.CanRead && propertyInfo.PropertyType == typeof(string))
                {
                    string propertyValue = Convert.ToString(propertyInfo.GetValue(entity, null));
                   propertyValue = GetSafeSqlBeforeSave(propertyValue);
                   propertyInfo.SetValue(entity, propertyValue, null);
                }
            }

            return entity;
        }

        /// <summary>
        /// 从SQL数据库获取数据实体后将非法信息还原（为了后续的页面显示）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>其仅能处理实体的一级属性</remarks>
        public static T RecoverOriginalEntityAfterLoad<T>(T entity)
        {
            Type entityType = typeof(T);
            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite && propertyInfo.CanRead && propertyInfo.PropertyType == typeof(string))
                {
                    string propertyValue = Convert.ToString(propertyInfo.GetValue(entity, null));
                    propertyValue = RecoverOriginalSqlAfterLoad(propertyValue);
                    propertyInfo.SetValue(entity, propertyValue, null);
                }
            }

            return entity;
        }
    }
}
