//using System;
//using System.IO;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web.Security;

//namespace Hiland.BasicLibrary.Security
//{
//    public class EncryptService
//    {
//        private static string encryptKey = "ILoveChina";
//        private static string encryptVector = "HiLandTech";

//        #region 1. 不可逆加密
//        /// <summary>
//        /// SHA1加密字符串
//        /// </summary>
//        /// <param name="originalString"></param>
//        /// <returns></returns>
//        public static string SHA1(string originalString)
//        {
//            return FormsAuthentication.HashPasswordForStoringInConfigFile(originalString, "SHA1");
//        }

//        /// <summary>
//        /// MD5加密字符串
//        /// </summary>
//        /// <param name="originalString"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// MD5的全称是message-digest algorithm 5（信息-摘要算法），
//        /// 在90年代初由mit laboratory for computer science和
//        /// rsa data security inc的ronald l. rivest开发出来， 经md2、md3和md4发展而来。
//        /// 它的作用是让大容量信息在用数字签名软件签署私人密匙前被"压缩"成一种保密的格式
//        /// （就是把一个任意长度的字节串变换成一定长的大整数）。
//        /// 不管是md2、md4还是md5，它们都需要获得一个随机长度的信息并产生一个128位的信息摘要。
//        /// 虽然这些算法的结构或多或少有些相似，但md2的设计与md4和md5完全不同，
//        /// 那是因为md2是为8位机器做过设计优化的，而md4和md5却是面向32位的电脑。
//        /// 这三个算法的描述和c语言源代码在internet rfcs 1321中有详细的描述
//        /// </remarks>
//        public static string MD5(string originalString)
//        {
//            return FormsAuthentication.HashPasswordForStoringInConfigFile(originalString, "MD5");
//        }
//        #endregion

//        #region 2.可逆（AES算法）
//        /// <summary>
//        /// AES加密
//        /// </summary>
//        /// <param name="originalData">被加密的明文</param>
//        /// <param name="key">密钥</param>
//        /// <param name="vector">向量</param>
//        /// <returns>密文</returns>
//        /// <remarks>
//        /// AES算法描述简介：
//        /// DES数据加密标准算法由于密钥长度较小(56位),
//        /// 已经不适应当今分布式开放网络对数据加密安全性的要求，
//        /// 因此1997年NIST公开征集新的数据加密标准,即AES。
//        /// 经过三轮的筛选,比利时Joan Daeman和Vincent Rijmen
//        /// 提交的Rijndael算法被提议为AES的最终算法。
//        /// 此算法将成为美国新的数据加密标准而被广泛应用在各个领域中。
//        /// 尽管人们对AES还有不同的看法,但总体来说,
//        /// AES作为新一代的数据加密标准汇聚了强安全性、高性能、高效率、易用和灵活等优点。
//        /// AES设计有三个密钥长度:128,192,256位，
//        /// 相对而言，AES的128密钥比DES的56密钥强1021倍。
//        /// </remarks>
//        public static Byte[] AESEncrypt(Byte[] originalData, String key, String vector)
//        {
//            Byte[] byteKey = new Byte[32];
//            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(byteKey.Length)), byteKey, byteKey.Length);
//            Byte[] byteVector = new Byte[16];
//            Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(byteVector.Length)), byteVector, byteVector.Length);

//            Byte[] encryptedData = null; // 加密后的密文

//            Rijndael aes = Rijndael.Create();
//            try
//            {
//                // 开辟一块内存流
//                using (MemoryStream memoryStream = new MemoryStream())
//                {
//                    // 把内存流对象包装成加密流对象
//                    using (CryptoStream encryptor = new CryptoStream(memoryStream, aes.CreateEncryptor(byteKey, byteVector), CryptoStreamMode.Write))
//                    {
//                        // 明文数据写入加密流
//                        encryptor.Write(originalData, 0, originalData.Length);
//                        encryptor.FlushFinalBlock();

//                        encryptedData = memoryStream.ToArray();
//                    }
//                }
//            }
//            catch
//            {
//                encryptedData = null;
//            }

