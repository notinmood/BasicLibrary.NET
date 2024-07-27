using System;

namespace HiLand.Utility.Entity
{
    /// <summary>
    /// 为元素添加验证信息的特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ValidateAttribute : Attribute
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        private ValidateTypes validateType;
        /// <summary>
        /// 最小长度
        /// </summary>
        private int minLength;
        /// <summary>
        /// 最大长度
        /// </summary>
        private int maxLength;
 
        /// <summary>
        /// 验证类型
        /// </summary>
        public ValidateTypes ValidateType
        {
            get { return this.validateType; }
        }

        /// <summary>
        /// 最小长度
        /// </summary>
        public int MinLength
        {
            get { return this.minLength; }
            set { this.minLength = value; }
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength
        {
            get { return this.maxLength; }
            set { this.maxLength = value; }
        }



        /// <summary>
        /// 指定采取何种验证方式来验证元素的有效性
        /// </summary>
        /// <param name="validateType"></param>
        public ValidateAttribute(ValidateTypes validateType)
        {
            this.validateType = validateType;
        }
    }

}
