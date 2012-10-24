using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Framework.BusinessCore
{
    public class ApplicationAttribute : Attribute
    {
        public ApplicationAttribute() { }
        public ApplicationAttribute(string name, string version, string author, string webpage, bool isLoadWhenStart)
        {
            this.name = name;
            this.version = version;
            this.author = author;
            this.webpage = webpage;
            this.isLoadWhenStart = isLoadWhenStart;
        }


        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public string Version
        {
            get { return version; }
            set { this.version = value; }
        }
        public string Author
        {
            get { return author; }
            set { this.author = value; }
        }
        public string Webpage
        {
            get { return webpage; }
            set { this.webpage = value; }
        }
        public bool IsLoadWhenStart
        {
            get { return isLoadWhenStart; }
            set { this.isLoadWhenStart = value; }
        }
        /// 
        /// 用来存储一些有用的信息 
        /// 
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        /// 
        /// 用来存储序号 
        /// 
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private string name = "";
        private string version = "";
        private string author = "";
        private string webpage = "";
        private object tag = null;
        private int index = 0;
        // 暂时不会用 
        private bool isLoadWhenStart = true;
    }
}
