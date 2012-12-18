using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 表示区间的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RegionData<T> where T : IComparable
    {
        public RegionData()
        { }

        public RegionData(T min, T max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// 区间内最小值
        /// </summary>
        public T Min { get; set; }
        /// <summary>
        /// 区间内最大值
        /// </summary>
        public T Max { get; set; }
    }

    /// <summary>
    /// 区间使用辅助器
    /// </summary>
    public static class RegionHelper
    {
        /// <summary>
        /// 判断两个区间是否有重叠部分
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regionDataA"></param>
        /// <param name="regionDataB"></param>
        /// <returns></returns>
        public static bool HasOverlap<T>(RegionData<T> regionDataA, RegionData<T> regionDataB)
            where T : IComparable
        {
            //TIP:xieran20121218 重叠规则可以参考RegionHelper.vsdx

            if (regionDataB.Min.CompareTo(regionDataA.Min) >= 0 && regionDataB.Min.CompareTo(regionDataA.Max) <= 0)
            {
                return true;
            }

            if (regionDataB.Max.CompareTo(regionDataA.Min) >= 0 && regionDataB.Max.CompareTo(regionDataA.Max) <= 0)
            {
                return true;
            }

            if (regionDataB.Min.CompareTo(regionDataA.Min) <= 0 && regionDataB.Max.CompareTo(regionDataA.Max) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
