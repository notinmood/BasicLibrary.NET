using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public enum MaritalStatuses
    {
        /// <summary>
        /// 所有
        /// </summary>
        [EnumItemDescription("zh-CN", "所有")]
        All = 0,

        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        UnSet = 5,

        /// <summary>
        /// 未婚
        /// </summary>
        [EnumItemDescription("zh-CN", "未婚")]
        Single = 10,

        /// <summary>
        /// 已婚
        /// </summary>
        [EnumItemDescription("zh-CN", "已婚")]
        Married = 20,

        /// <summary>
        /// 同居
        /// </summary>
        [EnumItemDescription("zh-CN", "同居")]
        Defacto = 30,

        /// <summary>
        /// 分居
        /// </summary>
        [EnumItemDescription("zh-CN", "分居")]
        Seperated = 40,

        /// <summary>
        /// 离婚
        /// </summary>
        [EnumItemDescription("zh-CN", "离婚")]
        Divorced = 50,

        /// <summary>
        /// 失偶
        /// </summary>
        [EnumItemDescription("zh-CN", "失偶")]
        Widowed = 60,
    }
}
