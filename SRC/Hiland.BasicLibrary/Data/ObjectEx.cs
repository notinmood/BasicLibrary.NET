using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary4.Data
{
    /// <summary>
    /// 对象帮助器的扩展
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(this T data)
        {
            return ObjectHelper.IsNullOrEmpty<T>(data);
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(this object data)
        {
            return ObjectHelper.IsNullOrEmpty(data);
        }
    }
}
