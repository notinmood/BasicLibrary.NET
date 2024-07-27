using System;
using System.Data;

namespace Hiland.BasicLibrary.DataBase
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataRowHelper
    {
        /// <summary>
        /// 判断某个字段在DataRow中是否存在
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistField(DataRow row, string fieldName)
        {
            bool isSuccessfule = false;
            fieldName = fieldName.ToLower();
            
            if (row == null || row.Table == null)
            {
                isSuccessfule = false;
            }
            else
            {
                DataColumnCollection dcc = row.Table.Columns;
                int fieldCount = dcc.Count;
                for (int i = 0; i < fieldCount; i++)
                {
                    string currentFieldName = dcc[i].ColumnName.ToLower();
                    if (currentFieldName == fieldName)
                    {
                        isSuccessfule = true;
                        break;
                    }
                }
            }
            return isSuccessfule;
        }


        /// <summary>
        /// 判断某个字段在DataRow中是否存在,并且其值不为null
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistFieldAndNotNull(DataRow row, string fieldName)
        {
            if (DataRowHelper.IsExistField(row, fieldName) && Convert.IsDBNull(row["fieldName"]) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取DataRow中的某个字段的值，如果不存在此字段或者值为null那么返回T的缺省值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFiledValue<T>(DataRow reader, string fieldName)
        {
            if (IsExistFieldAndNotNull(reader, fieldName) == true)
            {
                object obj = reader[fieldName];
                return (T)obj;
            }
            else
            {
                T defaultValue = default(T);
                return defaultValue;
            }
        }
    }
}
