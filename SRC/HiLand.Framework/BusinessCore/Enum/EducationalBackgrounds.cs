using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Framework.BusinessCore.Enum
{
    /// <summary>
    /// 教育背景
    /// </summary>
    public enum EducationalBackgrounds
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        NoSetting=0,

        /// <summary>
        /// 未读书
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        NoSchool=1,
        
        /// <summary>
        /// 小学
        /// </summary>
        [EnumItemDescription("zh-CN", "小学")]
        PrimarySchool=2,
        
        /// <summary>
        /// 初中
        /// </summary>
        [EnumItemDescription("zh-CN", "初中")]
        JuniorSchool=3,
        
        /// <summary>
        /// 高中
        /// </summary>
        [EnumItemDescription("zh-CN", "高中")]
        SeniorSchool=4,

        /// <summary>
        /// 中专
        /// </summary>
        [EnumItemDescription("zh-CN", "中专")]
        SpecialSchool = 5,

        /// <summary>
        /// 专科
        /// </summary>
        [EnumItemDescription("zh-CN", "专科")]
        JuniorCollege =7,

        /// <summary>
        ///  本科
        /// </summary>
        [EnumItemDescription("zh-CN", "本科")]
        RegularCollege =8,

        /// <summary>
        /// 研究生
        /// </summary>
        [EnumItemDescription("zh-CN", "研究生")]
        Graduate=9,

        /// <summary>
        /// 博士
        /// </summary>
        [EnumItemDescription("zh-CN", "博士")]
        Doctor=10,

        /// <summary>
        /// 博士后
        /// </summary>
        [EnumItemDescription("zh-CN", "博士后")]
        PostDoctor=11,

        /// <summary>
        /// 其他
        /// </summary>
        [EnumItemDescription("zh-CN", "其他")]
        Other = 99,
    }
}
