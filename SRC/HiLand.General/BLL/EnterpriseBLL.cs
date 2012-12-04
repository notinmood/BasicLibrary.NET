using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting;
using System.Collections.Generic;

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
            return base.Create(model);
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
            return base.Update(model);
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
            EnterpriseEntity originalModel = Get(model.EnterpriseGuid,true);
            bool isExistCompanyName = IsExistEnterpriseName(model, originalModel.CompanyName);
            if (isExistCompanyName == true)
            {
                entityOperateStatus = EntityOperateStatuses.FailureDuplicateName;
            }
            else
            {
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
            return IsExistEnterpriseName(model,string.Empty);
        }

        /// <summary>
        /// 系统内是否存在相同的企业名称
        /// </summary>
        /// <param name="model"></param>
        /// <param name="excluedName">不进行验证的名称</param>
        /// <returns></returns>
        public bool IsExistEnterpriseName(EnterpriseEntity model,string excluedName)
        {
            model = ConfirmTrimSpaceInCompanyName(model);

            string whereClause = string.Format(" CompanyName='{0}' ", model.CompanyName);
            if (string.IsNullOrEmpty(excluedName)==false)
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
            if(enterpriseList==null || enterpriseList.Count==0)
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
    }
}
