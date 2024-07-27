using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Mathes.StringParse
{
    /// <summary>
    /// 运算符类型(从上到下优先级依次递减)，数值越大，优先级越低
    /// </summary>
    public enum OperatorTypes
    {
        /// <summary>
        /// 左括号:(,left bracket
        /// </summary>
        LB = 10,

        /// <summary>
        /// 右括号),right bracket
        /// </summary>
        RB = 11,

        /// <summary>
        /// 逻辑非,!,NOT
        /// </summary>
        NOT = 20,

        /// <summary>
        /// 正号,+,positive sign
        /// </summary>
        PS = 21,

        /// <summary>
        /// 负号,-,negative sign
        /// </summary>
        NS = 22,

        /// <summary>
        /// 正切，tan
        /// </summary>
        TAN = 23,
        /// <summary>
        /// 反正切，atan
        /// </summary>
        ATAN = 24,


        /// <summary>
        /// 乘,*,multiplication
        /// </summary>
        MUL = 30,

        /// <summary>
        /// 除,/,division
        /// </summary>
        DIV = 31,

        /// <summary>
        /// 余,%,modulus
        /// </summary>
        MOD = 32,

        /// <summary>
        /// 加,+,Addition
        /// </summary>
        ADD = 40,

        /// <summary>
        /// 减,-,subtraction
        /// </summary>
        SUB = 41,

        /// <summary>
        /// 小于,less than
        /// </summary>
        LT = 50,

        /// <summary>
        /// 小于或等于,less than or equal to
        /// </summary>
        LE = 51,

        /// <summary>
        /// 大于,>,greater than
        /// </summary>
        GT = 52,

        /// <summary>
        /// 大于或等于,>=,greater than or equal to
        /// </summary>
        GE = 53,

        /// <summary>
        /// 等于,=,equal to
        /// </summary>
        ET = 60,

        /// <summary>
        /// 不等于,unequal to
        /// </summary>
        UT = 61,

        /// <summary>
        /// 逻辑与,&,AND
        /// </summary>
        AND = 70,

        /// <summary>
        /// 逻辑或,|,OR
        /// </summary>
        OR = 71,

        /// <summary>
        /// 逗号,comma
        /// </summary>
        CA = 80,

        /// <summary>
        /// 结束符号 @
        /// </summary>
        END = 255,

        /// <summary>
        /// 错误符号
        /// </summary>
        ERR = 256,
    }
}
