using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.General
{
    /// <summary>
    /// 人员规模
    /// </summary>
    public enum StaffScopes
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("staffCount", "未设置")]
        [EnumItemDescription("zh-CN", "未设置")]
        NonSet=0,

        /// <summary>
        /// 微型
        /// </summary>
        [EnumItemDescription("staffCount", "1-10人")]
        [EnumItemDescription("zh-CN", "微型")]
        Little = 10,

        /// <summary>
        /// 小型
        /// </summary>
        [EnumItemDescription("staffCount", "10-50人")]
        [EnumItemDescription("zh-CN", "小型")]
        Small = 50,
        
        /// <summary>
        /// 中型
        /// </summary>
        [EnumItemDescription("staffCount", "50-100人")]
        [EnumItemDescription("zh-CN", "中型")]
        Middle = 100,

        /// <summary>
        /// 大型
        /// </summary>
        [EnumItemDescription("staffCount", "100-300人")]
        [EnumItemDescription("zh-CN", "大型")]
        Big = 300,

        /// <summary>
        /// 非常大型
        /// </summary>
        [EnumItemDescription("staffCount", "300-1000人")]
        [EnumItemDescription("zh-CN", "非常大型")]
        Large = 1000,

        /// <summary>
        /// 巨型
        /// </summary>
        [EnumItemDescription("staffCount", "1000人以上")]
        [EnumItemDescription("zh-CN", "巨型")]
        Huge = 1001,
    }

    /// <summary>
    /// 企业性质
    /// </summary>
    public enum EnterpriseTypes
    {
        /// <summary>
        /// 个体工商户
        /// </summary>
        [EnumItemDescription("zh-CN", "个体工商户")]
        Individual = 10,

        /// <summary>
        /// 私营企业
        /// </summary>
        [EnumItemDescription("zh-CN", "私营企业")]
        SoleTrader = 20,

        /// <summary>
        /// 合伙企业
        /// </summary>
        [EnumItemDescription("zh-CN", "合伙企业")]
        Partnership = 30,

        /// <summary>
        /// 一般公司
        /// </summary>
        [EnumItemDescription("zh-CN", "一般公司")]
        Company = 40,

        /// <summary>
        /// 有限责任公司
        /// </summary>
        [EnumItemDescription("zh-CN", "有限责任公司")]
        CompanyLTD = 50,

        /// <summary>
        /// 外资企业
        /// </summary>
        [EnumItemDescription("zh-CN", "国外企业")]
        CompanyForeign = 60,

        /// <summary>
        /// 股份有限公司
        /// </summary>
        [EnumItemDescription("zh-CN", "股份有限公司")]
        CompanyShareLTD = 70,

        /// <summary>
        /// 三资企业
        /// </summary>
        [EnumItemDescription("zh-CN", "三资企业")]
        CompanyThreeInvestment = 80,

        /// <summary>
        /// 国有企业
        /// </summary>
        [EnumItemDescription("zh-CN", "国有企业")]
        CompanyNation = 90,

        /// <summary>
        /// 集体企业
        /// </summary>
        [EnumItemDescription("zh-CN", "集体企业")]
        CompanyGroup = 100,

        /// <summary>
        /// 政府机构，国家机关
        /// </summary>
        [EnumItemDescription("zh-CN", "国家机关")]
        GovernmentAgency = 110,

        /// <summary>
        /// 社会团体
        /// </summary>
        [EnumItemDescription("zh-CN", "社会团体")]
        SocialOrganization = 120,


        /// <summary>
        /// 教育
        /// </summary>
        [EnumItemDescription("zh-CN", "教育")]
        Eduction = 130,

        /// <summary>
        /// 事业单位
        /// </summary>
        [EnumItemDescription("zh-CN", "事业单位")]
        PublicInstitution = 140,


        /// <summary>
        /// 军队
        /// </summary>
        [EnumItemDescription("zh-CN", "军队")]
        Army = 150,

        /// <summary>
        /// 其他
        /// </summary>
        [EnumItemDescription("zh-CN", "其他")]
        Other = 200,
    }

    /// <summary>
    /// 行业类型
    /// </summary>
    /// <remarks>
    /// 考虑到类型数据的扩展性，可以使用BasicSetting内类型为IndustryType的记录
    /// </remarks>
    public enum IndustryTypes
    {
        /// <summary>
        /// 未设置
        /// </summary>
        NonSet = 0,

        /// <summary>
        /// 机械
        /// </summary>
        Machine = 5,

        /// <summary>
        /// 电子
        /// </summary>
        Electron = 10,

        /// <summary>
        /// 包装
        /// </summary>
        Packege = 15,

        /// <summary>
        /// 食品
        /// </summary>
        Food = 20,

        /// <summary>
        /// 箱包服装
        /// </summary>
        Clothes = 25,


        /// <summary>
        /// 医药
        /// </summary>
        Pharmacy = 30,

        /// <summary>
        /// 工艺品
        /// </summary>
        Arts = 35,

        /// <summary>
        /// 塑料制品
        /// </summary>
        Plastics = 40,

        /// <summary>
        /// 文化体育业
        /// </summary>
        Culture = 45,

        /// <summary>
        /// 印刷业
        /// </summary>
        Print = 50,

        /// <summary>
        /// 餐饮业
        /// </summary>
        Catering = 55,

        /// <summary>
        /// 服务业
        /// </summary>
        Service = 60,

        /// <summary>
        /// 其他行业
        /// </summary>
        Other = 100,
    }

    public enum LoanTypes
    {
        Secured = 0,
        UnSecured = 1,
    }

    /// <summary>
    /// 客户贷款的状态
    /// </summary>
    public enum LoanStatuses
    {
        Deleted = -10,
        UserUnCompleted = 0,
        New = 10,
        //Checking = 20,
        Rejected = 30,
        Approved = 40,
        Blacklisted = 90,
        Collection = 100,
        Completed = 110,
    }

    public enum ScheduleStatuses
    {
        Normal = 0,
        /// <summary>
        /// 本应付记录已经被重新分期到其他新的账期中
        /// </summary>
        ReInstalled = 10,
    }

    public enum ResidentalTypes
    {
        Renting = 10,
        OwnHomeWithoutMortgage = 20,
        OwnHomeWithMortgage = 30,
        Parent = 40,
        Relative = 50,
        Boarding = 60,
    }

    /// <summary>
    /// 提醒类型
    /// </summary>
    public enum RemindCategories
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        None = 0,

        /// <summary>
        /// 员工生日提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "员工生日提醒")]
        BirthdayRemindOfEmployee = 1,

        /// <summary>
        /// 劳务人员生日提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "劳务人员生日提醒")]
        BirthdayRemindOfLabor = 2,

        /// <summary>
        /// 企业用户生日提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "企业用户生日提醒")]
        BirthdayRemindOfEnterpriseStaff = 3,

        /// <summary>
        /// 企业合同到期提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "企业合同到期提醒")]
        ContractRemindOfEnterprise = 11,

        /// <summary>
        /// 劳务人员合同到期提醒
        /// </summary>
        [EnumItemDescription("zh-CN", "劳务人员合同到期提醒")]
        ContractRemindOfLabor = 12,
    }

    /// <summary>
    /// 预定产品或者服务的履行状态
    /// </summary>
    public enum ForeOrderStatuses
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        UnSet=0,

        /// <summary>
        /// 已预订
        /// </summary>
        [EnumItemDescription("zh-CN", "已预订")]
        Fore=1,
        
        /// <summary>
        /// 已兑付
        /// </summary>
        [EnumItemDescription("zh-CN", "已兑付")]
        Excuted=10,
        
        /// <summary>
        /// 已取消
        /// </summary>
        [EnumItemDescription("zh-CN", "已取消")]
        Cancle= 101,
    }
}
