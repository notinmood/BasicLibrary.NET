using System;
using System.IO;
using System.Security.Cryptography;

namespace HiLand.Utility.Security
{
    /// <summary>
    /// 哈希算法辅助器
    /// </summary>
    public static class HashHelper
    {
        /*
         * 扩展阅读：继承于HashAlgorithm类型的其他哈希算法有 SHA1Managed，SHA256Managed，SHA512Managed
         * 即以上类型的实例可以传入GetFileHashValue的第二个重载方法中
         */


        /// <summary>
        /// 获取指定文件的hash值
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        /// <remarks>使用MD5算法进行计算，如果使用其他hash算法可以使用另外一个重载</remarks>
        public static string GetFileHashValue(string fileFullPath)
        {
            using (HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider())
            {
                return GetFileHashValue(hashAlgorithm,fileFullPath);
            }
        }

        /// <summary>
        /// 获取指定文件的hash值
        /// </summary>
        /// <param name="hashAlgorithm">hash算法</param>
        /// <param name="fileFullPath">需要计算文件的全路径</param>
        /// <returns></returns>
        public static string GetFileHashValue(HashAlgorithm hashAlgorithm, string fileFullPath)
        {
            if (string.IsNullOrEmpty(fileFullPath))
            {
                return string.Empty;
            }

            using (Stream file = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
            {
                byte[] hash = hashAlgorithm.ComputeHash(file);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}
