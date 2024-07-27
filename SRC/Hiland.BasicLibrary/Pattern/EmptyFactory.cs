using System;
using System.Collections.Generic;

namespace HiLand.Utility.Pattern
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// 虽然这样写可以满足要求，但是可以发现基本没什么好处，写EmptyFactory还不如new 来得快。
    /// 不过里面实现缓存对象的机制，在一定程度上能缓解对系统的压力。
    /// </remarks>
    public static class EmptyFactory<T> where T : /*IEnumerable, */ new()
    {

        private static Dictionary<Type, T> cacheEmptyObjects = new Dictionary<Type, T>();

        public static T Empty()
        {
            Type genericType = typeof(T);
            if (cacheEmptyObjects.ContainsKey(genericType))
            {
                return cacheEmptyObjects[genericType];
            }
            else
            {
                T tempEmptyObject = new T();
                cacheEmptyObjects.Add(genericType, tempEmptyObject);
                return tempEmptyObject;
            }
        }
    }
}
