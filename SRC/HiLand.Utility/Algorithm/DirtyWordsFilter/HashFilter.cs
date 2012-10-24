using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Algorithm.DirtyWordsFilter
{
    public class HashFilter : IWordFilter
    {
        int m_maxLen; //关键字最大长度
        HashStringSet m_keys = new HashStringSet();

        /// <summary>
        /// 插入新的Key.
        /// </summary>
        /// <param name="name"></param>
        public void AddKey(string key)
        {
            if ((!string.IsNullOrEmpty(key)) && m_keys.Add(key) && key.Length > m_maxLen)
            {
                m_maxLen = key.Length;
            }
        }

        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public bool HasDirtyWord(string text)
        {
            for (int len = 1; len <= text.Length; len++)
            {
                int maxIndex = text.Length - len;
                for (int index = 0; index <= maxIndex; index++)
                {
                    if (m_keys.Contains(text, index, len))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public string FindOne(string text)
        {
            for (int len = 1; len <= text.Length; len++)
            {
                int maxIndex = text.Length - len;
                for (int index = 0; index <= maxIndex; index++)
                {
                    if (m_keys.Contains(text, index, len))
                    {
                        return text.Substring(index, len);
                    }
                }
            }
            return string.Empty;
        }

        //查找所有非法字符
        public List<string> FindAll(string text)
        {
            List<string> result = new List<string>();
            for (int len = 1; len <= text.Length; len++)
            {
                int maxIndex = text.Length - len;
                for (int index = 0; index <= maxIndex; index++)
                {
                    if (m_keys.Contains(text, index, len))
                    {
                        result.Add(text.Substring(index, len));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 替换非法字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="c">用于代替非法字符</param>
        /// <returns>替换后的字符串</returns>
        public string Replace(string text)
        {
            return Replace(text, '*');
        }

        /// <summary>
        /// 替换非法字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="c">用于代替非法字符</param>
        /// <returns>替换后的字符串</returns>
        public string Replace(string text, char c)
        {
            int maxLen = Math.Min(m_maxLen, text.Length);
            for (int len = 1; len <= maxLen; len++)
            {
                int maxIndex = text.Length - len;
                for (int index = 0; index <= maxIndex; index++)
                {
                    if (m_keys.Contains(text, index, len))
                    {
                        string key = text.Substring(index, len);
                        text = text.Replace(key, new string(c, len));
                        index += (len - 1);
                    }
                }
            }
            return text;
        }
    }
}
