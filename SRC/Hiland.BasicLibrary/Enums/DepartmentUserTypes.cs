using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 部门内的人员职责类型
    /// </summary>
    public enum DepartmentUserTypes
    {
        /// <summary>
        /// 部门负责人
        /// </summary>
        [EnumItemDescription("zh-CN", "负责人")]
        Leader= 1,

        /// <summary>
        /// 部门副负责人
        /// </summary>
         [EnumItemDescription("zh-CN", "副负责人")]
        ViceLeader=2,

        /// <summary>
        /// 秘书助手
        /// </summary>
         [EnumItemDescription("zh-CN", "秘书助手")]
        Assist=4,

        /// <summary>
        /// 一般员工
        /// </summary>
         [EnumItemDescription("zh-CN", "一般员工")]
        Staff=8,
    }
}
