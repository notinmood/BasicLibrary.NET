using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 数据权限配置项实体-应用程序
    /// </summary>
    public class PermissionDataApplication
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

        Dictionary<Guid, PermissionDataModule> modules = new Dictionary<Guid, PermissionDataModule>();
        /// <summary>
        /// 模块集合
        /// </summary>
        public Dictionary<Guid, PermissionDataModule> Modules
        {
            get { return this.modules; }
            set { this.modules = value; }
        }
    }

    /// <summary>
    /// 数据权限配置项实体-模块
    /// </summary>
    public class PermissionDataModule
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

        Dictionary<Guid, PermissionDataSubModule> subModules = new Dictionary<Guid, PermissionDataSubModule>();
        /// <summary>
        /// 子模块集合
        /// </summary>
        public Dictionary<Guid, PermissionDataSubModule> SubModules
        {
            get { return this.subModules; }
            set { this.subModules = value; }
        }
    }

    /// <summary>
    /// 数据权限配置项实体-子模块
    /// </summary>
    public class PermissionDataSubModule
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

        private string area = string.Empty;
        /// <summary>
        /// 功能操作的对应的MVC中Area的名称
        /// </summary>
        public string Area
        {
            get { return this.area; }
            set { this.area = value; }
        }

        private string controller = string.Empty;
        /// <summary>
        /// 功能操作的对应的MVC中控制器名称
        /// </summary>
        public string Controller
        {
            get { return this.controller; }
            set { this.controller = value; }
        }
    }
}
