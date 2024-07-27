//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web;

//namespace HiLand.Utility.Core
//{
//    /// <summary>
//    /// 系统环境操作辅助类
//    /// </summary>
//    public static class EnvironmentHelper
//    {
//        /// <summary>
//        /// 判定当前运行的系统是Web模式还是Windows模式
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsWebApplicatonMode
//        {
//            get
//            {
//                bool flag = false;
//                try
//                {
//                    if (AppDomain.CurrentDomain.ShadowCopyFiles == true && HttpContext.Current != null)
//                    {
//                        flag = true;
//                    }
//                }
//                catch { }

//                return flag;
//            }
//        }

//        /// <summary>
//        /// 获取当前运行的系统是Web模式还是Windows模式
//        /// </summary>
//        /// <returns></returns>
//        public static ApplicationModes GetApplictionMode()
//        {
//            if (IsWebApplicatonMode == true)
//            {
//                return ApplicationModes.WebApp;
//            }
//            else
//            {
//                return ApplicationModes.NativeApp;
//            }
//        }
//    }
//}
