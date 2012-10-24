using System;
using System.Configuration;
using System.Web;
using HiLand.Utility.Setting.SectionHandler;
using HiLand.Utility.Web;

namespace HiLand.Framework.Permission.Module
{
    public class GeneralLoginValidateModule : IHttpModule
    {
        protected HttpApplication context;

        public void Dispose()
        {
            //
        }

        public void Init(HttpApplication context)
        {
            this.context = context;
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }

        protected virtual void context_AuthenticateRequest(object sender, EventArgs e)
        {
            GeneralValidateConfig config = (GeneralValidateConfig)ConfigurationManager.GetSection("generalValidate");
            if (config == null)
            {
                config = (GeneralValidateConfig)ConfigurationManager.GetSection("permissionValidate/generalValidate");
            }

            if (config != null)
            {
                bool isSuccessful = PermissionValidation.GeneralPageValidate(); 

                if (isSuccessful == false)
                {
                    PermissionValidation.RedirectToNoPermissionPage();
                }
            }
        }
    }
}
