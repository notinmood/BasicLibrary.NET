using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 树节点可以选择的模式
    /// </summary>
    public enum TreeNodeSelecteModes
    {
        /// <summary>
        /// 根节点允许选择
        /// </summary>
        Root,

        /// <summary>
        /// 树枝节点允许选择
        /// </summary>
        Branch,

        /// <summary>
        /// 树叶节点允许选择
        /// </summary>
        Leaf,

        /// <summary>
        /// 所有节点允许选择
        /// </summary>
        ALL,
    }
}
