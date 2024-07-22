//using System;
//using System.IO;
//using System.Text;
//using System.Threading;
//using HiLand.Utility.Data;
//using HiLand.Utility.IO.Extend;
//using Microsoft.Win32.SafeHandles;

//namespace HiLand.Utility.IO
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public static class FileHelper
//    {
//        #region 文件基本信息
//        /// <summary>
//        /// GetFileSize 获取目标文件的大小
//        /// </summary>       
//        /// <param name="fileFullPath"></param>
//        public static int GetFileSize(string fileFullPath)
//        {
//            int size = 0;
//            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
//            {
//                size = (int)fs.Length;
//                fs.Close();
//            }

//            return size;
//        }

//        /// <summary>
//        /// 获取一个文本文件的编码格式
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 文件的字符集在Windows下有两种，一种是ANSI，一种Unicode。
//        /// 对于Unicode，Windows支持了它的三种编码方式，一种是小尾编码（Unicode)，一种是大尾编码(BigEndianUnicode)，一种是UTF-8编码。
//        /// 我们可以从文件的头部来区分一个文件是属于哪种编码。当头部开始的两个字节为 FF FE时，是Unicode的小尾编码；
//        /// 当头部的两个字节为FE FF时，是Unicode的大尾编码；当头部两个字节为EF BB时，是Unicode的UTF-8编码；当它不为这些时，则是ANSI编码。
//        /// 按照如上所说，我们可以通过读取文件头的两个字节来判断文件的编码格式，代码如下(C#代码）：
//        /// 程序中System.Text.Encoding.Default是指操作系统的当前 ANSI 代码页的编码。
//        /// 
//        /// 更详细的说明(http://www.cnblogs.com/mgen/archive/2011/07/13/2105649.html)
//        /// UTF-8: EF BB BF
//        /// UTF-16 big endian: FE FF
//        /// UTF-16 little endian: FF FE
//        /// UTF-32 big endian: 00 00 FE FF
//        /// UTF-32 little endian: FF FE 00 00
//        /// </remarks>
//        public static Encoding GetFileEncoding(string fileFullName)
//        {
//            FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
//            BinaryReader br = new BinaryReader(fs);
//            Byte[] buffer = br.ReadBytes(2);
//            if (buffer[0] >= 0xEF)
//            {
//                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
//                {
//                    return System.Text.Encoding.UTF8;
//                }
//                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
//                {
//                    return System.Text.Encoding.BigEndianUnicode;
//                }
//                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
//                {
//                    return System.Text.Encoding.Unicode;
//                }
//                else
//                {
//                    return System.Text.Encoding.Default;
//                }
//            }
//            else
//            {
//                return System.Text.Encoding.Default;
//            }
//        }
//        #endregion

//        #region 文件名称的处理
//        /// <summary>
//        /// 通过文件的全路径名称获取其不包含路径的文件名称
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static string GetFileShortName(string fileFullName)
//        {
//            return Path.GetFileName(fileFullName);
//        }

//        /// <summary>
//        /// 获取不带扩展信息的文件名称
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static string GetFileMainName(string fileFullName)
//        {
//            return Path.GetFileNameWithoutExtension(fileFullName);
//        }

//        /// <summary>
//        /// 获取文件的扩展名称（带点号）
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static string GetFileExtensionName(string fileFullName)
//        {
//            return Path.GetExtension(fileFullName);
//        }

//        /// <summary>
//        /// 获取去不带点号的文件扩展名
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static string GeFileExtensionNameWithoutDot(string fileFullName)
//        {
//            string extensionName = GetFileExtensionName(fileFullName);

//            string fileExtensionWithoutDot = string.Empty;
//            if (extensionName.StartsWith("."))
//            {
//                fileExtensionWithoutDot = extensionName.Substring(1, extensionName.Length - 1);
//            }
//            else
//            {
//                fileExtensionWithoutDot = extensionName;
//            }

//            return fileExtensionWithoutDot;
//        }

//        /// <summary>
//        /// 通过给定的基地址和文件名称，获取文件应该保存的安装年月日期格式话后的目录（年月目录作为基地址的子目录）
//        /// </summary>
//        /// <param name="basePath"></param>
//        /// <param name="fileShortName"></param>
//        /// <param name="datePathFormater"></param>
//        /// <returns></returns>
//        public static string GenerateYearMonthSeperatedFileFullName(string basePath, string fileShortName, DatePathFormaters datePathFormater)
//        {
//            string pathWithDateInfo = IOHelper.EnsureDatePath(basePath, datePathFormater);

//            if (string.IsNullOrEmpty(fileShortName) == true)
//            {
//                fileShortName = GuidHelper.NewGuidString() + ".xml";
//            }

//            return Path.Combine(pathWithDateInfo, fileShortName);
//        }
//        #endregion

