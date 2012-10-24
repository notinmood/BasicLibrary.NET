//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data.SqlClient;
//using HiLand.Utility.DataBase;
//using HiLand.Utility.Enums;
//using HiLand.Utility.Membership;
//using HiLand.Framework.FoundationLayer;
//using HiLand.Utility.Serialization;
//using HiLand.Framework.BusinessCore.DAL;

//namespace HiLand.Framework.BusinessCore.DALSqlite
//{
//    public class BusinessRoleDAL : IBusinessRoleDAL
//    {
//        /// <summary>
//        /// 判断是否存在某个名称的角色
//        /// </summary>
//        /// <param name="roleName"></param>
//        /// <returns></returns>
//        public bool IsExistRole(string roleName)
//        {
//            if (string.IsNullOrEmpty(roleName))
//            {
//                return false;
//            }

//            string commnadString = "select count(1) from [CoreRole] where [RoleName]=@RoleName";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@RoleName", roleName) };
//            return SqlHelperEx.IsExist(commnadString, sqlParas);
//        }

//        /// <summary>
//        /// 创建角色
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <param name="status"></param>
//        /// <returns></returns>
//        public virtual BusinessRole CreateRole(IBusinessRole entity, out CreateUserRoleStatuses status)
//        {
//            bool isExist = true;
//            status = CreateUserRoleStatuses.Successful;

//            //1.判断角色名称是否存在
//            isExist = IsExistRole(entity.RoleName);
//            if (isExist == true)
//            {
//                status = CreateUserRoleStatuses.FailureDuplicateName;
//                return null;
//            }

//            //2.具体创建角色
//            string commandText = @"Insert Into [CoreRole] (
//			        [RoleGuid],
//			        [RoleName],
//			        [RoleDescrition],
//			        [PropertyNames],
//			        [PropertyValues]
//                ) 
//                Values (
//			        @RoleGuid,
//			        @RoleName,
//			        @RoleDescrition,
//			        @PropertyNames,
//			        @PropertyValues
//                )";

//            if (entity.RoleGuid == Guid.Empty)
//            {
//                entity.RoleGuid = Guid.NewGuid();
//            }

//            SqlParameter[] sqlParas = PrepareParasAll(entity);

//            try
//            {
//                bool isSuccessful = SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);

//                if (isSuccessful == true)
//                {
//                    status = CreateUserRoleStatuses.Successful;
//                }
//                else
//                {
//                    status = CreateUserRoleStatuses.FailureUnknowReason;
//                }

//                entity = Get(entity.RoleGuid);
//            }
//            catch
//            {
//                status = CreateUserRoleStatuses.FailureUnknowReason;
//            }

//            return entity as BusinessRole;
//        }

//        /// <summary>
//        /// 更新角色
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public virtual bool UpdateRole(IBusinessRole entity)
//        {
//            string commandText = @"Update [CoreRole] Set  
//					[RoleGuid] = @RoleGuid,
//					[RoleName] = @RoleName,
//					[RoleDescrition] = @RoleDescrition,
//					[PropertyNames] = @PropertyNames,
//					[PropertyValues] = @PropertyValues
//                Where [RoleGuid] = @RoleGuid";

//            SqlParameter[] sqlParas = PrepareParasAll(entity);

//            return SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 删除角色
//        /// </summary>
//        /// <param name="roleGuid"></param>
//        /// <returns></returns>
//        public virtual bool DeleteRole(Guid roleGuid)
//        {
//            string commandText = "DELETE FROM [CoreRole] WHERE [RoleGuid] = @RoleGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@RoleGuid", roleGuid) };
//            return SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 获取角色
//        /// </summary>
//        /// <param name="roleGuid"></param>
//        /// <returns></returns>
//        public virtual BusinessRole Get(Guid roleGuid)
//        {
//            string commandText = "SELECT * FROM [CoreRole] WHERE  [RoleGuid] = @RoleGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@RoleGuid", roleGuid) };
//            return Get(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 获取角色
//        /// </summary>
//        /// <param name="roleName"></param>
//        /// <returns></returns>
//        public virtual BusinessRole Get(string roleName)
//        {
//            string commandText = "SELECT * FROM [CoreRole] WHERE  [RoleName] = @RoleName";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@RoleName", roleName) };
//            return Get(commandText, sqlParas);
//        }

//        private BusinessRole Get(string commandText, SqlParameter[] sqlParas)
//        {
//            BusinessRole entity = BusinessRole.Empty;

//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null && reader.Read())
//                {
//                    entity = Load(reader);
//                }
//            }

//            return entity;
//        }

//        /// <summary>
//        /// 获取所有的角色
//        /// </summary>
//        /// <returns></returns>
//        public virtual List<BusinessRole> GetList()
//        {
//            List<BusinessRole> collection = new List<BusinessRole>();
//            string commandText = "SELECT * FROM [CoreRole] ";
//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        BusinessRole entity = Load(reader);
//                        collection.Add(entity);
//                    }
//                }
//            }

//            return collection;
//        }

