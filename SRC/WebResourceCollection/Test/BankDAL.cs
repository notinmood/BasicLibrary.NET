using System;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;

namespace WebResourceCollection.Test
{
    public class BankDAL : BaseSqlDAL<BankEntity>, IDAL<BankEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public override int GetTotalCount(string whereClause)
        {
            return 20;
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

        public override bool Create(BankEntity model)
        {
            throw new NotImplementedException();
        }

        public override bool Update(BankEntity model)
        {
            throw new NotImplementedException();
        }
    }
}