using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.General.BLL;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Utility.Enums;
using HiLand.Utility.Reflection;

namespace WebApplicationConsole.Membership
{
    public partial class ExtensiblePropertyTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Guid bankGuid = GuidHelper.NewGuid();

            BankEntityExt entity = new BankEntityExt();
            entity.BankGuid = bankGuid;
            entity.AccountName = "aaaaaaaaaaa";
            entity.AccountNumber = "bbbbbbbb";
            //entity.AccountStatus= 
            entity.AreaCode = "ccccccccccccc";
            entity.BankAddress = "dddddddddddddd";
            entity.ExtProteryA = "sssssssssssssss";
            entity.ExtProteryB = 66666666;

            BankEntity entityOriginal = entity as BankEntityExt;
            BankBLL.Instance.Create(entityOriginal);

            BankEntity aa = BankBLL.Instance.Get(bankGuid);
            BankEntityExt bb = aa as BankEntityExt;


            this.Literal1.Text = bb.AccountName + "--------" + bb.ExtProteryA + "---" + bb.ExtProteryB;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Guid userGuid = GuidHelper.NewGuid();
            BusinessUserExt entity = new BusinessUserExt();
            entity.UserGuid = userGuid;
            entity.AreaCode = "aaaaaaaaaaaaa";
            entity.CompanyMail = "bbbbbbbb" + userGuid.ToString();
            entity.DepartmentCode = "ccccccccccc";
            entity.UserName = "dddddddddddd" + userGuid.ToString();
            entity.ExtProteryA = "ssssssssss";
            entity.ExtProteryB = 88888;

            //BusinessUser entityOriginal = entity as BusinessUser;
            CreateUserRoleStatuses status;
            BusinessUserBLL.CreateUser(entity, out status);
            if (status == CreateUserRoleStatuses.Successful)
            {
                BusinessUser aa = BusinessUserBLL.Get(userGuid);
                BusinessUserExt bb = Converter.InheritedEntityConvert<BusinessUser, BusinessUserExt>(aa);
                this.Literal1.Text = bb.UserName + "----" + bb.ExtProteryA + "---" + bb.ExtProteryB;
            }
            else
            {
                this.Literal1.Text = status.ToString();
            }
        }
    }
}