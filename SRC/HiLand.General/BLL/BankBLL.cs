using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Enums;
using HiLand.General.DALCommon;

namespace HiLand.General.BLL
{
    public class BankBLL : BaseBLL<BankBLL, BankEntity, BankDAL, IBankDAL>
    {
        public override bool Create(BankEntity model)
        {
            bool isSuccessful = base.Create(model);

            if (isSuccessful == true && model.IsPrimary == Logics.True)
            {
                UpdatePrimaryData(model);
            }

            return isSuccessful;
        }

        public override bool Update(BankEntity model)
        {
            bool isSuccessful = base.Update(model);
            if (isSuccessful == true && model.IsPrimary == Logics.True)
            {
                UpdatePrimaryData(model);
            }

            return isSuccessful;
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
