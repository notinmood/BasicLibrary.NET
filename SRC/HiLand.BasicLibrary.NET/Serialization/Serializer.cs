//using System;
//using System.Collections.Specialized;
//using System.Globalization;
//using System.Security;
//using System.Security.Permissions;
//using System.Text;
//using System.Xml;
//using HiLand.Utility.Data;
//using HiLand.Utility.IO;

//namespace HiLand.Utility.Serialization
//{
//    /// <summary>
//    /// Summary description for Serializer.
//    /// </summary>
//    public class Serializer
//    {
//        //Do not allow this class to be instantiated
//        private Serializer()
//        {

//        }

//        /// <summary>
//        /// Static Constructor is used to set the CanBinarySerialize value only once for the given security policy
//        /// </summary>
//        static Serializer()
//        {
//            SecurityPermission sp = new SecurityPermission(SecurityPermissionFlag.SerializationFormatter);
//            try
//            {
//                sp.Demand();
//                CanBinarySerialize = true;
//            }
//            catch (SecurityException)
//            {
//                CanBinarySerialize = false;
//            }
//        }

//        /// <summary>
//        /// Readonly value indicating if Binary Serialization (using BinaryFormatter) is allowed
//        /// </summary>
//        public static readonly bool CanBinarySerialize;

//        #region Binary 二进制序列化
//        /// <summary>
//        /// Converts a .NET object to a byte array. Before the conversion happens, a check with 
//        /// Serializer.CanBinarySerialize will be made
//        /// </summary>
//        /// <param name="objectToConvert">Object to convert</param>
//        /// <returns>A byte arry representing the object paramter. Null will be return if CanBinarySerialize is false</returns>
//        public static byte[] ConvertToBytes(object objectToConvert)
//        {
//            byte[] byteArray = null;

//            if (CanBinarySerialize)
//            {
//                byteArray = Converter.ToBytes(objectToConvert);
//            }
//            return byteArray;
//        }

//        /// <summary>
//        /// Saves an object to disk as a binary file. 
//        /// </summary>
//        /// <param name="objectToSave">Object to Save</param>
//        /// <param name="fileFullName">Location of the file</param>
//        /// <returns>true if the save was succesful.</returns>
//        public static bool SaveFileAsBinary(object objectToSave, string fileFullName)
//        {
//            bool result = false;

//            if (objectToSave != null && CanBinarySerialize)
//            {
//                result = FileHelper.SaveBinaryFile(objectToSave, fileFullName);
//            }
//            return result;
//        }

//        /// <summary>
//        /// Converts a byte array to a .NET object. You will need to cast this object back to its expected type. 
//        /// If the array is null or empty, it will return null.
//        /// </summary>
//        /// <param name="byteArray">An array of bytes represeting a .NET object</param>
//        /// <returns>The byte array converted to an object or null if the value of byteArray is null or empty</returns>
//        public static object ConvertToObject(byte[] byteArray)
//        {
//            object convertedObject = null;
//            if (CanBinarySerialize && byteArray != null && byteArray.Length > 0)
//            {
//                convertedObject = Converter.ToObject(byteArray);
//            }
//            return convertedObject;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="byteArray"></param>
//        /// <returns></returns>
//        public static T ConvertToObject<T>(byte[] byteArray)
//        {
//            object convertedObject = ConvertToObject(byteArray);
//            return Converter.ChangeType<T>(convertedObject);
//        }

//        /// <summary>
//        /// 转换二进制文件为对象
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static object ConvertBinaryFileToObject(string fileFullName)
//        {
//            object convertedObject = null;

//            if (fileFullName != null && fileFullName.Length > 0)
//            {
//                convertedObject = FileHelper.LoadBinaryFile(fileFullName);
//            }
//            return convertedObject;
//        }

//        /// <summary>
//        /// 转换二进制文件为对象
//        /// </summary>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static T ConvertBinaryFileToObject<T>(string fileFullName)
//        {
//            object convertedObject = ConvertBinaryFileToObject(fileFullName);
//            return Converter.ChangeType<T>(convertedObject);
//        }
//        #endregion

//        #region Xml 序列化
//        /// <summary>
//        /// Converts a .NET object to a string of XML. The object must be marked as Serializable or an exception
//        /// will be thrown.
//        /// </summary>
//        /// <param name="objectToConvert">Object to convert</param>
//        /// <returns>A xml string represting the object parameter. The return value will be null of the object is null</returns>
//        public static string ConvertToXmlString(object objectToConvert)
//        {
//            string result = string.Empty;

//            if (objectToConvert != null)
//            {
//                result = XmlHelper.Serialize(objectToConvert);
//            }

//            return result;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="objectToConvert"></param>
//        /// <param name="fileFullName"></param>
//        public static void SaveFileAsXML(object objectToConvert, string fileFullName)
//        {
//            string value = ConvertToXmlString(objectToConvert);
//            FileHelper.WriteContentToFile(fileFullName, value);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static T ConvertXmlFileToObject<T>(string fileFullName)
//        {
//            T convertedObject = default(T);

//            if (fileFullName != null && fileFullName.Length > 0)
//            {
//                string value = FileHelper.GetFileContent(fileFullName);
//                convertedObject = XmlHelper.DeSerialize<T>(value);
//            }

//            return convertedObject;
//        }


//        /// <summary>
//        /// Converts a string of xml to the supplied object type. 
//        /// </summary>
//        /// <param name="xmlValue">Xml representing a .NET object</param>
//        /// <param name="objectType">The type of object which the xml represents</param>
//        /// <returns>A instance of object or null if the value of xml is null or empty</returns>
//        public static T ConvertXmlToObject<T>(string xmlValue)
//        {
//            return XmlHelper.DeSerialize<T>(xmlValue);
//        }

