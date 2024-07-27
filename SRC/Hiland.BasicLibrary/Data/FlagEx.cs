using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary4.Data
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class FlagEx
    {
        #region 具有flag功能的数字组合的判断
        /// <summary>
        /// 判断某一个flag类型的枚举值，是否在一个给定的枚举集合内
        /// </summary>
        /// <param name="flaggedItem"></param>
        /// <param name="flaggedCollection"></param>
        /// <returns></returns>
        public static bool ContainsFlag(this int flaggedCollection, int flaggedItem)
        {
            return FlagHelper.ContainsFlag(flaggedCollection, flaggedItem);
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static int AddFlag(this int flaggedCollection, int flaggedItem)
        {
            return FlagHelper.AddFlag(flaggedCollection, flaggedItem);
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static int RemoveFlag(this int flaggedCollection, int flaggedItem)
        {
            return FlagHelper.RemoveFlag(flaggedCollection, flaggedItem);
        }
        #endregion

        #region 具有flag功能的枚举组合的判断
        /// <summary>
        /// 判断某一个flag类型的枚举值，是否在一个给定的枚举集合内
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flaggedCollection"></param>
        /// <param name="valueToValidate"></param>
        /// <returns></returns>
        public static bool ContainsFlag<TEnum>(this TEnum flaggedCollection, TEnum valueToValidate) where TEnum : struct
        {
            return FlagHelper.ContainsFlag(flaggedCollection, valueToValidate);
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static TEnum AddFlag<TEnum>(this TEnum flaggedCollection, TEnum flaggedItem) where TEnum : struct
        {
            return FlagHelper.AddFlag(flaggedCollection, flaggedItem);
        }

        /// <summary>
        /// 向flag类型的枚举集合中添加某个flag枚举项目
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flaggedCollection"></param>
        /// <param name="flaggedItem"></param>
        /// <returns></returns>
        public static TEnum RemoveFlag<TEnum>(this TEnum flaggedCollection, TEnum flaggedItem) where TEnum : struct
        {
            return FlagHelper.RemoveFlag(flaggedCollection, flaggedItem);
        }
        #endregion
    }
}
