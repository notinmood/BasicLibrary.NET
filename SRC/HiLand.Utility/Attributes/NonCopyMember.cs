using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Attributes
{
    /// <summary>
    /// 类型的非拷贝成员
    /// </summary>
    /// <remarks>
    /// 两个（同类型）实体之间拷贝数据的时候，不传递的成员需要标注此特性
    /// </remarks>
    public class NonCopyMember : Attribute
    {
    }
}
