using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiLand.Utility.Algorithm.Sort
{
    /// <summary>
    /// 计数排序
    /// </summary>
    public class CountingSorter
    {
        //计数排序
        /// <summary>
        /// counting sort
        /// </summary>
        /// <param name="arrayA">input array</param>
        /// <param name="arrange">the value arrange in input array</param>
        /// <returns></returns>
        public int[] CountingSort(int[] arrayA, int arrange)
        {
            //array to store the sorted result,  
            //size is the same with input array. 
            int[] arrayResult = new int[arrayA.Length];
            //array to store the direct value in sorting process   
            //include index 0;    
            //size is arrange+1;    
            int[] arrayTemp = new int[arrange + 1];
            //clear up the temp array    
            for (int i = 0; i <= arrange; i++)
            {
                arrayTemp[i] = 0;
            }
            //now temp array stores the count of value equal  
            for (int j = 0; j < arrayA.Length; j++)
            {
                arrayTemp[arrayA[j]] += 1;
            }
            //now temp array stores the count of value lower and equal  
            for (int k = 1; k <= arrange; k++)
            {
                arrayTemp[k] += arrayTemp[k - 1];
            }
            //output the value to result    
            for (int m = arrayA.Length - 1; m >= 0; m--)
            {
                arrayResult[arrayTemp[arrayA[m]] - 1] = arrayA[m];
                arrayTemp[arrayA[m]] -= 1;
            }
            return arrayResult;
        }
    }
}
