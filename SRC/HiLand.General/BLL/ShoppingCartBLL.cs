using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Utility.Enums;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 购物车业务逻辑类
    /// </summary>
    public class ShoppingCartBLL : BaseBLL<ShoppingCartBLL, ShoppingCartEntity, ShoppingCartDAL>
    {
        /// <summary>
        ///  (为当前登录人员)向购物车中添加购物项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Buy(ShoppingCartEntity model)
        {
            return Buy(model,string.Empty);
        }

        /// <summary>
        ///  向购物车中添加购物项
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ownerKey">购物车所有人标识</param>
        /// <returns></returns>
        public bool Buy(ShoppingCartEntity model, string ownerKey)
        {
            bool result = false;

            if (string.IsNullOrEmpty(ownerKey))
            {
                if (BusinessUserBLL.IsLogined)
                {
                    ownerKey = BusinessUserBLL.CurrentUser.UserGuid.ToString();
                }
                else
                {
                    ownerKey = BusinessUserBLL.CurrentUser.UserTempGuid.ToString();
                }
            }

            List<ShoppingCartEntity> shoppingCartList = null;
            if (model.IsFavoriteItem == Logics.True)
            {
                shoppingCartList = GetListFavorite(ownerKey,string.Empty);
            }
            else
            {
                shoppingCartList = GetListShop(ownerKey,string.Empty);
            }

            if (model != null)
            {
                if (BusinessUserBLL.IsLogined)
                {
                    model.IsTempOwner = Logics.False;
                }
                else
                {
                    model.IsTempOwner = Logics.True;
                }

                model.OwnerKey = ownerKey;
                model.CreateTime = DateTime.Now;

                ShoppingCartEntity shopedEnity = shoppingCartList.Find(delegate(ShoppingCartEntity item) { return item.ProductKey == model.ProductKey; });
                if (shopedEnity == null)
                {
                    result = base.Create(model);
                }
                else
                {
                    shopedEnity.ProductPrice = model.ProductPrice;
                    shopedEnity.ProductQuantity += model.ProductQuantity;
                    shopedEnity.CreateTime = model.CreateTime;
                    result = base.Update(shopedEnity);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="ownerKey">购物车所有人，（如果为空则表示当前获取用户的购物车）</param>
        /// <param name="whereClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public List<ShoppingCartEntity> GetListShop(string ownerKey,string whereClause,  params System.Data.IDbDataParameter[] paras)
        {
            return GetListDetails(Logics.False, ownerKey, whereClause, paras);
        }

        /// <summary>
        /// 获取心愿单
        /// </summary>
        /// <param name="ownerKey">购物车所有人，（如果为空则表示当前获取用户的购物车）</param>
        /// <param name="whereClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public List<ShoppingCartEntity> GetListFavorite(string ownerKey,string whereClause,  params System.Data.IDbDataParameter[] paras)
        {
            return GetListDetails(Logics.True, ownerKey, whereClause, paras);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerKey">购物车所有人，（如果为空则表示当前获取用户的购物车）</param>
        /// <param name="IsFavoriteItem"></param>
        /// <param name="whereClause"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private List<ShoppingCartEntity> GetListDetails(Logics? IsFavoriteItem, string ownerKey, string whereClause, params System.Data.IDbDataParameter[] paras)
        {
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            if (IsFavoriteItem.HasValue)
            {
                whereClause += " AND IsFavoriteItem = " + (int)IsFavoriteItem.Value;
            }

            if (string.IsNullOrEmpty(ownerKey))
            {
                if (BusinessUserBLL.IsLogined)
                {
                    //1.首先询问是否要将临时数据导入
                    MigrateList(BusinessUserBLL.CurrentUser.UserTempGuid.ToString(), BusinessUserBLL.CurrentUser.UserGuid.ToString());
                    ownerKey = BusinessUserBLL.CurrentUser.UserGuid.ToString();
                }
                else
                {
                    ownerKey = BusinessUserBLL.CurrentUser.UserTempGuid.ToString();
                }
            }

            whereClause += string.Format(" AND  OwnerKey='{0}' ", ownerKey);

            return base.GetList(whereClause, paras);
        }

        /// <summary>
        /// 将临时用户数据迁移到登录用户的名下
        /// </summary>
        /// <param name="sourceOwnerKey"></param>
        /// <param name="destOwnerKey"></param>
        /// <returns></returns>
        private void MigrateList(string sourceOwnerKey,string destOwnerKey)
        {
            if (string.IsNullOrEmpty(sourceOwnerKey) == true)
            {
                sourceOwnerKey = BusinessUserBLL.CurrentUser.UserTempGuid.ToString();
            }

            List<ShoppingCartEntity> shoppingCartList = GetListDetails(null, sourceOwnerKey, string.Empty);
            foreach (ShoppingCartEntity currentItem in shoppingCartList)
            {
                Buy(currentItem,destOwnerKey);
                Delete(currentItem.ShoppingItemGuid);
            }
        }
    }
}
