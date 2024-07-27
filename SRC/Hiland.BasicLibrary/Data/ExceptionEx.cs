using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary4.Data
{
    /// <summary>
    /// 异常信息扩展类
    /// </summary>
    public static class ExceptionEx
    {
        /// <summary>
        /// 得到异常的消息串，主要获得了StackTrace属性
        /// </summary>
        /// <param name="e"></param>
        /// <param name="showAllMessage">是否显示所有错误信息</param>
        /// <returns></returns>
        public static string GetExceptionMessage(this Exception e, bool showAllMessage= false)
        {
            return ExceptionHelper.GetExceptionMessage(e,showAllMessage);
        }
    }
}
