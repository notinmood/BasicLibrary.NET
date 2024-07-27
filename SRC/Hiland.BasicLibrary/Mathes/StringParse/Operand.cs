using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Mathes.StringParse
{
    /// <summary>
    /// 操作数类
    /// </summary>
    public class Operand
    {
        #region Constructed Function
        public Operand(OperandTypes type, object value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Operand(string opd, object value)
        {
            this.Type = ConvertOperand(opd);
            this.Value = value;
        }
        #endregion

        #region Variable &　Property
        /// <summary>
        /// 操作数类型
        /// </summary>
        public OperandTypes Type { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 操作数值
        /// </summary>
        public object Value { get; set; }

        #endregion

        #region Public Method
        /// <summary>
        /// 转换操作数到指定的类型
        /// </summary>
        /// <param name="opd">操作数</param>
        /// <returns>返回对应的操作数类型</returns>
        public static OperandTypes ConvertOperand(string opd)
        {
            if (opd.IndexOf("(") > -1)
            {
                return OperandTypes.Func;
            }
            else if (IsNumber(opd))
            {
                return OperandTypes.Number;
            }
            else if (IsDate(opd))
            {
                return OperandTypes.Date;
            }
            else
            {
                return OperandTypes.String;
            }
        }

        /// <summary>
        /// 判断对象是否为数字
        /// </summary>
        /// <param name="value">对象值</param>
        /// <returns>是返回真,否返回假</returns>
        public static bool IsNumber(object value)
        {
            double val;
            return double.TryParse(value.ToString(), out val);
        }

        /// <summary>
        /// 判断对象是否为日期
        /// </summary>
        /// <param name="value">对象值</param>
        /// <returns>是返回真,否返回假</returns>
        public static bool IsDate(object value)
        {
            DateTime dt;
            return DateTime.TryParse(value.ToString(), out dt);
        }
        #endregion
    }
}
