using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Algorithm.DirtyWordsFilter
{
    /// <summary>
    /// 脏词过滤接口
    /// </summary>
    public interface IWordFilter
    {
        /// <summary>
        /// 添加待过滤的脏词
        /// </summary>
        /// <param name="key"></param>
        void AddKey(string key);
        
        /// <summary>
        /// 是否存在脏词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        bool HasDirtyWord(string text);
        
        /// <summary>
        /// 获取一个脏词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string FindOne(string text);
        
        /// <summary>
        /// 获取所以脏词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        List<string> FindAll(string text);
        
        /// <summary>
        /// 替换脏词
        /// </summary>
        /// <param name="text"></param>
        /// <returns>替换后的字符串</returns>
        string Replace(string text);

        /// <summary>
        /// 替换脏词
        /// </summary>
        /// <param name="text"></param>
        /// <param name="mask">用于代替非法字符</param>
        /// <returns>替换后的字符串</returns>
        string Replace(string text, char mask);
    }
}
