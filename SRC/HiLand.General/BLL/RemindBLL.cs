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
        public void Create(Guid receiveExcutorGuid, ExecuterTypes receiveExecutorType, RemindEntity model)
        {
            List<BusinessUser> userList = new List<BusinessUser>();

            switch (receiveExecutorType)
            {
                case ExecuterTypes.Department:
                    userList = BusinessUserBLL.GetUsersByDepartment(receiveExcutorGuid);
                    break;
                case ExecuterTypes.Group:
                    //TODO:xieran 20120927 
                    break;
                case ExecuterTypes.Role:
                    userList = BusinessRoleBLL.GetUsers(receiveExcutorGuid);
                    break;
                case ExecuterTypes.User:
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

                    base.Create(model);
                }
            }
        }
    }
}
