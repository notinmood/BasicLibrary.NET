using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Paging
{
    public class PagedEntityCollection<T>// where T : new()
    {
        private IList<T> records = new List<T>();
        /// <summary>
        /// 当前页面上的记录集
        /// </summary>
        public IList<T> Records
        {
            get { return records; }
            set { records = value; }
        }

        private int totalCount = 0;
        /// <summary>
        /// 总的数量
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }

        private int pageIndex = 1;
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex
        {
            get { return this.pageIndex; }
            set { this.pageIndex = value; }
        }

        private int pageSize = 10;
        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }
    }
}
