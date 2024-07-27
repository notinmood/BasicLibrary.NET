using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Data;

namespace HiLand.Utility.Entity
{
    /// <summary>
    /// 自动完成功能传递的数据实体
    /// </summary>
    public class AutoCompleteEntity
    {
        private string _value = string.Empty;
        /// <summary>
        /// 自动完成提示列表中选定项后，实际填充文本框的值
        /// </summary>
        public string value
        {
            get { return this._value;}
            set { this._value = value; }
        }

        private string _label = string.Empty;
        /// <summary>
        /// 自动完成提示列表中显示的提示内容
        /// </summary>
        public string label
        {
            get { return this._label; }
            set { this._label = value; }
        }

        private string _key = string.Empty;
        /// <summary>
        /// 自动完成提示列表中选定项后，后台对应的实际标志值（通常可以是某实体的ID信息）
        /// </summary>
        public string key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        private string _details = string.Empty;
        /// <summary>
        /// 自动完成提示列表中选定项后，可以传递和展示的其他内容
        /// </summary>
        public string details
        {
            get { return this._details; }
            set { this._details = value; }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public string GetJSON()
        //{
        //    return JsonHelper.Serialize(this);
        //}
    }
}
