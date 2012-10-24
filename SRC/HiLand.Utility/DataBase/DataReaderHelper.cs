using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HiLand.Utility.DataBase
{
    public static class DataReaderHelper
    {
        /// <summary>
        /// 判定某个字段在IDataReader中是否存在
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistField(IDataReader reader, string fieldName)
        {
            bool isSuccessfule = false;
            fieldName = fieldName.ToLower();
            int fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                string currentFieldName = reader.GetName(i).ToLower();
                if (currentFieldName == fieldName)
                {
                    isSuccessfule = true;
                    break;
                }
            }
            return isSuccessfule;
        }

        /// <summary>
        /// 判定某个字段在IDataReader中是否存在，并且其值不为null
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistFieldAndNotNull(IDataReader reader, string fieldName)
        {
            if (DataReaderHelper.IsExistField(reader, fieldName) && Convert.IsDBNull(reader[fieldName]) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取IDataReader中的某个字段的值，如果不存在此字段或者值为null那么返回T的缺省值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFiledValue<T>(IDataReader reader, string fieldName)
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
