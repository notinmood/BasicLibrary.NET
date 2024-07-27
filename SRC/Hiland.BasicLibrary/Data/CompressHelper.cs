using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Hiland.BasicLibrary.Data
{
    /// <summary>
    /// 压缩解压帮助器
    /// </summary>
    public class CompressHelper
    {
        /// <summary>
        /// 对字符串进行压缩(压缩完成后进行base64转码)
        /// </summary>
        /// <param name="data">待压缩的字符串</param>
        /// <returns>压缩后的字符串(已经过base64转码)</returns>
        public static string Compress(string data)
        {
            string compressString = "";
            byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(data);
            byte[] compressAfterByte = Compress(compressBeforeByte);
            compressString = Convert.ToBase64String(compressAfterByte);
            return compressString;
        }

        /// <summary>
        /// 对字符串进行解压缩
        /// </summary>
        /// <param name="data">待解压缩的字符串</param>
        /// <returns>解压缩后的字符串</returns>
        public static string Decompress(string data)
        {
            string compressString = "";
            byte[] compressBeforeByte = Convert.FromBase64String(data);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);
            return compressString;
        }
        /// <summary>
        /// 对文件进行压缩
        /// </summary>
        /// <param name="sourceFile">待压缩的文件名</param>
        /// <param name="destinationFile">压缩后的文件名</param>
        public static void Compress(string sourceFile, string destinationFile)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// 对文件进行解压缩
        /// </summary>
        /// <param name="sourceFile">待解压缩的文件名</param>
        /// <param name="destinationFile">解压缩后的文件名</param>
        /// <returns></returns>
        public static void Decompress(string sourceFile, string destinationFile)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 对byte数组进行压缩
        /// </summary>
        /// <param name="data">待压缩的byte数组</param>
        /// <returns>压缩后的byte数组</returns>
        public static byte[] Compress(byte[] data)
        {
            try
            {
                byte[] buffer = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                    {
                        zip.Write(data, 0, data.Length);
                        zip.Close();
                    }
                    buffer = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(buffer, 0, buffer.Length);
                    ms.Close();
                }
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 对byte数组进行解压
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                byte[] buffer = null;
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true))
                    {
                        using (MemoryStream msreader = new MemoryStream())
                        {
                            buffer = new byte[0x1000];
                            while (true)
                            {
                                int reader = zip.Read(buffer, 0, buffer.Length);
                                if (reader <= 0)
                                {
                                    break;
                                }
                                msreader.Write(buffer, 0, reader);
                            }
                            zip.Close();
                            ms.Close();
                            msreader.Position = 0;
                            buffer = msreader.ToArray();
                            msreader.Close();
                        }
                    }
                }
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
