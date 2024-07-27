using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Algorithm
{
    /// <summary>
    /// 
    /// </summary>
    public static class AlgorithmMisc
    {
        /// <summary>
        /// 获取百分比值（去掉百分号后的数字）
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="totalValue"></param>
        /// <returns></returns>
        public static double GetPercent(double currentValue, double totalValue)
        {
            return currentValue / totalValue * 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="totalValue"></param>
        /// <returns></returns>
        public static double GetRealValue(double percent,double totalValue)
        {
            return totalValue * percent / 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="totalValue"></param>
        /// <returns></returns>
        public static double GetRestValue(double percent, double totalValue)
        {
            return totalValue - GetRealValue(percent, totalValue);
        }
    }
}
