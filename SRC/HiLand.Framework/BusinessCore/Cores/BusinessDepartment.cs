using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class BusinessDepartment : BaseModel<BusinessDepartment>, IBusinessDepartment, IExecutorObject
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "DepartmentGuid" }; }
        }

        #region 实体信息

        private int departmentID;
        public int DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        private Guid departmentGuid = Guid.Empty;
        public Guid DepartmentGuid
        {
            get { return departmentGuid; }
            set { departmentGuid = value; }
        }

        private string departmentName = String.Empty;
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        private string departmentNameShort = String.Empty;
        public string DepartmentNameShort
        {
            get { return departmentNameShort; }
            set { departmentNameShort = value; }
        }

        private string departmentDescription = String.Empty;
        public string DepartmentDescription
        {
            get { return departmentDescription; }
            set { departmentDescription = value; }
        }

        private string departmentFullPath = String.Empty;
        /// <summary>
        /// 部门的全路径信息
        /// </summary>
        /// <remarks>
        /// 全路径信息的结构类似如下：根部门名称/子部门名称/子子部门名称/.../当前部门名称
        /// </remarks>
        public string DepartmentFullPath
        {
            get { return departmentFullPath; }
            set { departmentFullPath = value; }
        }
        

        private string departmentCode = String.Empty;
        public string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; }
        }

        private Guid departmentParentGuid = Guid.Empty;
        public Guid DepartmentParentGuid
        {
            get { return departmentParentGuid; }
            set { departmentParentGuid = value; }
        }

        private BusinessDepartment departmentParent = null;
        /// <summary>
        /// 父部门信息
        /// </summary>
        public BusinessDepartment DepartmentParent
        {
            get 
            {
                if (this.departmentParent == null)
                {
                    this.departmentParent = BusinessDepartmentBLL.Instance.Get(this.departmentParentGuid);
                }
                return this.departmentParent;
            }
        }

        private DepartmentTypes departmentType= DepartmentTypes.CommonDepartment;
        public DepartmentTypes DepartmentType
        {
            get { return departmentType; }
            set { departmentType = value; }
        }

        private Logics departmentIsSpecial= Logics.False;
        public Logics DepartmentIsSpecial
        {
            get { return departmentIsSpecial; }
            set { departmentIsSpecial = value; }
        }

        private Logics canUsable= Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }
        #endregion

        #region 权限信息
        private bool permissionItemsGetted = false;
        private Dictionary<Guid, PermissionItem> permissionItems = new Dictionary<Guid, PermissionItem>();
        /// <summary>
        /// 权限集合
        /// </summary>
        /// <remarks>TODO:xieran 需要考虑多级部门的情形，目前只实现了直接部门的权限</remarks>
        public virtual Dictionary<Guid, PermissionItem> PermissionItems
        {
            get
            {
                if (this.permissionItemsGetted == false)
                {
                    List<BusinessPermission> permissionList = BusinessPermissionBLL.Instance.GetPermissions(this.DepartmentGuid.ToString(), PermissionModes.Allow);
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
        #endregion

        #region IExecuterObject 主体、客体行为对象接口
        /// <summary>
        /// 主体、客体行为对象的Guid
        /// </summary>
        public Guid ExecutorGuid
        {
            get { return this.departmentGuid; }
        }

        /// <summary>
        /// 主体、客体行为对象的名称
        /// </summary>
        public string ExecutorName
        {
            get { return this.departmentName; }
        }

        /// <summary>
        /// 主体、客体行为对象的类型
        /// </summary>
        public ExecuterTypes ExecutorType
        {
            get { return ExecuterTypes.Department; }
        }
        #endregion

        #region 其他
        private bool departmentLevelCalculated = false;
        private int departmentLevel = 0;
        /// <summary>
        /// 部门的层级数
        /// </summary>
        /// <remarks>
        /// 1、这个值通过计算得到，不保存在数据库内
        /// 2、其中頂級部門的層級數為0
        /// </remarks>
        public int DepartmentLevel
        {
            get
            {
                if (this.departmentLevelCalculated == false)
                {
                    this.departmentLevel = (this.departmentCode.Length / 2) -1;
                }
                return this.departmentLevel;
            }
        }

        
        #endregion
    }
}
