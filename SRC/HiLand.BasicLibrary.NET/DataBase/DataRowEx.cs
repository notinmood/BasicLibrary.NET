using System.Data;
using HiLand.Utility.DataBase;

namespace HiLand.Utility4.DataBase
{
    /// <summary>
    ///  DataRow扩展类
    /// </summary>
    public static class DataRowEx
    {
        /// <summary>
        /// 判断某个字段在DataRow中是否存在
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistField( this DataRow row, string fieldName)
        {
            return DataRowHelper.IsExistField(row,fieldName);
        }

        /// <summary>
        /// 判断某个字段在DataRow中是否存在,并且其值不为null
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsExistFieldAndNotNull(this DataRow row, string fieldName)
        {
            return DataRowHelper.IsExistFieldAndNotNull(row,fieldName);
        }

        /// <summary>
        /// 获取DataRow中的某个字段的值，如果不存在此字段或者值为null那么返回T的缺省值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFiledValue<T>(this DataRow reader, string fieldName)
        {
            return DataRowHelper.GetFiledValue<T>(reader,fieldName);
        }
    }
}
