using System;
using System.Collections.Generic;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.FoundationLayer
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TEntity">具体的实体类型</typeparam>
    public abstract class BaseModel<TEntity> : IModel, IModelExtensible where TEntity : BaseModel<TEntity>,new()
    {
        /// <summary>
        /// 实体的名称
        /// </summary>
        public virtual string ModelName
        {
            get
            {
                Type type = typeof(TEntity);
                return type.Name;
            }
        }

        private string[] businessKeyNames;
        /// <summary>
        /// 实体的业务主键（区别与数据库的物理主键）
        /// </summary>
        /// <remarks>
        /// 缺省为反射实现。但可以在具体派生类里面重写，以提高性能。
        /// </remarks>
        public virtual string[] BusinessKeyNames
        {
            get
            {
                if (businessKeyNames == null || businessKeyNames.Length==0)
                {
                    List<string> businessKeyNameList = PropertyInfoWithDBFieldAttributeCollection.GetBusinessPrimaryKeyNames<TEntity>();

                    if (businessKeyNameList != null)
                    {
                        businessKeyNames = businessKeyNameList.ToArray();
                    }
                }

                return businessKeyNames;
            }
        }

        private string[] businessKeyValues;
        /// <summary>
        /// 实体的业务主键的值
        /// </summary>
        /// <returns></returns>
        public virtual string[] BusinessKeyValues
        {
            get
            {
                if (businessKeyValues == null || businessKeyValues.Length == 0)
                {
                    List<string> businessKeyValueList =PropertyInfoWithDBFieldAttributeCollection.GetBusinessPrimaryKeyValues<TEntity>(this as TEntity);
                    
                    if(businessKeyValueList!=null)
                    {
                        businessKeyValues = businessKeyValueList.ToArray();
                    }
                }

                return businessKeyValues;
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

        /// <summary>
        /// 实体克隆
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 对方法MemberwiseClone的简单暴漏
        /// </remarks>
        public TEntity Clone()
        {
            return this.MemberwiseClone() as TEntity;
        }

        /// <summary>
        /// 获取实体的JSON表述
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonHelper.Serialize(this);
        }


        #region 空实例信息
        private static TEntity empty = null;
        /// <summary>
        /// 空实体信息
        /// </summary>
        public static TEntity Empty
        {
            get
            {
                if (empty == null)
                {
                    empty = new TEntity();
                    empty.isEmpty = true;
                }

                return empty;
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

        /// <summary>
        /// 将当前实例强制设置为空对象（二次开发中请勿直接使用）
        /// </summary>
        public void ForceSetEmpty()
        {
            this.isEmpty = true;
        }
        #endregion

        

        private ExtentiblePropertyRepository extensiableRepository = null;
        /// <summary>
        /// 扩展属性记录库
        /// </summary>
        public ExtentiblePropertyRepository ExtensiableRepository
        {
            get
            {
                if (extensiableRepository == null)
                {
                    extensiableRepository = new ExtentiblePropertyRepository(this.PropertyNames,this.PropertyValues);
                }

                return extensiableRepository;
            }
        }
    }
}
