//using System;
//using System.Collections.Generic;
//using System.Text;
//using Hiland.BasicLibrary.Setting;

//namespace Hiland.BasicLibrary.Cache
//{
//    /// <summary>
//    /// 缓存实例工厂
//    /// </summary>
//    public class CacheFactory
//    {
//        /* 可选配置项
//         * cacheClassType 表示一个能提供缓存功能的类型描述
//         */

//        private static ICache cache;
//        /// <summary>
//        /// 创建ICache的具体实例
//        /// </summary>
//        /// <returns></returns>
//        /// <remarks>
//        /// 如果引入自定义的缓存系统（此自定义缓存系统必须实现接口ICache），
//        /// 请配置节点cacheClassType
//        /// </remarks>
//        public static ICache Create()
//        {
//            if (cache == null)
//            {
//                string cacheClassTypeString = Config.GetAppSetting("cacheClassType");
//                if (string.IsNullOrEmpty(cacheClassTypeString))
//                {
//                    cache = WebCache.Instance;
//                }
//                else
//                {
//                    Type cacheClassType = Type.GetType(cacheClassTypeString);
//                    cache = Activator.CreateInstance(cacheClassType) as ICache;
//                }
//            }

//            return cache;
//        }
//    }
//}
