using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Hiland.BasicLibrary.Data
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

        ///// <summary>
        ///// 根据xpath获取json的节点内容
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="jsonValue"></param>
        ///// <param name="xpath"></param>
        ///// <returns></returns>
        //public T getItemValue<T>(string jsonValue, string xpath)
        //{
        //    JObject jObject = JObject.Parse(jsonValue);
        //    return (dynamic)jObject.SelectToken(xpath);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="jsonValue"></param>
        ///// <param name="xpath"></param>
        ///// <param name="newValue"></param>
        ///// <returns></returns>
        //public string updateItemValue<T>(string jsonValue, string xpath, T newValue)
        //{
        //    JObject jObject = JObject.Parse(jsonValue);

        //    dynamic parentItem;
        //    var itemName = "";

        //    var pos = xpath.LastIndexOf(".");
        //    if (pos > -1)
        //    {
        //        var newPath = xpath.Substring(0, pos);
        //        itemName = xpath.Substring(pos + 1);

        //        parentItem = (dynamic)jObject.SelectToken(newPath);
        //    }
        //    else
        //    {
        //        itemName = xpath;
        //        parentItem = (dynamic)jObject;
        //    }

        //    parentItem[itemName] = newValue;


        //    var newContent = JsonConvert.SerializeObject(jObject);
        //    return newContent;
        //}
    }
}
