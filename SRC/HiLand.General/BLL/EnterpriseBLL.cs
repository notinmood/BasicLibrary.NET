using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 企业业务逻辑类
    /// </summary>
    public class EnterpriseBLL : BaseBLL<EnterpriseBLL, EnterpriseEntity, EnterpriseDAL>
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool Create(EnterpriseEntity model)
        {
            model = ConfirmTrimSpaceInCompanyName(model);
            bool result = base.Create(model);

            RecordOperateLog(model, null,string.Format("创建企业{0}", result == true ? "成功" : "失败"));
            return result;
        }

       
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entityOperateStatus"></param>
        /// <returns></returns>
        public EnterpriseEntity Create(EnterpriseEntity model, out EntityOperateStatuses entityOperateStatus)
        {
            model = ConfirmTrimSpaceInCompanyName(model);
            bool isExistCompanyName = IsExistEnterpriseName(model);
            if (isExistCompanyName == true)
            {
                entityOperateStatus = EntityOperateStatuses.FailureDuplicateName;
            }
            else
            {
                bool isSuccessful = Create(model);
                if (isSuccessful == true)
                {
                    entityOperateStatus = EntityOperateStatuses.Successful;
                }
                else
                {
                    entityOperateStatus = EntityOperateStatuses.FailureUnknowReason;
                }
            }

            return model;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool Update(EnterpriseEntity model)
        {
            model = ConfirmTrimSpaceInCompanyName(model);
            model = ConfirmLockEnterprise(model);
            EnterpriseEntity originalModel = EnterpriseBLL.Instance.Get(model.EnterpriseGuid, true);
            bool result= base.Update(model);
            RecordOperateLog(model, originalModel, string.Format("修改企业{0}", result == true ? "成功" : "失败"));
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entityOperateStatus"></param>
        /// <returns></returns>
        public EnterpriseEntity Update(EnterpriseEntity model, out EntityOperateStatuses entityOperateStatus)
        {
            model = ConfirmTrimSpaceInCompanyName(model);
            EnterpriseEntity originalModel = Get(model.EnterpriseGuid, true);
            bool isExistCompanyName = IsExistEnterpriseName(model, originalModel.CompanyName);
            if (isExistCompanyName == true)
            {
                entityOperateStatus = EntityOperateStatuses.FailureDuplicateName;
            }
            else
            {
                model = ConfirmLockEnterprise(model);
                bool isSuccessful = Update(model);
                if (isSuccessful == true)
                {
                    entityOperateStatus = EntityOperateStatuses.Successful;
                }
                else
                {
                    entityOperateStatus = EntityOperateStatuses.FailureUnknowReason;
                }
            }

            return model;
        }

        /// <summary>
        /// 系统内是否存在相同的企业名称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsExistEnterpriseName(EnterpriseEntity model)
        {
            return IsExistEnterpriseName(model, string.Empty);
        }

        /// <summary>
        /// 系统内是否存在相同的企业名称
        /// </summary>
        /// <param name="model"></param>
        /// <param name="excluedName">不进行验证的名称</param>
        /// <returns></returns>
        public bool IsExistEnterpriseName(EnterpriseEntity model, string excluedName)
        {
            model = ConfirmTrimSpaceInCompanyName(model);

            string whereClause = string.Format(" CompanyName='{0}' ", model.CompanyName);
            if (string.IsNullOrEmpty(excluedName) == false)
            {
                if (model.CompanyName == excluedName)
                {
                    whereClause = " 1=2 ";
                }
                else
                {
                    whereClause += string.Format(" AND CompanyName !='{0}' ", excluedName);
                }
            }
            List<EnterpriseEntity> enterpriseList = base.GetList(whereClause);
            if (enterpriseList == null || enterpriseList.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 确保去除企业名称中的空格
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private EnterpriseEntity ConfirmTrimSpaceInCompanyName(EnterpriseEntity model)
        {
            bool isTrimSpaceInCompanyName = Config.GetAppSetting<bool>("isTrimSpaceInCompanyName", true);
            if (isTrimSpaceInCompanyName == true)
            {
                model.CompanyName = model.CompanyName.Replace(" ", "");
            }

            return model;
        }

        /// <summary>
        /// 确保锁定资源
        /// </summary>
        /// <param name="model"></param>
        private EnterpriseEntity ConfirmLockEnterprise(EnterpriseEntity model)
        {
            bool isProtectedEnterpriseWhenUpdate = Config.GetAppSetting<bool>("isProtectedEnterpriseWhenUpdate", true);
            if (isProtectedEnterpriseWhenUpdate == true)
            {
                model.IsProtectedByOwner = Logics.True;
            }

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enterpriseGuid"></param>
        /// <returns></returns>
        public bool ReleaseEnterprise(Guid enterpriseGuid)
        {
            EnterpriseEntity model = Get(enterpriseGuid);
            model.IsProtectedByOwner = Logics.False;
            model.CreateUserKey = "";
            model.CreateUserName = "共享";
            return base.Update(model);
        }

        private static void RecordOperateLog(EnterpriseEntity newModel, EnterpriseEntity originalModel, string logTitle)
        {
            if (Config.IsRecordOperateLog == true)
            {
                try
                {
                    OperateLogEntity logEntity = new OperateLogEntity();
                    logEntity.CanUsable = Logics.True;
                    logEntity.LogCategory = "Enterprise";
                    logEntity.LogDate = DateTime.Now;
                    if (originalModel == null)
                    {
                        logEntity.LogOperateName = OperateTypes.Create.ToString();
                    }
                    else
                    {
                        logEntity.LogOperateName = OperateTypes.Update.ToString();
                    }
                    logEntity.LogStatus = (int)Logics.True;
                    logEntity.LogType = 0;
                    logEntity.LogUserKey = BusinessUserBLL.CurrentUserGuid.ToString();
                    logEntity.LogUserName = BusinessUserBLL.CurrentUser.UserNameDisplay;
                    logEntity.RelativeKey = newModel.EnterpriseGuid.ToString();
                    logEntity.RelativeName = newModel.CompanyName;
                    logEntity.LogTitle = logTitle;

                    if (originalModel != null)
                    {
                        List<string> compareResult= new List<string>();
                        string[] excludePropertyArray= new string[]{"LastUpdateUserKey",
                            "LastUpdateUserName",
                            "LastUpdateDate",
                            "PropertyNames",
                            "PropertyValues"
                        };
                        EntityHelper.Compare(originalModel, newModel, out compareResult, excludePropertyArray);
                        if (compareResult != null && compareResult.Count > 0)
                        {
                            logEntity.LogMessage = CollectionHelper.Concat(";", compareResult as IEnumerable<String>);
                        }
                        else
                        {
                            logEntity.LogMessage = "没有修改任何信息";
                        }
                    }

                    OperateLogBLL.Instance.Create(logEntity);
                }
                catch
                {
                }
            }
        }
    }
}