//            return encryptedData;
//        }

//        public static string AESEncrypt(string originalString, String key, String vector)
//        {
//            byte[] originalData = Encoding.UTF8.GetBytes(originalString);
//            byte[] encyptedData = AESEncrypt(originalData,key,vector);
//            return Encoding.UTF8.GetString(encyptedData);
//        }

//        public static string AESEncrypt(string originalString,string key)
//        {
//            return AESEncrypt(originalString, key, encryptVector);
//        }

//        public static string AESEncrypt(string originalString)
//        {
//            return AESEncrypt(originalString,encryptKey,encryptVector);
//        }

//        public static Byte[] AESEncrypt(Byte[] Data)
//        {
//            return AESEncrypt(Data, encryptKey, encryptVector);
//        }

//        public static Byte[] AESEncrypt(Byte[] Data,string key)
//        {
//            return AESEncrypt(Data, key, encryptVector);
//        }



//        /// <summary>
//        /// AES解密
//        /// </summary>
//        /// <param name="encryptedData">被解密的密文</param>
//        /// <param name="key">密钥</param>
//        /// <param name="vector">向量</param>
//        /// <returns>明文</returns>
//        public static Byte[] AESDecrypt(Byte[] encryptedData, String key, String vector)
//        {
//            Byte[] byteKey = new Byte[32];
//            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(byteKey.Length)), byteKey, byteKey.Length);
//            Byte[] byteVector = new Byte[16];
//            Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(byteVector.Length)), byteVector, byteVector.Length);

//            Byte[] originalData = null; // 解密后的明文

//            Rijndael aes = Rijndael.Create();
//            try
//            {
//                // 开辟一块内存流，存储密文
//                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
//                {
//                    // 把内存流对象包装成加密流对象
//                    using (CryptoStream decryptor = new CryptoStream(memoryStream, aes.CreateDecryptor(byteKey, byteVector), CryptoStreamMode.Read))
//                    {
//                        // 明文存储区
//                        using (MemoryStream originalMemory = new MemoryStream())
//                        {
//                            Byte[] Buffer = new Byte[1024];
//                            Int32 readBytes = 0;
//                            while ((readBytes = decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
//                            {
//                                originalMemory.Write(Buffer, 0, readBytes);
//                            }

//                            originalData = originalMemory.ToArray();
//                        }
//                    }
//                }
//            }
//            catch
//            {
//                originalData = null;
//            }

//            return originalData;
//        }

//        public static Byte[] AESDecrypt(Byte[] encryptedData)
//        { 
//            return AESDecrypt(encryptedData, encryptKey, encryptVector);
//        }

//        public static Byte[] AESDecrypt(Byte[] encryptedData,string key)
//        {
//            return AESDecrypt(encryptedData, key, encryptVector);
//        }

//        public static string AESDecrypt(string encryptedString,string key,string vector)
//        {
//            byte[] encrytedData = Encoding.UTF8.GetBytes(encryptedString);
//            byte[] originalData = AESDecrypt(encrytedData, key, vector);
//            return Encoding.UTF8.GetString(originalData);
//        }

//        public static string AESDecrypt(string encryptedString,string key)
//        {
//            return AESDecrypt(encryptedString, key, encryptVector);
//        }

//        public static string AESDecrypt(string encryptedString)
//        {
//            return AESDecrypt(encryptedString,encryptKey,encryptVector);
//        }
//        #endregion

//        #region 3.可逆（DES算法）
//        /// <summary>
//        /// DES加密字符串
//        /// </summary>
//        /// <param name="originalString">待加密的字符串</param>
//        /// <param name="key">加密密钥</param>
//        /// <param name="vector"></param>
//        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
//        public static string DESEncrypt(string originalString, string key,string vector)
//        {
//            try
//            {
//                byte[] byteKey = new byte[8];
//                Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(byteKey.Length)), byteKey, byteKey.Length);
//                Byte[] byteVector = new Byte[8];
//                Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(byteVector.Length)), byteVector, byteVector.Length);
//                byte[] originalData = Encoding.UTF8.GetBytes(originalString);
//                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
//                using (MemoryStream memoryStream = new MemoryStream())
//                {
//                    using (CryptoStream encryptor = new CryptoStream(memoryStream, des.CreateEncryptor(byteKey, byteVector), CryptoStreamMode.Write))
//                    {
//                        encryptor.Write(originalData, 0, originalData.Length);
//                        encryptor.FlushFinalBlock();
//                        return Convert.ToBase64String(memoryStream.ToArray());
//                    }
//                }
//            }
//            catch
//            {
//                return originalString;
//            }
//        }

