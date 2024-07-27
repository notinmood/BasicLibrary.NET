using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Setting.SectionHandler
{
    /// <summary>
    /// SSL对站点页面的控制模式
    /// </summary>
    public enum ControlModes
    {
        /// <summary>
        /// 仅本部分声明的部分使用ssl,其他部分不使用ssl
        /// </summary>
        OnlyThus,
        /// <summary>
        /// 允许其他部分使用ssl(亦可不使用)(缺省)
        /// </summary>
        AllowOther,
    }

    /// <summary>
    /// 部署站点时对页面SSL的模式
    /// </summary>
    public enum DeployModes
    {
        /// <summary>
        /// 来自各个方向的请求使用SSL(缺省)
        /// </summary>
        On,
        /// <summary>
        /// 来自于远程客户的请求使用SSL 网站部署到服务器上使用此属性
        /// </summary>
        RemoteOnly,
        /// <summary>
        /// 本地调试时使用
        /// </summary>
        LocalOnly,
        /// <summary>
        /// SSL不可用
        /// </summary>
        Off,
    }
}
