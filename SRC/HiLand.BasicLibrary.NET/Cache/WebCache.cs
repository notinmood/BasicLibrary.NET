//using System;
//using System.Collections;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Caching;
//using HiLand.Utility.Pattern;

//namespace HiLand.Utility.Cache
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class WebCache : ICache
//    {
//        /// <summary>
//        /// CacheDependency 说明
//        /// 如果您向 Cache 中添加某个具有依赖项的项，当依赖项更改时，
//        /// 该项将自动从 Cache 中删除。例如，假设您向 Cache 中添加某项，
//        /// 并使其依赖于文件名数组。当该数组中的某个文件更改时，
//        /// 与该数组关联的项将从缓存中删除。
//        /// [C#] 
//        /// Insert the cache item.
//        /// CacheDependency dep = new CacheDependency(fileName, dt);
//        /// cache.Insert("key", "value", dep);
//        /// </summary>
//        public static readonly int DayFactor = 17280;
//        public static readonly int HourFactor = 720;
//        public static readonly int MinuteFactor = 12;
//        public static readonly double SecondFactor = 0.2;

//        private static readonly System.Web.Caching.Cache cache;

//        private static int Factor = 1440;


//        static WebCache()
//        {
//            HttpContext context = HttpContext.Current;
//            if (context != null)
//            {
//                cache = context.Cache;
//            }
//            else
//            {
//                cache = HttpRuntime.Cache;
//            }
//        }

//        /// <summary>
//        /// 单件模式
//        /// </summary>
//        public static WebCache Instance
//        {
//            get { return Singleton<WebCache>.Instance; }
//        }

//        /// <summary>
//        /// 一次性清除所有缓存
//        /// </summary>
//        public void Clear()
//        {
//            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
//            ArrayList al = new ArrayList();
//            while (CacheEnum.MoveNext()) //逐个清除
//            {
//                al.Add(CacheEnum.Key);
//            }

//            foreach (string key in al)
//            {
//                cache.Remove(key);
//            }

//        }

//        /// <summary>
//        /// 按照匹配模式移除缓存信息
//        /// </summary>
//        /// <param name="pattern"></param>
//        public void RemoveByPattern(string pattern)
//        {
//            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
//            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
//            while (CacheEnum.MoveNext())
//            {
//                if (regex.IsMatch(CacheEnum.Key.ToString()))
//                {
//                    cache.Remove(CacheEnum.Key.ToString());
//                }
//            }
//        }

//        /// <summary>
//        /// 清除特定的缓存
//        /// </summary>
//        /// <param name="key"></param>
//        public void Remove(string key)
//        {
//            cache.Remove(key);
//        }

//        /// <summary>
//        /// 缓存OBJECT. 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        public void Insert(string key, object obj)
//        {
//            Insert(key, obj, null, 1);
//        }


//        /// <summary>
//        /// 缓存obj 并建立依赖项
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="dep"></param>
//        public void Insert(string key, object obj, CacheDependency dep)
//        {
//            Insert(key, obj, dep, MinuteFactor * 3);
//        }


//        /// <summary>
//        /// 按秒缓存对象
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="seconds"></param>
//        public void Insert(string key, object obj, int seconds)
//        {
//            Insert(key, obj, null, seconds);
//        }


//        /// <summary>
//        /// 按秒缓存对象 并存储优先级
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="seconds"></param>
//        /// <param name="priority"></param>
//        public void Insert(string key, object obj, int seconds, CacheItemPriority priority)
//        {
//            Insert(key, obj, null, seconds, priority);
//        }


//        /// <summary>
//        /// 按秒缓存对象 并建立依赖项
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="dep"></param>
//        /// <param name="seconds"></param>
//        public void Insert(string key, object obj, CacheDependency dep, int seconds)
//        {
//            Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
//        }


//        /// <summary>
//        /// 按秒缓存对象 并建立具有优先级的依赖项
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="dep"></param>
//        /// <param name="seconds"></param>
//        /// <param name="priority"></param>
//        public void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
//        {
//            if (obj != null)
//            {
//                cache.Insert(key, obj, dep, DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero, priority, null);
//            }

//        }

//        /// <summary>
//        /// 最大时间缓存
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        public void Max(string key, object obj)
//        {
//            Max(key, obj, null);
//        }


//        /// <summary>
//        /// 具有依赖项的最大时间缓存
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="dep"></param>
//        public void Max(string key, object obj, CacheDependency dep)
//        {
//            if (obj != null)
//            {
//                cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
//            }
//        }


//        /// <summary>
//        /// Insert an item into the cache for the Maximum allowed time
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        public void Permanent(string key, object obj)
//        {
//            Permanent(key, obj, null);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <param name="dep"></param>
//        public void Permanent(string key, object obj, CacheDependency dep)
//        {
//            if (obj != null)
//            {
//                cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public object Get(string key)
//        {
//            return cache[key];
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public T Get<T>(string key)
//        {
//            T t = default(T);
//            object o = cache[key];
//            if (o != null)
//            {
//                try
//                {
//                    t = (T)o;
//                }
//                catch{}
//            }
            
//            return t;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public object this[string key]
//        {
//            get { return cache[key]; }
//        }


//        /// <summary>
//        /// Return int of seconds * SecondFactor
//        /// </summary>
//        public static int SecondFactorCalculate(int seconds)
//        {
//            // Insert method below takes integer seconds, so we have to round any fractional values
//            return Convert.ToInt32(Math.Round((double)seconds * SecondFactor));
//        }

//        /// <summary>
//        /// 获取缓存池中缓存对象的数量
//        /// </summary>
//        public int Count
//        {
//            get { return cache.Count; }
//        }
//    }
//}
