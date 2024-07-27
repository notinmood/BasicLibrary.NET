using System;
using System.Text.RegularExpressions;

namespace Hiland.BasicLibrary.Finance
{
    public class RMB
    {
        /// <summary> 
        /// 获取财务数据的人民币表示形式
        /// </summary> 
        /// <param name="financeValue">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string GetChineseDisplayValue(decimal financeValue)
        {
            string chineseDisplayValue = "";  //人民币大写金额形式 

            string chineseNumbers = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string chineseUnits = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 

            string chineseNumber = "";    //数字的汉语读法 
            string chineseUnit = "";    //数字位的汉字读法 

            string financeValueString = "";    //数字的字符串形式 
            int financeValueChar = 0;            //从原num值中取出的值 
            string financeValueCharString = "";    //从原num值中取出的值 
            int financeValueLength;    //num的值乘以100的字符串长度 

            int i;    //循环变量 
            int zeroCount = 0;  //用来计算连续的零值的个数

            financeValue = Math.Round(Math.Abs(financeValue), 2);    //将num取绝对值并四舍五入取2位小数 
            financeValueString = ((long)(financeValue * 100)).ToString();        //将num乘100并转换成字符串形式 
            financeValueLength = financeValueString.Length;      //找出最高位 

            if (financeValueLength > 15)
            {
                return "溢出";
            }

            chineseUnits = chineseUnits.Substring(15 - financeValueLength);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < financeValueLength; i++)
            {
                financeValueCharString = financeValueString.Substring(i, 1);          //取出需转换的某一位的值 
                financeValueChar = Convert.ToInt32(financeValueCharString);      //转换为数字 
                if (i != (financeValueLength - 3) && i != (financeValueLength - 7) && i != (financeValueLength - 11) && i != (financeValueLength - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (financeValueCharString == "0")
                    {
                        chineseNumber = "";
                        chineseUnit = "";
                        zeroCount = zeroCount + 1;
                    }
                    else
                    {
                        if (financeValueCharString != "0" && zeroCount != 0)
                        {
                            chineseNumber = "零" + chineseNumbers.Substring(financeValueChar * 1, 1);
                            chineseUnit = chineseUnits.Substring(i, 1);
                            zeroCount = 0;
                        }
                        else
                        {
                            chineseNumber = chineseNumbers.Substring(financeValueChar * 1, 1);
                            chineseUnit = chineseUnits.Substring(i, 1);
                            zeroCount = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (financeValueCharString != "0" && zeroCount != 0)
                    {
                        chineseNumber = "零" + chineseNumbers.Substring(financeValueChar * 1, 1);
                        chineseUnit = chineseUnits.Substring(i, 1);
                        zeroCount = 0;
                    }
                    else
                    {
                        if (financeValueCharString != "0" && zeroCount == 0)
                        {
                            chineseNumber = chineseNumbers.Substring(financeValueChar * 1, 1);
                            chineseUnit = chineseUnits.Substring(i, 1);
                            zeroCount = 0;
                        }
                        else
                        {
                            if (financeValueCharString == "0" && zeroCount >= 3)
                            {
                                chineseNumber = "";
                                chineseUnit = "";
                                zeroCount = zeroCount + 1;
                            }
                            else
                            {
                                if (financeValueLength >= 11)
                                {
                                    chineseNumber = "";
                                    zeroCount = zeroCount + 1;
                                }
                                else
                                {
                                    chineseNumber = "";
                                    chineseUnit = chineseUnits.Substring(i, 1);
                                    zeroCount = zeroCount + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (financeValueLength - 11) || i == (financeValueLength - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    chineseUnit = chineseUnits.Substring(i, 1);
                }
                chineseDisplayValue = chineseDisplayValue + chineseNumber + chineseUnit;

                if (i == financeValueLength - 1 && financeValueCharString == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    chineseDisplayValue = chineseDisplayValue + '整';
                }
            }
            if (financeValue == 0)
            {
                chineseDisplayValue = "零元整";
            }
            return chineseDisplayValue;
        }

        /// <summary> 
        /// 获取财物数据的人民币表示形式
        /// </summary> 
        /// <param name="financeValue">金额</param> 
        /// <returns>返回大写形式</returns> 
        public string GetChineseDisplayValue2(decimal financeValue)
        {
            string s = financeValue.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            string value = Regex.Replace(d, ".", delegate(Match m)
            { return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
            return value;
        }
    }
}
