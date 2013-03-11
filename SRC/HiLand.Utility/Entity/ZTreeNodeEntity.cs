using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Entity
{
    /// <summary>
    /// zTree树节点的实体
    /// </summary>
    public class ZTreeNodeEntity
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public string pId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 节点是否为打开状态
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// 节点前是否出现选择框
        /// </summary>
        public bool nocheck { get; set; }

        private string _addonData = string.Empty;
        /// <summary>
        /// 节点上的附加信息
        /// </summary>
        public string addonData
        {
            get { return this._addonData; }
            set { this._addonData = value; }
        }
    }
}
