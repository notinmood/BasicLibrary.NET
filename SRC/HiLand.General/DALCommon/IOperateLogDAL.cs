using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;

namespace HiLand.General.DALCommon
{
    public interface IOperateLogDAL : IDAL<OperateLogEntity>
    {
        /// <summary>
        /// 获取所有的类别
        /// </summary>
        /// <returns></returns>
        List<string> GetCategoryList();
    }
}
