using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore.DAL;
using HiLand.Framework.BusinessCore.DALCommon;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Cache;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;
using HiLand.Utility.Enums;
using HiLand.Utility.Event;

namespace HiLand.Framework.BusinessCore.BLL
{
    /// <summary>
    /// 部门业务逻辑类
    /// </summary>
    public class BusinessDepartmentBLL : BaseBLL<BusinessDepartmentBLL, BusinessDepartment, BusinessDepartmentDAL, IBusinessDepartmentDAL>
    {
        /// <summary>
        /// 全路径改变的事件
        /// </summary>
        public event CommonEventHandle<DataForChange<string>> FullPathChanged;

        public override bool Create(BusinessDepartment model)
        {
            string parentCode = string.Empty;
            if (model.DepartmentParentGuid != Guid.Empty)
            {
                BusinessDepartment parentDepartment = Get(model.DepartmentParentGuid);
                if (parentDepartment != null && parentDepartment.IsEmpty == false)
                {
                    parentCode = parentDepartment.DepartmentCode;
                }
            }

            model.DepartmentCode = RandomHelper.GetUniqueRandomString(CharCategories.NumberAndCharIgnoreCase, 2, parentCode, s =>
            {
                bool isSuccessful = true;
                //如果某编码在数据库内已经存在，那么这个编码就不可使用，需要重新计算新的编码
                isSuccessful = !LoadDAL.IsExistCode(s);

                return isSuccessful;
            });


            if (string.IsNullOrEmpty(parentCode) == true)
            {
                model.DepartmentFullPath = model.DepartmentName;
            }
            else
            {
                model.DepartmentFullPath = GetFullPath(model);
            }

            return base.Create(model);
        }

        public override bool Update(BusinessDepartment model)
        {
            BusinessDepartment originalModel = BusinessDepartmentBLL.Instance.Get(model.DepartmentGuid, true);
            if (string.IsNullOrEmpty(model.DepartmentFullPath) || 
                originalModel.DepartmentName != model.DepartmentName)
            {
                model.DepartmentFullPath = GetFullPath(model);
            }

            bool isSuccessfule = base.Update(model);
            if (isSuccessfule == true)
            {
                if (model.DepartmentFullPath != originalModel.DepartmentFullPath)
                {
                    ChangeFullPath(originalModel.DepartmentFullPath, model.DepartmentFullPath);
                }
            }

            return isSuccessfule;
        }

        /// <summary>
        /// 获取从根部门到当前部门的全路径
        /// </summary>
        /// <param name="departmentGuid"></param>
        /// <returns></returns>
        public string GetFullPath(Guid departmentGuid)
        {
            BusinessDepartment targetDepartment = Get(departmentGuid);
            return GetFullPath(targetDepartment);
        }

        /// <summary>
        /// 获取从根部门到当前部门的全路径
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public string GetFullPath(BusinessDepartment department)
        {
            string result = string.Empty;
            while (department != null && department.IsEmpty == false)
            {
                result = string.Format("{0}//{1}", department.DepartmentName, result);
                department = department.DepartmentParent;
            }

            if (result.EndsWith("//"))
            {
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }

        /// <summary>
        /// 变更部门的全路径
        /// </summary>
        /// <param name="originalFullPath"></param>
        /// <param name="newFullpath"></param>
        public bool ChangeFullPath(string originalFullPath, string newFullpath)
        {
            bool isSuccessful = false;

            DataForChange<string> changeData = new DataForChange<string>();
            changeData.NewData = newFullpath;
            changeData.OriginalData = originalFullPath;

            isSuccessful = SaveDAL.ChangeFullPath(changeData.OriginalData, changeData.NewData);

            if (FullPathChanged != null)
            {
                FullPathChanged(this, changeData);
            }

            if (isSuccessful == true)
            {
                CleanUpAllCache();
            }

            return isSuccessful;
        }

        /// <summary>
        /// 根据编码获取部门信息
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public BusinessDepartment GetByCode(string departmentCode)
        {
            string cacheKey = GeneralCacheKeys<BusinessDepartment>.GetEntityCustomKey("DepartmentCode", departmentCode);
            return CacheHelper.Access<string, BusinessDepartment>(cacheKey, CacheMintues, LoadDAL.GetByCode, departmentCode);
        }

        /// <summary>
        /// 获取按照部门子部门排序后的列表
        /// </summary>
        /// <param name="onlyDisplayUsable"></param>
        /// <param name="whereClause"></param>
        /// <param name="isFormat">子部门名称是否进行缩减格式</param>
        /// <returns></returns>
        public List<BusinessDepartment> GetOrdedList(Logics onlyDisplayUsable, string whereClause)
        {
            List<BusinessDepartment> result = new List<BusinessDepartment>();
            int roundCount = 0;
            SortDepartment(Guid.Empty, onlyDisplayUsable, ref result, ref roundCount);
            return result;
        }

        private void SortDepartment(Guid parentGuid, Logics onlyDisplayUsable, ref List<BusinessDepartment> result, ref int roundCount)
        {
            int currentRountNumber = roundCount;
            roundCount++;

            List<BusinessDepartment> subs = GetSubList(parentGuid, onlyDisplayUsable);

            foreach (BusinessDepartment item in subs)
            {
                result.Add(item);
                SortDepartment(item.DepartmentGuid, onlyDisplayUsable, ref result, ref roundCount);
            }
        }


        /// <summary>
        /// 获取子部门列表
        /// </summary>
        /// <param name="departmentGuid"></param>
        /// <returns></returns>
        public List<BusinessDepartment> GetSubList(Guid departmentGuid)
        {
            return GetSubList(departmentGuid, Logics.False);
        }


        /// <summary>
        /// 获取子部门列表
        /// </summary>
        /// <param name="departmentGuid"></param>
        /// <param name="onlyDisplayUsable">是否仅显示有效的子部门</param>
        /// <returns></returns>
        public List<BusinessDepartment> GetSubList(Guid departmentGuid, Logics onlyDisplayUsable)
        {
            List<BusinessDepartment> result = new List<BusinessDepartment>();
            List<BusinessDepartment> allDepartments = GetList(Logics.False, string.Empty, 0, "", null);

            foreach (BusinessDepartment item in allDepartments)
            {
                if (item.CanUsable == Logics.False && onlyDisplayUsable == Logics.True)
                {
                    continue;
                }

                if (item.DepartmentParentGuid == departmentGuid)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
