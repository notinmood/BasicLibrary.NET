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

            OperateLogBLL.RecordOperateLog(string.Format("创建企业预定信息{0}", isSuccessful == true ? "成功" : "失败"), model.ForeOrderCategory, model.OwnerKey, model.OwnerName, model, null);
            return isSuccessful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool Update(ForeOrderEntity model)
        {
            ForeOrderEntity originalModel = Get(model.ForeOrderGuid, true);
            bool isSuccessful = base.Update(model);

            OperateLogBLL.RecordOperateLog(string.Format("修改企业预定信息{0}", isSuccessful == true ? "成功" : "失败"), model.ForeOrderCategory, model.OwnerKey, model.OwnerName, model, originalModel);
            return isSuccessful;
        }
    }
}
