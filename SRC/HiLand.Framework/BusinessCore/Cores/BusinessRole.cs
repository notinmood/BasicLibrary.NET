using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class BusinessRole : IBusinessRole, IExecutorObject, IModelExtensible
    {
        public static BusinessRole Empty
        {
            get
            {
                BusinessRole empty = new BusinessRole();
                //ProxyGenerator proxy = new ProxyGenerator();
                //BusinessRole empty = proxy.CreateClassProxy<BusinessRole>(new EmptyObjectInterceptor());
                empty.isEmpty = true;

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
        public BusinessRole Clone()
        {
            return this.MemberwiseClone() as BusinessRole;
        }

        private string roleName = string.Empty;
        public virtual string RoleName
        {
            get { return this.roleName; }
            set { this.roleName = value; }
        }

        private int roleID = 0;
        public virtual int RoleID
        {
            get { return this.roleID; }
            set { this.roleID = value; }
        }


        private Guid roleGuid = Guid.Empty;
        public virtual Guid RoleGuid
        {
            get { return this.roleGuid; }
            set { this.roleGuid = value; }
        }

        private string roleDescrition = String.Empty;
        public virtual string RoleDescrition
        {
            get { return roleDescrition; }
            set { roleDescrition = value; }
        }


        private Logics canUsable = Logics.True;
        /// <summary>
        ///  是否可用
        /// </summary>
        public virtual Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private Logics isInnerRole = Logics.False;
        /// <summary>
        ///  是否为内部角色
        /// </summary>
        public virtual Logics IsInnerRole
        {
            get { return isInnerRole; }
            set { isInnerRole = value; }
        }

        protected bool isEmpty = false;
        public bool IsEmpty
        {
            get { return this.isEmpty; }
        }


        private bool permissionItemsGetted = false;
        private Dictionary<Guid, PermissionItem> permissionItems = new Dictionary<Guid, PermissionItem>();
        /// <summary>
        /// 角色的权限集合
        /// </summary>
        public virtual Dictionary<Guid, PermissionItem> PermissionItems
        {
            get
            {
                if (this.permissionItemsGetted == false)
                {
                    List<BusinessPermission> permissionList = BusinessPermissionBLL.Instance.GetPermissions(this.roleGuid.ToString(), PermissionModes.Allow);
                    if (permissionList != null)
                    {
                        foreach (BusinessPermission item in permissionList)
                        {
                            permissionItems[item.PermissionItemGuid] = item;
                        }
                    }

                    this.permissionItemsGetted = true;
                }

                return this.permissionItems;
            }
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
            get { return this.roleGuid; }
        }

        /// <summary>
        /// 主体、客体行为对象的名称
        /// </summary>
        public string ExecutorName
        {
            get { return this.roleName; }
        }

        /// <summary>
        /// 主体、客体行为对象的类型
        /// </summary>
        public ExecuterTypes ExecutorType
        {
            get { return ExecuterTypes.Role; }
        }
        #endregion
    }
}
