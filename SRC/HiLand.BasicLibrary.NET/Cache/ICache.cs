using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 插入值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="o">值</param>
        void Insert(string key, object o);

        /// <summary>
        /// 插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="o"></param>
        /// <param name="seconds"></param>
        void Insert(string key, object o, int seconds);

        /// <summary>
        /// 最大时间缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="o"></param>
        void Max(string key, object o);

        /// <summary>
        /// 移除值
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// 按照规则移除缓存
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清除所有信息
        /// </summary>
        void Clear();

        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>返回值</returns>
        object this[string key]
        {
            get;
        }

        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        ///  获得值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 获得使用缓存总数
        /// </summary>
        int Count
        {
            get;
        }
    }
}
