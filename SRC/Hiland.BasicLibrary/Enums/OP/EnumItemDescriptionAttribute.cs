using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Hiland.BasicLibrary.Enums.OP
{
    /// <summary>
    /// 枚举项描述的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumItemDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 显示序列的名称
        /// </summary>
        private string displaySerialName;

        /// <summary>
        /// 枚举项的描述
        /// </summary>
        private string displaySerialValue;


        /// <summary>
        ///   Internal constructor.
        /// </summary>
        /// <param name="displaySerialName">special <see cref="CultureInfo"/></param>
        /// <param name="displaySerialValue">the content of Descroption</param>
        public EnumItemDescriptionAttribute(string displaySerialName, string displaySerialValue)
        {
            this.displaySerialName = displaySerialName;
            this.displaySerialValue = displaySerialValue;
        }


        /// <summary>
        ///   Gets the <see cref="CultureInfo"/> for current decription.
        /// </summary>
        public string DisplaySerialName
        {
            get { return displaySerialName; }
        }


        /// <summary>
        ///   Gets the description content.
        /// </summary>
        public string DisplaySerialValue
        {
            get { return displaySerialValue; }
        }
    }
}