//        /// <summary>
//        /// Converts a string of xml to the supplied object type. 
//        /// </summary>
//        /// <param name="xml">Xml representing a .NET object</param>
//        /// <param name="objectType">The type of object which the xml represents</param>
//        /// <returns>A instance of object or null if the value of xml is null or empty</returns>
//        public static T ConvertXmlToObject<T>(XmlNode xmlNode)
//        {
//            if (xmlNode != null)
//            {
//                return XmlHelper.DeSerialize<T>(xmlNode.OuterXml);
//            }
//            else
//            {
//                return default(T);
//            }
//        }
//        #endregion

//        #region JSON 序列化
//        public static string ConvertToJSONString(object objectToConvert)
//        {
//            string result = string.Empty;

//            if (objectToConvert != null)
//            {
//                result = JsonHelper.Serialize(objectToConvert);
//            }

//            return result;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="objectToConvert"></param>
//        /// <param name="fileFullName"></param>
//        public static void SaveFileAsJSON(object objectToConvert, string fileFullName)
//        {
//            string value = ConvertToJSONString(objectToConvert);
//            FileHelper.WriteContentToFile(fileFullName, value);
//        }

//        /// <summary>
//        /// Converts a string of json to the supplied object type. 
//        /// </summary>
//        /// <param name="xmlValue">Xml representing a .NET object</param>
//        /// <param name="objectType">The type of object which the xml represents</param>
//        /// <returns>A instance of object or null if the value of xml is null or empty</returns>
//        public static T ConvertJSONToObject<T>(string jsonValue)
//        {
//            return JsonHelper.DeSerialize<T>(jsonValue);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="fileFullName"></param>
//        /// <returns></returns>
//        public static T ConvertJSONFileToObject<T>(string fileFullName)
//        {
//            T convertedObject = default(T);

//            if (fileFullName != null && fileFullName.Length > 0)
//            {
//                string value = FileHelper.GetFileContent(fileFullName);
//                convertedObject = JsonHelper.DeSerialize<T>(value);
//            }

//            return convertedObject;
//        }
//        #endregion

//        #region KeyValue 集合的序列化
//        /// <summary>
//        /// Creates a NameValueCollection from two string. The first contains the key pattern and the second contains the values
//        /// spaced according to the kys
//        /// </summary>
//        /// <param name="serializerData"></param>
//        /// <returns>A NVC populated based on the keys and vaules</returns>
//        /// <example>
//        /// string keys = "key1:S:0:3:key2:S:3:2:";
//        /// string values = "12345";
//        /// This would result in a NameValueCollection with two keys (Key1 and Key2) with the values 123 and 45
//        /// </example>
//        public static NameValueCollection ConvertToNameValueCollection(SerializerData serializerData)
//        {
//            return ConvertToNameValueCollection(serializerData.Keys, serializerData.Values);
//        }

//        /// <summary>
//        /// Creates a NameValueCollection from two string. The first contains the key pattern and the second contains the values
//        /// spaced according to the kys
//        /// </summary>
//        /// <param name="keys">Keys for the namevalue collection</param>
//        /// <param name="values">Values for the namevalue collection</param>
//        /// <returns>A NVC populated based on the keys and vaules</returns>
//        /// <example>
//        /// string keys = "key1:S:0:3:key2:S:3:2:";
//        /// string values = "12345";
//        /// This would result in a NameValueCollection with two keys (Key1 and Key2) with the values 123 and 45
//        /// </example>
//        public static NameValueCollection ConvertToNameValueCollection(string keys, string values)
//        {
//            NameValueCollection nvc = new NameValueCollection();

//            if (keys != null && values != null && keys.Length > 0 && values.Length > 0)
//            {
//                char[] splitter = new char[1] { ':' };
//                string[] keyNames = keys.Split(splitter);

//                for (int i = 0; i < (keyNames.Length / 4); i++)
//                {
//                    int start = int.Parse(keyNames[(i * 4) + 2], CultureInfo.InvariantCulture);
//                    int len = int.Parse(keyNames[(i * 4) + 3], CultureInfo.InvariantCulture);
//                    string key = keyNames[i * 4];

//                    //Future version will support more complex types	
//                    if (((keyNames[(i * 4) + 1] == "S") && (start >= 0)) && (len > 0) && (values.Length >= (start + len)))
//                    {
//                        nvc[key] = values.Substring(start, len);
//                    }
//                }
//            }

//            return nvc;
//        }

//        /// <summary>
//        /// Creates a the keys and values strings for the simple serialization based on a NameValueCollection
//        /// </summary>
//        /// <param name="nvc">NameValueCollection to convert</param>
//        /// <param name="keys">the ref string will contain the keys based on the key format</param>
//        /// <param name="values">the ref string will contain all the values of the namevaluecollection</param>
//        public static SerializerData ConvertToSerializerData(NameValueCollection nvc)
//        {
//            SerializerData serializerData = new SerializerData();
//            if (nvc != null && nvc.Count > 0)
//            {
//                StringBuilder sbKey = new StringBuilder();
//                StringBuilder sbValue = new StringBuilder();

//                int index = 0;
//                foreach (string key in nvc.AllKeys)
//                {
//                    if (key.IndexOf(':') != -1)
//                    {
//                        throw new ArgumentException("ExtendedAttributes Key can not contain the character \":\"");
//                    }

//                    string v = nvc[key];
//                    if (!string.IsNullOrEmpty(v))
//                    {
//                        sbKey.AppendFormat("{0}:S:{1}:{2}:", key, index, v.Length);
//                        sbValue.Append(v);
//                        index += v.Length;
//                    }
//                }
//                serializerData.Keys = sbKey.ToString();
//                serializerData.Values = sbValue.ToString();
//            }

//            return serializerData;
//        }
//        #endregion
//    }
//}