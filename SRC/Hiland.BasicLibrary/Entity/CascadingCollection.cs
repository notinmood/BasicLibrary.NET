using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Data;

namespace HiLand.Utility.Entity
{
    /// <summary>
    /// 配合jquery.cascadingDropDown.js使用的选择项集合
    /// </summary>
    public class CascadingCollection
    {
        private List<CascadingItem> _data = new List<CascadingItem>();
        public List<CascadingItem> data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public int count
        {
            get { return _data.Count; }
        }

        public string success = "true";
        public string error = string.Empty;

        /// <summary>
        /// 添加选择项目
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void AddItem(string text, string value)
        {
            CascadingItem entity = new CascadingItem();
            entity.text = text;
            entity.value = value;
            this.data.Add(entity);
        }

        /// <summary>
        /// 生成jquery.cascadingDropDown.js需要的JSON字符串
        /// </summary>
        /// <returns></returns>
        public string GetJSON()
        {
           return JsonHelper.Serialize(this);
        }

        public class CascadingItem
        {
            private string _value = string.Empty;
            public string value
            {
                get { return this._value; }
                set { this._value = value; }
            }

            private string _text = string.Empty;
            public string text
            {
                get { return this._text; }
                set { this._text = value; }
            }
        }
    }
}
