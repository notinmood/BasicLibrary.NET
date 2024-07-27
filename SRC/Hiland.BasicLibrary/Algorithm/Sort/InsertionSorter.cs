namespace Hiland.BasicLibrary.Algorithm.Sort
{
    /// <summary>
    /// 插入排序
    /// </summary>
    public class InsertionSorter
    {
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="arr">待排序的数组</param>
        public void Sort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int t = arr[i];
                int j = i;
                while ((j > 0) && (arr[j - 1] > t))
                {
                    arr[j] = arr[j - 1];//交换顺序    
                    --j;
                }
                arr[j] = t;
            }
        }

    }
}
