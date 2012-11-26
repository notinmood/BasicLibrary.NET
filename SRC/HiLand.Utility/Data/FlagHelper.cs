using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// Flag类型的数据操作辅助类
    /// </summary>
    public static class FlagHelper
    {
        #region 具有flag功能的数字组合的判断
        /// <summary>
        /// 判断某一个flag类型的枚举值，是否在一个给定的枚举集合内
        /// </summary>
        /// <param name="flaggedItem"></param>
        /// <param name="flaggedCollection"></param>
        /// <returns></returns>
        public static bool ContainsFlag(int flaggedCollection, int flaggedItem)
        {
            bool result = false;
            if ((flaggedItem & flaggedCollection) == flaggedItem)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static int AddFlag(int flaggedCollection, int flaggedItem)
        {
            return Convert.ToInt32(flaggedCollection) | Convert.ToInt32(flaggedItem);
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static int RemoveFlag(int flaggedCollection, int flaggedItem)
        {
            return Convert.ToInt32(flaggedCollection) & ~Convert.ToInt32(flaggedItem);
        }
        #endregion

        #region 具有flag功能的枚举组合的判断
        /// <summary>
        /// 判断某一个flag类型的枚举值，是否在一个给定的枚举集合内
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flaggedItem"></param>
        /// <param name="flaggedCollection"></param>
        /// <returns></returns>
        public static bool ContainsFlag<TEnum>(TEnum flaggedCollection, TEnum flaggedItem) where TEnum : struct
        {
            return ContainsFlag(Convert.ToInt32(flaggedCollection), Convert.ToInt32(flaggedItem));
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static TEnum AddFlag<TEnum>(TEnum flaggedCollection, TEnum flaggedItem) where TEnum : struct
        {
            int result= AddFlag(Convert.ToInt32(flaggedCollection) , Convert.ToInt32(flaggedItem));
            return (TEnum)Enum.Parse(typeof(TEnum), (result).ToString());
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static TEnum RemoveFlag<TEnum>(TEnum flaggedCollection, TEnum flaggedItem) where TEnum : struct
        {
            int result = RemoveFlag(Convert.ToInt32(flaggedCollection),Convert.ToInt32(flaggedItem));
            return (TEnum)Enum.Parse(typeof(TEnum), (result).ToString());
        }
        #endregion
    }
}
