using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.FoundationLayer;
using HiLand.General.Entity;

namespace HiLand.General.DALCommon
{
    public interface IBasicSettingDAL : IDAL<BasicSettingEntity>
    {
        /// <summary>
        /// 根据配置键名称获取实体信息
        /// </summary>
        /// <param name="settingKey">配置键名称</param>
        /// <returns></returns>
        BasicSettingEntity GetBySettingKey(string settingKey);
    }
}