//        #region 文本信息在文件中的读写
//        /// <summary>
//        /// 读取文本文件的内容
//        /// </summary>       
//        /// <param name="fileFullPath"></param>
//        public static string GetFileContent(string fileFullPath)
//        {
//            if (File.Exists(fileFullPath) == false)
//            {
//                return null;
//            }

//            string content = string.Empty;
//            using (StreamReader reader = new StreamReader(fileFullPath, System.Text.Encoding.Default))
//            {
//                content = reader.ReadToEnd();
//                reader.Close();
//            }
//            return content;
//        }

//        /// <summary>
//        /// 将字符串写成文件
//        /// </summary>       
//        public static void WriteContentToFile(string fileFullPath, string text)
//        {
//            string directoryPath = Path.GetDirectoryName(fileFullPath);
//            if (!Directory.Exists(directoryPath))
//            {
//                Directory.CreateDirectory(directoryPath);
//            }

//            using (FileStream fs = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                using (StreamWriter sw = new StreamWriter(fs))
//                {
//                    sw.Write(text);

//                    sw.Flush();
//                    sw.Close();
//                    fs.Close();
//                }
//            }
//        }


//        /// <summary>
//        /// 读取文本文件最后的内容
//        /// </summary>
//        /// <param name="fileWithFullPath">文件名</param>
//        /// <param name="lineCount">行数</param>
//        /// <param name="encoding">字符编码</param>
//        /// <returns>返回读取的内容</returns>
//        public static string ReadLastLine(string fileWithFullPath, int lineCount = 1, Encoding encoding = null)
//        {
//            if (lineCount <= 0) return string.Empty;
//            if (!File.Exists(fileWithFullPath)) return string.Empty; // 文件不存在
//            if (encoding == null) encoding = Encoding.UTF8;
//            using (FileStream fileStream = new FileStream(fileWithFullPath,
//                FileMode.Open, FileAccess.Read, FileShare.Read))
//            {

//                if (fileStream.Length <= 0) return string.Empty; // 空文件
//                byte[] buffers = new byte[0x1000]; // 缓冲区
//                int readLength; // 读取到的大小
//                int readLineCount = 0; // 读取的行数
//                int readCount = 0; // 读取的次数
//                int scanCount = 0; // 扫描过的字符数
//                long offset;
//                do
//                {
//                    offset = buffers.Length * ++readCount;
//                    int space = 0; // 偏移超出的空间
//                    if (offset >= fileStream.Length) // 超出范围
//                    {
//                        space = (int)(offset - fileStream.Length);
//                        offset = fileStream.Length;
//                    }
//                    fileStream.Seek(-offset, SeekOrigin.End); //“SeekOrigin.End”反方向偏移读取位置

//                    readLength = fileStream.Read(buffers, 0, buffers.Length - space);
//                    #region 所读的缓冲里有多少行
//                    for (int i = readLength - 1; i >= 0; i--)
//                    {
//                        if (buffers[i] == 10)
//                        {
//                            if (scanCount > 0) readLineCount++; // #13#10为回车换行
//                            if (readLineCount >= lineCount) break;
//                        }
//                        scanCount++;
//                    }
//                    #endregion 所读的缓冲里有多少行
//                } while (readLength >= buffers.Length && offset < fileStream.Length &&
//                    readLineCount < lineCount);

//                if (readCount > 1) // 读的次数超过一次，则需重分配缓冲区
//                {
//                    buffers = new byte[scanCount];
//                    fileStream.Seek(-scanCount, SeekOrigin.End);
//                    readLength = fileStream.Read(buffers, 0, buffers.Length);
//                }

//                return encoding.GetString(buffers, readLength - scanCount, scanCount);
//            }
//        }
//        #endregion

//        #region 二进制信息在文件中的读写
//        /// <summary>
//        /// 读取文件，返回字节数组
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static byte[] ReadFileBytes(string fileFullName)
//        {
//            if (!File.Exists(fileFullName))
//            {
//                return null;
//            }

//            byte[] buffer = null;
//            using (FileStream stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
//            {
//                using (BinaryReader reader = new BinaryReader(stream))
//                {
//                    buffer = reader.ReadBytes((int)stream.Length);
//                    reader.Close();
//                    stream.Close();
//                }
//            }
//            return buffer;
//        }

//        /// <summary>
//        /// 将二进制数据写入文件中
//        /// </summary>   
//        public static void WriteBytesToFile(string fileFullPath, byte[] buffer)
//        {
//            WriteBytesToFile(fileFullPath, buffer, 0, 0);
//        }

//        /// <summary>
//        /// 将二进制数据写入文件中
//        /// </summary>    
//        public static void WriteBytesToFile(string fileFullPath, byte[] buffer, int offset, int len)
//        {
//            string directoryPath = Path.GetDirectoryName(fileFullPath);
//            if (!Directory.Exists(directoryPath))
//            {
//                Directory.CreateDirectory(directoryPath);
//            }

