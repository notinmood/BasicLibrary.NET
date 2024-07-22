//using System.Collections.Generic;
//using HiLand.Utility.Paging;
//using Webdiyer.WebControls.Mvc;

//namespace HiLand.Utility4.MVC.Paging
//{
//    public static class PagedListEx
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="pagedEntityCollection"></param>
//        /// <returns></returns>
//        /// <remarks>pagedEntityCollection内部的PageSize，PageIndex，TotalCount属性必须有值</remarks>
//        public static PagedList<T> ToPagedList<T>(this PagedEntityCollection<T> pagedEntityCollection) where T : new()
//        {
//            var pageOfItems = pagedEntityCollection.Records;
//            int totalItemCount = pagedEntityCollection.TotalCount;
//            int pageSize = pagedEntityCollection.PageSize;

//            int pageIndex = pagedEntityCollection.PageIndex;
//            if (pageIndex < 1)
//            {
//                pageIndex = 1;
//            }
//            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="entityCollection"></param>
//        /// <param name="pageIndex"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="totalItemCount"></param>
//        /// <returns></returns>
//        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> entityCollection, int pageIndex, int pageSize, int totalItemCount) where T : new()
//        {
//            return new PagedList<T>(entityCollection, pageIndex, pageSize, totalItemCount);
//        }
//    }
//}
