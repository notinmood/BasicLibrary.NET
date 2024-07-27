using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.IO
{
    public enum DatePathFormaters
    {
        /// <summary>
        /// 年月日在一起的目录格式,比如 20120303
        /// </summary>
        YMD,
        /// <summary>
        /// 年/月日的格式,比如 2012/0303
        /// </summary>
        Y_MD,
        /// <summary>
        /// 年/月/日的格式,比如 2012/03/03
        /// </summary>
        Y_M_D,
    }
}