//        public static string DESEncrypt(string originalString, string key)
//        {
//            return DESEncrypt(originalString, key, encryptVector);
//        }

//        public static string DESEncrypt(string originalString)
//        {
//            return DESEncrypt(originalString,encryptKey, encryptVector);
//        }

//        /// <summary>
//        /// DES解密字符串
//        /// </summary>
//        /// <param name="encryptedString">待解密的字符串</param>
//        /// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
//        /// <param name="vector"></param>
//        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
//        public static string DESDecrypt(string encryptedString, string key,string vector)
//        {
//            try
//            {
//                byte[] byteKey = new byte[8];
//                Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(byteKey.Length)), byteKey, byteKey.Length);
//                Byte[] byteVector = new Byte[8];
//                Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(byteVector.Length)), byteVector, byteVector.Length);
//                byte[] encrytedData = Convert.FromBase64String(encryptedString);
//                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
//                using (MemoryStream mStream = new MemoryStream())
//                {
//                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(byteKey, byteVector), CryptoStreamMode.Write))
//                    {
//                        cStream.Write(encrytedData, 0, encrytedData.Length);
//                        cStream.FlushFinalBlock();
//                        return Encoding.UTF8.GetString(mStream.ToArray());
//                    }
//                }
//            }
//            catch
//            {
//                return encryptedString;
//            }
//        }

//        public static string DESDecrypt(string encryptedString, string key)
//        { 
//            return DESDecrypt( encryptedString,  key,encryptVector);
//        }

//        public static string DESDecrypt(string encryptedString)
//        {
//            return DESDecrypt(encryptedString, encryptKey, encryptVector);
//        }
//        #endregion

//        #region 4.可逆（RSA算法）（//TODO:这个算法有问题）
//        /// <summary>
//        /// RSA Encrypt
//        /// </summary>
//        /// <param name="sourceString" >Source string</param>
//        /// <param name="publicKey" >public key</param>
//        /// <returns>
//        /// RSA 属不对称加密，使用一个公钥一个私钥，公钥可以公开用以加密，
//        /// 私钥严格保密用于解密，RSA 适合于数据量不大的加密，比如加密对称加密的密钥。
//        /// RSA 实际应用中是：接收方产生公钥和私钥，发送方用其公钥加密，再把加密后的内容发送给接收方。
//        /// </returns>
//        public static string RSAEncrypt(string sourceString, string publicKey)
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            string plaintext = sourceString;
//            rsa.FromXmlString(publicKey);

//            byte[] cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);

//            StringBuilder sbString = new StringBuilder();
//            for (int i = 0; i < cipherbytes.Length; i++)
//            {
//                sbString.Append(cipherbytes[i] + ",");
//            }
//            return sbString.ToString();
//        }



//        /// <summary>
//        /// RSA Decrypt
//        /// </summary>
//        /// <param name="sourceString">Source string</param>
//        /// <param name="privateKey">Private Key</param>
//        /// <returns></returns>
//        public static string RSADecrypt(String sourceString, string privateKey)
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            rsa.FromXmlString(privateKey);
//            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("a"), false);
//            string[] sBytes = sourceString.Split(',');

//            for (int j = 0; j < sBytes.Length; j++)
//            {
//                if (sBytes[j] != "")
//                {
//                    byteEn[j] = Byte.Parse(sBytes[j]);
//                }
//            }
//            byte[] plaintbytes = rsa.Decrypt(byteEn, false);
//            return Encoding.UTF8.GetString(plaintbytes);
//        }

//        #endregion
//    }
//}