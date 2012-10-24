using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility4.MVC.SectionHandler
{
    /// <summary>
    /// 权限控制的应用程序
    /// </summary>
    public class PermissionValidateApplication
    {
        private Guid guid = Guid.Empty;
        /// <summary>
        /// 应用程序Guid
        /// </summary>
        public Guid Guid
        {
            get { return this.guid; }
            set { this.guid=value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private bool isVisible = true;
        /// <summary>
        /// 在权限配置页面中是否可见
        /// </summary>
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }
        
        Dictionary<Guid, PermissionValidateModule> modules = new Dictionary<Guid, PermissionValidateModule>();
        /// <summary>
        /// 应用程序内的模块集合
        /// </summary>
        public Dictionary<Guid, PermissionValidateModule> Modules
        {
            get { return this.modules; }
            set { this.modules = value; }
        }
    }

    /// <summary>
    /// 权限控制的模块
    /// </summary>
    public class PermissionValidateModule
    {
        private Guid guid = Guid.Empty;
        /// <summary>
        /// 模块的Guid
        /// </summary>
        public Guid Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 模块的名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private bool isVisible = true;
        /// <summary>
        /// 在权限配置页面中是否可见
        /// </summary>
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        Dictionary<Guid, PermissionValidateSubModule> subModules = new Dictionary<Guid, PermissionValidateSubModule>();
        /// <summary>
        /// 模块内的子模块集合
        /// </summary>
        public Dictionary<Guid, PermissionValidateSubModule> SubModules
        {
            get { return this.subModules; }
            set { this.subModules = value; }
        }
    }

    /// <summary>
    /// 权限控制的子模块
    /// </summary>
    public class PermissionValidateSubModule
    {
        private Guid guid = Guid.Empty;
        /// <summary>
        /// 子模块的Guid
        /// </summary>
        public Guid Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 子模块的名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private bool isVisible = true;
        /// <summary>
        /// 在权限配置页面中是否可见
        /// </summary>
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        Dictionary<string, PermissionValidateOperation> operations = new Dictionary<string, PermissionValidateOperation>();
        /// <summary>
        /// 子模块内的功能操作集合
        /// </summary>
        public Dictionary<string, PermissionValidateOperation> Operations
        {
            get { return this.operations; }
            set { this.operations = value; }
        }
    }

    /// <summary>
    /// 权限控制的功能操作
    /// </summary>
    /// <remarks>
    /// 每个具体操作的描述如下<operation name="List" text="列表" value="2" area="manage" controller="Home" action="List"></operation>
    /// </remarks>
    public class PermissionValidateOperation
    {
        /// <summary>
        /// 权限控制的功能操作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="area"></param>
        public PermissionValidateOperation(string name, string text, int value, string controller, string action, string area = "", bool isVisible= true)
        {
            this.name = name;
            this.text = text;
            this.value = value;
            this.area = area;
            this.controller = controller;
            this.action = action;
            this.isVisible = isVisible;
        }

        private int value = 0;
        /// <summary>
        /// 功能操作的值
        /// </summary>
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
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

        private string action = string.Empty;
        /// <summary>
        /// 功能操作的对应的MVC中Action名称
        /// </summary>
        public string Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        private string name = string.Empty;
        /// <summary>
        /// 功能操作的名称
        /// </summary>
        /// <remarks>通常是几个固定的枚举值，比如Add，Edit等</remarks>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private string text = string.Empty;
        /// <summary>
        /// 功能操作显示的文本
        /// </summary>
        /// <remarks>比如将Add显示为添加，将Edit显示为修改等</remarks>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        private bool isVisible = true;
        /// <summary>
        /// 在权限配置页面中是否可见
        /// </summary>
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }
    }
}
