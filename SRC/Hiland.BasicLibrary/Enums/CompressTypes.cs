using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 数据压缩类型
    /// </summary>
    public enum CompressTypes
    {
        /// <summary>
        /// GZip压缩
        /// </summary>
        GZip,
        
        ///<summary>
        ///Deflate 压缩
        /// </summary>
        Deflate,
        
        ///<summary>
        /// 不支持压缩
        /// </summary>
        None,
    }
}
