using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using HiLand.Utility.Enums.OP;
using HiLand.Utility.Setting;
using HiLand.Utility.Resources;

namespace HiLand.Utility.UI
{
    /// <summary>
    /// 通用的下拉列表控件(不可实例化,作为其他枚举列表控件的基类使用)
    /// </summary>
    /// <typeparam name="T">T必须为枚举类型</typeparam>
    public abstract class DDLCommonControl<T> : DropDownList
    {
        private ListItemCollection items;
        public override ListItemCollection Items
        {
            get
            {
                if (items == null)
                {
                    items = EnumBuilder.BuildItemCollection<T>(displayTextCategory);
                   
                    if (this.isDisplayChoosenItem == true)
                    {
                        string itemText = ResourcesManager.GetValue("PleaseChoose");
                        ListItem choosenItme = new ListItem(itemText, "");
                        items.Insert(0, choosenItme);
                    }
                }
                return items;
            }
        }

        private string displayTextCategory = Config.GetAppSetting("enumDisplayCategory");
        /// <summary>
        /// 显示哪个文本描述类别
        /// </summary>
        public string DisplayTextCategory
        {
            get { return this.displayTextCategory; }
            set { this.displayTextCategory = value; }
        }

        private bool isDisplayChoosenItem = false;
        /// <summary>
        /// 是否显示"请选择..."选项
        /// </summary>
        public bool IsDisplayChoosenItem
        {
            get { return this.isDisplayChoosenItem; }
            set { this.isDisplayChoosenItem = value; }
        }
    }
}
