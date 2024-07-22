using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Attributes
{
    /// <summary>
    /// 枚举项是否允许出现在列表中
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumItemIsDisplayInListAttribute : Attribute
    {
        private bool isDisplayInList = true;
        /// <summary>
        /// 枚举项是否允许出现在列表中
        /// </summary>
        public bool  IsDisplayInList 
        {
            get { return this.isDisplayInList; }
        
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isDisplayInList">枚举项是否允许出现在列表中</param>
        public EnumItemIsDisplayInListAttribute(bool isDisplayInList)
        {
            this.isDisplayInList = isDisplayInList;
        }
    }
}
