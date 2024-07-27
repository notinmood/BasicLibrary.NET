//using Hiland.BasicLibrary.Enums;
//using Hiland.BasicLibrary.Reflection;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Globalization;
//using System.IO;
//using System.Reflection;
//using System.Runtime.Serialization.Formatters.Binary;

//namespace Hiland.BasicLibrary.Data
//{
//    /// <summary>
//    /// 数据类型转换器
//    /// </summary>
//    public static class Converter
//    {
//        /// <summary>
//        /// 对系统Convert.ChangeType方法进行扩展（其可以对可空值类型进行转换）
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="conversionType"></param>
//        /// <returns></returns>
//        /// <remarks>当使用系统Convert.ChangeType方法进行可空值类型进行转换的时候，会抛出异常。本扩展解决了这个问题。</remarks>
//        public static object ChangeType(object data, Type conversionType)
//        {
//            return ChangeType(data, conversionType, null);
//        }

//        /// <summary>
//        /// 对系统Convert.ChangeType方法进行扩展（其可以对可空值类型进行转换）
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="conversionType"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        /// <remarks>当使用系统Convert.ChangeType方法进行可空值类型进行转换的时候，会抛出异常。本扩展解决了这个问题。</remarks>
//        public static object ChangeType(object data, Type conversionType, object defaultValue)
//        {
//            object result = defaultValue;

//            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
//            {
//                if (data == null)
//                {
//                    return null;
//                }

//                NullableConverter nullableConverter = new NullableConverter(conversionType);
//                conversionType = nullableConverter.UnderlyingType;
//            }

//            try
//            {
//                if (defaultValue == null && (data == null || data.ToString().Trim() == string.Empty))
//                {
//                    if (TypeHelper.ConfirmIsNumberType(conversionType))
//                    {
//                        data = 0;
//                    }

//                    if (conversionType == typeof(DateTime))
//                    {
//                        data = DateTimeHelper.Min;
//                    }
//                }

//                result = Convert.ChangeType(data, conversionType);
//            }
//            catch
//            {
//                result = defaultValue;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 对系统Convert.ChangeType方法进行扩展（其可以对可空值类型进行转换,并且使用泛型的方式调用更方便）
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static T ChangeType<T>(object data)
//        {
//            return ChangeType<T>(data, default(T));
//        }

//        /// <summary>
//        /// 对系统Convert.ChangeType方法进行扩展（其可以对可空值类型进行转换,并且使用泛型的方式调用更方便）
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="data"></param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static T ChangeType<T>(object data, T defaultValue)
//        {
//            T result = default(T);
//            try
//            {
//                //Guid是一个非常特殊的类型，需要单独转换
//                if (typeof(T) == typeof(Guid))
//                {
//                    result = (T)(object)GuidHelper.TryConvert(data.ToString(), (Guid)(object)defaultValue);
//                }
//                else
//                {
//                    result = (T)ChangeType(data, typeof(T), defaultValue);
//                }
//            }
//            catch
//            {
//                result = defaultValue;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
//        /// </summary>
//        /// <param name="data">要转换的值,即原值</param>
//        /// <param name="fromBase">原值的进制,只能是2,8,10,16四个值。</param>
//        /// <param name="toBase">要转换到的目标进制，只能是2,8,10,16四个值。</param>
//        public static string ConvertBase(string data, NumberBase fromBase, NumberBase toBase)
//        {
//            return ConvertBase(data, (int)fromBase, (int)toBase);
//        }

//        /// <summary>
//        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
//        /// </summary>
//        /// <param name="data">要转换的值,即原值</param>
//        /// <param name="fromBase">原值的进制,只能是2,8,10,16四个值。</param>
//        /// <param name="toBase">要转换到的目标进制，只能是2,8,10,16四个值。</param>
//        public static string ConvertBase(string data, int fromBase, int toBase)
//        {
//            try
//            {
//                int intValue = Convert.ToInt32(data, fromBase);  //先转成10进制
//                string result = Convert.ToString(intValue, toBase);  //再转成目标进制
//                return result;
//            }
//            catch
//            {
//                return "0";
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static int ToInt32(bool data)
//        {
//            int result = 0;
//            if (data == true)
//            {
//                result = 1;
//            }
//            else
//            {
//                result = 0;
//            }

