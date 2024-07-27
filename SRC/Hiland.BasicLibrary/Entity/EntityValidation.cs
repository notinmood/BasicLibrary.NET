using System;
using System.Reflection;
using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary.Entity
{
    public class EntityValidation
    {
        /// <summary>
        /// 验证实体对象的所有带验证特性的元素  并返回验证结果  如果返回结果为String.Empty 则说明元素符合验证要求
        /// </summary>
        /// <param name="entityObject">实体对象</param>
        /// <returns></returns>
        public static string GetValidateResult(object entityObject)
        {
            if (entityObject == null)
            {
                throw new ArgumentNullException("entityObject");
            }

            Type type = entityObject.GetType();
            PropertyInfo[] properties = type.GetProperties();

            string validateResult = string.Empty;

            foreach (PropertyInfo property in properties)
            {
                //获取验证特性
                object[] validateContent = property.GetCustomAttributes(typeof(ValidateAttribute), true);

                if (validateContent != null)
                {
                    //获取属性的值
                    object propertyValue = property.GetValue(entityObject, null);
                    
                    Array array = Enum.GetValues(typeof(ValidateTypes));
                    int validateTypesLength = array.Length;
                    
                    foreach (ValidateAttribute validateAttribute in validateContent)
                    {
                        for (int i = 0; i < validateTypesLength; i++)
                        {
                            ValidateTypes currentValidateType = (ValidateTypes)array.GetValue(i);

                            if ((validateAttribute.ValidateType & currentValidateType) == currentValidateType)
                            {
                               validateResult += validateDetails(property.Name, propertyValue, validateAttribute);
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(validateResult))
                {
                    break;
                }
            }

            return validateResult;
        }

        private static string validateDetails(string  propertyName, object propertyValue, ValidateAttribute validateAttribute)
        {
            string validateResult= string.Empty;
            if (validateAttribute.ValidateType != ValidateTypes.IsEmpty)
            {
                if (null == propertyValue || propertyValue.ToString().Length < 1)
                {
                    return string.Empty;
                }
            }

            switch (validateAttribute.ValidateType)
            {
                //验证元素是否为空字串
                case ValidateTypes.IsEmpty:
                    if (null == propertyValue || propertyValue.ToString().Length < 1)
                        validateResult = string.Format("元素 {0} 不能为空;", propertyName);
                    break;
                //验证元素的长度是否小于指定最小长度
                case ValidateTypes.MinLength:
                    if (propertyValue.ToString().Length < validateAttribute.MinLength)
                        validateResult = string.Format("元素 {0} 的长度不能小于 {1};", propertyName, validateAttribute.MinLength);
                    break;
                //验证元素的长度是否大于指定最大长度
                case ValidateTypes.MaxLength:
                    if (propertyValue.ToString().Length > validateAttribute.MaxLength)
                        validateResult = string.Format("元素 {0} 的长度不能大于{1};", propertyName, validateAttribute.MaxLength);
                    break;
                //验证元素的长度是否符合指定的最大长度和最小长度的范围
                case ValidateTypes.MinLength | ValidateTypes.MaxLength:
                    if (propertyValue.ToString().Length > validateAttribute.MaxLength || propertyValue.ToString().Length < validateAttribute.MinLength)
                        validateResult = string.Format("元素 {0} 不符合指定的最小长度和最大长度的范围,应该在 {1} 与 {2} 之间;", propertyName, validateAttribute.MinLength, validateAttribute.MaxLength);
                    break;
                //验证元素的值是否为值类型
                case ValidateTypes.IsNumber:
                    if (StringHelper.IsInt(propertyValue.ToString()) == false)
                        validateResult = string.Format("元素 {0} 的值不是值类型;", propertyName);
                    break;
                //验证元素的值是否为正确的时间格式
                case ValidateTypes.IsDateTime:
                    if (StringHelper.IsDateTime(propertyValue.ToString())==false)
                        validateResult = string.Format("元素 {0} 不是正确的时间格式;", propertyName);
                    break;
                //验证元素的值是否为正确的浮点格式
                case ValidateTypes.IsDecimal:
                    if (StringHelper.IsDecimal(propertyValue.ToString()) == false)
                        validateResult = string.Format("元素 {0} 不是正确的金额格式;", propertyName);
                    break;
                //验证元素的值是否为固定电话号码格式
                case ValidateTypes.IsTelphone:
                    if (!System.Text.RegularExpressions.Regex.IsMatch(propertyValue.ToString(), @"^(\d{3,4}-)?\d{6,8}$"))
                        validateResult = string.Format("元素 {0} 不是正确的固定电话号码格式;", propertyName);
                    break;
                //验证元素的值是否为手机号码格式
                case ValidateTypes.IsMobile:
                    if (!System.Text.RegularExpressions.Regex.IsMatch(propertyValue.ToString(), @"^[1]+[3,5]+\d{9}$"))
                        validateResult = string.Format("元素 {0} 不是正确的手机号码格式;", propertyName);
                    break;
                //验证元素的值是否为Email格式
                case ValidateTypes.IsEmail:
                    if (StringHelper.IsEmail(propertyValue.ToString()) == false)
                        validateResult = string.Format("元素 {0} 不是正确的Email格式;", propertyName);
                    break;
                //验证元素的值是否为IP格式
                case ValidateTypes.IsIP:
                    if (StringHelper.IsIP(propertyValue.ToString()) == false)
                        validateResult = string.Format("元素 {0} 不是正确的IP格式;", propertyName);
                    break;
                default:
                    break;
            }
            return validateResult;
        }
    }
}
