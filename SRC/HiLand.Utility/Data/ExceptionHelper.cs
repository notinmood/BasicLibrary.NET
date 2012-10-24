using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 异常操作辅助类
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 得到异常的消息串，主要获得了StackTrace属性
        /// </summary>
        /// <param name="e"></param>
        /// <param name="showAllMessage">是否显示所有错误信息</param>
        /// <returns></returns>
        public static string GetExceptionMessage(Exception e)
        {
            return GetExceptionMessage(e, false);
        }

        /// <summary>
        /// 得到异常的消息串，主要获得了StackTrace属性
        /// </summary>
        /// <param name="e"></param>
        /// <param name="showAllMessage">是否显示所有错误信息</param>
        /// <returns></returns>
        public static string GetExceptionMessage(Exception e, bool showAllMessage)
        {
            string result = e.Message;
            if (string.IsNullOrEmpty(e.StackTrace) == false)
            {
                if (showAllMessage == true)
                {
                    result = string.Concat(result, e.StackTrace);
                }
                else
                {
                    string[] stackTraceArray = e.StackTrace.Split(new char[] { '\r', '\n' });
                    List<string> stackTraceList = new List<string>(stackTraceArray);

                    //仅保留调用队列中带文件行号的堆栈行（开发人员自己的代码），舍弃其他微软框架中的逻辑
                    List<string> effectList = new List<string>();
                    string effectString = string.Empty;
                    stackTraceList = stackTraceList.FindAll(s => s.Contains("\\"));
                    stackTraceList.ForEach(currentString =>
                    {
                        if (currentString.Contains("\\") == true)
                        {
                            string currentStringEffect= currentString.Substring(currentString.IndexOf("\\"));
                            effectList.Add(currentStringEffect);
                            effectString += "\r\n" + currentStringEffect;
                        }
                    });

                    result = string.Concat(result, effectString);
                }
            }

            return result;
        }
    }
}
