using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Hiland.BasicLibrary.Entity
{
    /// <summary>
    /// 表示状态的信息
    /// </summary>
    public class LogicStatusInfo : ILogicStatusInfo
    {
        public LogicStatusInfo(bool isSuccessful=true,string message="")
        {
            this.isSuccessful = isSuccessful;
            this.message = message;
        }

        private bool isSuccessful = true;
        /// <summary>
        /// 成功还是失败的状态
        /// </summary>
        public virtual bool IsSuccessful
        {
            get
            {
                return this.isSuccessful;
            }
            set
            {
                this.isSuccessful = value;
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

        public string ToJSON()
        {
            string result = JsonConvert.SerializeObject(this);
            return result;
        }
    }
}
