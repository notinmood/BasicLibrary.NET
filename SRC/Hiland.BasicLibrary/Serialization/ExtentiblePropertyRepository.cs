//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using HiLand.Utility.Data;

//namespace HiLand.Utility.Serialization
//{
//    /// <summary>
//    /// 可扩展属性的库
//    /// </summary>
//    /// <remarks>
//    /// 此库内可以有多个NameValueCollection（用字典表示，缺省情形下我们使用缺省的一个就可以了）；每个NameValueCollection内都可以记录多个属性的名字和值。
//    /// </remarks>
//    [Serializable]
//    public class ExtentiblePropertyRepository
//    {
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="keys"></param>
//        /// <param name="values"></param>
//        public ExtentiblePropertyRepository(string keys, string values)
//        {
//            this.keys = keys;
//            this.values = values;
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="serializerData"></param>
//        public ExtentiblePropertyRepository(SerializerData serializerData)
//        {
//            this.keys = serializerData.Keys;
//            this.values = serializerData.Values;
//        }

//        private string keys = string.Empty;
//        private string values = string.Empty;

//        /// <summary>
//        /// 传递进来的（用逗号分隔的字符串集合类型的）名称和值是否解析进入NameValueCollection内。
//        /// （为了提高系统的性能，扩展属性部分采用了延迟加载，即有请求才加载。本字段标明扩展属性是否经过解析和加载）
//        /// </summary>
//        public bool isParserd = false;


//        string defaultSettingName = "defaultSettingNameForSystem";
//        private Dictionary<string, NameValueCollection> extendedAttributesDic = new Dictionary<string, NameValueCollection>();

//        /// <summary>
//        /// 获取可扩展属性的值
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public string GetExtentibleProperty(string name)
//        {
//            return GetExtentibleProperty(string.Empty, name);
//        }

//        /// <summary>
//        /// 获取可扩展属性的值
//        /// </summary>
//        /// <param name="settingName"></param>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public string GetExtentibleProperty(string settingName, string name)
//        {
//            NameValueCollection nvc = GetOrAddNVC(settingName);

//            string returnValue = nvc[name];

//            if (returnValue == null)
//            {
//                return string.Empty;
//            }
//            else
//            {
//                return returnValue;
//            }
//        }

//        /// <summary>
//        /// 设置可扩展属性的值
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="value"></param>
//        public virtual void SetExtentibleProperty(string name, string value)
//        {
//            SetExtentibleProperty(string.Empty, name, value);
//        }

//        /// <summary>
//        /// 设置可扩展属性的值
//        /// </summary>
//        /// <param name="settingName"></param>
//        /// <param name="name"></param>
//        /// <param name="value"></param>
//        public virtual void SetExtentibleProperty(string settingName, string name, string value)
//        {
//            NameValueCollection nvc = GetOrAddNVC(settingName);

//            if ((value == null) || (value == string.Empty))
//            {
//                nvc.Remove(name);
//            }
//            else
//            {
//                nvc[name] = value;
//            }
//        }

//        /// <summary>
//        /// 获取可扩展属性的数量
//        /// </summary>
//        /// <returns></returns>
//        public int GetExtentiblePropertyCount()
//        {
//            return GetExtentiblePropertyCount(string.Empty);
//        }

//        /// <summary>
//        /// 获取可扩展属性的数量
//        /// </summary>
//        /// <param name="settingName"></param>
//        /// <returns></returns>
//        public int GetExtentiblePropertyCount(string settingName)
//        {
//            NameValueCollection nvc = GetOrAddNVC(settingName);
//            return nvc.Count;
//        }

//        /// <summary>
//        /// 获取可扩展属性的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public virtual T GetExtentibleProperty<T>(string name)
//        {
//            return GetExtentibleProperty<T>(string.Empty, name);
//        }

//        /// <summary>
//        /// 获取可扩展属性的值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="settingName"></param>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public virtual T GetExtentibleProperty<T>(string settingName, string name)
//        {
//            T t;
//            string b = GetExtentibleProperty(settingName, name);
//            if (string.IsNullOrEmpty(b))
//            {
//                return default(T);
//            }
//            else
//            {
//                t = (T)Converter.ChangeType(b, typeof(T));
//            }

//            return t;
//        }

//        /// <summary>
//        /// 获取指定的NameValueCollection(如果字典中不存在此NVC，则同时创建)
//        /// </summary>
//        /// <param name="settingName">NameValueCollection的名称</param>
//        /// <returns></returns>
//        private NameValueCollection GetOrAddNVC(string settingName)
//        {
//            if (string.IsNullOrEmpty(settingName))
//            {
//                settingName = defaultSettingName;
//            }

//            if (extendedAttributesDic.ContainsKey(settingName) == false)
//            {
//                NameValueCollection nvc = Serializer.ConvertToNameValueCollection(this.keys, this.values);
//                extendedAttributesDic[settingName] = nvc;
//                this.isParserd = true;
//            }

//            return extendedAttributesDic[settingName];
//        }


//        #region Serialization
//        public SerializerData GetSerializerData()
//        {
//            return GetSerializerData(string.Empty);
//        }

//        public SerializerData GetSerializerData(string settingName)
//        {
//            if (string.IsNullOrEmpty(settingName))
//            {
//                settingName = defaultSettingName;
//            }

//            NameValueCollection nvc = GetOrAddNVC(settingName);
//            SerializerData data = Serializer.ConvertToSerializerData(nvc);

//            return data;
//        }

//        /// <summary>
//        /// 将一个序列化数据集写入“库”中
//        /// </summary>
//        /// <param name="data"></param>
//        public void PushSerializerData(SerializerData data)
//        {
//            PushSerializerData(defaultSettingName, data);
//        }

//        /// <summary>
//        /// 将一个序列化数据集写入“库”中
//        /// </summary>
//        /// <param name="settingName">序列化数据集在库中的名字</param>
//        /// <param name="data"></param>
//        public void PushSerializerData(string settingName, SerializerData data)
//        {
//            NameValueCollection nvc = Serializer.ConvertToNameValueCollection(data.Keys, data.Values);
//            if (nvc == null)
//            {
//                nvc = new NameValueCollection();
//            }

//            extendedAttributesDic[settingName] = nvc;
//        }
//        #endregion
//    }
//}