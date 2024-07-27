using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HiLand.Utility.DataBase;

namespace HiLand.Utility4.DataBase
{
    /// <summary>
    /// DataReader扩展类
    /// </summary>
    public static class DataReaderEx
    {
        /// <summary>
        /// 判定某个字段在IDataReader中是否存在
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistField(this IDataReader reader, string fieldName)
        {
            return DataReaderHelper.IsExistField(reader,fieldName);
        }

        /// <summary>
        /// 判定某个字段在IDataReader中是否存在，并且其值不为null
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistFieldAndNotNull(this IDataReader reader, string fieldName)
        {
            return DataReaderHelper.IsExistFieldAndNotNull(reader,fieldName);
        }


        /// <summary>
        /// 获取IDataReader中的某个字段的值，如果不存在此字段或者值为null那么返回T的缺省值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFiledValue<T>(this IDataReader reader, string fieldName)
        {
            return DataReaderHelper.GetFiledValue<T>(reader,fieldName);
        }
    }
}