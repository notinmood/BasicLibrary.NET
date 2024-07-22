//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Globalization;
//using System.Threading;
//using HiLand.Utility.Setting;

//namespace HiLand.Utility.Enums.OP
//{
//    /// <summary>
//    ///   Wrapper a data source object for enumerator <paramref name="EnumType"/>.
//    /// </summary>
//    /// <typeparam name="TEnum">
//    ///   The Enum type.
//    /// </typeparam>
//    /// <exception cref="NotSupportedException"/>
//    /// <example>
//    /// this.comboBox1.DataSource = new EnumDataSource&lt;MyEnum&gt;();
//    /// this.comboBox1.DisplayMember = "DisplayValue";
//    /// this.comboBox1.ValueMember = "Value";
//    /// </example>
//    public class EnumDataSource<TEnum> : List<EnumDataSource<TEnum>.EnumAdapter> where TEnum : struct
//    {
//        /// <summary>
//        ///  Constructor a new <see cref="EnumDataSource"/>
//        /// </summary>
//        /// <exception cref="NotSupportedException"/>
//        public EnumDataSource()
//            : this(string.Empty)
//        {
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="displaySerialName"></param>
//        public EnumDataSource(string displaySerialName)
//        {
//            if (typeof(TEnum).IsEnum == false)
//            {
//                throw new NotSupportedException("Can not support type: " + typeof(TEnum).FullName);
//            }

//            // Use Enum helper enumerator list all enum values and add to current context.
//            foreach (TEnum currentEnumItem in Enum.GetValues(typeof(TEnum)))
//            {
//                base.Add(new EnumAdapter(currentEnumItem, displaySerialName));
//            }
//        }

//        /// <summary>
//        ///  Enum value adapter, used to get values from each Cultures.
//        /// </summary>
//        public sealed class EnumAdapter
//        {
//            /// <summary>
//            /// Constructor an <see cref="EnumAdapter"/>.
//            /// </summary>
//            /// <param name="value">The enum value.</param>
//            /// <exception cref="">
//            ///   
//            /// </exception>
//            public EnumAdapter(TEnum value)
//                : this(value, string.Empty)
//            {
//            }

//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="value"></param>
//            /// <param name="displaySerialName"></param>
//            public EnumAdapter(TEnum value, string displaySerialName)
//            {
//                if (Enum.IsDefined(typeof(TEnum), value) == false)
//                {
//                    throw new ArgumentException(string.Format("{0} is not defined in {1}", value, typeof(TEnum).Name), "value");
//                }
//                this.enumItemValue = value;
//                this.displaySerialName = displaySerialName;
//            }

//            /// <summary>
//            /// Storage the actual Enum value.
//            /// </summary>
//            private TEnum enumItemValue;

//            /// <summary>
//            /// 在一个枚举项上，可能有多个Attrbute描述（有某种语言的，可能也有其他的），此项指定要显示哪一个
//            /// </summary>
//            private string displaySerialName = string.Empty;

//            /// <summary>
//            ///   Gets the actual enum value.
//            /// </summary>
//            public TEnum EnumItemValue
//            {
//                get { return enumItemValue; }
//            }

//            /// <summary>
//            ///   Gets the display value for enum value by search local resource with currrent UI culture 
//            ///   and special key which is concated from Enum type name and Enum value name.
//            /// </summary>
//            /// <remarks>
//            ///   This would get correct display value by accessing location resource with current UI Culture.
//            /// </remarks>
//            public string EnumItemText
//            {
//                get
//                {
//                    Dictionary<string, EnumItemDescriptionAttribute> disc = EnumItemDescriptionHelper.GetDisplayValues(enumItemValue, typeof(TEnum));

//                    CultureInfo currentUICulute = Thread.CurrentThread.CurrentUICulture;

//                    if (string.IsNullOrEmpty(this.displaySerialName))
//                    {
//                        this.displaySerialName = Config.GetAppSetting("currentLanguage");
//                    }

//                    if (string.IsNullOrEmpty(this.displaySerialName))
//                    {
//                        this.displaySerialName = currentUICulute.Name;
//                    }

//                    if (disc.ContainsKey(this.displaySerialName))
//                    {
//                        return disc[this.displaySerialName].DisplaySerialValue;
//                    }

//                    return enumItemValue.ToString();
//                }
//            }
//        }
//    }
//}