//            return result;
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static bool ToBoolean(int data)
//        {
//            bool result = false;
//            if (data <= 0)
//            {
//                result = false;
//            }
//            else
//            {
//                result = true;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 将Logics枚举项转换成布尔值
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static bool ToBoolean(Logics data)
//        {
//            bool result = false;
//            if (data == Logics.True)
//            {
//                result = true;
//            }
//            else
//            {
//                result = false;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 将布尔值转换为中文显示
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static string ToChineseString(bool data)
//        {
//            return ToChineseString(data, string.Empty);
//        }

//        /// <summary>
//        /// 将布尔值转换为中文显示
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="displaySerialName"></param>
//        /// <returns></returns>
//        public static string ToChineseString(bool data, string displaySerialName)
//        {
//            string result = string.Empty;
//            if (displaySerialName == null)
//            {
//                displaySerialName = string.Empty;
//            }
//            displaySerialName = displaySerialName.ToLower();

//            switch (displaySerialName)
//            {
//                case "effect":
//                    if (data == true)
//                    {
//                        result = "有效";
//                    }
//                    else
//                    {
//                        result = "无效";
//                    }
//                    break;
//                default:
//                    if (data == true)
//                    {
//                        result = "是";
//                    }
//                    else
//                    {
//                        result = "否";
//                    }
//                    break;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 将某个枚举项的字符串值转化成其对应的枚举类型
//        /// </summary>
//        /// <typeparam name="TEnum"></typeparam>
//        /// <param name="enumItemValue"></param>
//        /// <returns></returns>
//        public static TEnum ToEnum<TEnum>(string enumItemValue) where TEnum : struct
//        {
//            return (TEnum)Enum.Parse(typeof(TEnum), enumItemValue, true);
//        }

//        /// <summary>
//        /// 将某个枚举项的字符串值转化成其对应的枚举类型
//        /// </summary>
//        /// <typeparam name="TEnum"></typeparam>
//        /// <param name="enumItemValue"></param>
//        /// <returns></returns>
//        public static TEnum TryToEnum<TEnum>(string enumItemValue) where TEnum : struct
//        {
//            TEnum defaultValue = default(TEnum);
//            return TryToEnum<TEnum>(enumItemValue, defaultValue);
//        }

//        /// <summary>
//        /// 将某个枚举项的字符串值转化成其对应的枚举类型
//        /// </summary>
//        /// <typeparam name="TEnum"></typeparam>
//        /// <param name="enumItemValue"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static TEnum TryToEnum<TEnum>(string enumItemValue, TEnum defaultValue) where TEnum : struct
//        {
//            TEnum result = defaultValue;
//            try
//            {
//                result = (TEnum)Enum.Parse(typeof(TEnum), enumItemValue, true);
//            }
//            catch
//            {
//                //Do Nothing;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 将某个枚举项的字符串值转化成其对应的枚举类型
//        /// </summary>
//        /// <param name="enumType"></param>
//        /// <param name="enumItemValue"></param>
//        /// <returns></returns>
//        public static object TryToEnum(Type enumType, string enumItemValue)
//        {
//            object result = null;
//            try
//            {
//                result = Enum.Parse(enumType, enumItemValue, true);
//            }
//            catch
//            {
//                //Do Nothing;
//            }

//            return result;
//        }

//        /// <summary>
//        /// 将字符串类型的bool值转换成bool类型
//        /// </summary>
//        /// <param name="data">字符串类型的bool值</param>
//        /// <returns></returns>
//        public static bool TryToBoolean(string data)
//        {
//            return TryToBoolean(data, false);
//        }

