//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;
//using System.Web;

//namespace HiLand.Utility.IO
//{
//    public static class PathHelper
//    {
//        /// <summary>
//        /// 合并为本地路径
//        /// </summary>
//        /// <param name="paths"></param>
//        /// <returns></returns>
//        public static string CombineForNative(params string[] paths)
//        {
//            string result = string.Empty;
//            foreach (string currentItem in paths)
//            {
//                string localCurrentItem = currentItem.Replace("/", "\\");
//                if (result == string.Empty)
//                {
//                    //如果第一个参数是不带“\”结尾的盘符，那么给其加上“\”
//                    if (localCurrentItem.Trim().EndsWith(":") == true)
//                    {
//                        localCurrentItem += "\\";
//                    }
//                    result = localCurrentItem;
//                }
//                else
//                {
//                    if (localCurrentItem.StartsWith("\\"))
//                    {
//                        localCurrentItem = localCurrentItem.Substring(1);
//                    }
//                    result = Path.Combine(result, localCurrentItem);
//                }
//            }

//            return result;
//        }

//        /// <summary>
//        /// 合并为虚拟路径
//        /// </summary>
//        /// <param name="paths"></param>
//        /// <returns></returns>
//        public static string CombineForVirtual(params string[] paths)
//        {
//            string result = string.Empty;
//            foreach (string currentItem in paths)
//            {
//                if (string.IsNullOrEmpty(currentItem))
//                {
//                    continue;
//                }

//                string localCurrentItem = currentItem.Replace("\\", "/");


//                if (result == string.Empty)
//                {
//                    result = localCurrentItem;
//                }
//                else
//                {
//                    if (localCurrentItem.StartsWith("/"))
//                    {
//                        localCurrentItem = localCurrentItem.Substring(1);
//                    }

//                    if (result.EndsWith("/") == false)
//                    {
//                        result += "/";
//                    }

//                    if (result.Contains("://") == true)
//                    {
//                        result = string.Format("{0}{1}",result,localCurrentItem);
//                    }
//                    else
//                    {
//                        result = VirtualPathUtility.Combine(result, localCurrentItem);
//                    }
//                }
//            }

//            return result;
//        }
//    }
//}
