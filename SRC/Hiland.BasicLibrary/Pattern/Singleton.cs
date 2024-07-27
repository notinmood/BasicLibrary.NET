using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Pattern
{
    /// <summary>
    /// 通用的单例模式类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>通过嵌套类，来对线程安全进行保证，具体可参考http://www.yoda.arachsys.com/csharp/singleton.html</remarks>
    public static class Singleton<T> where T : new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 此处的静态构建函数能保证属性的延时加载
        /// </remarks>
        static Singleton()
        {
        }

        /// <summary>
        /// 具体的单例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                return SingletonCreator.Instance;
            }
        }

        private class SingletonCreator 
        {
            static SingletonCreator() {}
            // 通过私有构造函数实例化的私有对象
            internal static readonly T Instance = new T();
        }
    }
}
