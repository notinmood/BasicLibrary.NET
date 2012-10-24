using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 提醒类型
    /// </summary>
    public enum RemindCategories
    {
        /// <summary>
        /// 无
        /// </summary>
        [EnumItemDescription("zh-CN", "无")]
        None = 0,

        /// <summary>
        /// 员工生日提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "员工生日提醒")]
        EmployeeBirthdayRemind = 1,

        /// <summary>
        /// 劳务人员生日提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "劳务人员生日提醒")]
        LaborBirthdayRemind = 2,

        /// <summary>
        /// 企业用户生日提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "企业用户生日提醒")]
        EnterpriseStaffBirthdayRemind = 3,

        /// <summary>
        /// 企业合同到期提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "企业合同到期提醒")]
        EnterpriseContractRemind = 11,

        /// <summary>
        /// 劳务人员合同到期提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "劳务人员合同到期提醒")]
        LaborContractRemind = 12,

    }
}
