//using System;
//using System.Collections.Generic;
//using System.Text;
//using Hiland.BasicLibrary.Enums;

//namespace Hiland.BasicLibrary.Reflection
//{
//    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
//    public sealed class ExtendKeyAttribute : Attribute,IDBFieldAttribute
//    {
//        public ExtendKeyAttribute()
//        {
//        }

//        private FieldExtendModes extendMode = FieldExtendModes.SelfTable;
//        /// <summary>
//        /// 扩展信息的存储方式
//        /// </summary>
//        public FieldExtendModes ExtendMode
//        {
//            get { return this.extendMode; }
//            set { this.extendMode = value; }
//        }


//        private string fieldName = string.Empty;
//        /// <summary>
//        /// 对应的数据库字段的名字
//        /// </summary>
//        public string FieldName
//        {
//            get
//            {
//                return this.fieldName;
//            }
//            set
//            {
//                this.fieldName = value;
//            }
//        }
//    }
//}
