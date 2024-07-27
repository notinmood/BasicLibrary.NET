using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 权限的类型
    /// </summary>
    [Flags]
    public enum PermissionTypes
    {
        /// <summary>
        /// 查看
        /// </summary>
        View=1,
        /// <summary>
        /// 列表
        /// </summary>
        List = 2,
        /// <summary>
        /// 新增
        /// </summary> 
        New = 4,
        /// <summary>
        /// 修改
        /// </summary>
        Edit = 8,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 16,
        /// <summary>
        /// 排序
        /// </summary>
        Orderby = 32,
        /// <summary>
        /// 打印
        /// </summary>
        Print = 64,
        /// <summary>
        /// 备用A
        /// </summary>
        A = 128,
        /// <summary>
        /// 备用B
        /// </summary>
        B = 256,
        /// <summary>
        /// 权限设置
        /// </summary>
        PermissionSetting = 512,
        /// <summary>
        /// 自定义权限1024
        /// </summary>
        Custom1024 = 1024,
        /// <summary>
        /// 自定义权限2048
        /// </summary>
        Custom2048 = 2048,
        /// <summary>
        /// 自定义权限4096 
        /// </summary>
        Custom4096 = 4096,
        /// <summary>
        /// 自定义权限8192
        /// </summary>
        Custom8192 = 8192,
        /// <summary>
        /// 自定义权限16384
        /// </summary>
        Custom16384 = 16384,
        /// <summary>
        /// 自定义权限32768
        /// </summary>
        Custom32768 = 32768,
        /// <summary>
        /// 自定义权限65536
        /// </summary>
        Custom65536 = 65536,
        /// <summary>
        /// 自定义权限131072
        /// </summary>
        Custom131072 = 131072,
        /// <summary>
        /// 自定义权限262144
        /// </summary>
        Custom262144 = 262144,
        /// <summary>
        /// 自定义权限524288
        /// </summary>
        Custom524288 = 524288,
        /// <summary>
        /// 自定义权限1048576
        /// </summary>
        Custom1048576 = 1048576,
        /// <summary>
        /// 自定义权限2097152
        /// </summary>
        Custom2097152 = 2097152,
        /// <summary>
        /// 自定义权限4194304
        /// </summary>
        Custom4194304 = 4194304,
        /// <summary>
        /// 自定义权限8388608
        /// </summary>
        Custom8388608 = 8388608,
        /// <summary>
        /// 自定义权限16777216
        /// </summary>
        Custom16777216 = 16777216,
        /// <summary>
        /// 自定义权限33554432
        /// </summary>
        Custom33554432 = 33554432,
        /// <summary>
        /// 自定义权限67108864
        /// </summary>
        Custom67108864 = 67108864,
        /// <summary>
        /// 自定义权限134217728
        /// </summary>
        Custom134217728 = 134217728,
        /// <summary>
        /// 自定义权限268435456
        /// </summary>
        Custom268435456 = 268435456,
        /// <summary>
        /// 自定义权限536870912
        /// </summary>
        Custom536870912 = 536870912,

        /// <summary>
        /// 所有权限
        /// </summary>
        ALL= List| New|Edit|Delete| Orderby|Print| A| B|Custom1024|Custom1048576|Custom131072|Custom134217728           
            | Custom16384|Custom16777216|Custom2048|Custom2097152|Custom262144|Custom268435456|Custom32768
            |Custom33554432|Custom4096|Custom4194304|PermissionSetting|Custom524288|Custom536870912|Custom65536
            |Custom67108864|Custom8192|Custom8388608
    }
}