//        /// <summary>
//        /// 将字符串类型的bool值转换成bool类型
//        /// </summary>
//        /// <param name="data">字符串类型的bool值</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static bool TryToBoolean(string data, bool defaultValue)
//        {
//            bool result = defaultValue;
//            bool.TryParse(data, out result);
//            return result;
//        }

//        /// <summary>
//        /// 将字符串类型的数字转换成数字类型
//        /// </summary>
//        /// <param name="data">字符串类型的数字</param>
//        /// <returns></returns>
//        public static int TryToInt32(string data)
//        {
//            return TryToInt32(data, 0);
//        }

//        /// <summary>
//        /// 将字符串类型的数字转换成数字类型
//        /// </summary>
//        /// <param name="data">字符串类型的数字</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static int TryToInt32(string data, int defaultValue)
//        {
//            int result = defaultValue;
//            int.TryParse(data, out result);
//            return result;
//        }


//        /// <summary>
//        /// 将字符串类型的Decimal转换成Decimal类型
//        /// </summary>
//        /// <param name="data">字符串类型的Decimal</param>
//        /// <returns></returns>
//        public static decimal TryToDecimal(string data)
//        {
//            return TryToDecimal(data, 0);
//        }

//        /// <summary>
//        /// 将字符串类型的Decimal转换成Decimal类型
//        /// </summary>
//        /// <param name="data">字符串类型的Decimal</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static decimal TryToDecimal(string data, decimal defaultValue)
//        {
//            decimal result = defaultValue;
//            decimal.TryParse(data, out result);
//            return result;
//        }

//        /// <summary>
//        /// 将字符串类型的Double转换成Double类型
//        /// </summary>
//        /// <param name="data">字符串类型的Double</param>
//        /// <returns></returns>
//        public static double TryToDouble(string data)
//        {
//            return TryToDouble(data, 0);
//        }

//        /// <summary>
//        /// 将字符串类型的Double转换成Double类型
//        /// </summary>
//        /// <param name="data">字符串类型的Double</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static double TryToDouble(string data, double defaultValue)
//        {
//            double result = defaultValue;
//            double.TryParse(data, out result);
//            return result;
//        }


//        /// <summary>
//        /// 将字符串类型的Single转换成Single类型
//        /// </summary>
//        /// <param name="data">字符串类型的Single</param>
//        /// <returns></returns>
//        public static float TryToSingle(string data)
//        {
//            return TryToSingle(data, 0);
//        }

//        /// <summary>
//        /// 将字符串类型的Single转换成Single类型
//        /// </summary>
//        /// <param name="data">字符串类型的Single</param>
//        /// <param name="defaultValue">缺省值</param>
//        /// <returns></returns>
//        public static float TryToSingle(string data, float defaultValue)
//        {
//            float result = defaultValue;
//            float.TryParse(data, out result);
//            return result;
//        }

//        /// <summary>
//        /// 将字符串类型的DateTime转换成DateTime类型
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static DateTime TryToDateTime(string data)
//        {
//            DateTime defaultValue = DateTimeHelper.Min;
//            return TryToDateTime(data, defaultValue);
//        }

//        /// <summary>
//        /// 将字符串类型的DateTime转换成DateTime类型
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static DateTime TryToDateTime(string data, DateTime defaultValue)
//        {
//            return TryToDateTime(data, defaultValue, DateFormats.YMD);
//        }

//        /// <summary>
//        /// 将字符串类型的DateTime转换成DateTime类型
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="defaultValue"></param>
//        /// <param name="dateFormat"></param>
//        /// <returns></returns>
//        public static DateTime TryToDateTime(string data, DateTime defaultValue, DateFormats dateFormat)
//        {
//            //TODO:需要验证
//            DateTime result = defaultValue;
//            string cultureName = string.Empty;
//            switch (dateFormat)
//            {
//                case DateFormats.MDY:
//                    cultureName = "en-US";
//                    break;
//                case DateFormats.DMY:
//                    cultureName = "en-AU";
//                    break;
//                case DateFormats.YMD:
//                default:
//                    cultureName = "zh-CN";
//                    break;
//            }
//            CultureInfo cultureInfo = new CultureInfo(cultureName);

