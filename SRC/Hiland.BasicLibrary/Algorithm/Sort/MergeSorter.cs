namespace Hiland.BasicLibrary.Algorithm.Sort
{
    /// <summary>
    /// 归并排序
    /// </summary>
    public class MergeSorter
    {
        /// <summary>
        /// 归并排序之归：归并排序入口
        /// </summary>
        /// <param name="data">无序的数组</param>
        /// <returns>有序数组</returns>
        /// <author>Lihua(www.zivsoft.com)</author>
        public int[] Sort(int[] data)
        {
            //取数组中间下标
            int middle = data.Length / 2;
            //初始化临时数组let,right，并定义result作为最终有序数组
            int[] left = new int[middle], right = new int[middle], result = new int[data.Length];
            if (data.Length % 2 != 0)//若数组元素奇数个，重新初始化右临时数组
            {
                right = new int[middle + 1];
            }
            if (data.Length <= 1)//只剩下1 or 0个元数，返回，不排序
            {
                return data;
            }
            int i = 0, j = 0;
            foreach (int x in data)//开始排序
            {
                if (i < middle)//填充左数组
                {
                    left[i] = x;
                    i++;
                }
                else//填充右数组
                {
                    right[j] = x;
                    j++;
                }
            }
            left = Sort(left);//递归左数组
            right = Sort(right);//递归右数组
            result = Merge(left, right);//开始排序
            //this.Write(result);//输出排序,测试用(lihua debug)
            return result;
        }
        /// <summary>
        /// 归并排序之并:排序在这一步
        /// </summary>
        /// <param name="a">左数组</param>
        /// <param name="b">右数组</param>
        /// <returns>合并左右数组排序后返回</returns>
        private int[] Merge(int[] a, int[] b)
        {
            //定义结果数组，用来存储最终结果
            int[] result = new int[a.Length + b.Length];
            int i = 0, j = 0, k = 0;
            while (i < a.Length && j < b.Length)
            {
                if (a[i] < b[j])//左数组中元素小于右数组中元素
                {
                    result[k++] = a[i++];//将小的那个放到结果数组
                }
                else//左数组中元素大于右数组中元素
                {
                    result[k++] = b[j++];//将小的那个放到结果数组
                }
            }
            while (i < a.Length)//这里其实是还有左元素，但没有右元素
            {
                result[k++] = a[i++];
            }
            while (j < b.Length)//右右元素，无左元素
            {
                result[k++] = b[j++];
            }
            return result;//返回结果数组
        }
        //注：此算法由周利华提供（http://www.cnblogs.com/architect/archive/2009/05/06/1450489.html
    }
}
