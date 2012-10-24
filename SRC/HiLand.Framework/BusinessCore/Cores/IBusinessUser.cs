using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 人员实体的接口
    /// </summary>
    public interface IBusinessUser : IUser, IExecutorObject, IModelExtensible
    {
        /// <summary>
        /// 当前实例是否为空对象
        /// </summary>
        bool IsEmpty
        {
            get;
        }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        List<BusinessRole> Roles
        {
            get;
        }

        List<BusinessGroup> Groups
        {
            get;
        }

        /// <summary>
        /// 用户ID号
        /// </summary>
        int UserID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Guid UserGuid
        {
            get;
            set;
        }

        /// <summary>
        /// 用户口令
        /// </summary>
        string Password
        {
            set;
            get;
        }

        /// <summary>
        /// 用户口令加密方式
        /// </summary>
        EncryptTypes PasswordEncrytType
        {
            get;
            set;
        }

        /// <summary>
        /// 用户口令加密盐
        /// </summary>
        string PasswordEncrytSalt
        {
            get;
            set;
        }

        /// <summary>
        /// 向外显示的用户名称信息
        /// </summary>
        /// <remarks>
        /// 向外显示名称信息(推荐)的优先级规则
        /// 1、存在中文名称，就返回中文名称
        /// 2、存在英文名称就返回英文名称
        /// 3、拼合firstName，middleName，lastName等
        /// 4、返回登录名称
        /// </remarks>
        string UserNameDisplay
        {
            get;
        }

        /// <summary>
        /// 中文姓名
        /// </summary>
        string UserNameCN
        {
            set;
            get;
        }

        /// <summary>
        /// 英文名
        /// </summary>
        string UserNameEN
        {
            set;
            get;
        }

        /// <summary>
        /// 用户的First名
        /// </summary>
        string UserNameFirst
        {
            get;
            set;
        }

        /// <summary>
        /// 用户的Last名
        /// </summary>
        string UserNameLast
        {
            get;
            set;
        }

        /// <summary>
        /// 用户的Middle名
        /// </summary>
        string UserNameMiddle
        {
            get;
            set;
        }

        /// <summary>
        /// 将用户的FirstName，MiddleName，LastName拼接后的显示名称
        /// </summary>
        string UserNameJointing
        {
            get;
        }

        /// <summary>
        /// 所属部门ID号
        /// </summary>
        int DepartmentID
        {
            get;
            set;
        }

        /// <summary>
        /// 所属部门GUID号
        /// </summary>
        Guid DepartmentGuid
        {
            get;
            set;
        }

        /// <summary>
        /// 所属部门的系统编号
        /// </summary>
        /// <remarks>
        /// 1.此号通常在部门模块中有系统按照规则自动生成，其主要用于有数据范围权限的情形（即某部门仅能查询操作本部门数据，而不能跨部门进行数据存取）：
        /// 2.各种业务数据中跟部门关联时，亦采用本DepartmentCode信息（与用户一样都关联部门的DepartmentCode信息）
        /// 3.在部门模块中生成DepartmentCode的规则
        ///     3.1各个部门的DepartmentCode不能重复
        ///     3.2部门是有级别的，那么我们的标号也应该有级别与之对应
        ///     3.3同一级别的部门，DepartmentCode的位数应该相同。
        ///     3.4下级部门的DepartmentCode，应包含上级部门的DepartmentCode（即在上级部门DepartmentCode后加入自己部门的Code）。这样便于业务数据按部门过滤（即便于使用在sql中 like 语句）。
        /// </remarks>
        string DepartmentCode
        {
            get;
            set;
        }

        /// <summary>
        /// 部门内的人员类型
        /// </summary>
        DepartmentUserTypes DepartmentUserType
        {
            get;
            set;
        }
        
        /// <summary>
        /// 如果是部门内的用户，那么记录包括部门在内的全路径
        /// </summary>
        /// <remarks>全路径信息的结构类似如下：根部门名称/子部门名称/子子部门名称/.../当前部门名称/用户名称</remarks>
        string UserFullPath
        {
            get;
            set;
        }
        

        /// <summary>
        /// 所属地区的系统编码
        /// </summary>
        /// <remarks>关联系统中地区表</remarks>
        string AreaCode
        {
            get;
            set;
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
        string UserEmail
        {
            set;
            get;
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        UserStatuses UserStatus
        {
            set;
            get;
        }

        MaritalStatuses MaritalStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 备注说明
        /// </summary>
        string UserRemark
        {
            set;
            get;
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        string UserCardID
        {
            set;
            get;
        }

        /// <summary>
        /// 身份证发证机关
        /// </summary>
        string UserCardIDIssued { set; get; }

        /// <summary>
        /// 驾照号码
        /// </summary>
        string DriversLicenceNumber { get; set; }

        /// <summary>
        /// 驾照发证机关
        /// </summary>
        string DriversLicenceNumberIssued { get; set; }

        /// <summary>
        /// 护照号码
        /// </summary>
        string PassportCode { get; set; }

        /// <summary>
        /// 护照发证机关
        /// </summary>
        string PassportCodeIssued { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        Sexes UserSex
        {
            set;
            get;
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        DateTime UserBirthDay
        {
            set;
            get;
        }

        /// <summary>
        /// 手机号
        /// </summary>
        string UserMobileNO
        {
            set;
            get;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        string UserCode
        {
            set;
            get;
        }

        /// <summary>
        /// 批准日期
        /// </summary>
        DateTime UserAgreeDate
        {
            set;
            get;
        }

        /// <summary>
        /// 注册日期
        /// </summary>
        DateTime UserRegisterDate
        {
            set;
            get;
        }

        /// <summary>
        /// 入职日期
        /// </summary>
        DateTime UserWorkStartDate
        {
            set;
            get;
        }

        /// <summary>
        /// 离职日期
        /// </summary>
        DateTime UserWorkEndDate
        {
            set;
            get;
        }

        /// <summary>
        /// 公司邮件地址
        /// </summary>
        string CompanyMail
        {
            set;
            get;
        }

        /// <summary>
        /// 职称
        /// </summary>
        string UserTitle
        {
            set;
            get;
        }

        /// <summary>
        /// 职位
        /// </summary>
        string UserPosition
        {
            set;
            get;
        }


        /// <summary>
        /// 工作电话
        /// </summary>
        string WorkTelphone
        {
            set;
            get;
        }

        /// <summary>
        /// 家中电话
        /// </summary>
        string HomeTelephone
        {
            set;
            get;
        }

        /// <summary>
        /// 用户照片
        /// </summary>
        string UserPhoto
        {
            set;
            get;
        }

        /// <summary>
        /// 锁定机器硬件地址
        /// </summary>
        string UserMacAddress
        {
            set;
            get;
        }

        /// <summary>
        /// 最后访问IP
        /// </summary>
        string UserLastIP
        {
            set;
            get;
        }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        DateTime UserLastDateTime
        {
            set;
            get;
        }

        /// <summary>
        /// 介绍人信息
        /// </summary>
        string BrokerKey
        {
            set;
            get;
        }

        /// <summary>
        /// 其所属企业的信息
        /// </summary>
        string EnterpriseKey
        {
            set;
            get;
        }

        /// <summary>
        /// 用户身高
        /// </summary>
        decimal UserHeight
        {
            set;
            get;
        }

        /// <summary>
        /// 用户体重
        /// </summary>
        decimal UserWeight
        {
            set;
            get;
        }

        /// <summary>
        /// 用户民族
        /// </summary>
        string UserNation
        {
            set;
            get;
        }

        /// <summary>
        /// 用户国籍
        /// </summary>
        string UserCountry
        {
            set;
            get;
        }

        /// <summary>
        /// 用户学历
        /// </summary>
        EducationalBackgrounds UserEducationalBackground
        {
            set;
            get;
        }
    }
}
