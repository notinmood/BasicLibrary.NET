using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore;
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
    /// （产品等）预定数据实体逻辑类
    /// </summary>
    public class ForeOrderBLL : BaseBLL<ForeOrderBLL, ForeOrderEntity, ForeOrderDAL>
    {
        public override bool Create(ForeOrderEntity model)
        {
            bool isSuccessful = base.Create(model);

            RecordOperateLog(model, null, string.Format("创建企业预定信息{0}", isSuccessful == true ? "成功" : "失败"));
            return isSuccessful;
        }

        public override bool Update(ForeOrderEntity model)
        {
            ForeOrderEntity originalModel = Get(model.ForeOrderGuid, true);
            bool isSuccessful = base.Update(model);

            RecordOperateLog(model, originalModel, string.Format("修改企业预定信息{0}", isSuccessful == true ? "成功" : "失败"));
            return isSuccessful;
        }

        private static void RecordOperateLog(ForeOrderEntity newModel, ForeOrderEntity originalModel, string logTitle)
        {
            if (Config.IsRecordOperateLog == true)
            {
                try
                {
                    OperateLogEntity logEntity = new OperateLogEntity();
                    logEntity.CanUsable = Logics.True;
                    logEntity.LogCategory = newModel.ForeOrderCategory;
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
                    logEntity.RelativeKey = newModel.OwnerKey;
                    logEntity.RelativeName = newModel.OwnerName;
                    logEntity.LogTitle = logTitle;

                    if (originalModel != null)
                    {
                        List<string> compareResult = new List<string>();
                        string[] excludePropertyArray = new string[]{"LastUpdateUserKey",
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
