using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.BusinessCore;
using System.Drawing;
using HiLand.Utility.Misc;

namespace HiLand.Framework.Plugins
{
    [ApplicationAttribute(Index=0,Tag="",Author="HilandTech",Webpage="",Name="",IsLoadWhenStart=true)]
    public class Application : IApplication
    {
        private BusinessKernal core;
        public virtual BusinessKernal Core
        {
            get { return this.core; }
            set { this.core = value; }
        }

        private Guid applicationGuid = Guid.Empty;
        /// <summary>
        /// 插件的标识符（在插件应用中必须重写）
        /// </summary>
        /// <remarks>
        /// 1.在插件应用中必须重写
        /// 2.同一个插件的不同版本，请使用同一个标识符，这样系统加载的时候新版插件能够自动替换旧版插件
        /// </remarks>
        public virtual Guid ApplicationGuid { get { return this.applicationGuid; } }

        private string applicationName = string.Empty;
        /// <summary>
        /// 插件名称
        /// </summary>
        public virtual string ApplicationName { get { return this.applicationName; } }

        private string applicationGroup = string.Empty;
        /// <summary>
        /// 插件所属分组
        /// </summary>
        public virtual string ApplicationGroup { get { return this.applicationGroup; } }

        private string applicationDescription = string.Empty;
        /// <summary>
        ///  插件的描述信息
        /// </summary>
        public virtual string ApplicationDescription { get { return this.applicationDescription; } }

        private bool isDisplayMenu = true;
        /// <summary>
        /// 是否在菜单上显示
        /// </summary>
        public virtual bool IsDisplayMenu { get { return this.isDisplayMenu; } }

        private bool isDisplayToolbar = true;
        /// <summary>
        /// 是否在工具栏显示
        /// </summary>
        public virtual bool IsDisplayToolbar { get { return this.isDisplayToolbar; } }

        private MenuInfo mainMenu;
        /// <summary>
        /// 主菜单
        /// </summary>
        public virtual MenuInfo MainMenu { get { return this.mainMenu; } }

        private List<MenuInfo> subMenus;
        /// <summary>
        /// 子菜单
        /// </summary>
        public virtual List<MenuInfo> SubMenus
        {
            get { return this.subMenus; }
        }

        private string toolbarText = string.Empty;
        /// <summary>
        /// 工具栏显示的文本
        /// </summary>
        public virtual string ToolbarText { get { return this.toolbarText; } }

        private Image toolbarIcon = null;
        /// <summary>
        /// 工具栏显示的图标
        /// </summary>
        public virtual Image ToolbarIcon { get { return this.toolbarIcon; } }

        public virtual void Load()
        {
            //1.载入前先执行Loading事件
            this.OnLoading();

            //2.执行正常业务逻辑

            //3.载入后执行Loaded事件
            this.OnLoaded();
        }

        public virtual void Run()
        {
            //throw new NotImplementedException();
        }

        public virtual void OnLoading()
        {
            if (this.Loading != null)
            {
                Loading(this, new PluginEventArgs());
            }
        }

        public virtual void OnLoaded()
        {
            if (this.Loaded != null)
            {
                Loaded(this, new PluginEventArgs());
            }
        }

        public event PluginEventHandler Loading;

        public event PluginEventHandler Loaded;
    }
}
