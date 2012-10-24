using System;
using System.Collections.Generic;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.Membership;
using HiLand.Utility.Serialization;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore
{
    /// <summary>
    /// 组实体类
    /// </summary>
    public class BusinessGroup : IBusinessGroup, IExecutorObject, IModelExtensible
    {
        /// <summary>
        /// 实体克隆
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 对方法MemberwiseClone的简单暴漏
        /// </remarks>
        public BusinessGroup Clone()
        {
            return this.MemberwiseClone() as BusinessGroup;
        }

        private int groupID = 0;
        /// <summary>
        /// 用户组ID
        /// </summary>
        public virtual int GroupID
        {
            get
            {
                return this.groupID;
            }
            set
            {
                this.groupID = value;
            }
        }

        private Guid groupGuid = Guid.Empty;
        /// <summary>
        /// 用户组GUID号
        /// </summary>
        public virtual Guid GroupGuid
        {
            get
            {
                return this.groupGuid;
            }
            set
            {
                this.groupGuid = value;
            }
        }

        private string groupName = string.Empty;
        /// <summary>
        /// 用户组的名称
        /// </summary>
        public virtual string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        #region IExecuterObject 主体、客体行为对象接口
        /// <summary>
        /// 主体、客体行为对象的Guid
        /// </summary>
        public Guid ExecutorGuid
        {
            get { return this.groupGuid; }
        }

        /// <summary>
        /// 主体、客体行为对象的名称
        /// </summary>
        public string ExecutorName
        {
            get { return this.groupName; }
        }

        /// <summary>
        /// 主体、客体行为对象的类型
        /// </summary>
        public ExecuterTypes ExecutorType
        {
            get { return ExecuterTypes.Group; }
        }
        #endregion

        /// <summary>
        /// 用户组的权限集合
        /// </summary>
        public virtual Dictionary<Guid, PermissionItem> PermissionItems
        {
            get;
            set;
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
    }
}