//        #region 权限
//        /// <summary>
//        /// 获取角色的权限
//        /// </summary>
//        /// <param name="roleGuid"></param>
//        /// <returns></returns>
//        public Dictionary<Guid, PermissionItem> GetPermissionItems(Guid roleGuid)
//        {
//            Dictionary<Guid, PermissionItem> permissionItems = new Dictionary<Guid, PermissionItem>();
//            string commandText = "SELECT * FROM [CoreRolePermission] WHERE [RoleGuid] = @RoleGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] 
//            { 
//                new SqlParameter("@RoleGuid", roleGuid)
//            };

//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        PermissionItem permissionItem = DALMisc.LoadPermissionItem(reader);
//                        if (permissionItem.PermissionKey != Guid.Empty && permissionItems.ContainsKey(permissionItem.PermissionKey) == false)
//                        {
//                            permissionItems.Add(permissionItem.PermissionKey, permissionItem);
//                        }
//                    }
//                }
//            }

//            return permissionItems;
//        }

//        /// <summary>
//        /// 更新角色的权限
//        /// </summary>
//        /// <param name="roleGuid"></param>
//        /// <param name="permissionItems"></param>
//        public virtual void UpdatePermissionItems(Guid roleGuid, Dictionary<Guid, PermissionItem> permissionItems)
//        {
//            if (permissionItems != null)
//            {
//                string deleteClause = string.Format("DELETE FROM [CoreRolePermission]  WHERE [RoleGuid]='{0}';", roleGuid);
//                string insertClause = string.Empty;
//                foreach (KeyValuePair<Guid, PermissionItem> kvp in permissionItems)
//                {
//                    insertClause += string.Format(@"INSERT INTO [CoreRolePermission]
//                               ([RoleGuid]
//                               ,[PermissionItemGuid]
//                               ,[PermissionItemValue]
//                               ,[CreateUserGuid]
//                               ,[CreateUserType]
//                               ,[IsFreeAwayCreator])
//                         VALUES
//                               ('{0}','{1}',{2},'{3}',{4},{5});",
//                            roleGuid, kvp.Value.PermissionKey, kvp.Value.PermissionValue,
//                            kvp.Value.CreateUserGuid, (int)kvp.Value.CreateUserType, (int)kvp.Value.IsFreeAwayCreator);
//                }

//                string commondText = deleteClause + insertClause;
//                SqlHelperEx.ExecuteNonQuery(commondText);
//            }
//        }
//        #endregion

//        public static BusinessRole Load(SqlDataReader reader)
//        {
//            BusinessRole entity = new BusinessRole();
//            if (reader != null && reader.IsClosed == false)
//            {
//                if (DataReaderHelper.IsExistField(reader, "RoleID") && Convert.IsDBNull(reader["RoleID"]) == false)
//                {
//                    entity.RoleID = reader.GetInt32(reader.GetOrdinal("RoleID"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "RoleGuid") && Convert.IsDBNull(reader["RoleGuid"]) == false)
//                {
//                    entity.RoleGuid = reader.GetGuid(reader.GetOrdinal("RoleGuid"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "RoleName") && Convert.IsDBNull(reader["RoleName"]) == false)
//                {
//                    entity.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "RoleDescrition") && Convert.IsDBNull(reader["RoleDescrition"]) == false)
//                {
//                    entity.RoleDescrition = reader.GetString(reader.GetOrdinal("RoleDescrition"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "PropertyNames") && Convert.IsDBNull(reader["PropertyNames"]) == false)
//                {
//                    entity.PropertyNames = reader.GetString(reader.GetOrdinal("PropertyNames"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "PropertyValues") && Convert.IsDBNull(reader["PropertyValues"]) == false)
//                {
//                    entity.PropertyValues = reader.GetString(reader.GetOrdinal("PropertyValues"));
//                }
//            }

//            return entity;
//        }

//        private static SqlParameter[] PrepareParasAll(IBusinessRole entity)
//        {
//            List<SqlParameter> list = new List<SqlParameter>()
//            {
//                new SqlParameter("@RoleGuid",entity.RoleGuid),
//                new SqlParameter("@RoleName",entity.RoleName),
//                new SqlParameter("@RoleDescrition",entity.RoleDescrition)
//                ,new SqlParameter("@PropertyNames","")
//                ,new SqlParameter("@PropertyValues","")
//            };

//            if (entity is IModelExtensible)
//            {
//                SqlParameter paraPropertyNames = list.Find(paramater => paramater.ParameterName == "@PropertyNames");
//                SqlParameter paraPropertyValues = list.Find(paramater => paramater.ParameterName == "@PropertyValues");

//                if (paraPropertyNames != null && paraPropertyValues != null)
//                {
//                    SerializerData serializerData = ((IModelExtensible)entity).ExtensiableRepository.GetSerializerData();
//                    paraPropertyNames.Value = serializerData.Keys ?? string.Empty;
//                    paraPropertyValues.Value = serializerData.Values ?? string.Empty;
//                }
//            }

//            return list.ToArray();
//        }
//    }
//}
