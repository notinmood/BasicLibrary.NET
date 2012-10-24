using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Setting.SectionHandler;
using System.Configuration;
using System.Collections.Specialized;

namespace WebApplicationConsole.GeneralValidateDemo.ModuleA.ModuleA2
{
    public partial class PermissionSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                BindInfor();
            }
        }

        private void BindInfor()
        {
            GeneralValidateConfig config = ConfigurationManager.GetSection("permissionValidate/generalValidate") as GeneralValidateConfig;
            //GeneralValidateConfig.OptimizeStrcture(config);
            this.RepeaterApplication.DataSource = config.Applications;
            this.RepeaterApplication.DataBind();
        }

        protected void RepeaterApplication_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Guid, GeneralValidateApplication>? currentRow = e.Item.DataItem as KeyValuePair<Guid, GeneralValidateApplication>?;
                if (currentRow != null)
                {
                    Literal applicationName = e.Item.FindControl("applicationName") as Literal;
                    if (applicationName != null)
                    {
                        applicationName.Text = currentRow.Value.Value.Name.ToString();
                    }

                    Repeater repeaterModule = e.Item.FindControl("RepeaterModule") as Repeater;
                    if (repeaterModule != null)
                    {
                        repeaterModule.ItemDataBound += new RepeaterItemEventHandler(repeaterModule_ItemDataBound);
                        repeaterModule.DataSource = currentRow.Value.Value.Modules;
                        repeaterModule.DataBind();
                    }
                }
            }
        }

        void repeaterModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Guid, GeneralValidateModule>? currentRow = e.Item.DataItem as KeyValuePair<Guid, GeneralValidateModule>?;
                if (currentRow != null)
                {
                    Literal moduleName = e.Item.FindControl("moduleName") as Literal;
                    if (moduleName != null)
                    {
                        moduleName.Text = currentRow.Value.Value.Name.ToString();
                    }

                    Repeater repeaterSubModule = e.Item.FindControl("RepeaterSubModule") as Repeater;
                    if (repeaterSubModule != null)
                    {
                        repeaterSubModule.ItemDataBound += new RepeaterItemEventHandler(repeaterSubModule_ItemDataBound);
                        repeaterSubModule.DataSource = currentRow.Value.Value.SubModules;
                        repeaterSubModule.DataBind();
                    }
                }
            }
        }

        void repeaterSubModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Guid, GeneralValidateSubModule>? currentRow = e.Item.DataItem as KeyValuePair<Guid, GeneralValidateSubModule>?;
                if (currentRow != null)
                {
                    Literal subModuleName = e.Item.FindControl("subModuleName") as Literal;
                    if (subModuleName != null)
                    {
                        subModuleName.Text = currentRow.Value.Value.Name.ToString();
                    }

                    Literal litDisplayPermissionItem = e.Item.FindControl("litDisplayPermissionItem") as Literal;
                    if (litDisplayPermissionItem != null)
                    {
                        foreach (KeyValuePair<string, GeneralValidateOperation> kvp in currentRow.Value.Value.Operations)
                        {
                            string itemText = kvp.Value.OperationText;
                            string itemValue = currentRow.Value.Key.ToString()+"||"+ kvp.Value.OperationValue.ToString();
                            string item = string.Format("<input id=\"{0}\" type=\"checkbox\" name=\"{0}\" /><label for=\"{0}\">{1}</label>", itemValue, itemText);
                            litDisplayPermissionItem.Text += item;
                        }
                    }

                    //CheckBoxList permissionItems = e.Item.FindControl("permissionItems") as CheckBoxList;
                    //if (permissionItems != null)
                    //{
                    //    foreach (KeyValuePair<string, GeneralValidateAction> kvp in currentRow.Value.Value.Actions)
                    //    {
                    //        ListItem item = new ListItem(kvp.Value.ActionText,"ssss"+kvp.Value.ActionValue.ToString());
                    //        permissionItems.Items.Add(item);
                    //    }
                    //}
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            NameValueCollection nvc = this.Request.Form;
            int i = 9;
            i++;
        }
    }
}