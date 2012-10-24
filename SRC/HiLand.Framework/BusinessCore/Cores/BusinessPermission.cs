using System;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 带归属者的权限控制项实体类（即当前权限项属于哪个所有者）
    /// </summary>
    public class BusinessPermission : PermissionItem, IModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessPermission()
        { 
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionItem"></param>
        public BusinessPermission(PermissionItem permissionItem)
        {
            this.PermissionKey = permissionItem.PermissionKey;
            this.PermissionItemGuid = permissionItem.PermissionItemGuid;
            this.PermissionItemValue = permissionItem.PermissionItemValue;
            this.CreateUserGuid = permissionItem.CreateUserGuid;
            this.CreateUserType = permissionItem.CreateUserType;
            this.IsFreeAwayCreator = permissionItem.IsFreeAwayCreator;
        }

        private string ownerKey = String.Empty;
        /// <summary>
        /// 权限所有者的键值（可以是权限所有者的id，guid，code等）
        /// </summary>
        public string OwnerKey
        {
            get { return ownerKey; }
            set { ownerKey = value; }
        }

        private ExecuterTypes ownerType;
        /// <summary>
        /// 权限所有者的类型
        /// </summary>
        public ExecuterTypes OwnerType
        {
            get { return ownerType; }
            set { ownerType = value; }
        }

        private PermissionModes permissionMode= PermissionModes.Allow;
        /// <summary>
        /// 权限模式
        /// </summary>
        public PermissionModes PermissionMode
        {
            get { return permissionMode; }
            set { permissionMode = value; }
        }

        private PermissionKinds permissionKind = PermissionKinds.Operating;
        /// <summary>
        /// 权限控制类型
        /// </summary>
        public PermissionKinds PermissionKind
        {
            get { return permissionKind; }
            set { permissionKind = value; }
        }

        public string ModelName
        {
            get { return "BusinessPermission"; }
        }

        public string[] BusinessKeyNames
        {
            get { return new string[] { "PermissionKey" }; }
        }

        public string[] BusinessKeyValues
        {
            get { return new string[] { this.PermissionKey.ToString() }; }
        }

        private string propertyName = string.Empty;
        /// <summary>
        /// 扩展属性的名字
        /// </summary>
        /// <remarks>目前未实行</remarks>
        public virtual string PropertyNames
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        private string propertyValue = string.Empty;
        /// <summary>
        /// 扩展属性的值
        /// </summary>
        /// <remarks>目前未实行</remarks>
        public virtual string PropertyValues
        {
            get { return this.propertyValue; }
            set { this.propertyValue = value; }
        }

        protected bool isEmpty = false;
        /// <summary>
        /// 当前实例是否为空对象
        /// </summary>
        public bool IsEmpty
        {
            get { return this.isEmpty; }
        }

        /// <summary>
        /// 将当前实例强制设置为空对象（二次开发中请勿直接使用）
        /// </summary>
        public void ForceSetEmpty()
        {
            this.isEmpty = true;
        }
    }
}
