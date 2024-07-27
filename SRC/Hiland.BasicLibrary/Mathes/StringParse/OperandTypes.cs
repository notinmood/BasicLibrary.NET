using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Mathes.StringParse
{
     /// <summary>
     /// 操作数类型
     /// </summary>
     public enum OperandTypes
     {
         /// <summary>
         /// 函数
         /// </summary>
         Func = 1,
 
         /// <summary>
         /// 日期
         /// </summary>
         Date = 2,
 
         /// <summary>
         /// 数字
         /// </summary>
         Number = 3,
 
         /// <summary>
         /// 布尔
         /// </summary>
         Boolean = 4,
 
         /// <summary>
         /// 字符串
         /// </summary>
         String = 5,
     }
}
