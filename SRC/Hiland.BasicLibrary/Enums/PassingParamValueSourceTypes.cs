using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 参数值来源类型（数据传递的方式）
    /// </summary>
    public enum PassingParamValueSourceTypes
    {
        Form=0,
        QueryString=1,
        Cookie=2,
        Session=3,
        Application=4,
        ViewState=5,
        ServerVariables=6,
        Database=9,
        Other=10,
    }
}
