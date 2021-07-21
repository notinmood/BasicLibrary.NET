using Newtonsoft.Json;
using System;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// JSON数据操作辅助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 获取某一个对象的JSON格式字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string Serialize(Object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        /// <summary>
        /// 从某对象的JSON格式字符串获取其实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonValue"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string jsonValue)
        { 
            return JsonConvert.DeserializeObject<T>(jsonValue);
        }
    }
}
