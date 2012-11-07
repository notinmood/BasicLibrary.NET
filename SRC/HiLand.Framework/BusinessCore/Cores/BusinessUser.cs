using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 人员实体类
    /// </summary>
    public class BusinessUser : IBusinessUser, IExecutorObject, IModelExtensible
    {
        private static BusinessUser empty = null;
        public static BusinessUser Empty
        {
            get
            {
                if (empty == null)
                {
                    empty = new BusinessUser();
                    empty.isEmpty = true;
                }

                return empty;
            }
        }

        /// <summary>
        /// 实体克隆
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 对方法MemberwiseClone的简单暴漏
        /// </remarks>
        public BusinessUser Clone()
        {
            return this.MemberwiseClone() as BusinessUser;
        }

        private bool rolesGetted = false;
        private List<BusinessRole> roles = new List<BusinessRole>();
        /// <summary>
        /// 用户所属角色
        /// </summary>
        public virtual List<BusinessRole> Roles
        {
            get
            {
                if (this.rolesGetted == false)
                {
                    this.roles = BusinessUserBLL.GetUserRoles(this.userGuid);
                    this.rolesGetted = true;
                }

                return this.roles;
            }
        }

        private List<BusinessGroup> groups = new List<BusinessGroup>();
        //TODO:xieran20120831 需要获取用户所属组的逻辑
        /// <summary>
        /// 用户所属组
        /// </summary>
        public virtual List<BusinessGroup> Groups
        {
            get { return this.groups; }
        }

        private bool permissionItemsGetted = false;
        private Dictionary<Guid, PermissionItem> permissionItems = new Dictionary<Guid, PermissionItem>();
        /// <summary>
        /// 用户的权限集合用户的权限集合(包括操作权限，包括数据权限)
        /// </summary>
        /// <remarks>
        /// 用户的权限来自于用户所属的角色用户组部门以及自身的允许和拒绝权限，因此获取用户的权限时，
        /// 就是将这些权限进行综合计算，获取用户最后的权限
        /// </remarks>
        public virtual Dictionary<Guid, PermissionItem> PermissionItems
        {
            get
            {
                if (this.permissionItemsGetted == false)
                {
                    //1.所属角色的权限
                    for (int i = 0; i < this.Roles.Count; i++)
                    {
                        if (this.Roles[i].CanUsable == Logics.False)
                        {
                            continue;
                        }

                        foreach (KeyValuePair<Guid, PermissionItem> kvp in this.Roles[i].PermissionItems)
                        {
                            if (this.permissionItems.ContainsKey(kvp.Key))
                            {
                                this.permissionItems[kvp.Key].PermissionItemValue |= kvp.Value.PermissionItemValue;
                            }
                            else
                            {
                                this.permissionItems.Add(kvp.Key, kvp.Value);
                            }
                        }
                    }

                    //2.用户自身的允许权限
                    foreach (KeyValuePair<Guid, PermissionItem> kvp in this.PermissionItemsSelfAllow)
                    {
                        if (this.permissionItems.ContainsKey(kvp.Key))
                        {
                            this.permissionItems[kvp.Key].PermissionItemValue |= kvp.Value.PermissionItemValue;
                        }
                        else
                        {
                            this.permissionItems.Add(kvp.Key, kvp.Value);
                        }
                    }

                    //3.用户所在部门的权限
                    if (this.Department != null && this.Department.IsEmpty == false)
                    {
                        foreach (KeyValuePair<Guid, PermissionItem> kvp in this.Department.PermissionItems)
                        {
                            if (this.permissionItems.ContainsKey(kvp.Key))
                            {
                                this.permissionItems[kvp.Key].PermissionItemValue |= kvp.Value.PermissionItemValue;
                            }
                            else
                            {
                                this.permissionItems.Add(kvp.Key, kvp.Value);
                            }
                        }
                    }

                    //4.用户组的权限

                    //5.用户自身的拒绝权限
                    foreach (KeyValuePair<Guid, PermissionItem> kvp in this.PermissionItemsSelfDeny)
                    {
                        if (this.permissionItems.ContainsKey(kvp.Key))
                        {
                            this.permissionItems[kvp.Key].PermissionItemValue &= (~kvp.Value.PermissionItemValue);
                        }
                    }

                    this.permissionItemsGetted = true;
                }
                return this.permissionItems;
            }
        }

        private bool permissionItemsSelfAllowGetted = false;
        private Dictionary<Guid, PermissionItem> permissionItemsSelfAllow = new Dictionary<Guid, PermissionItem>();
        /// <summary>
        /// 用户自身的允许权限
        /// </summary>
        public virtual Dictionary<Guid, PermissionItem> PermissionItemsSelfAllow
        {
            get
            {
                if (this.permissionItemsSelfAllowGetted == false)
                {
                    List<BusinessPermission> permissionList = BusinessPermissionBLL.Instance.GetPermissions(this.userGuid.ToString(), PermissionModes.Allow);
                    if (permissionList != null)
                    {
                        foreach (BusinessPermission item in permissionList)
                        {
                            permissionItemsSelfAllow[item.PermissionItemGuid] = item;
                        }
                    }

                    this.permissionItemsSelfAllowGetted = true;
                }

                return this.permissionItemsSelfAllow;
            }
        }

        private bool permissionItemsSelfDenyGetted = false;
        private Dictionary<Guid, PermissionItem> permissionItemsSelfDeny = new Dictionary<Guid, PermissionItem>();
        /// <summary>
        /// 用户自身的拒绝权限
        /// </summary>
        public virtual Dictionary<Guid, PermissionItem> PermissionItemsSelfDeny
        {
            get
            {
                if (this.permissionItemsSelfDenyGetted == false)
                {
                    List<BusinessPermission> permissionList = BusinessPermissionBLL.Instance.GetPermissions(this.userGuid.ToString(), PermissionModes.Deny);
                    if (permissionList != null)
                    {
                        foreach (BusinessPermission item in permissionList)
                        {
                            permissionItemsSelfDeny[item.PermissionItemGuid] = item;
                        }
                    }

                    this.permissionItemsSelfDenyGetted = true;
                }

                return this.permissionItemsSelfDeny;
            }
        }

        private string userName = string.Empty;
        public virtual string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        private int userID = 0;
        /// <summary>
        /// 用户ID号
        /// </summary>
        public virtual int UserID
        {
            set { this.userID = value; }
            get { return this.userID; }
        }

        private Guid userGuid = Guid.Empty;
        public virtual Guid UserGuid
        {
            get
            {
                return this.userGuid;
            }
            set
            {
                this.userGuid = value;
            }
        }

        private Guid userTempGuid = Guid.Empty;
        /// <summary>
        /// 未登录用户为其分配一个临时ID，用于记录用户的行为信息
        /// </summary>
        public virtual Guid UserTempGuid
        {
            get
            {
                return this.userTempGuid;
            }
            internal set
            {
                this.userTempGuid = value;
            }
        }


        private string userCode = string.Empty;
        public virtual string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
            }
        }

        private string password = string.Empty;
        public virtual string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        private EncryptTypes passwordEncrytType = EncryptTypes.UnSet;
        public virtual EncryptTypes PasswordEncrytType
        {
            get
            {
                return this.passwordEncrytType;
            }
            set
            {
                this.passwordEncrytType = value;
            }
        }

        private string passwordEncrytSalt = string.Empty;
        public virtual string PasswordEncrytSalt
        {
            get
            {
                return this.passwordEncrytSalt;
            }
            set
            {
                this.passwordEncrytSalt = value;
            }
        }

        /// <summary>
        /// 向外显示的用户名称信息
        /// </summary>
        /// <remarks>
        /// 向外显示名称信息的优先级规则
        /// 1、存在中文名称，就返回中文名称
        /// 2、存在英文名称就返回英文名称
        /// 3、拼合firstName，middleName，lastName等
        /// 4、返回登录名称
        /// </remarks>
        public virtual string UserNameDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(this.userNameCN) == false)
                {
                    return this.userNameCN;
                }

                if (string.IsNullOrEmpty(this.userNameEN) == false)
                {
                    return this.userNameEN;
                }

                if (string.IsNullOrEmpty(this.UserNameJointing) == false)
                {
                    return this.UserNameJointing;
                }

                return this.userName;
            }
        }

        private string userNameCN = string.Empty;
        public virtual string UserNameCN
        {
            get
            {
                return this.userNameCN;
            }
            set
            {
                this.userNameCN = value;
            }
        }

        private string userNameEN = string.Empty;
        public virtual string UserNameEN
        {
            get
            {
                return this.userNameEN;
            }
            set
            {
                this.userNameEN = value;
            }
        }

        private string userNameFirst = string.Empty;
        /// <summary>
        /// 用户的First名
        /// </summary>
        public virtual string UserNameFirst
        {
            get
            {
                return this.userNameFirst;
            }
            set
            {
                this.userNameFirst = value;
            }
        }

        private string userNameLast = string.Empty;
        /// <summary>
        /// 用户的Last名
        /// </summary>
        public virtual string UserNameLast
        {
            get
            {
                return this.userNameLast;
            }
            set
            {
                this.userNameLast = value;
            }
        }

        private string userNameMiddle = string.Empty;
        /// <summary>
        /// 用户的Middle名
        /// </summary>
        public virtual string UserNameMiddle
        {
            get
            {
                return this.userNameMiddle;
            }
            set
            {
                this.userNameMiddle = value;
            }
        }

        /// <summary>
        /// 将用户的FirstName，MiddleName，LastName拼接后的显示名称
        /// </summary>
        public virtual string UserNameJointing
        {
            get
            {
                string result = string.Empty;
                if (string.IsNullOrEmpty(this.userNameFirst) == false)
                {
                    if (string.IsNullOrEmpty(this.userNameLast) == false)
                    {
                        if (string.IsNullOrEmpty(this.userNameMiddle) == false)
                        {
                            result = string.Format("{0} {1} {2}", this.userNameFirst, this.userNameMiddle, this.userNameLast);
                        }
                        else
                        {
                            result = string.Format("{0} {1}", this.userNameFirst, this.userNameLast);
                        }
                    }
                    else
                    {
                        result = this.userNameFirst;
                    }
                }

                return result;
            }
        }


        private int departmentID = 0;
        /// <summary>
        /// 所属部门ID号
        /// </summary>
        public virtual int DepartmentID
        {
            get
            {
                return this.departmentID;
            }
            set
            {
                this.departmentID = value;
            }
        }

        private Guid departmentGuid = Guid.Empty;
        /// <summary>
        /// 所属部门GUID号
        /// </summary>
        public virtual Guid DepartmentGuid
        {
            get
            {
                return this.departmentGuid;
            }
            set
            {
                this.departmentGuid = value;
            }
        }

        private string departmentCode = string.Empty;
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
        public virtual string DepartmentCode
        {
            get
            {
                return this.departmentCode;
            }
            set
            {
                this.departmentCode = value;
            }
        }

        private BusinessDepartment department = BusinessDepartment.Empty;
        /// <summary>
        /// 用户所属的部门
        /// </summary>
        public virtual BusinessDepartment Department
        {
            get
            {
                if (this.department.IsEmpty == true && this.DepartmentGuid != Guid.Empty)
                {
                    this.department = BusinessDepartmentBLL.Instance.Get(this.DepartmentGuid);
                }

                if (this.department.IsEmpty == true && this.DepartmentCode != string.Empty)
                {
                    this.department = BusinessDepartmentBLL.Instance.GetByCode(this.DepartmentCode);
                }

                if (this.department == null)
                {
                    this.department = BusinessDepartment.Empty;
                }

                return this.department;
            }
        }

        private DepartmentUserTypes departmentUserType = DepartmentUserTypes.Staff;
        /// <summary>
        /// 用户在其部门内的人员类型
        /// </summary>
        public virtual DepartmentUserTypes DepartmentUserType
        {
            get { return this.departmentUserType; }
            set { this.departmentUserType = value; }
        }

        private string userFullPath = string.Empty;
        /// <summary>
        /// 如果是部门内的用户，那么记录包括部门在内的全路径
        /// </summary>
        /// <remarks>全路径信息的结构类似如下：根部门名称/子部门名称/子子部门名称/.../当前部门名称/用户名称</remarks>
        public virtual string UserFullPath
        {
            get { return this.userFullPath; }
            set { this.userFullPath = value; }
        }

        private string areaCode = string.Empty;
        /// <summary>
        /// 所属地区的系统编码
        /// </summary>
        /// <remarks>关联系统中地区表</remarks>
        public virtual string AreaCode
        {
            get
            {
                return this.areaCode;
            }
            set
            {
                this.areaCode = value;
            }
        }

        private string userEmail = string.Empty;
        public virtual string UserEmail
        {
            get
            {
                return this.userEmail;
            }
            set
            {
                this.userEmail = value;
            }
        }

        private UserTypes userType = UserTypes.CommonUser;
        public virtual UserTypes UserType
        {
            get
            {
                return this.userType;
            }
            set
            {
                this.userType = value;
            }
        }

        private UserStatuses userStatus = UserStatuses.Normal;
        public virtual UserStatuses UserStatus
        {
            get
            {
                return this.userStatus;
            }
            set
            {
                this.userStatus = value;
            }
        }


        private MaritalStatuses maritalStatus = MaritalStatuses.UnSet;
        public virtual MaritalStatuses MaritalStatus
        {
            get
            {
                return this.maritalStatus;
            }
            set
            {
                this.maritalStatus = value;
            }
        }


        private string userRemark = string.Empty;
        public virtual string UserRemark
        {
            get
            {
                return this.userRemark;
            }
            set
            {
                this.userRemark = value;
            }
        }

        private string userCardID = string.Empty;
        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string UserCardID
        {
            get
            {
                return this.userCardID;
            }
            set
            {
                this.userCardID = value;
            }
        }

        private string userCardIDIssued = string.Empty;
        /// <summary>
        /// 身份证发证机关
        /// </summary>
        public virtual string UserCardIDIssued
        {
            get
            {
                return this.userCardIDIssued;
            }
            set
            {
                this.userCardIDIssued = value;
            }
        }

        private string driversLicenceNumber = string.Empty;
        /// <summary>
        /// 驾照号码
        /// </summary>
        public virtual string DriversLicenceNumber
        {
            get
            {
                return this.driversLicenceNumber;
            }
            set
            {
                this.driversLicenceNumber = value;
            }
        }

        private string driversLicenceNumberIssued = string.Empty;
        /// <summary>
        /// 驾照发证机关
        /// </summary>
        public virtual string DriversLicenceNumberIssued
        {
            get
            {
                return this.driversLicenceNumberIssued;
            }
            set
            {
                this.driversLicenceNumberIssued = value;
            }
        }

        private string passportCode = string.Empty;
        /// <summary>
        /// 护照号码
        /// </summary>
        public virtual string PassportCode
        {
            get
            {
                return this.passportCode;
            }
            set
            {
                this.passportCode = value;
            }
        }

        private string passportCodeIssued = string.Empty;
        /// <summary>
        /// 护照发证机关
        /// </summary>
        public virtual string PassportCodeIssued
        {
            get
            {
                return this.passportCodeIssued;
            }
            set
            {
                this.passportCodeIssued = value;
            }
        }

        private Sexes userSex = Sexes.UnSet;
        public virtual Sexes UserSex
        {
            get
            {
                return this.userSex;
            }
            set
            {
                this.userSex = value;
            }
        }

        /// <summary>
        /// 用户的年龄
        /// </summary>
        /// <remarks>
        /// 用户的年龄通过生日计算得来；如果未设置生日，年龄将返回0；
        /// </remarks>
        public virtual int UserAge
        {
            get 
            {
                if (this.userBirthDay == DateTimeHelper.Min)
                {
                    return 0;
                }
                else
                {
                    return DateTime.Today.Year - this.userBirthDay.Year;
                }
            }
        }

        private DateTime userBirthDay = DateTimeHelper.Min;
        /// <summary>
        /// 用户的生日
        /// </summary>
        public virtual DateTime UserBirthDay
        {
            get
            {
                return this.userBirthDay;
            }
            set
            {
                this.userBirthDay = value;
            }
        }

        private string userMobileNO = string.Empty;
        public virtual string UserMobileNO
        {
            get
            {
                return this.userMobileNO;
            }
            set
            {
                this.userMobileNO = value;
            }
        }



        private DateTime userAgreeDate = DateTimeHelper.Min;
        /// <summary>
        /// 批准日期
        /// </summary>
        public virtual DateTime UserAgreeDate
        {
            get
            {
                return this.userAgreeDate;
            }
            set
            {
                this.userAgreeDate = value;
            }
        }

        private DateTime userRegisterDate = DateTimeHelper.Min;
        /// <summary>
        /// 注册日期
        /// </summary>
        public virtual DateTime UserRegisterDate
        {
            get
            {
                return this.userRegisterDate;
            }
            set
            {
                this.userRegisterDate = value;
            }
        }

        private DateTime userWorkStarDate = DateTimeHelper.Min;
        public virtual DateTime UserWorkStartDate
        {
            get
            {
                return this.userWorkStarDate;
            }
            set
            {
                this.userWorkStarDate = value;
            }
        }

        private DateTime userWordEndDate = DateTimeHelper.Min;
        public virtual DateTime UserWorkEndDate
        {
            get
            {
                return this.userWordEndDate;
            }
            set
            {
                this.userWordEndDate = value;
            }
        }

        private string companyMail = string.Empty;
        public virtual string CompanyMail
        {
            get
            {
                return this.companyMail;
            }
            set
            {
                this.companyMail = value;
            }
        }

        private string userTitle = string.Empty;
        public virtual string UserTitle
        {
            get
            {
                return this.userTitle;
            }
            set
            {
                this.userTitle = value;
            }
        }


        private string userPosition = string.Empty;
        public virtual string UserPosition
        {
            get
            {
                return this.userPosition;
            }
            set
            {
                this.userPosition = value;
            }
        }

        private string workTelephone = string.Empty;
        public virtual string WorkTelphone
        {
            get
            {
                return this.workTelephone;
            }
            set
            {
                this.workTelephone = value;
            }
        }

        private string homeTelephone = string.Empty;
        public virtual string HomeTelephone
        {
            get
            {
                return this.homeTelephone;
            }
            set
            {
                this.homeTelephone = value;
            }
        }

        private string userPhoto = string.Empty;
        public virtual string UserPhoto
        {
            get
            {
                return this.userPhoto;
            }
            set
            {
                this.userPhoto = value;
            }
        }

        private string userMacAddress = string.Empty;
        public virtual string UserMacAddress
        {
            get
            {
                return this.userMacAddress;
            }
            set
            {
                this.userMacAddress = value;
            }
        }

        private string userLastIP = string.Empty;
        public virtual string UserLastIP
        {
            get
            {
                return this.userLastIP;
            }
            set
            {
                this.userLastIP = value;
            }
        }

        private DateTime userLastDateTime = DateTimeHelper.Min;
        public virtual DateTime UserLastDateTime
        {
            get
            {
                return this.userLastDateTime;
            }
            set
            {
                this.userLastDateTime = value;
            }
        }

        private string brokerKey = string.Empty;
        /// <summary>
        /// 介绍人信息
        /// </summary>
        public virtual string BrokerKey
        {
            get
            {
                return this.brokerKey;
            }
            set
            {
                this.brokerKey = value;
            }
        }

        private string enterpriseKey = string.Empty;
        /// <summary>
        /// 其所属企业的信息
        /// </summary>
        public virtual string EnterpriseKey
        {
            get
            {
                return this.enterpriseKey;
            }
            set
            {
                this.enterpriseKey = value;
            }
        }

        private decimal userHeight;
        /// <summary>
        /// 用户身高
        /// </summary>
        public virtual decimal UserHeight
        {
            get
            {
                return this.userHeight;
            }
            set
            {
                this.userHeight = value;
            }
        }


        /// <summary>
        /// 用户体重
        /// </summary>
        private decimal userWeight;
        /// <summary>
        /// 用户身高
        /// </summary>
        public virtual decimal UserWeight
        {
            get
            {
                return this.userWeight;
            }
            set
            {
                this.userWeight = value;
            }
        }

        private string userNation = string.Empty;
        /// <summary>
        /// 用户民族
        /// </summary>
        public virtual string UserNation
        {
            get
            {
                return this.userNation;
            }
            set
            {
                this.userNation = value;
            }
        }


        private string userCountry = string.Empty;
        /// <summary>
        /// 用户国籍
        /// </summary>
        public virtual string UserCountry
        {
            get
            {
                return this.userCountry;
            }
            set
            {
                this.userCountry = value;
            }
        }


        private EducationalBackgrounds userEducationalBackground = EducationalBackgrounds.NoSetting;
        /// <summary>
        /// 用户学历
        /// </summary>
        public virtual EducationalBackgrounds UserEducationalBackground
        {
            get
            {
                return this.userEducationalBackground;
            }
            set
            {
                this.userEducationalBackground = value;
            }
        }

        protected bool isEmpty = false;
        /// <summary>
        /// 当前实例是否为空对象
        /// </summary>
        public bool IsEmpty
        {
            get { return this.isEmpty; }
        }

        private string propertyName = string.Empty;
        /// <summary>
        /// 扩展属性的名字
        /// </summary>
        public virtual string PropertyNames
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        private string propertyValue = string.Empty;
        /// <summary>
        /// 扩展属性的值
        /// </summary>
        public virtual string PropertyValues
        {
            get { return this.propertyValue; }
            set { this.propertyValue = value; }
        }


        private ExtentiblePropertyRepository extensiableRepository = null;
        public ExtentiblePropertyRepository ExtensiableRepository
        {
            get
            {
                if (extensiableRepository == null)
                {
                    extensiableRepository = new ExtentiblePropertyRepository(this.PropertyNames, this.PropertyValues);
                }

                return extensiableRepository;
            }
        }

        #region IExecuterObject 主体、客体行为对象接口
        /// <summary>
        /// 主体、客体行为对象的Guid
        /// </summary>
        public Guid ExecutorGuid
        {
            get { return this.userGuid; }
        }

        /// <summary>
        /// 主体、客体行为对象的名称
        /// </summary>
        public string ExecutorName
        {
            get { return this.userName; }
        }

        /// <summary>
        /// 主体、客体行为对象的类型
        /// </summary>
        public ExecuterTypes ExecutorType
        {
            get { return ExecuterTypes.User; }
        }
        #endregion
    }
}
