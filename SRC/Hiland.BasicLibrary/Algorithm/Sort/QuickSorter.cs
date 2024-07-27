namespace Hiland.BasicLibrary.Algorithm.Sort
{
    /// <summary>
    /// 快速排序
    /// </summary>
    public class QuickSorter
    {
        private void swap(ref int l, ref int r)
        {
            int temp;
            temp = l;
            l = r;
            r = temp;
        }
       /// <summary>
        /// 快速排序
       /// </summary>
       /// <param name="list">待排序的数组</param>
       /// <param name="low"></param>
       /// <param name="high"></param>
        public void Sort(int[] list, int low, int high)
        {
            int pivot;//存储分支点    
            int l, r;
            int mid;
            if (high <= low)
                return;
            else if (high == low + 1)
            {
                if (list[low] > list[high])
                    swap(ref list[low], ref list[high]);
                return;
            }
            mid = (low + high) >> 1;
            pivot = list[mid];
            swap(ref list[low], ref list[mid]);
            l = low + 1;
            r = high;
            do
            {
                while (l <= r && list[l] < pivot)
                    l++;
                while (list[r] >= pivot)
                    r--;
                if (l < r)
                    swap(ref list[l], ref list[r]);
            } while (l < r);
            list[low] = list[r];
            list[r] = pivot;
            if (low + 1 < r)
                Sort(list, low, r - 1);
            if (r + 1 < high)
                Sort(list, r + 1, high);
        }

    }
}
