using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.DALCommon;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting;

namespace HiLand.General.BLL
{
    /// <summary>
    /// 操作日志业务逻辑类
    /// </summary>
    public class OperateLogBLL : BaseBLL<OperateLogBLL, OperateLogEntity, OperateLogDAL, IOperateLogDAL>
    {
        /// <summary>
        /// 获取所有的类别
        /// </summary>
        /// <returns></returns>
        public List<string> GetCategoryList()
        {
            return this.LoadDAL.GetCategoryList();
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logTitle"></param>
        /// <param name="logCategory"></param>
        /// <param name="relativeKey"></param>
        /// <param name="relativeName"></param>
        /// <param name="operateType"></param>
        /// <param name="logMessage"></param>
        /// <remarks>本方法对Create方法做一次简单包装（在本方法内自动创建一个OperateLogEntity并为其赋值）</remarks>
        public static void RecordOperateLog(string logTitle, string logCategory, string relativeKey, string relativeName, string operateType, string logMessage)
        {
            if (Config.IsRecordOperateLog == true)
            {
                try
                {
                    OperateLogEntity logEntity = new OperateLogEntity();
                    logEntity.CanUsable = Logics.True;
                    logEntity.LogCategory = logCategory;
                    logEntity.LogDate = DateTime.Now;

                    logEntity.LogOperateName = operateType;

                    logEntity.LogStatus = (int)Logics.True;
                    logEntity.LogType = 0;
                    logEntity.LogUserKey = BusinessUserBLL.CurrentUserGuid.ToString();
                    logEntity.LogUserName = BusinessUserBLL.CurrentUser.UserNameDisplay;
                    logEntity.RelativeKey = relativeKey;
                    logEntity.RelativeName = relativeName;
                    logEntity.LogTitle = logTitle;
                    logEntity.LogMessage = logMessage;

                    OperateLogBLL.Instance.Create(logEntity);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logTitle"></param>
        /// <param name="logCategory"></param>
        /// <param name="relativeKey"></param>
        /// <param name="relativeName"></param>
        /// <param name="newEntity"></param>
        /// <param name="originalEntity"></param>
        /// <remarks>本方法对Create方法做一次简单包装（在本方法内自动创建一个OperateLogEntity并为其赋值）</remarks>
        public static void RecordOperateLog<T>(string logTitle, string logCategory, string relativeKey, string relativeName, T newEntity, T originalEntity)
        {
            string operateType = string.Empty;
            if (originalEntity == null)
            {
                operateType = OperateTypes.Create.ToString();
            }
            else
            {
                operateType = OperateTypes.Update.ToString();
            }

            string logMessage = string.Empty;
            if (originalEntity != null)
            {
                Dictionary<string, DataForChange<string>> compareResult = null;
                string[] excludePropertyArray = new string[]{"LastUpdateUserKey",
                            "LastUpdateUserName",
                            "LastUpdateDate",
                            "PropertyNames",
                            "PropertyValues"
                        };
                EntityHelper.Compare(originalEntity, newEntity, out compareResult, excludePropertyArray);

                if (compareResult != null && compareResult.Count > 0)
                {
                    foreach (KeyValuePair<string,DataForChange<string>> item in compareResult)
                    {
                        logMessage += string.Format(string.Format("属性[{0}]从[{1}]变更到[{2}];", item.Key,item.Value.OriginalData,item.Value.NewData));
                    }
                }
                else
                {
                    logMessage = "没有修改任何信息";
                }
            }

            RecordOperateLog(logTitle, logCategory, relativeKey, relativeName, operateType, logMessage);
        }
    }
}
