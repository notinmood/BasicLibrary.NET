using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using HiLand.Utility.Setting;
using HiLand.Utility.Setting.SectionHandler;
using System.Configuration;
using HiLand.Utility.Web;

namespace HiLand.Framework.Permission.Module
{
    public class PathRoleLoginValidateModule : IHttpModule
    {
        private HttpApplication context;
        #region IHttpModule 成员

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            this.context = context;
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            PathRoleValidateConfig config = (PathRoleValidateConfig)ConfigurationManager.GetSection("pathRoleValidate");
            if (config == null)
            {
                config = (PathRoleValidateConfig)ConfigurationManager.GetSection("permissionValidate/pathRoleValidate");
            }

            if (config != null)
            {
                bool isSuccessful = true;
                foreach ( KeyValuePair<string,PathRoleValidateEntity> kvp in config.ValidateEntities)
                {
                   isSuccessful= PermissionValidation.PathRoleValidate(kvp.Value.PathToValidate, kvp.Value.RolesToValidate.ToArray());
                   if (isSuccessful == false)
                   {
                       break;
                   }
                }

                if (isSuccessful == false)
                {
                    PermissionValidation.RedirectToLoginPage();
                }
            }
        }

        #endregion
    }
}
