using System;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.General.DALCommon;

namespace WebResourceCollection.Test
{
    public class LoanBasicDAL : BaseSqlDAL<LoanBasicEntity>, ILoanBasicDAL
    {
        public int GetCountTest()
        {
            return 98;
        }

        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        protected override string[] KeyNames
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Guid主键名称
        /// </summary>
        protected override string GuidKeyName
        {
            get { throw new NotImplementedException(); }
        }

        protected override string PagingSPName
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Create(LoanBasicEntity model)
        {
            throw new NotImplementedException();
        }

        public override bool Update(LoanBasicEntity model)
        {
            throw new NotImplementedException();
        }

        public bool UpdataReadInfo(Guid loanGuid, Guid readUserID, DateTime readDate)
        {
            throw new NotImplementedException();
        }
    }
}