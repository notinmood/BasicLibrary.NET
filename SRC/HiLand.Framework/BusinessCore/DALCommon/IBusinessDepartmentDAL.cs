using HiLand.Framework.FoundationLayer;

namespace HiLand.Framework.BusinessCore.DALCommon
{
    public interface IBusinessDepartmentDAL : IDAL<BusinessDepartment>
    {
        /// <summary>
        /// 是否存在当前的系统编码
        /// </summary>
        /// <param name="departmentCode">待验证的部门编码</param>
        /// <returns></returns>
        bool IsExistCode(string departmentCode);

        /// <summary>
        /// 根据编码获取部门信息
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        BusinessDepartment GetByCode(string departmentCode);

        /// <summary>
        /// 变更部门的全路径
        /// </summary>
        /// <param name="originalFullPath"></param>
        /// <param name="newFullpath"></param>
        bool ChangeFullPath(string originalFullPath, string newFullpath);
    }
}