//            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write))
//            {
//                using (BinaryWriter bw = new BinaryWriter(fs))
//                {
//                    if (offset == 0 && len == 0)
//                    {
//                        bw.Write(buffer);
//                    }
//                    else
//                    {
//                        bw.Write(buffer, offset, len);
//                    }

//                    bw.Flush();
//                    bw.Close();
//                    fs.Close();
//                }
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="objectToSave"></param>
//        /// <param name="fileFullPath"></param>
//        /// <returns></returns>
//        public static bool SaveBinaryFile(object objectToSave, string fileFullPath)
//        {
//            if (objectToSave != null)
//            {
//                byte[] buffer = Converter.ToBytes(objectToSave);
//                if (buffer != null)
//                {
//                    using (FileStream stream = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.Write))
//                    {
//                        using (BinaryWriter writer = new BinaryWriter(stream))
//                        {
//                            writer.Write(buffer);
//                            return true;
//                        }
//                    }
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// 从二进制文件中加载对象
//        /// </summary>
//        /// <param name="fileFullPath"></param>
//        /// <returns></returns>
//        public static object LoadBinaryFile(string fileFullPath)
//        {
//            if (File.Exists(fileFullPath) == false)
//            {
//                return null;
//            }

//            using (FileStream stream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
//            {
//                using (BinaryReader reader = new BinaryReader(stream))
//                {
//                    byte[] buffer = new byte[stream.Length];
//                    reader.Read(buffer, 0, (int)stream.Length);
//                    return Converter.ToObject(buffer);
//                }
//            }
//        }
//        #endregion

//        #region 文件与流的转换
//        /// <summary>
//        /// 从文件中获取流
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static Stream GetStreamFromFile(string fileFullName)
//        {
//            FileStream fileStream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
//            return fileStream;
//        }

//        /// <summary>
//        /// 将流写入到文件中
//        /// </summary>
//        /// <param name="inputStream"></param>
//        /// <param name="fileFullName"></param>
//        public static void WriteStreamToFile(Stream inputStream, string fileFullName)
//        {
//            using (FileStream fileStream = new FileStream(fileFullName, FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                IOHelper.BlocklyCircularlyOperateStream(inputStream, BlocklyWriteToFileStream, fileStream);
//                fileStream.Flush();
//                fileStream.Close();
//            }
//        }

//        private static bool BlocklyWriteToFileStream(object sender, OperateStreamEnventArgs args)
//        {
//            FileStream fileStream = (FileStream)args.CallBackObject;
//            fileStream.Write(args.BytesReaded, 0, args.BytesReaded.Length);
//            fileStream.Flush();
//            return true;
//        }
//        #endregion

//        #region 判断文件是否被其他线程占用
//        /// <summary>
//        /// 确认某文件当前是否在被其他程序打开（或者使用）
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static bool ConfirmIsOccupied(string fileFullName)
//        {
//            bool isOccupied = false;
//            using (SafeFileHandle fileHandle = FileWrapper.CreateFile(fileFullName,
//                                                                  FileAccesses.GenericRead,
//                                                                  FileShares.Read,
//                                                                  IntPtr.Zero,
//                                                                  CreationDispositions.OpenExisting,
//                                                                  FileOperateAttributes.Readonly,
//                                                                  IntPtr.Zero))
//            {
//                if (fileHandle.IsInvalid)
//                {
//                    isOccupied = true;
//                }
//                else
//                {
//                    isOccupied = false;
//                }
//            }

//            return isOccupied;
//        }

//        /// <summary>
//        /// 确认某文件当前是否在被其他程序打开（或者使用）
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        /// <remarks>判断文件是否被占用的另外一种方式</remarks>
//        public static bool ConfirmIsOccupied2(string fileFullName)
//        {
//            bool isOccupied = false;
//            IntPtr vHandle = FileWrapper._lopen(fileFullName, FileWrapper.OF_READWRITE | FileWrapper.OF_SHARE_DENY_NONE);
//            if (vHandle == FileWrapper.HFILE_ERROR)
//            {
//                isOccupied = true;
//            }
//            FileWrapper.CloseHandle(vHandle);

//            return isOccupied;
//        }

//        /// <summary>
//        /// 等待文件直到其他程序都已经释放了对它的使用
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <remarks>由于使用了线程等待，此方法最好在在多线程的单独线程中调用，以防止线程阻塞</remarks>
//        public static void WaitFileUntilFree(string fileFullName)
//        {
//            bool isBusy = true;
//            while (isBusy)
//            {
//                isBusy = ConfirmIsOccupied(fileFullName);
//                if (isBusy == true)
//                {
//                    Thread.Sleep(200);
//                }
//            }
//        }
//        #endregion
//    }
//}