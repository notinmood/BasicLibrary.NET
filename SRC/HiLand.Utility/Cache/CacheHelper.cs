using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Event;
using HiLand.Utility.Setting;

namespace HiLand.Utility.Cache
{
    /// <summary>
    /// 缓存应用辅助类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 对象获取（首先从缓存池内获取，如果获取不到那么会调用func方法）
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Access<TResult>(string cacheKey, int cacheSeconds, Funcs<TResult> func)
        {
            TResult cachedObject = default(TResult);
            if (IsUseCache == true)
            {
                ICache cache = CacheFactory.Create();
                cachedObject = cache.Get<TResult>(cacheKey);
                TResult defaultValue = default(TResult);

                if (object.Equals(cachedObject, defaultValue))
                {
                    cachedObject = func();
                    cache.Insert(cacheKey, cachedObject, cacheSeconds);
                }
            }
            else
            {
                cachedObject = func();
            }

            return cachedObject;
        }

        /// <summary>
        /// 对象获取（首先从缓存池内获取，如果获取不到那么会调用func方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="func"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TResult Access<T, TResult>(string cacheKey, int cacheSeconds, Funcs<T, TResult> func, T t)
        {
            TResult cachedObject = default(TResult);
            if (IsUseCache == true)
            {
                ICache cache = CacheFactory.Create();
                cachedObject = cache.Get<TResult>(cacheKey);
                TResult defaultValue = default(TResult);

                if (object.Equals(cachedObject, defaultValue))
                {
                    cachedObject = func(t);
                    cache.Insert(cacheKey, cachedObject, cacheSeconds);
                }
            }
            else
            {
                cachedObject = func(t);
            }

            return cachedObject;
        }

        /// <summary>
        /// 对象获取（首先从缓存池内获取，如果获取不到那么会调用func方法）
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="func"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static TResult Access<T1, T2, TResult>(string cacheKey, int cacheSeconds, Funcs<T1, T2, TResult> func, T1 t1, T2 t2)
        {
            TResult cachedObject = default(TResult);
            if (IsUseCache == true)
            {
                ICache cache = CacheFactory.Create();
                cachedObject = cache.Get<TResult>(cacheKey);
                TResult defaultValue = default(TResult);

                if (object.Equals(cachedObject, defaultValue))
                {
                    cachedObject = func(t1, t2);
                    cache.Insert(cacheKey, cachedObject, cacheSeconds);
                }
            }
            else
            {
                cachedObject = func(t1, t2);
            }

            return cachedObject;
        }

        /// <summary>
        /// 对象获取（首先从缓存池内获取，如果获取不到那么会调用func方法）
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="func"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <returns></returns>
        public static TResult Access<T1, T2, T3, TResult>(string cacheKey, int cacheSeconds, Funcs<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3)
        {
            TResult cachedObject = default(TResult);
            if (IsUseCache == true)
            {
                ICache cache = CacheFactory.Create();
                cachedObject = cache.Get<TResult>(cacheKey);
                TResult defaultValue = default(TResult);

                if (object.Equals(cachedObject, defaultValue))
                {
                    cachedObject = func(t1, t2, t3);
                    cache.Insert(cacheKey, cachedObject, cacheSeconds);
                }
            }
            else
            {
                cachedObject = func(t1, t2, t3);
            }

            return cachedObject;
        }

        /// <summary>
        /// 对象获取（首先从缓存池内获取，如果获取不到那么会调用func方法）
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="func"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <returns></returns>
        public static TResult Access<T1, T2, T3, T4, TResult>(string cacheKey, int cacheSeconds, Funcs<T1, T2, T3, T4, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            TResult cachedObject = default(TResult);
            if (IsUseCache == true)
            {
                ICache cache = CacheFactory.Create();
                cachedObject = cache.Get<TResult>(cacheKey);
                TResult defaultValue = default(TResult);

                if (object.Equals(cachedObject, defaultValue))
                {
                    cachedObject = func(t1, t2, t3, t4);
                    cache.Insert(cacheKey, cachedObject, cacheSeconds);
                }
            }
            else
            {
                cachedObject = func(t1, t2, t3, t4);
            }

            return cachedObject;
        }

        /// <summary>
        /// 对象获取（首先从缓存池内获取，如果获取不到那么会调用func方法）
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="func"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <param name="t5"></param>
        /// <returns></returns>
        public static TResult Access<T1, T2, T3, T4, T5, TResult>(string cacheKey, int cacheSeconds, Funcs<T1, T2, T3, T4, T5, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            TResult cachedObject = default(TResult);
            if (IsUseCache == true)
            {
                ICache cache = CacheFactory.Create();
                cachedObject = cache.Get<TResult>(cacheKey);
                TResult defaultValue = default(TResult);

                if (object.Equals(cachedObject, defaultValue))
                {
                    cachedObject = func(t1, t2, t3, t4, t5);
                    cache.Insert(cacheKey, cachedObject, cacheSeconds);
                }
            }
            else
            {
                cachedObject = func(t1, t2, t3, t4, t5);
            }

            return cachedObject;
        }

        /// <summary>
        /// 在缓存池中加入缓冲对象
        /// </summary>
        /// <param name="cacheKey">缓冲对象的Key</param>
        /// <param name="cachedObject">缓冲对象实体</param>
        /// <param name="cacheSeconds">缓冲对象的时间（秒为单位）</param>
        public static void Set(string cacheKey, object cachedObject, int cacheSeconds)
        {
            ICache cache = CacheFactory.Create();
            cache.Insert(cacheKey, cachedObject, cacheSeconds);
        }

        /// <summary>
        /// 在缓存池中移除缓冲对象
        /// </summary>
        /// <param name="cacheKey">缓冲对象的Key</param>
        public static void Remove(string cacheKey)
        {
            ICache cache = CacheFactory.Create();
            cache.Remove(cacheKey);
        }

        /// <summary>
        /// 在缓存池中按照缓存键匹配的方式移除缓冲对象
        /// </summary>
        /// <param name="cacheKeyPattern">缓存键匹配的模式字符串</param>
        public static void RemoveByPattern(string cacheKeyPattern)
        {
            ICache cache = CacheFactory.Create();
            cache.RemoveByPattern(cacheKeyPattern);
        }


        private static bool? isUseCache = null;
        /// <summary>
        /// 是否启用缓存
        /// </summary>
        public static bool IsUseCache
        {
            get
            {
                if (isUseCache.HasValue == false)
                {
                    isUseCache = Config.GetAppSetting<bool>("IsUseCache", true);
                }

                return isUseCache.Value;
            }
        }



        private static int afewTime = 0;
        /// <summary>
        /// 这个地方使用配置，允许开发人员在config中配置这个具体的缓存时间（单位为分钟；如果不配置其缺省为1分钟）
        /// </summary>
        public static int AFewTime
        {
            get
            {
                if (afewTime == 0)
                {
                    int cacheMintues = Config.GetAppSetting<int>("GeneralCacheMintues");
                    if (cacheMintues <= 0)
                    {
                        cacheMintues = 1;
                    }
                    afewTime = cacheMintues * 60;
                }

                return afewTime;
            }
        }

        /// <summary>
        /// 缓存常量（一分钟）
        /// </summary>
        public static int OneMintue = 60;

        /// <summary>
        /// 缓存常量（十分钟）
        /// </summary>
        public static int TenMintues = 600;

        /// <summary>
        /// 缓存常量（一小时）
        /// </summary>
        public static int OneHour = 3600;

        /// <summary>
        /// 缓存常量（最大缓冲时间）
        /// </summary>
        public static int MaxMintues = int.MaxValue;
    }
}
