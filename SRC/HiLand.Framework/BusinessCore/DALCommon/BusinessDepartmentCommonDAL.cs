using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;

namespace HiLand.Framework.BusinessCore.DALCommon
{
    public class BusinessDepartmentCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseDAL<BusinessDepartment, TTransaction, TConnection, TCommand, TDataReader, TParameter>, IBusinessDepartmentDAL
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        #region 基本信息
        protected override string TableName
        {
            get { return "CoreDepartment"; }
        }

        protected override string[] KeyNames
        {
            get { return new string[] { "DepartmentGuid" }; }
        }

        protected override string GuidKeyName
        {
            get { return "DepartmentGuid"; }
        }

        //TODO:此存储过程未实行
        protected override string PagingSPName
        {
            get { return "usp_Core_Department_SelectPaging"; }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 是否存在当前的部门编码
        /// </summary>
        /// <param name="departmentCode">待验证的部门编码</param>
        /// <returns></returns>
        public bool IsExistCode(string departmentCode)
        {
            if (string.IsNullOrEmpty(departmentCode))
            {
                return false;
            }

            string commnadString = string.Format("select count(1) from [{0}] where [DepartmentCode]={1}DepartmentCode", TableName, ParameterNamePrefix);
            List<TParameter> sqlParas = new List<TParameter>() { GenerateParameter("DepartmentCode", departmentCode) };
            return HelperExInstance.IsExist(commnadString, sqlParas.ToArray());
        }
        #endregion

        #region 逻辑操作
        public override bool Create(BusinessDepartment entity)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.DepartmentGuid == Guid.Empty)
            {
                entity.DepartmentGuid = GuidHelper.NewGuid();
            }

            string commandText = string.Format(@"Insert Into [CoreDepartment] (
			    [DepartmentGuid],
			    [DepartmentName],
			    [DepartmentNameShort],
			    [DepartmentDescription],
                [DepartmentFullPath],
			    [DepartmentCode],
			    [DepartmentParentGuid],
			    [DepartmentType],
			    [DepartmentIsSpecial],
			    [CanUsable],
			    [PropertyNames],
			    [PropertyValues]
            ) 
            Values (
			    {0}DepartmentGuid,
			    {0}DepartmentName,
			    {0}DepartmentNameShort,
			    {0}DepartmentDescription,
                {0}DepartmentFullPath,
			    {0}DepartmentCode,
			    {0}DepartmentParentGuid,
			    {0}DepartmentType,
			    {0}DepartmentIsSpecial,
			    {0}CanUsable,
			    {0}PropertyNames,
			    {0}PropertyValues
            )", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        public override bool Update(BusinessDepartment entity)
        {
            string commandText = string.Format(@"Update [CoreDepartment] Set   
					[DepartmentGuid] = {0}DepartmentGuid,
					[DepartmentName] = {0}DepartmentName,
					[DepartmentNameShort] = {0}DepartmentNameShort,
					[DepartmentDescription] = {0}DepartmentDescription,
                    [DepartmentFullPath]= {0}DepartmentFullPath,
					[DepartmentCode] = {0}DepartmentCode,
					[DepartmentParentGuid] = {0}DepartmentParentGuid,
					[DepartmentType] = {0}DepartmentType,
					[DepartmentIsSpecial] = {0}DepartmentIsSpecial,
					[CanUsable] = {0}CanUsable,
					[PropertyNames] = {0}PropertyNames,
					[PropertyValues] = {0}PropertyValues
             Where [DepartmentID] = {0}DepartmentID", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
            return isSuccessful;
        }

        /// <summary>
        /// 根据编码获取部门信息
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public BusinessDepartment GetByCode(string departmentCode)
        {
            string commandText = string.Format("SELECT * FROM [{0}]  WHERE  {1}", TableName, GetKeysWhereClause());
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("DepartmentCode", departmentCode) };
            return CommonGeneralInstance.GetEntity<BusinessDepartment>(commandText, sqlParas, Load);
        }

        /// <summary>
        /// 变更部门的全路径
        /// </summary>
        /// <param name="originalFullPath"></param>
        /// <param name="newFullpath"></param>
        public bool ChangeFullPath(string originalFullPath, string newFullpath)
        {
            string commandText = string.Format(@"Update [CoreDepartment] Set   
					DepartmentFullPath= REPLACE(DepartmentFullPath,{0}originalFullPath,{0}newFullpath)
            WHERE DepartmentFullPath like {0}originalFullPathLeftLike", ParameterNamePrefix);

            TParameter[] sqlParas = new TParameter[]{
                GenerateParameter("newFullpath",newFullpath),
                GenerateParameter("originalFullPath",originalFullPath),
                GenerateParameter("originalFullPathLeftLike",string.Format("{0}%",originalFullPath))
            };

            int countEffected = HelperExInstance.ExecuteNonQuery(commandText, sqlParas);
            bool isSuccessful = false;
            if (countEffected > 0)
            {
                isSuccessful = true;
            }
            return isSuccessful;
        }
        #endregion

        #region 辅助方法
        protected override void InnerPrepareParasAll(BusinessDepartment entity, ref List<TParameter> paraList)
        {
            List<TParameter> list = new List<TParameter>(){
			    GenerateParameter("DepartmentID",entity.DepartmentID),
                GenerateParameter("DepartmentGuid",entity.DepartmentGuid),
			    GenerateParameter("DepartmentName",entity.DepartmentName?? String.Empty),
			    GenerateParameter("DepartmentNameShort",entity.DepartmentNameShort?? String.Empty),
			    GenerateParameter("DepartmentDescription",entity.DepartmentDescription?? String.Empty),
                GenerateParameter("DepartmentFullPath",entity.DepartmentFullPath?? String.Empty),
			    GenerateParameter("DepartmentCode",entity.DepartmentCode?? String.Empty),
			    GenerateParameter("DepartmentParentGuid",entity.DepartmentParentGuid),
			    GenerateParameter("DepartmentType",entity.DepartmentType),
			    GenerateParameter("DepartmentIsSpecial",entity.DepartmentIsSpecial),
			    GenerateParameter("CanUsable",entity.CanUsable)
            };

            paraList.AddRange(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        protected override void InnerLoad(IDataReader reader, ref BusinessDepartment entity)
        {
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentID"))
                {
                    entity.DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentGuid"))
                {
                    entity.DepartmentGuid = reader.GetGuid(reader.GetOrdinal("DepartmentGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentName"))
                {
                    entity.DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentNameShort"))
                {
                    entity.DepartmentNameShort = reader.GetString(reader.GetOrdinal("DepartmentNameShort"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentDescription"))
                {
                    entity.DepartmentDescription = reader.GetString(reader.GetOrdinal("DepartmentDescription"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentFullPath"))
                {
                    entity.DepartmentFullPath = reader.GetString(reader.GetOrdinal("DepartmentFullPath"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentCode"))
                {
                    entity.DepartmentCode = reader.GetString(reader.GetOrdinal("DepartmentCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentParentGuid"))
                {
                    entity.DepartmentParentGuid = reader.GetGuid(reader.GetOrdinal("DepartmentParentGuid"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentType"))
                {
                    entity.DepartmentType = (DepartmentTypes)reader.GetInt32(reader.GetOrdinal("DepartmentType"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DepartmentIsSpecial"))
                {
                    entity.DepartmentIsSpecial = (Logics)reader.GetInt32(reader.GetOrdinal("DepartmentIsSpecial"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "CanUsable"))
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
            }
        }
        #endregion
    }
}
