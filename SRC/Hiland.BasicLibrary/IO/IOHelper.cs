//using System;
//using System.IO;
//using System.Web;

//namespace HiLand.Utility.IO
//{
//    public class IOHelper
//    {
//        /// <summary>
//        /// 获取物理路径（兼容WebApp/NativeApp两种方式）
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <returns></returns>
//        public static string GetNativeFilePath(string filePath)
//        {
//            string result = null;
//            if (filePath.IndexOf(@":\") != -1 || filePath.IndexOf(@"\\") != -1)
//            {
//                result = filePath;
//            }
//            else
//            {
//                HttpContext current = HttpContext.Current;
//                if (current != null)
//                {
//                    result = current.Server.MapPath(filePath);
//                }
//                else
//                {
//                    filePath = filePath.Replace('/', Path.DirectorySeparatorChar).Replace("~", "");
//                    return (AppDomain.CurrentDomain.BaseDirectory.Replace('/', Path.DirectorySeparatorChar).TrimEnd(new char[] { Path.DirectorySeparatorChar }) + Path.DirectorySeparatorChar.ToString() + filePath.TrimStart(new char[] { Path.DirectorySeparatorChar }));
//                }
//            }

//            if (result.EndsWith(Path.DirectorySeparatorChar.ToString()) == false)
//            {
//                result = result + Path.DirectorySeparatorChar;
//            }
//            return result;
//        }

//        /// <summary>
//        /// 将本地全路径（即以盘符开头的路径）转换成（相对）虚拟路径
//        /// </summary>
//        /// <param name="fullNativePath"></param>
//        /// <param name="baseVirtualPath"></param>
//        /// <returns></returns>
//        public static string GetRelativeVirtualPath(string fullNativePath, string baseVirtualPath)
//        {
//            string result = "~/";
//            string baseVirtualPathBroker = baseVirtualPath;
//            if (string.IsNullOrEmpty(baseVirtualPath))
//            {
//                baseVirtualPathBroker = "~/";
//            }

//            string baseNativePath = HttpContext.Current.Request.MapPath(baseVirtualPathBroker);
//            int matchPosition = fullNativePath.IndexOf(baseNativePath);

//            string relativeNativePath = string.Empty;
//            if (matchPosition != -1)
//            {
//                relativeNativePath = fullNativePath.Substring(baseNativePath.Length, fullNativePath.Length - baseNativePath.Length);
//            }

//            if (string.IsNullOrEmpty(relativeNativePath) == false)
//            {
//                if (relativeNativePath.StartsWith("\\") == false)
//                {
//                    relativeNativePath = string.Format("\\{0}", relativeNativePath);
//                }

//                result = relativeNativePath.Replace("\\", "/");

//                if (string.IsNullOrEmpty(baseVirtualPath))
//                {
//                    result = string.Format("~{0}", result);
//                }
//            }

//            return result;
//        }



//        /// <summary>
//        /// 以分块的方式对流进行循环地操作
//        /// </summary>
//        /// <param name="inputSteam">输入的流</param>
//        /// <param name="operateStreamEnventHandler">对流分块后，可以调用的具体的操作方法</param>
//        public static void BlocklyCircularlyOperateStream(Stream inputSteam, OperateStreamEnventHandler operateStreamEnventHandler)
//        {
//            BlocklyCircularlyOperateStream(inputSteam, operateStreamEnventHandler, null);
//        }

//        /// <summary>
//        /// 以分块的方式对流进行循环地操作
//        /// </summary>
//        /// <param name="inputSteam">输入的流</param>
//        /// <param name="operateStreamEnventHandler">对流分块后，可以调用的具体的操作方法</param>
//        /// <param name="callBackObject">操作方法operateStreamEnventHandler内部可以使用的回调对象（即可以让调用本分方法的客户端程序，传递一个对象给operateStreamEnventHandler）</param>
//        public static void BlocklyCircularlyOperateStream(Stream inputSteam, OperateStreamEnventHandler operateStreamEnventHandler, object callBackObject)
//        {
//            long streamLength = inputSteam.Length;
//            int size = 10240;//10K一--10K为1块,每次读取10k
//            byte[] bytesReaded = new byte[size];
//            if (size > streamLength)
//            {
//                size = Convert.ToInt32(streamLength);
//            }

//            long currentPosition = 0;
//            bool isEnd = false;
//            while (isEnd == false)
//            {
//                if ((currentPosition + size) >= streamLength)
//                {
//                    size = Convert.ToInt32(streamLength - currentPosition);
//                    isEnd = true;
//                }
//                bytesReaded = new byte[size];
//                inputSteam.Position = currentPosition;
//                inputSteam.Read(bytesReaded, 0, size);

//                if (operateStreamEnventHandler != null)
//                {
//                    operateStreamEnventHandler(null, new OperateStreamEnventArgs() { BytesReaded = bytesReaded, CallBackObject = callBackObject });
//                }

//                currentPosition += size;
//            }
//        }

//        /// <summary>
//        /// 在给定的基目录下建立以日期分割的子目录，并返回这个子目录
//        /// </summary>
//        /// <param name="nativeBasePath">基地址</param>
//        /// <param name="datePathFormater">作为子目录的日期格式</param>
//        /// <returns></returns>
//        public static string EnsureDatePath(string nativeBasePath, DatePathFormaters datePathFormater)
//        {
//            string result = string.Empty;
//            if (string.IsNullOrEmpty(nativeBasePath) == false)
//            {
//                if (Directory.Exists(nativeBasePath) == false)
//                {
//                    Directory.CreateDirectory(nativeBasePath);
//                }

//                string relativePath = string.Empty;
//                switch (datePathFormater)
//                {
//                    case DatePathFormaters.YMD:
//                        relativePath = DateTime.Today.ToString("yyyyMMdd");
//                        break;
//                    case DatePathFormaters.Y_MD:
//                        relativePath = string.Format(@"{0}\{1}{2}", DateTime.Today.Year.ToString("0000"), DateTime.Today.Month.ToString("00"), DateTime.Today.Day.ToString("00"));
//                        break;
//                    case DatePathFormaters.Y_M_D:
//                    default:
//                        relativePath = string.Format(@"{0}\{1}\{2}", DateTime.Today.Year.ToString("0000"), DateTime.Today.Month.ToString("00"), DateTime.Today.Day.ToString("00"));
//                        break;
//                }

//                string fullPath = Path.Combine(nativeBasePath, relativePath);

//                if (Directory.Exists(fullPath) == false)
//                {
//                    Directory.CreateDirectory(fullPath);
//                }

//                result = fullPath;
//            }

//            return result;
//        }
//    }
//}
