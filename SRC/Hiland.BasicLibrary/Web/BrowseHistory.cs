//using System.Collections.Generic;
//using System.Web;
//using Hiland.BasicLibrary.Cache;

//namespace Hiland.BasicLibrary.Web
//{
//    public class BrowseHistory
//    {
//        public void Push(string url)
//        {
//            Stack<string> urlStack = GetStack();
//            if (urlStack != null)
//            {
//                urlStack.Push(url);
//            }
//        }

//        public string Pop()
//        {
//            Stack<string> urlStack = GetStack();
//            if (urlStack != null && urlStack.Count > 0)
//            {
//                return urlStack.Pop();
//            }
//            else
//            {
//                return string.Empty;
//            }
//        }

//        public string Peek()
//        {
//            Stack<string> urlStack = GetStack();
//            if (urlStack != null && urlStack.Count > 0)
//            {
//                return urlStack.Peek();
//            }
//            else
//            {
//                return string.Empty;
//            }
//        }


//        private Stack<string> GetStack()
//        {
//            string userName = string.Empty;
//            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated == true)
//            {
//                userName = HttpContext.Current.User.Identity.Name;
//                string cacheKey = CoreCacheKeys.GetUserBrowseHistoryKey(userName);
//                return CacheHelper.Access<Stack<string>>(cacheKey, CacheHelper.AFewTime, delegate() { return new Stack<string>(); });
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
