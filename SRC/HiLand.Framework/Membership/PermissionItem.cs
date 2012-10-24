using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;
using HiLand.Utility.Data;

namespace HiLand.Framework.Membership
{
    /// <summary>
    /// 权限控制项
    /// </summary>
    public class PermissionItem
    {
        public PermissionItem()
        { 
            
        }

        public PermissionItem(Guid permissionItemGuid, int permissionItemValue)
            : this(permissionItemGuid, permissionItemValue, Guid.Empty, UserTypes.CommonUser, Logics.False)
        {

        }

        public PermissionItem(Guid permissionItemGuid, int permissionItemValue, Guid createUserGuid, UserTypes createUserType, Logics isFreeAwayCreator)
        {
            this.permissionKey = GuidHelper.NewGuid();
            this.permissionItemGuid = permissionItemGuid;
            this.permissionItemValue = permissionItemValue;
            this.createUserGuid = createUserGuid;
            this.createUserType = createUserType;
            this.isFreeAwayCreator = isFreeAwayCreator;
        }

        private Guid permissionKey = Guid.Empty;
        /// <summary>
        /// 权限的名字
        /// </summary>
        public Guid PermissionKey
        {
            get { return this.permissionKey; }
            set { this.permissionKey = value; }
        }

        private Guid permissionItemGuid = Guid.Empty;
        /// <summary>
        /// 权限的键
        /// </summary>
        public Guid PermissionItemGuid
        {
            get { return this.permissionItemGuid; }
            set { this.permissionItemGuid = value; }
        }

        private int permissionItemValue = 0;
        /// <summary>
        /// 权限的值
        /// </summary>
        public int PermissionItemValue
        {
            get { return this.permissionItemValue; }
            set { this.permissionItemValue = value; }
        }

        private Guid createUserGuid = Guid.Empty;
        /// <summary>
        /// 创建人的Guid
        /// </summary>
        public Guid CreateUserGuid
        {
            get { return this.createUserGuid; }
            set { this.createUserGuid = value; }
        }

        private UserTypes createUserType = UserTypes.CommonUser;
        /// <summary>
        /// 创建人的用户类型
        /// </summary>
        public UserTypes CreateUserType
        {
            get { return this.createUserType; }
            set { this.createUserType = value; }
        }

        private Logics isFreeAwayCreator = Logics.False;
        /// <summary>
        /// 此权限是否可以游离于其创建人之外
        /// </summary>
        /// <remarks>
        /// 在权限允许下放的情况下,非系统管理员也可以给其他用户设置权限.但是其可以设置的权限必须自己有的权限,否则不能给其他用户设置.
        /// 在某种情形下,其给某用户设置的的时候,其有某种权限,但是过后其已经没有这种权限了,那么被设置的用户这个权限也应该去掉(具体如何去掉
        /// 在不同的系统中可以可以不同实现).此字段就是保证在设置人没有某权限的时候,被设置人还可以拥有这个权限.
        /// </remarks>
        public Logics IsFreeAwayCreator
        {
            get { return this.isFreeAwayCreator; }
            set { this.isFreeAwayCreator = value; }
        }
    }
}
