using System.Collections.Generic;
using System.Data;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.DALCommon;
using HiLand.General.Entity;
using HiLand.Utility.Cache;
using HiLand.Utility.Entity;
using HiLand.Utility.Enums;
using HiLand.Utility.Event;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 基础设置业务逻辑类
    /// </summary>
    public class BasicSettingBLL : BaseBLL<BasicSettingBLL, BasicSettingEntity, BasicSettingDAL, IBasicSettingDAL>
    {
        /// <summary>
        /// 配置项变更的事件
        /// </summary>
        public event CommonEventHandle<DataForChange<BasicSettingEntity>> BasicSettingChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool Update(BasicSettingEntity model)
        {
            BasicSettingEntity entity = null;
            if (this.BasicSettingChanged != null)
            {
                entity = BaseBLL<BasicSettingBLL, BasicSettingEntity, BasicSettingDAL, IBasicSettingDAL>.Instance.Get(model.SettingID, true);
            }
            bool flag = base.Update(model);
            if (flag && (this.BasicSettingChanged != null))
            {
                DataForChange<BasicSettingEntity> args = new DataForChange<BasicSettingEntity>
                {
                    NewData = model,
                    OriginalData = entity
                };
                this.BasicSettingChanged(this, args);
            }
            return flag;
        }

        /// <summary>
        ///  获取实体对象
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public virtual BasicSettingEntity GetBySettingKey(string settingKey)
        {
            string cacheKey = GeneralCacheKeys<BasicSettingEntity>.GetEntityBusinessKey("SettingKey", settingKey);
            return CacheHelper.Access<string, BasicSettingEntity>(cacheKey, CacheMintues, SaveDAL.GetBySettingKey, settingKey);
        }

        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public override List<BasicSettingEntity> GetList(string whereClause, params IDbDataParameter[] paras)
        {
            return GetList(false, whereClause, paras);
        }

        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="isDisplayInnerSetting">是否显示内部配置项</param>
        /// <param name="whereClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public List<BasicSettingEntity> GetList(bool isDisplayInnerSetting, string whereClause, params IDbDataParameter[] paras)
        {
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            if (isDisplayInnerSetting == false)
            {
                //没有设置IsInnerSetting属性，或者此属性设置的为非内部配置项，均显示为非内部配置项
                whereClause += string.Format(" AND ([IsInnerSetting] IS NULL OR [IsInnerSetting]!={0}) ", (int)Logics.True);
            }

            string orderbyClause = " OrderNumber desc ";
            return base.GetList(whereClause, orderbyClause, paras);
        }

        /// <summary>
        /// 按照类别获取实体对象列表
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <returns></returns>
        public List<BasicSettingEntity> GetListByCategory(string categoryName)
        {
            string whereClause = " 1=1 ";
            if (string.IsNullOrEmpty(categoryName) == false)
            {
                whereClause = string.Format(" [SettingCategory]= '{0}' ", categoryName);
            }

            return base.GetList(whereClause);
        }

        #region 各种常用的按照类别获取数据的需求(需要数据库内的数据支持)
        /// <summary>
        /// 获取所有的行业类型
        /// </summary>
        /// <returns></returns>
        public List<BasicSettingEntity> GetListOfIndustryType()
        {
            return GetListByCategory("IndustryType");
        }
        #endregion
    }
}
