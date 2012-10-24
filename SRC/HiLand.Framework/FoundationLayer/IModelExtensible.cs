using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.FoundationLayer
{
    public interface IModelExtensible
    {
        ExtentiblePropertyRepository ExtensiableRepository
        {
            get;
        }

        /// <summary>
        /// 扩展属性的名字（用逗号分隔的字符串集合）
        /// </summary>
        string PropertyNames
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性的值（用逗号分隔的字符串集合）
        /// </summary>
        string PropertyValues
        {
            get;
            set;
        }
    }
}
