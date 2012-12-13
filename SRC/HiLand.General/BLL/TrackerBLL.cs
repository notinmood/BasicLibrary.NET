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
    /// 回访、跟踪业务逻辑类
    /// </summary>
    public class TrackerBLL : BaseBLL<TrackerBLL, TrackerEntity, TrackerDAL>
    {
        public override bool Create(TrackerEntity model)
        {
            bool isSuccessful = base.Create(model);
                RecordOperateLog(model, null, string.Format("创建企业回访信息{0}", isSuccessful == true ? "成功" : "失败"));
            return isSuccessful;
        }

        public override bool Update(TrackerEntity model)
        {
            TrackerEntity originalModel = Get(model.TrackerGuid, true);
            bool isSuccessful = base.Update(model);
            RecordOperateLog(model, originalModel, string.Format("修改企业回访信息{0}", isSuccessful == true ? "成功" : "失败"));
            return isSuccessful;
        }


        private static void RecordOperateLog(TrackerEntity newModel, TrackerEntity originalModel, string logTitle)
        {
            if (Config.IsRecordOperateLog == true)
            {
                try
                {
                    OperateLogEntity logEntity = new OperateLogEntity();
                    logEntity.CanUsable = Logics.True;
                    logEntity.LogCategory = "EnterpriseTracker";
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
                    logEntity.RelativeKey = newModel.TrackerGuid.ToString();
                    logEntity.RelativeName = newModel.RelativeName;
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
