namespace HiLand.Utility.Algorithm.Sort
{
    /// <summary>
    /// 
    /// </summary>
    public class HeapSorter
    {
        /// <summary>
        /// 小根堆排序
        /// </summary>
        /// <param name="dblArray"></param>
        /// <returns></returns>
        public void HeapSort(ref double[] dblArray)
        {
            for (int i = dblArray.Length - 1; i >= 0; i--)
            {
                if (2 * i + 1 < dblArray.Length)
                {
                    int MinChildrenIndex = 2 * i + 1;
                    //比较左子树和右子树，记录最小值的Index
                    if (2 * i + 2 < dblArray.Length)
                    {
                        if (dblArray[2 * i + 1] > dblArray[2 * i + 2])
                            MinChildrenIndex = 2 * i + 2;
                    }
                    if (dblArray[i] > dblArray[MinChildrenIndex])
                    {


                        ExchageValue(ref dblArray[i], ref dblArray[MinChildrenIndex]);
                        NodeSort(ref dblArray, MinChildrenIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 节点排序
        /// </summary>
        /// <param name="dblArray"></param>
        /// <param name="StartIndex"></param>
        private void NodeSort(ref double[] dblArray, int StartIndex)
        {
            while (2 * StartIndex + 1 < dblArray.Length)
            {
                int MinChildrenIndex = 2 * StartIndex + 1;
                if (2 * StartIndex + 2 < dblArray.Length)
                {
                    if (dblArray[2 * StartIndex + 1] > dblArray[2 * StartIndex + 2])
                    {
                        MinChildrenIndex = 2 * StartIndex + 2;
                    }
                }
                if (dblArray[StartIndex] > dblArray[MinChildrenIndex])
                {
                    ExchageValue(ref dblArray[StartIndex], ref dblArray[MinChildrenIndex]);
                    StartIndex = MinChildrenIndex;
                }
            }
        }

        /// <summary>
        /// 交换值
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        private void ExchageValue(ref double A, ref double B)
        {
            double Temp = A;
            A = B;
            B = Temp;
        }
    }
}
