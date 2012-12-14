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
            //RecordOperateLog(model, null, string.Format("创建企业回访信息{0}", isSuccessful == true ? "成功" : "失败"));
            OperateLogBLL.RecordOperateLog(string.Format("创建企业回访信息{0}", isSuccessful == true ? "成功" : "失败"), "EnterpriseTracker", model.TrackerGuid.ToString(), model.RelativeName, model, null);
            return isSuccessful;
        }

        public override bool Update(TrackerEntity model)
        {
            TrackerEntity originalModel = Get(model.TrackerGuid, true);
            bool isSuccessful = base.Update(model);
            //RecordOperateLog(model, originalModel, string.Format("修改企业回访信息{0}", isSuccessful == true ? "成功" : "失败"));
            OperateLogBLL.RecordOperateLog(string.Format("修改企业回访信息{0}", isSuccessful == true ? "成功" : "失败"), "EnterpriseTracker", model.TrackerGuid.ToString(), model.RelativeName, model, originalModel);
            return isSuccessful;
        }
    }
}
