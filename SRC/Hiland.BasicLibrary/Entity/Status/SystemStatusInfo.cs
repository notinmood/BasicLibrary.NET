using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums;

namespace Hiland.BasicLibrary.Entity.Status
{
    /// <summary>
    /// 系统级别的状态信息
    /// </summary>
    public class SystemStatusInfo : ISystemStatusInfo
    {
        public SystemStatusInfo(SystemStatuses systemStatus = SystemStatuses.Success, string message = "")
        {
            this.systemStatus = systemStatus;
            this.message = message;
        }

        private SystemStatuses systemStatus = SystemStatuses.Success;
        /// <summary>
        /// 状态的类型
        /// </summary>
        public virtual SystemStatuses SystemStatus
        {
            get
            {
                return this.systemStatus;
            }
            set
            {
                this.systemStatus= value;
            }
        }

        private string message = string.Empty;
        /// <summary>
        /// 具体要显示的信息
        /// </summary>
        public virtual string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }
    }
}
