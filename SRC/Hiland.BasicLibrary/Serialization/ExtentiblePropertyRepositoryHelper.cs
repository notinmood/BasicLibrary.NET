//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using Hiland.BasicLibrary.DataBase;

//namespace Hiland.BasicLibrary.Serialization
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public static class ExtentiblePropertyRepositoryHelper
//    {
//        /// <summary>
//        /// 从IDataReader里获取序列化信息
//        /// </summary>
//        /// <param name="reader">IDataReader</param>
//        /// <returns>SerializerData</returns>
//        public static SerializerData PopulateSerializerDataFromDataReader(IDataReader reader)
//        {
//            return PopulateSerializerDataFromDataReader(reader, "PropertyNames", "PropertyValues");
//        }

//        /// <summary>
//        /// 从IDataReader里获取序列化信息
//        /// </summary>
//        /// <param name="reader">IDataReader</param>
//        /// <param name="propertyNames">propertyNames字段名称</param>
//        /// <param name="propertyValues">propertyValues字段名称</param>        
//        /// <returns>SerializerData</returns>
//        public static SerializerData PopulateSerializerDataFromDataReader(IDataReader reader, string propertyNames, string propertyValues)
//        {
//            SerializerData data = new SerializerData();

//            if (DataReaderHelper.IsExistField(reader, propertyNames) && Convert.IsDBNull(reader[propertyNames]) == false)
//            {
//                data.Keys = Convert.ToString(reader[propertyNames]);
//            }
//            else
//            {
//                data.Keys = string.Empty;
//            }

//            if (DataReaderHelper.IsExistField(reader, propertyValues) && Convert.IsDBNull(reader[propertyValues]) == false)
//            {
//                data.Values = Convert.ToString(reader[propertyValues]);
//            }
//            else
//            {
//                data.Values = string.Empty;
//            }

//            return data;
//        }
//    }
//}
