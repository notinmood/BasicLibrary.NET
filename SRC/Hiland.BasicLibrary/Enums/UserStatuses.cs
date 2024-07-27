using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 用户的状态
    /// </summary>
    public enum UserStatuses
    {
        /// <summary>
        /// 注册后未激活
        /// </summary>
        [EnumItemDescription("zh-CN", "未激活")]
        Unactivated =0,
        
        /// <summary>
        /// 正常
        /// </summary>
         [EnumItemDescription("zh-CN", "正常")]
        Normal=1,
        
        /// <summary>
        /// 停用（表示计算机逻辑上的删除）
        /// </summary>
         [EnumItemDescription("zh-CN", "停用")]
        Stopped=2,
        
        /// <summary>
        /// 被删除(表示业务逻辑上的物理删除，实际并未删除)
        /// </summary>
         [EnumItemDescription("zh-CN", "被删除")]
        Deleted=3,
    }
}
