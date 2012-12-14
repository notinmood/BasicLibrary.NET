using System;
using System.Collections.Generic;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.DALCommon;
using HiLand.General.Entity;
using HiLand.Utility.Enums;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 人员在银行的账户信息业务逻辑类
    /// </summary>
    public class BankBLL : BaseBLL<BankBLL, BankEntity, BankDAL, IBankDAL>
    {
        public override bool Create(BankEntity model)
        {
            bool isSuccessful = base.Create(model);

            if (isSuccessful == true && model.IsPrimary == Logics.True)
            {
                UpdatePrimaryData(model);
            }

            OperateLogBLL.RecordOperateLog(string.Format("创建银行账户信息{0}", isSuccessful == true ? "成功" : "失败"), "Bank", model.BankGuid.ToString(), model.User.UserNameDisplay, model, null);
            return isSuccessful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool Update(BankEntity model)
        {
            BankEntity originalModel = Get(model.BankGuid, true);
            bool isSuccessful = base.Update(model);
            if (isSuccessful == true && model.IsPrimary == Logics.True)
            {
                UpdatePrimaryData(model);
            }

            OperateLogBLL.RecordOperateLog(string.Format("修改银行账户信息{0}", isSuccessful == true ? "成功" : "失败"), "Bank", model.BankGuid.ToString(), model.User.UserNameDisplay, model, originalModel);
            return isSuccessful;
        }

        /// <summary>
        /// 获取某用户缺省的银行账户信息
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public BankEntity GetPrimary(Guid userGuid)
        {
            BankEntity bankEntity = BankEntity.Empty;
            string whereClause = string.Format(" UserGuid='{0}' ",userGuid);
            List < BankEntity > list= base.GetList(whereClause);
            if (list != null)
            {
                bankEntity=  list.Find(e=>e.IsPrimary== Logics.True);
            }

            if (bankEntity == null)
            {
                bankEntity = BankEntity.Empty;
            }

            return bankEntity;
        }

        /// <summary>
        /// 将当前用户的其他银行账户的IsPrimary设置为false
        /// </summary>
        /// <param name="model"></param>
        private void UpdatePrimaryData(BankEntity model)
        {
            this.SaveDAL.RemovePrimaryStatusOfUser(model.UserGuid, model.BankGuid);
        }
    }
}
