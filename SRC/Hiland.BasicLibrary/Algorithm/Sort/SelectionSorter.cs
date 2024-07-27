namespace Hiland.BasicLibrary.Algorithm.Sort
{
    /// <summary>
    /// 选择排序
    /// </summary>
    public class SelectionSorter
    {
        private int min;
       /// <summary>
        /// 选择排序
       /// </summary>
       /// <param name="arr">需要排序的数组</param>
        public void Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[j] < arr[min])
                        min = j;
                }
                int t = arr[min];
                arr[min] = arr[i];
                arr[i] = t;
            }
        }

    }
}
