using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.BusinessCore.DAL;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Serialization;

namespace HiLand.Framework.BusinessCore.DALCommon
{
    public class BusinessRoleCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseComputerDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>, IBusinessRoleDAL
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        /// <summary>
        /// 判断是否存在某个名称的角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsExistRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }

            string commnadString = string.Format("select count(1) from [CoreRole] where [RoleName]={0}RoleName", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("RoleName", roleName) };
            return HelperExInstance.IsExist(commnadString, sqlParas);
        }

        #region CRUD逻辑
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public virtual BusinessRole CreateRole(IBusinessRole entity, out CreateUserRoleStatuses status)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.RoleGuid == Guid.Empty)
            {
                entity.RoleGuid = GuidHelper.NewGuid();
            }

            bool isExist = true;
            status = CreateUserRoleStatuses.Successful;

            //1.判断角色名称是否存在
            isExist = IsExistRole(entity.RoleName);
            if (isExist == true)
            {
                status = CreateUserRoleStatuses.FailureDuplicateName;
                return null;
            }

            //2.具体创建角色
            string commandText = string.Format(@"Insert Into [CoreRole] (
			        [RoleGuid],
			        [RoleName],
			        [RoleDescrition],
                    [CanUsable],
                    [IsInnerRole],
			        [PropertyNames],
			        [PropertyValues]
                ) 
                Values (
			        {0}RoleGuid,
			        {0}RoleName,
			        {0}RoleDescrition,
                    {0}CanUsable,
                    {0}IsInnerRole,
			        {0}PropertyNames,
			        {0}PropertyValues
                )", ParameterNamePrefix);

            if (entity.RoleGuid == Guid.Empty)
            {
                entity.RoleGuid = Guid.NewGuid();
            }

            TParameter[] sqlParas = PrepareParasAll(entity);

            try
            {
                bool isSuccessful = HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);

                if (isSuccessful == true)
                {
                    status = CreateUserRoleStatuses.Successful;
                }
                else
                {
                    status = CreateUserRoleStatuses.FailureUnknowReason;
                }

                entity = Get(entity.RoleGuid);
            }
            catch
            {
                status = CreateUserRoleStatuses.FailureUnknowReason;
            }

            return entity as BusinessRole;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool UpdateRole(IBusinessRole entity)
        {
            string commandText = string.Format(@"Update [CoreRole] Set  
					[RoleGuid] = {0}RoleGuid,
					[RoleName] = {0}RoleName,
					[RoleDescrition] = {0}RoleDescrition,
                    [CanUsable]={0}CanUsable,
                    [IsInnerRole]={0}IsInnerRole,
					[PropertyNames] = {0}PropertyNames,
					[PropertyValues] = {0}PropertyValues
                Where [RoleGuid] = {0}RoleGuid", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity);

            return HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public virtual bool DeleteRole(Guid roleGuid)
        {
            string commandText = string.Format("DELETE FROM [CoreRole] WHERE [RoleGuid] ={0}RoleGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("RoleGuid", roleGuid) };
            return HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public virtual BusinessRole Get(Guid roleGuid)
        {
            string commandText = string.Format("SELECT * FROM [CoreRole] WHERE  [RoleGuid] = {0}RoleGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("RoleGuid", roleGuid) };
            return Get(commandText, sqlParas);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual BusinessRole Get(string roleName)
        {
            string commandText = string.Format("SELECT * FROM [CoreRole] WHERE  [RoleName] = {0}RoleName", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("RoleName", roleName) };
            return Get(commandText, sqlParas);
        }

        private BusinessRole Get(string commandText, TParameter[] sqlParas)
        {
            BusinessRole entity = BusinessRole.Empty;

            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
            {
                if (reader != null && reader.Read())
                {
                    entity = Load(reader);
                }
            }

            return entity;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public virtual List<BusinessRole> GetList(Logics onlyDisplayUsable, string whereClause)
        {
            List<BusinessRole> collection = new List<BusinessRole>();

            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            if (onlyDisplayUsable == Logics.True)
            {
                whereClause += string.Format(" AND CanUsable= {0} ", (int)Logics.True);
            }

            string commandText = string.Format("SELECT * FROM [CoreRole] WHERE {0}", whereClause);
            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        BusinessRole entity = Load(reader);
                        collection.Add(entity);
                    }
                }
            }

            return collection;
        }
        #endregion

        #region 角色用户关联
        /// <summary>
        /// 获取角色内的用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual List<BusinessUser> GetUsers(string roleName)
        {
            Guid roleGuid = Guid.Empty;
            BusinessRole role = Get(roleName);

            if (role != null)
            {
                roleGuid = role.RoleGuid;
            }

            return GetUsers(roleGuid);
        }

        /// <summary>
        /// 获取角色内的用户
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public virtual List<BusinessUser> GetUsers(Guid roleGuid)
        {
            List<BusinessUser> roleList = new List<BusinessUser>();

            string commandText = "SELECT u.* From [CoreUser] u INNER JOIN [CoreUserInRole] uir ON u.UserGuid= uir.UserGuid WHERE RoleGuid=@RoleGuid ";
            TParameter[] sqlParas = new TParameter[] 
            { 
                GenerateParameter("RoleGuid", roleGuid)
            };

            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        BusinessUser entity = BusinessUserCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>.Load(reader);
                        if (entity != null)
                        {
                            roleList.Add(entity);
                        }
                    }
                }
            }

            return roleList;
        }




        #endregion

        #region 辅助方法
        public static BusinessRole Load(TDataReader reader)
        {
            BusinessRole entity = new BusinessRole();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistField(reader, "RoleID") && Convert.IsDBNull(reader["RoleID"]) == false)
                {
                    entity.RoleID = reader.GetInt32(reader.GetOrdinal("RoleID"));
                }
                if (DataReaderHelper.IsExistField(reader, "RoleGuid") && Convert.IsDBNull(reader["RoleGuid"]) == false)
                {
                    entity.RoleGuid = reader.GetGuid(reader.GetOrdinal("RoleGuid"));
                }
                if (DataReaderHelper.IsExistField(reader, "RoleName") && Convert.IsDBNull(reader["RoleName"]) == false)
                {
                    entity.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
                }
                if (DataReaderHelper.IsExistField(reader, "CanUsable") && Convert.IsDBNull(reader["CanUsable"]) == false)
                {
                    entity.CanUsable = (Logics)reader.GetInt32(reader.GetOrdinal("CanUsable"));
                }
                if (DataReaderHelper.IsExistField(reader, "IsInnerRole") && Convert.IsDBNull(reader["IsInnerRole"]) == false)
                {
                    entity.IsInnerRole = (Logics)reader.GetInt32(reader.GetOrdinal("IsInnerRole"));
                }
                if (DataReaderHelper.IsExistField(reader, "RoleDescrition") && Convert.IsDBNull(reader["RoleDescrition"]) == false)
                {
                    entity.RoleDescrition = reader.GetString(reader.GetOrdinal("RoleDescrition"));
                }
                if (DataReaderHelper.IsExistField(reader, "PropertyNames") && Convert.IsDBNull(reader["PropertyNames"]) == false)
                {
                    entity.PropertyNames = reader.GetString(reader.GetOrdinal("PropertyNames"));
                }
                if (DataReaderHelper.IsExistField(reader, "PropertyValues") && Convert.IsDBNull(reader["PropertyValues"]) == false)
                {
                    entity.PropertyValues = reader.GetString(reader.GetOrdinal("PropertyValues"));
                }
            }

            return entity;
        }

        private TParameter[] PrepareParasAll(IBusinessRole entity)
        {
            List<TParameter> list = new List<TParameter>()
            {
                GenerateParameter("RoleGuid",entity.RoleGuid),
			    GenerateParameter("RoleName",entity.RoleName),
			    GenerateParameter("RoleDescrition",entity.RoleDescrition),
                GenerateParameter("CanUsable",entity.CanUsable),
                GenerateParameter("IsInnerRole",entity.IsInnerRole)
                ,GenerateParameter("PropertyNames","")
                ,GenerateParameter("PropertyValues","")
            };

            if (entity is IModelExtensible)
            {
                TParameter paraPropertyNames = list.Find(paramater => paramater.ParameterName == string.Format("{0}PropertyNames", ParameterNamePrefix));
                TParameter paraPropertyValues = list.Find(paramater => paramater.ParameterName == string.Format("{0}PropertyValues", ParameterNamePrefix));

                if (paraPropertyNames != null && paraPropertyValues != null)
                {
                    SerializerData serializerData = ((IModelExtensible)entity).ExtensiableRepository.GetSerializerData();
                    paraPropertyNames.Value = serializerData.Keys ?? string.Empty;
                    paraPropertyValues.Value = serializerData.Values ?? string.Empty;
                }
            }

            return list.ToArray();
        }
        #endregion
    }
}
