
namespace Hiland.BasicLibrary.Algorithm
{
    /// <summary>
    /// 判断一个整数是奇数还是偶数。
    /// </summary>
    public class OddEven
    {
        /// <summary>
        /// 是否为偶数
        /// </summary>
        /// <param name="i">参数</param>
        /// <returns></returns>
        public static bool IsEven(int i)
        {
            return (i & 1) == 0;
        }
        /// <summary>
        /// 是否为奇数
        /// </summary>
        /// <param name="i">参数</param>
        /// <returns></returns>
        public static bool IsOdd(int i)
        {
            return !IsEven(i);
        }
    }
}
