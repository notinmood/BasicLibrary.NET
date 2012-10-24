using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 随机信息辅助类
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRandomString()
        {
            return GetRandomString(CharCategories.NumberAndCharIgnoreCase, 5);
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="charCount">随机字符串的长度</param>
        /// <param name="charCategory">字符种类序列</param>
        /// <returns></returns>
        public static string GetRandomString(CharCategories charCategory,int charCount)
        {
            string chociableString = string.Empty;
            switch (charCategory)
            {
                case CharCategories.Number:
                    chociableString = "0,1,2,3,4,5,6,7,8,9";
                    break;
                case CharCategories.NumberAndCharIgnoreCase:
                    chociableString = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
                    break;
                case CharCategories.NumberAndChar:
                default:
                    chociableString = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
                    break;
            }

            string[] chociableArray = chociableString.Split(new Char[] { ',' });
            string result = "";
            Random rnd = new Random();
            for (int i = 0; i < charCount; i++)
            {
                int j = rnd.Next(chociableArray.Length);//随机数不能大于数组的长度
                result += chociableArray[j].ToString();
            }
            return result;
        }

        /// <summary>
        /// 根据规则，获取不重复的随机字符串
        /// </summary>
        /// <param name="charCount">随机字符串的长度</param>
        /// <param name="charCategory">字符种类序列</param>
        /// <param name="uniquePredicate">不重复的规则算法（唯一返回true；否则返回false）</param>
        /// <returns></returns>
        public static string GetUniqueRandomString(CharCategories charCategory, int charCount, Predicate<string> uniquePredicate)
        { 
            return GetUniqueRandomString(charCategory,  charCount,string.Empty, uniquePredicate);
        }

        /// <summary>
        /// 根据规则，获取不重复的随机字符串
        /// </summary>
        /// <param name="charCount">随机字符串的长度</param>
        /// <param name="charCategory">字符种类序列</param>
        /// <param name="prefixer">返回字符串的前缀(其长度不包括在charCount内)</param>
        /// <param name="uniquePredicate">不重复的规则算法（唯一返回true；否则返回false）</param>
        /// <returns></returns>
        public static string GetUniqueRandomString(CharCategories charCategory, int charCount,string prefixer, Predicate<string> uniquePredicate)
        {
            string result = string.Empty;
            bool isSuccessful = false;
            do
            {
                result = prefixer + GetRandomString(charCategory, charCount);
                isSuccessful = uniquePredicate(result);
            } while (isSuccessful == false);

            return result;
        }
    }
}
