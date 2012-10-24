using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Algorithm
{
    //TODO:Sort文件夹下的各种排序方法均应整理到这个类中
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Sorts<T> where T : IComparable
    {
        /// <summary>
        /// 希尔排序 
        /// </summary>
        /// <param name="arr">待排序的数组</param>
        public static void ShellSort(params T[] arr)
        {
            int inc;
            for (inc = 1; inc <= arr.Length / 9; inc = 3 * inc + 1)
                ;
            for (; inc > 0; inc /= 3)
            {
                for (int i = inc + 1; i <= arr.Length; i += inc)
                {
                    T t = arr[i - 1];
                    int j = i;
                    while ((j > inc) && (arr[j - inc - 1].CompareTo(t) > 0))
                    {
                        arr[j - 1] = arr[j - inc - 1];
                        j -= inc;
                    }
                    arr[j - 1] = t;
                }
            }
        }
    }
}
