using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Misc
{
    public struct MenuInfo
    {
        public MenuInfo(string menuText, string menuFile)
        {
            this.menuFile = menuFile;
            this.menuText = menuText;
        }

        private string menuText;
        /// <summary>
        /// 菜单显示的文本
        /// </summary>
        public string MenuText
        {
            get { return this.menuText; }
            set { this.menuText = value; }
        }

        private string menuFile;
        /// <summary>
        /// 菜单对应的文件
        /// </summary>
        /// <remarks>
        /// 点击菜单是要打开的文件
        /// </remarks>
        public string MenuFile
        {
            get { return this.menuFile; }
            set { this.menuFile = value; }
        }
    }
}
