using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 加密类型
    /// </summary>
    public enum EncryptTypes
    {
        /// <summary>
        /// 未设置
        /// </summary>
        UnSet=-1,
        /// <summary>
        /// 不加密口令
        /// </summary>
        NoEncrypt=0,
        /// <summary>
        /// hash方式加密
        /// </summary>
        HashEncrypt=1,
        /// <summary>
        /// MD5方式加密
        /// </summary>
        MD5Encrypt=2,
    }
}
