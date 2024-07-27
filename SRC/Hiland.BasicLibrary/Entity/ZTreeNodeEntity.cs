using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Entity
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
    }
}
