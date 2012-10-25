using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Utility.Enums;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 地区逻辑类
    /// </summary>
    public class AreaBLL : BaseBLL<AreaBLL, AreaEntity, AreaDAL>
    {
        /// <summary>
        /// 通过父级别的地区编码获取子地区列表
        /// </summary>
        /// <param name="parentCode">父级别的地区编码</param>
        /// <returns></returns>
        public List<AreaEntity> GetListByParentCode(string parentCode)
        {
            string whereClause = string.Format(" AreaCode like '{0}%' AND Len(AreaCode)={1} ",parentCode, parentCode.Length + 2);
            return base.GetList(whereClause);
        }
    }
}
