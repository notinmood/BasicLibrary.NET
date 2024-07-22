using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 集合类型操作辅助器
    /// </summary>
    /// <remarks>
    /// 此类中的方法仅为dotnet2提供快捷操作方法，在dotnet4中请使用IEnumerable的扩展方法
    /// </remarks>
    public static class CollectionHelper
    {
        /// <summary>
        /// 判断两个集合中是否存在至少一个共同的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection1"></param>
        /// <param name="collection2"></param>
        /// <returns></returns>
        public static bool IsExistAtLeastOneElement<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            foreach (T currentItemAt1 in collection1)
            {
                foreach (T currentItemAt2 in collection2)
                {
                    if (object.Equals(currentItemAt2, currentItemAt1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 判断某个值，在给定的集合中是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetValue"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <remarks>在dotnet4中，直接请直接使用IEnumerable`T`的扩展方法Contains</remarks>
        public static bool IsExist<T>(T targetValue, IEnumerable<T> collection)
        {
            foreach (T currentItem in collection)
            {
                if (object.Equals(targetValue, currentItem))
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 判断某个值，在给定的集合中是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetValue"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <remarks>在dotnet4中，直接请直接使用IEnumerable的扩展方法Contains</remarks>
        public static bool IsExist<T>(T targetValue, params T[] collection)
        {
            IEnumerable<T> enumerableCollection = collection as IEnumerable<T>;
            return IsExist<T>(targetValue, enumerableCollection);
        }

        /// <summary>
        /// 将两个字典中的内容合并后返回
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="originalData"></param>
        /// <param name="addedData"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果两个字典中有相同的项目，则用addedData中项的值覆盖originalData中项的值
        /// </remarks>
        public static IDictionary<K, V> Merger<K, V>(IDictionary<K, V> originalData, IDictionary<K, V> addedData)
        {
            return Merger<K, V>(originalData, addedData, true);
        }

        /// <summary>
        /// 将两个字典中的内容合并后返回
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="originalData"></param>
        /// <param name="addedData"></param>
        /// <param name="isCoverSameOriginalItem">如果两个字典中有相同的项目，是否使用addedData中项的值覆盖originalData中项的值</param>
        /// <returns></returns>
        public static IDictionary<K, V> Merger<K, V>(IDictionary<K, V> originalData, IDictionary<K, V> addedData, bool isCoverSameOriginalItem)
        {
            foreach (KeyValuePair<K, V> kvp in addedData)
            {
                if (originalData.ContainsKey(kvp.Key))
                {
                    if (isCoverSameOriginalItem == true)
                    {
                        originalData[kvp.Key] = kvp.Value;
                    }
                    else
                    {
                        //保留原值不动
                    }
                }
                else
                {
                    originalData.Add(kvp);
                }
            }

            return originalData;
        }

        /// <summary>
        /// 连接数组中的各个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seperator">连接时的分割符</param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string Concat<T>(string seperator, params T[] collection)
        {
            IEnumerable<T> enumerableCollection = collection as IEnumerable<T>;
            return Concat(seperator, enumerableCollection);
        }

        /// <summary>
        /// 连接可枚举集合中的各个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seperator">连接时的分割符</param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string Concat<T>(string seperator, IEnumerable<T> collection)
        {
            return Concat<T>(seperator, string.Empty, string.Empty, collection);
        }

        /// <summary>
        /// 连接可枚举集合中的各个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seperator">连接时的分割符</param>
        /// <param name="itemPostfix">元素的后置字符串</param>
        /// <param name="itemPrefix">元素的前置字符串</param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string Concat<T>(string seperator, string itemPrefix, string itemPostfix, IEnumerable<T> collection)
        {
            string result = string.Empty;
            foreach (T currentItem in collection)
            {
                result += string.Format("{0}{1}{2}{3}", itemPrefix, currentItem.ToString(), itemPostfix, seperator);
            }

            if (string.IsNullOrEmpty(result) == false && string.IsNullOrEmpty(seperator) == false && result.EndsWith(seperator))
            {
                int lastSeperatorPosition = result.LastIndexOf(seperator);
                result = result.Substring(0, lastSeperatorPosition);
            }

            return result;
        }
    }
}

