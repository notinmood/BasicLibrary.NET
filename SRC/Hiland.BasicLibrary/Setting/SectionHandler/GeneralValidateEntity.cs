using System;
using System.Collections.Generic;

namespace HiLand.Utility.Setting.SectionHandler
{
    public class GeneralValidateApplication
    {
        private Guid guid = Guid.Empty;
        /// <summary>
        /// Guid信息
        /// </summary>
        public Guid Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 名称信息
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        
        Dictionary<Guid, GeneralValidateModule> modules = new Dictionary<Guid, GeneralValidateModule>();

        public Dictionary<Guid, GeneralValidateModule> Modules
        {
            get { return this.modules; }
            set { this.modules = value; }
        }
    }

    public class GeneralValidateModule
    {
        private Guid guid = Guid.Empty;
        /// <summary>
        /// Guid信息
        /// </summary>
        public Guid Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 名称信息
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        Dictionary<Guid, GeneralValidateSubModule> subModules = new Dictionary<Guid, GeneralValidateSubModule>();

        public Dictionary<Guid, GeneralValidateSubModule> SubModules
        {
            get { return this.subModules; }
            set { this.subModules = value; }
        }
    }

    public class GeneralValidateSubModule
    {
        private Guid guid = Guid.Empty;
        /// <summary>
        /// Guid信息
        /// </summary>
        public Guid Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 名称信息
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        Dictionary<string, string> pages = new Dictionary<string, string>();
        Dictionary<string, GeneralValidateOperation> operations = new Dictionary<string, GeneralValidateOperation>();

        public Dictionary<string, string> Pages
        {
            get { return this.pages; }
            set { this.pages = value; }
        }

        public Dictionary<string, GeneralValidateOperation> Operations
        {
            get { return this.operations; }
            set { this.operations = value; }
        }
    }

    public class GeneralValidateOperation
    {
        public GeneralValidateOperation(string operationName, string operationText, int operationValue)
        {
            this.operationName = operationName;
            this.operationText = operationText;
            this.operationValue = operationValue;
        }

        private int operationValue = 0;
        /// <summary>
        /// 功能操作的值
        /// </summary>
        public int OperationValue
        {
            get { return this.operationValue; }
            set { this.operationValue = value; }
        }

        private string operationName = string.Empty;
        /// <summary>
        /// 功能操作的名称
        /// </summary>
        /// <remarks>通常是几个固定的枚举值，比如Add，Edit等</remarks>
        public string OperationName
        {
            get { return this.operationName; }
            set { this.operationName = value; }
        }

        private string operationText = string.Empty;
        /// <summary>
        /// 功能操作显示的文本
        /// </summary>
        /// <remarks>比如将Add显示为添加，将Edit显示为修改等</remarks>
        public string OperationText
        {
            get { return this.operationText; }
            set { this.operationText = value; }
        }
    }
}
