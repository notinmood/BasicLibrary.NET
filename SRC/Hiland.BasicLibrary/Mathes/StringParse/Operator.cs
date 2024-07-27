using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Mathes.StringParse
{
    /// <summary>
    /// 运算符类
    /// </summary>
    public class Operator
    {
        public Operator(OperatorTypes type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// 运算符类型
        /// </summary>
        public OperatorTypes Type { get; set; }

        /// <summary>
        /// 运算符值
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// 对于>或者&lt;运算符，判断实际是否为>=,&lt;&gt;、&lt;=，并调整当前运算符位置
        /// </summary>
        /// <param name="currentOpt">当前运算符</param>
        /// <param name="currentExp">当前表达式</param>
        /// <param name="currentOptPos">当前运算符位置</param>
        /// <param name="adjustOptPos">调整后运算符位置</param>
        /// <returns>返回调整后的运算符</returns>
        public static string AdjustOperator(string currentOpt, string currentExp, ref int currentOptPos)
        {
            switch (currentOpt)
            {
                case "<":
                    if (currentExp.Substring(currentOptPos, 2) == "<=")
                    {
                        currentOptPos++;
                        return "<=";
                    }
                    if (currentExp.Substring(currentOptPos, 2) == "<>")
                    {
                        currentOptPos++;
                        return "<>";
                    }
                    return "<";

                case ">":
                    if (currentExp.Substring(currentOptPos, 2) == ">=")
                    {
                        currentOptPos++;
                        return ">=";
                    }
                    return ">";
                case "t":
                    if (currentExp.Substring(currentOptPos, 3) == "tan")
                    {
                        currentOptPos += 2;
                        return "tan";
                    }
                    return "error";
                case "a":
                    if (currentExp.Substring(currentOptPos, 4) == "atan")
                    {
                        currentOptPos += 3;
                        return "atan";
                    }
                    return "error";
                default:
                    return currentOpt;
            }
        }

        /// <summary>
        /// 转换运算符到指定的类型
        /// </summary>
        /// <param name="opt">运算符</param>
        /// <param name="isBinaryOperator">是否为二元运算符</param>
        /// <returns>返回指定的运算符类型</returns>
        public static OperatorTypes ConvertOperator(string opt, bool isBinaryOperator)
        {
            switch (opt)
            {
                case "!": return OperatorTypes.NOT;
                case "+": return isBinaryOperator ? OperatorTypes.ADD : OperatorTypes.PS;
                case "-": return isBinaryOperator ? OperatorTypes.SUB : OperatorTypes.NS;
                case "*": return isBinaryOperator ? OperatorTypes.MUL : OperatorTypes.ERR;
                case "/": return isBinaryOperator ? OperatorTypes.DIV : OperatorTypes.ERR;
                case "%": return isBinaryOperator ? OperatorTypes.MOD : OperatorTypes.ERR;
                case "<": return isBinaryOperator ? OperatorTypes.LT : OperatorTypes.ERR;
                case ">": return isBinaryOperator ? OperatorTypes.GT : OperatorTypes.ERR;
                case "<=": return isBinaryOperator ? OperatorTypes.LE : OperatorTypes.ERR;
                case ">=": return isBinaryOperator ? OperatorTypes.GE : OperatorTypes.ERR;
                case "<>": return isBinaryOperator ? OperatorTypes.UT : OperatorTypes.ERR;
                case "=": return isBinaryOperator ? OperatorTypes.ET : OperatorTypes.ERR;
                case "&": return isBinaryOperator ? OperatorTypes.AND : OperatorTypes.ERR;
                case "|": return isBinaryOperator ? OperatorTypes.OR : OperatorTypes.ERR;
                case ",": return isBinaryOperator ? OperatorTypes.CA : OperatorTypes.ERR;
                case "@": return isBinaryOperator ? OperatorTypes.END : OperatorTypes.ERR;
                default: return OperatorTypes.ERR;
            }
        }

        /// <summary>
        /// 转换运算符到指定的类型
        /// </summary>
        /// <param name="opt">运算符</param>
        /// <returns>返回指定的运算符类型</returns>
        public static OperatorTypes ConvertOperator(string opt)
        {
            switch (opt)
            {
                case "!": return OperatorTypes.NOT;
                case "+": return OperatorTypes.ADD;
                case "-": return OperatorTypes.SUB;
                case "*": return OperatorTypes.MUL;
                case "/": return OperatorTypes.DIV;
                case "%": return OperatorTypes.MOD;
                case "<": return OperatorTypes.LT;
                case ">": return OperatorTypes.GT;
                case "<=": return OperatorTypes.LE;
                case ">=": return OperatorTypes.GE;
                case "<>": return OperatorTypes.UT;
                case "=": return OperatorTypes.ET;
                case "&": return OperatorTypes.AND;
                case "|": return OperatorTypes.OR;
                case ",": return OperatorTypes.CA;
                case "@": return OperatorTypes.END;
                case "tan": return OperatorTypes.TAN;
                case "atan": return OperatorTypes.ATAN;
                default: return OperatorTypes.ERR;
            }
        }

        /// <summary>
        /// 运算符是否为二元运算符,该方法有问题，暂不使用
        /// </summary>
        /// <param name="tokens">语法单元堆栈</param>
        /// <param name="operators">运算符堆栈</param>
        /// <param name="currentOpd">当前操作数</param>
        /// <returns>是返回真,否返回假</returns>
        public static bool IsBinaryOperator(ref Stack<object> tokens, ref Stack<Operator> operators, string currentOpd)
        {
            if (currentOpd != "")
            {
                return true;
            }
            else
            {
                object token = tokens.Peek();
                if (token is Operand)
                {
                    if (operators.Peek().Type != OperatorTypes.LB)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (((Operator)token).Type == OperatorTypes.RB)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 运算符优先级比较
        /// </summary>
        /// <param name="optA">运算符类型A</param>
        /// <param name="optB">运算符类型B</param>
        /// <returns>A与B相比，-1，低；0,相等；1，高</returns>
        public static int ComparePriority(OperatorTypes optA, OperatorTypes optB)
        {
            if (optA == optB)
            {
                //A、B优先级相等
                return 0;
            }

            //乘,除,余(*,/,%)
            if ((optA >= OperatorTypes.MUL && optA <= OperatorTypes.MOD) &&
                (optB >= OperatorTypes.MUL && optB <= OperatorTypes.MOD))
            {
                return 0;
            }
            //加,减(+,-)
            if ((optA >= OperatorTypes.ADD && optA <= OperatorTypes.SUB) &&
                (optB >= OperatorTypes.ADD && optB <= OperatorTypes.SUB))
            {
                return 0;
            }
            //小于,小于或等于,大于,大于或等于(<,<=,>,>=)
            if ((optA >= OperatorTypes.LT && optA <= OperatorTypes.GE) &&
                (optB >= OperatorTypes.LT && optB <= OperatorTypes.GE))
            {
                return 0;
            }
            //等于,不等于(=,<>)
            if ((optA >= OperatorTypes.ET && optA <= OperatorTypes.UT) &&
                (optB >= OperatorTypes.ET && optB <= OperatorTypes.UT))
            {
                return 0;
            }
            //三角函数
            if ((optA >= OperatorTypes.TAN && optA <= OperatorTypes.ATAN) &&
                    (optB >= OperatorTypes.TAN && optB <= OperatorTypes.ATAN))
            {
                return 0;
            }

            if (optA < optB)
            {
                //A优先级高于B
                return 1;
            }

            //A优先级低于B
            return -1;

        }
    }
}