//            DateTime.TryParse(data, cultureInfo, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out result);
//            return result;
//        }

//        /// <summary>
//        /// 将对象转换为字节数组
//        /// </summary>
//        /// <param name="objectToConvert"></param>
//        /// <returns></returns>
//        public static byte[] ToBytes(object objectToConvert)
//        {
//            byte[] buffer = null;

//            BinaryFormatter formatter = new BinaryFormatter();
//            using (MemoryStream stream = new MemoryStream())
//            {
//                formatter.Serialize(stream, objectToConvert);
//                stream.Position = 0;
//                buffer = new byte[stream.Length];
//                stream.Read(buffer, 0, buffer.Length);
//                stream.Close();
//            }

//            return buffer;
//        }

//        /// <summary>
//        /// 将字节数组转换为对象
//        /// </summary>
//        /// <param name="byteArray"></param>
//        /// <returns></returns>
//        public static object ToObject(byte[] byteArray)
//        {
//            object result = null;
//            if (byteArray != null && byteArray.Length > 0)
//            {
//                BinaryFormatter formatter = new BinaryFormatter();
//                using (MemoryStream stream = new MemoryStream())
//                {
//                    stream.Write(byteArray, 0, byteArray.Length);
//                    stream.Position = 0;
//                    if (byteArray.Length > 4)
//                    {
//                        result = formatter.Deserialize(stream);
//                    }
//                    stream.Close();
//                }
//            }
//            return result;
//        }



//        /// <summary>
//        /// 有继承关系的基类实例向派生类实例的强制转换（主要为有继承关系的实体类型之间的转换）
//        /// </summary>
//        /// <typeparam name="TBase">基类类型</typeparam>
//        /// <typeparam name="TDeriver">派生类类型</typeparam>
//        /// <param name="baseInstance">基类实例</param>
//        /// <returns>派生类实例</returns>
//        public static TDeriver InheritedEntityConvert<TBase, TDeriver>(TBase baseInstance) where TDeriver : TBase, new()
//        {
//            if (baseInstance == null)
//            {
//                return default(TDeriver);
//            }

//            TDeriver deliveredInstance = new TDeriver();

//            return ReflectHelper.CopyMemberValue(baseInstance, deliveredInstance);
//        }


//        /// <summary>
//        /// 将字符串转换成Guid
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static Guid TryToGuid(string data)
//        {
//            return GuidHelper.TryConvert(data);
//        }

//        /// <summary>
//        /// 将位于DataTable内的实体集合转换为List`T`集合
//        /// </summary>
//        /// <param name="data">待转换的DataTable数据</param>
//        /// <returns></returns>
//        public static IList<T> ToList<T>(DataTable data) where T : new()
//        {
//            IList<T> result = new List<T>();

//            // 取得泛型的类型
//            Type type = typeof(T);

//            //// 创建类型的对象（用于比较用）
//            //object convertObj = Activator.CreateInstance(type, null);

//            // 反射取得类型实例的属性数组
//            PropertyInfo[] propertys = type.GetProperties();

//            foreach (DataRow dr in data.Rows)
//            {
//                // 创建类型的对象（用于赋值用）
//                //object outputObj = Activator.CreateInstance(type, null);
//                T outputObj = new T();

//                foreach (PropertyInfo pi in propertys)
//                {
//                    // 如果DataTable的数据列中包含有对应的属性
//                    if (data.Columns.Contains(pi.Name))
//                    {
//                        if (pi.CanWrite == false)
//                        {
//                            continue;
//                        }

//                        // 取得属性的值
//                        object value = dr[pi.Name];

//                        if (value != DBNull.Value)
//                        {
//                            // 将对应属性的值赋给创建的类型实例的对应的属性
//                            pi.SetValue(outputObj, value, null);
//                        }
//                    }
//                }

//                // 添加到List中
//                result.Add(outputObj);
//            }

//            return result;
//        }
//    }
//}
