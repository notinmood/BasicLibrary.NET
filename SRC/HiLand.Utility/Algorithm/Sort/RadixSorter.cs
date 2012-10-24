using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiLand.Utility.Algorithm.Sort
{
    /// <summary>
    /// 基数排序
    /// </summary>
   public class RadixSorter
    {
        /// <summary>
        /// 基数排序
        /// </summary>
        /// <param name="ArrayToSort"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public int[] RadixSort(int[] ArrayToSort, int digit)
        {
            //low to high digit
            for (int k = 1; k <= digit; k++)
            {
                //temp array to store the sort result inside digit
                int[] tmpArray = new int[ArrayToSort.Length];
                //temp array for countingsort 
                int[] tmpCountingSortArray = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                //CountingSort        
                for (int i = 0; i < ArrayToSort.Length; i++)
                {
                    //split the specified digit from the element 
                    int tmpSplitDigit = ArrayToSort[i] / (int)Math.Pow(10, k - 1) - (ArrayToSort[i] / (int)Math.Pow(10, k)) * 10;
                    tmpCountingSortArray[tmpSplitDigit] += 1;
                }
                for (int m = 1; m < 10; m++)
                {
                    tmpCountingSortArray[m] += tmpCountingSortArray[m - 1];
                }
                //output the value to result      
                for (int n = ArrayToSort.Length - 1; n >= 0; n--)
                {
                    int tmpSplitDigit = ArrayToSort[n] / (int)Math.Pow(10, k - 1) - (ArrayToSort[n] / (int)Math.Pow(10, k)) * 10;
                    tmpArray[tmpCountingSortArray[tmpSplitDigit] - 1] = ArrayToSort[n];
                    tmpCountingSortArray[tmpSplitDigit] -= 1;
                }
                //copy the digit-inside sort result to source array       
                for (int p = 0; p < ArrayToSort.Length; p++)
                {
                    ArrayToSort[p] = tmpArray[p];
                }
            }
            return ArrayToSort;
        }
    }
}
