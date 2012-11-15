using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 信息提醒逻辑类
    /// </summary>
    public class RemindBLL : BaseBLL<RemindBLL, RemindEntity, RemindDAL>
    {
        /// <summary>
        /// 创建提醒信息
        /// </summary>
        /// <param name="receiveExcutorGuid">提醒信息接收行为对象的Guid</param>
        /// <param name="receiveExecutorType">提醒信息接收行为对象的类型</param>
        /// <param name="model">除ReceiverKey之外带有其他属性的提醒信息实体</param>
        public void Create(Guid receiveExcutorGuid, ExecutorTypes receiveExecutorType, RemindEntity model)
        {
            List<BusinessUser> userList = new List<BusinessUser>();

            switch (receiveExecutorType)
            {
                case ExecutorTypes.Department:
                    userList = BusinessUserBLL.GetUsersByDepartment(receiveExcutorGuid);
                    break;
                case ExecutorTypes.Group:
                    //TODO:xieran20120927 
                    break;
                case ExecutorTypes.Role:
                    userList = BusinessRoleBLL.GetUsers(receiveExcutorGuid);
                    break;
                case ExecutorTypes.User:
                    BusinessUser user = BusinessUserBLL.Get(receiveExcutorGuid);
                    userList.Add(user);
                    break;
                default:
                    break;
            }

            if (userList != null)
            {
                foreach (BusinessUser currentItem in userList)
                {
                    model.ReceiverKey = currentItem.UserGuid.ToString();
                    model.ReceiverName = currentItem.UserNameDisplay;

                    //每次都新生产Guid
                    model.RemindGuid = GuidHelper.NewGuid();
                    base.Create(model);
                }
            }
        }

        /// <summary>
        /// 获取某人未读数据的数量信息
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public int GetCountUnRead(Guid userGuid)
        {
            string whereClause = string.Format(" ReceiverKey = '{0}' AND ReadStatus='{1}' ", userGuid, (int)Logics.False);
            return base.GetTotalCount(whereClause);
        }

        /// <summary>
        /// 获取某人未读的前N条数据
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public List<RemindEntity> GetListUnRead(Guid userGuid, int topCount)
        {
            string whereClause = string.Format(" ReceiverKey = '{0}' AND ReadStatus='{1}' ", userGuid, (int)Logics.False);
            string orderByClause = string.Format(" CreateDate DESC ");
            return base.GetList(Logics.False, whereClause, topCount, orderByClause);
        }

        /// <summary>
        /// 设置某提醒信息为已读
        /// </summary>
        /// <param name="remindGuid">提醒信息Guid</param>
        /// <returns></returns>
        public bool SetRead(Guid remindGuid)
        {
            bool result = false;
            RemindEntity entity = base.Get(remindGuid);
            if (entity.IsEmpty == false)
            {
                entity.ReadDate = DateTime.Now;
                entity.ReadStatus = Logics.True;
                result = base.Update(entity);
            }

            return result;
        }
    }
}
