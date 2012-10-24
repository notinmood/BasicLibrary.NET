using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using HiLand.Utility.Misc;

namespace HiLand.Framework.BusinessCore
{
    public interface IApplication
    {
        /// <summary>
        /// 系统核心
        /// </summary>
        BusinessKernal Core { get; set; }

        /// <summary>
        /// 插件标示
        /// </summary>
        Guid ApplicationGuid { get; }
        /// <summary>
        /// 插件名称
        /// </summary>
        string ApplicationName { get; }
        /// <summary>
        /// 插件所属分组
        /// </summary>
        string ApplicationGroup { get; }

        /// <summary>
        ///  插件的描述信息
        /// </summary>
        string ApplicationDescription { get; }

        /// <summary>
        /// 是否在菜单上显示
        /// </summary>
        bool IsDisplayMenu { get; }
        /// <summary>
        /// 是否在工具栏显示
        /// </summary>
        bool IsDisplayToolbar { get; }
        
        /// <summary>
        /// 主菜单
        /// </summary>
        MenuInfo MainMenu { get; }

        /// <summary>
        /// 子菜单
        /// </summary>
        List<MenuInfo> SubMenus { get; }

        /// <summary>
        /// 工具栏显示的文本
        /// </summary>
        string ToolbarText { get; }
        /// <summary>
        /// 工具栏显示的图标
        /// </summary>
        Image ToolbarIcon { get; }

        void Load();
        void Run();


        event PluginEventHandler Loading;
        event PluginEventHandler Loaded;
    }
}