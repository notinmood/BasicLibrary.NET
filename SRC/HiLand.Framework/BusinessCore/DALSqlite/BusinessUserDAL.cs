//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using HiLand.Utility.DataBase;
//using HiLand.Utility.Enums;
//using HiLand.Utility.Membership;
//using HiLand.Utility.Misc;
//using HiLand.Utility.Paging;
//using HiLand.Utility.Security;
//using HiLand.Utility.Setting;
//using HiLand.Framework.FoundationLayer;
//using HiLand.Utility.Serialization;
//using HiLand.Framework.BusinessCore.DAL;

//namespace HiLand.Framework.BusinessCore.DALSqlite
//{
//    public class BusinessUserDAL : IBusinessUserDAL
//    {
//        /// <summary>
//        /// 判断用户的用户名和EMail是否在系统内存在
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <param name="userEMail"></param>
//        /// <returns></returns>
//        public virtual bool IsExistUser(string userName, string userEMail)
//        {
//            bool isSuccessful = true;
//            isSuccessful = IsExistUserName(userName);

//            if (isSuccessful == false)
//            {
//                return false;
//            }

//            isSuccessful = IsExistUserEMail(userEMail);
//            if (isSuccessful == false)
//            {
//                return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// 判断用户账号是否存在
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <returns></returns>
//        public virtual bool IsExistUserName(string userName)
//        {
//            if (string.IsNullOrEmpty(userName))
//            {
//                return false;
//            }

//            string commnadString = "select count(1) from [CoreUser] where [UserName]=@UserName";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserName", userName) };
//            return SqlHelperEx.IsExist(commnadString, sqlParas);
//        }

//        /// <summary>
//        /// 判断用户的EMail是否存在
//        /// </summary>
//        /// <param name="userEMail"></param>
//        /// <returns></returns>
//        public virtual bool IsExistUserEMail(string userEMail)
//        {
//            if (string.IsNullOrEmpty(userEMail))
//            {
//                return false;
//            }

//            string commnadString = "select count(1) from [CoreUser] where [UserEmail]=@UserEmail";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserEmail", userEMail) };
//            return SqlHelperEx.IsExist(commnadString, sqlParas);
//        }


//        /// <summary>
//        /// 创建用户
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <param name="status"></param>
//        /// <returns></returns>
//        public virtual BusinessUser CreateUser(IBusinessUser entity, out CreateUserRoleStatuses status)
//        {
//            bool isExist = true;
//            status = CreateUserRoleStatuses.Successful;

//            //1.判断用户账号是否存在
//            isExist = IsExistUserName(entity.UserName);
//            if (isExist == true)
//            {
//                status = CreateUserRoleStatuses.FailureDuplicateName;
//                return null;
//            }

//            //2.判断用户的EMail是否存在
//            bool isEMailMustUnique = Config.GetAppSetting<bool>("isEMailMustUnique", true);
//            if (isEMailMustUnique == true)
//            {
//                isExist = IsExistUserEMail(entity.UserEmail);
//                if (isExist == true)
//                {
//                    status = CreateUserRoleStatuses.FailureDuplicateEMail;
//                    return null;
//                }
//            }

//            //3.具体创建用户
//            string commandText = "INSERT INTO [CoreUser] ([UserGuid],[UserName],[UserCode],[Password],[PasswordEncrytType],[PasswordEncrytSalt],[UserNameCN],[UserNameEN],[userNameFirst],[userNameLast],[UserNameMiddle],[DepartmentID],[DepartmentGuid],[DepartmentCode],[AreaCode],[UserEmail],[UserType],[UserStatus],[MaritalStatus],[UserRemark],[UserCardID],[UserCardIDIssued],[DriversLicenceNumber],	[DriversLicenceNumberIssued],[PassportCode],[PassportCodeIssued],[UserSex],[UserBirthDay],[UserMobileNO],[UserRegisterDate],[UserAgreeDate],[UserWorkStartDate],[UserWorkEndDate],[CompanyMail],[UserTitle],[UserPosition],[WorkTelphone],[HomeTelephone],[UserPhoto],[UserMacAddress],[UserLastIP],[UserLastDateTime],[BrokerKey],[PropertyNames],[PropertyValues])" +
//                " VALUES (@UserGuid,@UserName,@UserCode,@Password,@PasswordEncrytType,@PasswordEncrytSalt,@UserNameCN,@UserNameEN,@userNameFirst,@userNameLast,@UserNameMiddle,@DepartmentID,@DepartmentGuid,@DepartmentCode,@AreaCode,@UserEmail,@UserType,@UserStatus,@MaritalStatus,@UserRemark,@UserCardID,@UserCardIDIssued,	@DriversLicenceNumber,@DriversLicenceNumberIssued,@PassportCode,@PassportCodeIssued,@UserSex,@UserBirthDay,@UserMobileNO,@UserRegisterDate,@UserAgreeDate,@UserWorkStartDate,@UserWorkEndDate,@CompanyMail,@UserTitle,@UserPosition,@WorkTelphone,@HomeTelephone,@UserPhoto,@UserMacAddress,@UserLastIP,@UserLastDateTime,@BrokerKey,@PropertyNames,@PropertyValues)";

//            if (entity.UserGuid == Guid.Empty)
//            {
//                entity.UserGuid = Guid.NewGuid();
//            }
//            SqlParameter[] sqlParas = PrepareParasAll(entity, true);

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

//                entity = Get(entity.UserGuid);
//            }
//            catch
//            {
//                status = CreateUserRoleStatuses.FailureUnknowReason;
//            }

//            return entity as BusinessUser;
//        }


//        /// <summary>
//        /// 更新用户
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public virtual bool UpdateUser(IBusinessUser entity)
//        {
//            string commandText = @"Update [CoreUser] Set   
//					[UserName] = @UserName,
//					[UserCode] = @UserCode,
//					[UserNameCN] = @UserNameCN,
//					[UserNameEN] = @UserNameEN,
//					[userNameFirst] = @userNameFirst,
//					[userNameLast] = @userNameLast,
//					[UserNameMiddle] = @UserNameMiddle,
//					[DepartmentID] = @DepartmentID,
//					[DepartmentGuid] = @DepartmentGuid,
//					[DepartmentCode] = @DepartmentCode,
//					[AreaCode] = @AreaCode,
//					[UserEmail] = @UserEmail,
//					[UserType] = @UserType,
//					[UserStatus] = @UserStatus,
//                    [MaritalStatus]=@MaritalStatus,
//					[UserRemark] = @UserRemark,
//					[UserCardID] = @UserCardID,
//                    [UserCardIDIssued] = @UserCardIDIssued,
//					[DriversLicenceNumber] = @DriversLicenceNumber,
//					[DriversLicenceNumberIssued] = @DriversLicenceNumberIssued,
//					[PassportCode] = @PassportCode,
//					[PassportCodeIssued] = @PassportCodeIssued,
//					[UserSex] = @UserSex,
//					[UserBirthDay] = @UserBirthDay,
//					[UserMobileNO] = @UserMobileNO,
//					[UserRegisterDate] = @UserRegisterDate,
//					[UserAgreeDate] = @UserAgreeDate,
//					[UserWorkStartDate] = @UserWorkStartDate,
//					[UserWorkEndDate] = @UserWorkEndDate,
//					[CompanyMail] = @CompanyMail,
//					[UserTitle] = @UserTitle,
//                    [UserPosition]= @UserPosition,
//					[WorkTelphone] = @WorkTelphone,
//					[HomeTelephone] = @HomeTelephone,
//					[UserPhoto] = @UserPhoto,
//					[UserMacAddress] = @UserMacAddress,
//					[UserLastIP] = @UserLastIP,
//					[UserLastDateTime] = @UserLastDateTime,
//					[BrokerKey]= @BrokerKey,
//                    [PropertyNames] = @PropertyNames,
//					[PropertyValues] = @PropertyValues
//             Where [UserGuid] = @UserGuid";

//            SqlParameter[] sqlParas = PrepareParasAll(entity, false);

//            return SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 删除用户
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <returns></returns>
//        public virtual bool DeleteUser(Guid userGuid)
//        {
//            string commandText = "DELETE FROM [CoreUser] WHERE [UserGuid] = @UserGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserGuid", userGuid) };
//            return SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 改变用户的状态
//        /// </summary>
//        /// <param name="newUserStatus"></param>
//        /// <param name="userGuid"></param>
//        /// <returns></returns>
//        public virtual bool SetUserStatus(Guid userGuid, UserStatuses newUserStatus)
//        {
//            string commandText = "Update [CoreUser] Set  [UserStatus] = @UserStatus WHERE  [UserGuid] = @UserGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserStatus", Convert.ToInt32(newUserStatus)), new SqlParameter("@UserGuid", userGuid) };
//            return SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
//        }

//        private static SqlParameter[] PrepareParasAll(IBusinessUser entity, bool isIncludePasswordInfo)
//        {
//            List<SqlParameter> list = new List<SqlParameter>(){
//                new SqlParameter("@UserGuid",entity.UserGuid),
//                new SqlParameter("@UserName",entity.UserName??string.Empty),
//                new SqlParameter("@UserCode",entity.UserCode??string.Empty),
//                new SqlParameter("@UserNameCN",entity.UserNameCN??string.Empty),
//                new SqlParameter("@UserNameEN",entity.UserNameEN??string.Empty),
//                new SqlParameter("@userNameFirst",entity.UserNameFirst??string.Empty),
//                new SqlParameter("@userNameLast",entity.UserNameLast??string.Empty),
//                new SqlParameter("@UserNameMiddle",entity.UserNameMiddle??string.Empty),
//                new SqlParameter("@DepartmentID",entity.DepartmentID),
//                new SqlParameter("@DepartmentGuid",entity.DepartmentGuid),
//                new SqlParameter("@DepartmentCode",entity.DepartmentCode??string.Empty),
//                new SqlParameter("@AreaCode",entity.AreaCode??string.Empty),
//                new SqlParameter("@UserEmail",entity.UserEmail??string.Empty),
//                new SqlParameter("@UserType",entity.UserType),
//                new SqlParameter("@UserStatus",entity.UserStatus),
//                new SqlParameter("@MaritalStatus",entity.MaritalStatus),
//                new SqlParameter("@UserRemark",entity.UserRemark??string.Empty),
//                new SqlParameter("@UserCardID",entity.UserCardID??string.Empty),
//                new SqlParameter("@UserCardIDIssued",entity.UserCardIDIssued??string.Empty),
//                new SqlParameter("@DriversLicenceNumber",entity.DriversLicenceNumber??string.Empty),
//                new SqlParameter("@DriversLicenceNumberIssued",entity.DriversLicenceNumberIssued??string.Empty),
//                new SqlParameter("@PassportCode",entity.PassportCode??string.Empty),
//                new SqlParameter("@PassportCodeIssued",entity.PassportCodeIssued??string.Empty),
//                new SqlParameter("@UserSex",entity.UserSex),
//                new SqlParameter("@UserBirthDay",entity.UserBirthDay),
//                new SqlParameter("@UserMobileNO",entity.UserMobileNO??string.Empty),
//                new SqlParameter("@UserRegisterDate",entity.UserRegisterDate),
//                new SqlParameter("@UserAgreeDate",entity.UserAgreeDate),
//                new SqlParameter("@UserWorkStartDate",entity.UserWorkStartDate),
//                new SqlParameter("@UserWorkEndDate",entity.UserWorkEndDate),
//                new SqlParameter("@CompanyMail",entity.CompanyMail??string.Empty),
//                new SqlParameter("@UserTitle",entity.UserTitle??string.Empty),
//                new SqlParameter("@UserPosition",entity.UserPosition??string.Empty),
//                new SqlParameter("@WorkTelphone",entity.WorkTelphone??string.Empty),
//                new SqlParameter("@HomeTelephone",entity.HomeTelephone??string.Empty),
//                new SqlParameter("@UserPhoto",entity.UserPhoto??string.Empty),
//                new SqlParameter("@UserMacAddress",entity.UserMacAddress??string.Empty),
//                new SqlParameter("@UserLastIP",entity.UserLastIP??string.Empty),
//                new SqlParameter("@UserLastDateTime",entity.UserLastDateTime),
//                new SqlParameter("@BrokerKey",entity.BrokerKey??string.Empty),
//                new SqlParameter("@PropertyNames",""),
//                new SqlParameter("@PropertyValues","")
//            };

//            if (isIncludePasswordInfo == true)
//            {
//                list.Add(new SqlParameter("@Password", entity.Password??string.Empty));
//                list.Add(new SqlParameter("@PasswordEncrytType", entity.PasswordEncrytType));
//                list.Add(new SqlParameter("@PasswordEncrytSalt", entity.PasswordEncrytSalt));
//            }

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

//        /// <summary>
//        /// 更新用户的最后访问信息
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <param name="lastIP"></param>
//        /// <param name="lastTime"></param>
//        public virtual void UpdateLastInfo(Guid userGuid, string lastIP, DateTime lastTime)
//        {
//            string commandText = "UPDATE [CoreUser]   SET [UserLastIP] = @UserLastIP,[UserLastDateTime] = @UserLastDateTime WHERE [UserGuid] = @UserGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] {
//                new SqlParameter("@UserGuid",userGuid),
//                new SqlParameter("@UserLastIP",lastIP),
//                new SqlParameter("@UserLastDateTime",lastTime)
//            };

//            SqlHelperEx.ExecuteNonQuery(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 修改用户的口令
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <param name="newPassword"></param>
//        /// <param name="passwordEncrytType"></param>
//        /// <param name="passwordEncrytSalt"></param>
//        public virtual bool ChangePassword(Guid userGuid, string newPassword, int passwordEncrytType, string passwordEncrytSalt)
//        {
//            string commandText = "UPDATE [CoreUser]   SET[Password] = @Password,[PasswordEncrytType] = @PasswordEncrytType,[PasswordEncrytSalt] = @PasswordEncrytSalt WHERE [UserGuid] = @UserGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] {
//                new SqlParameter("@UserGuid",userGuid),
//                new SqlParameter("@Password",newPassword),
//                new SqlParameter("@PasswordEncrytType",passwordEncrytType),
//                new SqlParameter("@PasswordEncrytSalt",passwordEncrytSalt)
//            };

//            return SqlHelperEx.ExecuteSingleRowNonQuery(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 用户登录
//        /// </summary>
//        /// <param name="userAccount">其可以是用户的UserName,也可以是其EMail</param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public virtual BusinessUser Login(string userAccount, string password, out LoginStatuses status)
//        {
//            BusinessUser entity = null;// IBusinessUser.Empty;
//            LoginStatuses statusForLoginWithUserName = LoginStatuses.Successful;
//            entity = LoginWithUserName(userAccount, password, out statusForLoginWithUserName);
//            if (statusForLoginWithUserName == LoginStatuses.Successful || LoginStatuses.FailureUserDenied== LoginStatuses.FailureUserDenied)
//            {
//                status = statusForLoginWithUserName;
//                return entity;
//            }

//            LoginStatuses statusForLoginWithUserEMail = LoginStatuses.Successful;
//            entity = LoginWithUserEMail(userAccount, password, out statusForLoginWithUserEMail);
//            if (statusForLoginWithUserEMail == LoginStatuses.Successful || LoginStatuses.FailureUserDenied == LoginStatuses.FailureUserDenied)
//            {
//                status = statusForLoginWithUserEMail;
//                return entity;
//            }

//            if (statusForLoginWithUserName == LoginStatuses.FailureNoName && statusForLoginWithUserEMail == LoginStatuses.FailureNoEMail)
//            {
//                status = LoginStatuses.FailureNoAccount;
//                return entity;
//            }

//            status = LoginStatuses.FailureNotMatchPassword;
//            return entity;
//        }

//        /// <summary>
//        /// 使用用户名称和口令登录
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public virtual BusinessUser LoginWithUserName(string userName, string password, out LoginStatuses status)
//        {
//            bool isSuccessful = true;

//            string commandText = string.Empty;
//            SqlParameter[] sqlParas = null;

//            //1.判断用户名称是否存在
//            commandText = "SELECT COUNT(1) FROM [CoreUser] WHERE [UserName]=@UserName";
//            sqlParas = new SqlParameter[] { new SqlParameter("@UserName", userName) };
//            isSuccessful = SqlHelperEx.IsExist(commandText, sqlParas);
//            if (isSuccessful == false)
//            {
//                status = LoginStatuses.FailureNoName;
//                return BusinessUser.Empty;
//            }

//            //2.判断用户名称跟口令是否匹配  //3.判断用户状态是否已被禁用
//            commandText = "SELECT * FROM [CoreUser] WHERE [UserName]=@UserName";
//            sqlParas = new SqlParameter[] { new SqlParameter("@UserName", userName) };
//            SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas);
//            if (reader != null && reader.Read() == true)
//            {
//                IBusinessUser entity = Load(reader);
//                string passwordInSystem = entity.Password;
//                entity.Password = password;
//                entity = DealWithPassword(entity);
//                string passwordEncrypted = entity.Password;
//                if (passwordInSystem != passwordEncrypted)
//                {
//                    status = LoginStatuses.FailureNotMatchPassword;
//                    return BusinessUser.Empty;
//                }

//                if (entity.UserStatus != UserStatuses.Normal)
//                {
//                    status = LoginStatuses.FailureUserDenied;
//                    return BusinessUser.Empty;
//                }
//            }

//            status = LoginStatuses.Successful;
//            return GetByUserName(userName);
//        }

//        /// <summary>
//        /// 使用Email和口令进行登录
//        /// </summary>
//        /// <param name="userEMail"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public virtual BusinessUser LoginWithUserEMail(string userEMail, string password, out LoginStatuses status)
//        {
//            bool isSuccessful = true;

//            string commandText = string.Empty;
//            SqlParameter[] sqlParas = null;

//            //1.判断用户EMail是否存在
//            commandText = "SELECT COUNT(1) FROM [CoreUser] WHERE [UserEmail]=@UserEmail";
//            sqlParas = new SqlParameter[] { new SqlParameter("@UserEmail", userEMail) };
//            isSuccessful = SqlHelperEx.IsExist(commandText, sqlParas);
//            if (isSuccessful == false)
//            {
//                status = LoginStatuses.FailureNoEMail;
//                return BusinessUser.Empty;
//            }

//            //2.判断用户EMail跟口令是否匹配 //3.判断用户状态是否已被禁用
//            commandText = "SELECT * FROM [CoreUser] WHERE  [UserEmail]=@UserEmail";
//            sqlParas = new SqlParameter[] { new SqlParameter("@UserEmail", userEMail) };
//            SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas);
//            if (reader != null && reader.Read() == true)
//            {
//                IBusinessUser entity = Load(reader);
//                string passwordInSystem = entity.Password;
//                entity.Password = password;
//                entity = DealWithPassword(entity);
//                string passwordEncrypted = entity.Password;
//                if (passwordInSystem != passwordEncrypted)
//                {
//                    status = LoginStatuses.FailureNotMatchPassword;
//                    return BusinessUser.Empty;
//                }

//                if (entity.UserStatus != UserStatuses.Normal)
//                {
//                    status = LoginStatuses.FailureUserDenied;
//                    return BusinessUser.Empty;
//                }
//            }

//            status = LoginStatuses.Successful;
//            return GetByUserEMail(userEMail);
//        }

//        /// <summary>
//        /// 获取用户
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <returns></returns>
//        public virtual BusinessUser Get(Guid userGuid)
//        {
//            string commandText = "SELECT * FROM [CoreUser] WHERE  [UserGuid] = @UserGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserGuid", userGuid) };
//            return Get(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 获取用户(根据用户账号)
//        /// </summary>
//        /// <param name="userAccount">此处的账号可以是用户名或者用户EMail</param>
//        /// <returns></returns>
//        public virtual BusinessUser Get(string userAccount)
//        {
//            BusinessUser entity = GetByUserName(userAccount);
//            if (entity.IsEmpty == true)
//            {
//                entity = GetByUserEMail(userAccount);
//            }

//            return entity;
//        }

//        /// <summary>
//        /// 获取用户(根据用户名)
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <returns></returns>
//        public virtual BusinessUser GetByUserName(string userName)
//        {
//            string commandText = "SELECT * FROM [CoreUser] WHERE  [UserName] = @UserName";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserName", userName) };
//            return Get(commandText, sqlParas);
//        }

//        /// <summary>
//        /// 根据部门或其用户集合
//        /// </summary>
//        /// <param name="departmentCode">部门编码</param>
//        /// <returns></returns>
//        public virtual List<BusinessUser> GetUsersByDepartment(string departmentCode)
//        {
//            List<BusinessUser> userList = new List<BusinessUser>();
//            string commandText = "SELECT * FROM [CoreUser] WHERE  [DepartmentCode] = @DepartmentCode";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@DepartmentCode", departmentCode) };

//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        BusinessUser entity = Load(reader);
//                        userList.Add(entity);
//                    }
//                }
//            }

//            return userList;
//        }

//        /// <summary>
//        /// 根据部门获取用户集合
//        /// </summary>
//        /// <param name="departmentID">部门ID</param>
//        /// <returns></returns>
//        public virtual List<BusinessUser> GetUsersByDepartment(int departmentID)
//        {
//            List<BusinessUser> userList = new List<BusinessUser>();
//            string commandText = "SELECT * FROM [CoreUser] WHERE  [DepartmentID] = @DepartmentID";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@DepartmentID", departmentID) };

//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        BusinessUser entity = Load(reader);
//                        userList.Add(entity);
//                    }
//                }
//            }

//            return userList;
//        }

//        /// <summary>
//        /// 根据部门获取用户集合
//        /// </summary>
//        /// <param name="departmentGuid">部门GUID</param>
//        /// <returns></returns>
//        public virtual List<BusinessUser> GetUsersByDepartment(Guid departmentGuid)
//        {
//            List<BusinessUser> userList = new List<BusinessUser>();
//            string commandText = "SELECT * FROM [CoreUser] WHERE  [DepartmentGuid] = @DepartmentGuid";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@DepartmentGuid", departmentGuid) };

//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        BusinessUser entity = Load(reader);
//                        userList.Add(entity);
//                    }
//                }
//            }

//            return userList;
//        }

//        /// <summary>
//        /// 获取用户(根据用户EMail)
//        /// </summary>
//        /// <param name="userEMail"></param>
//        /// <returns></returns>
//        public virtual BusinessUser GetByUserEMail(string userEMail)
//        {
//            string commandText = "SELECT * FROM [CoreUser] WHERE  [UserEmail] = @UserEmail";
//            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@UserEmail", userEMail) };
//            return Get(commandText, sqlParas);
//        }

//        private BusinessUser Get(string commandText, SqlParameter[] sqlParas)
//        {
//            BusinessUser entity = BusinessUser.Empty;

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
//        /// 获取总的条目
//        /// </summary>
//        /// <param name="whereClause"></param>
//        /// <returns></returns>
//        public virtual int GetTotalCount(string whereClause)
//        {
//            if (string.IsNullOrEmpty(whereClause))
//            {
//                whereClause = " 1=1 ";
//            }

//            string commandText = string.Format("SELECT COUNT(1) FROM [CoreUser] WHERE {0}", whereClause);
//            return Convert.ToInt32(SqlHelperEx.ExecuteScalar(commandText));
//        }

//        /// <summary>
//        /// 获取一批用户
//        /// </summary>
//        /// <param name="startIndex"></param>
//        /// <param name="endIndex"></param>
//        /// <param name="whereClause"></param>
//        /// <param name="orderClause"></param>
//        /// <returns></returns>
//        public virtual PagedEntityCollection<BusinessUser> GetPagedCollection(int startIndex, int endIndex, string whereClause, string orderClause)
//        {
//            PagedEntityCollection<BusinessUser> collection = new PagedEntityCollection<BusinessUser>();
//            using (SqlDataReader reader = SqlHelperEx.ExecuteReader(CommandType.StoredProcedure, "usp_Core_User_SelectPaging", SqlGeneral.PrepareParameterPaging(startIndex, endIndex, whereClause, orderClause)))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        BusinessUser entity = Load(reader);
//                        collection.Records.Add(entity);
//                    }
//                }
//            }

//            return collection;
//        }

//        #region 权限
//        /// <summary>
//        /// 获取用户的允许权限
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <returns></returns>
//        public virtual Dictionary<Guid, PermissionItem> GetPermissionItemsSelfAllow(Guid userGuid)
//        {
//            return GetPermissionItemsSelf(userGuid, Logics.True);
//        }

//        /// <summary>
//        /// 获取用户的拒绝权限
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <returns></returns>
//        public virtual Dictionary<Guid, PermissionItem> GetPermissionItemsSelfDeny(Guid userGuid)
//        {
//            return GetPermissionItemsSelf(userGuid, Logics.False);
//        }

//        private Dictionary<Guid, PermissionItem> GetPermissionItemsSelf(Guid userGuid, Logics permissionMode)
//        {
//            Dictionary<Guid, PermissionItem> permissionItems = new Dictionary<Guid, PermissionItem>();
//            string commandText = "SELECT * FROM [CoreUserPermission] WHERE [UserGuid] = @UserGuid AND [PermissionMode]=@PermissionMode";
//            SqlParameter[] sqlParas = new SqlParameter[] 
//            { 
//                new SqlParameter("@UserGuid", userGuid),
//                new SqlParameter("@PermissionMode",(int)permissionMode)
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
//        /// 更新用户的允许权限
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <param name="permissionItems"></param>
//        public virtual void UpdatePermissionSelfAllow(Guid userGuid, Dictionary<Guid, PermissionItem> permissionItems)
//        {
//            UpdatePermissionSelf(userGuid, permissionItems, Logics.True);
//        }

//        /// <summary>
//        /// 更新用户的拒绝权限
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <param name="permissionItems"></param>
//        public virtual void UpdatePermissionSelfDeny(Guid userGuid, Dictionary<Guid, PermissionItem> permissionItems)
//        {
//            UpdatePermissionSelf(userGuid, permissionItems, Logics.False);
//        }

//        private void UpdatePermissionSelf(Guid userGuid, Dictionary<Guid, PermissionItem> permissionItems, Logics permissionMode)
//        {
//            if (permissionItems != null)
//            {
//                string deleteClause = string.Format("DELETE FROM [CoreUserPermission]  WHERE [UserGuid]='{0}' AND [PermissionMode]={1};", userGuid, (int)permissionMode);
//                string insertClause = string.Empty;
//                foreach (KeyValuePair<Guid, PermissionItem> kvp in permissionItems)
//                {
//                    insertClause += string.Format(@"INSERT INTO [CoreUserPermission]
//                           ([UserGuid]
//                           ,[PermissionItemGuid]
//                           ,[PermissionItemValue]
//                           ,[PermissionMode]
//                           ,[CreateUserGuid]
//                           ,[CreateUserType]
//                           ,[IsFreeAwayCreator])
//                     VALUES
//                           ('{0}','{1}',{2},{3},'{4}',{5},{6});",
//                            userGuid, kvp.Value.PermissionKey, kvp.Value.PermissionValue, (int)permissionMode,
//                            kvp.Value.CreateUserGuid, (int)kvp.Value.CreateUserType, (int)kvp.Value.IsFreeAwayCreator);
//                }

//                string commondText = deleteClause + insertClause;
//                SqlHelperEx.ExecuteNonQuery(commondText);
//            }
//        }
//        #endregion

//        #region 角色
//        /// <summary>
//        /// 获取用户所拥有的角色
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <returns></returns>
//        public virtual List<BusinessRole> GetUserRoles(Guid userGuid)
//        {
//            List<BusinessRole> roleList = new List<BusinessRole>();

//            //TODO:
//            //string commandText = "SELECT r.* FROM [CoreUserInRole] as rs left join [CoreRole] r ON rs.RoleGuid= r.RoleGuid WHERE rs.[UserGuid] = @UserGuid ";
//            //SqlParameter[] sqlParas = new SqlParameter[] 
//            //{ 
//            //    new SqlParameter("@UserGuid", userGuid)
//            //};

//            //using (SqlDataReader reader = SqlHelperEx.ExecuteReader(commandText, sqlParas))
//            //{
//            //    if (reader != null)
//            //    {
//            //        while (reader.Read())
//            //        {
//            //            RoleEntity entity = RoleDAL.Load(reader);
//            //            if (entity != null)
//            //            {
//            //                roleList.Add(entity);
//            //            }
//            //        }
//            //    }
//            //}

//            return roleList;
//        }

//        /// <summary>
//        /// 更新用户所属的角色
//        /// </summary>
//        /// <param name="userGuid"></param>
//        /// <param name="roleGuidList"></param>
//        public virtual void UpdateUserRoles(Guid userGuid, List<Guid> roleGuidList)
//        {
//            string deleteClause = string.Format("DELETE FROM [CoreUserInRole]  WHERE [UserGuid]='{0}' ;", userGuid);
//            string insertClause = string.Empty;
//            foreach (Guid roleGuid in roleGuidList)
//            {
//                insertClause += string.Format(@"INSERT INTO [CoreUserInRole] ([UserGuid] ,[RoleGuid]) VALUES ( '{0}','{1}');", userGuid, roleGuid);
//            }

//            string commondText = deleteClause + insertClause;
//            SqlHelperEx.ExecuteNonQuery(commondText);
//        }
//        #endregion

//        #region 辅助方法
//        /// <summary>
//        /// 处理用户口令的加密逻辑
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public static BusinessUser DealWithPassword(IBusinessUser entity)
//        {
//            string passwordEncrytSalt = entity.PasswordEncrytSalt;
//            if (string.IsNullOrEmpty(entity.PasswordEncrytSalt))
//            {
//                passwordEncrytSalt = Randoms.GetRandomString(CharCategories.NumberAndChar, 6);
//            }

//            EncryptTypes encryptType = entity.PasswordEncrytType;//.NoEncrypt;
//            if (encryptType == EncryptTypes.UnSet)
//            {
//                string encryptTypeString = Config.GetAppSetting("userPaswordEncryptType");
//                if (string.IsNullOrEmpty(encryptTypeString) == false)
//                {
//                    encryptTypeString = encryptTypeString.ToLower();
//                    switch (encryptTypeString)
//                    {
//                        case "md5":
//                        case "md5encrypt":
//                            encryptType = EncryptTypes.MD5Encrypt;
//                            break;
//                        case "hash":
//                        case "hashencrypt":
//                            encryptType = EncryptTypes.HashEncrypt;
//                            break;
//                        default:
//                            encryptType = EncryptTypes.NoEncrypt;
//                            break;
//                    }
//                }
//                else
//                {
//                    encryptType = EncryptTypes.NoEncrypt;
//                }
//            }

//            string passwordOriginal = entity.Password;
//            string passwordNeedEncrypt = entity.Password + passwordEncrytSalt;

//            entity.PasswordEncrytType = encryptType;

//            switch (encryptType)
//            {
//                case EncryptTypes.HashEncrypt:
//                    entity.PasswordEncrytSalt = passwordEncrytSalt;
//                    entity.Password = EncryptService.SHA1(passwordNeedEncrypt);
//                    break;
//                case EncryptTypes.MD5Encrypt:
//                    entity.PasswordEncrytSalt = passwordEncrytSalt;
//                    entity.Password = EncryptService.MD5(passwordNeedEncrypt);
//                    break;
//                case EncryptTypes.NoEncrypt:
//                default:
//                    entity.PasswordEncrytSalt = string.Empty;
//                    entity.PasswordEncrytType = EncryptTypes.NoEncrypt;
//                    break;
//            }

//            return entity as BusinessUser;
//        }

//        public static BusinessUser Load(SqlDataReader reader)
//        {
//            BusinessUser entity = new BusinessUser();
//            if (reader != null && reader.IsClosed == false)
//            {
//                if (DataReaderHelper.IsExistField(reader, "UserID") && Convert.IsDBNull(reader["UserID"]) == false)
//                {
//                    entity.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserGuid") && Convert.IsDBNull(reader["UserGuid"]) == false)
//                {
//                    entity.UserGuid = reader.GetGuid(reader.GetOrdinal("UserGuid"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserName") && Convert.IsDBNull(reader["UserName"]) == false)
//                {
//                    entity.UserName = reader.GetString(reader.GetOrdinal("UserName"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserCode") && Convert.IsDBNull(reader["UserCode"]) == false)
//                {
//                    entity.UserCode = reader.GetString(reader.GetOrdinal("UserCode"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "Password") && Convert.IsDBNull(reader["Password"]) == false)
//                {
//                    entity.Password = reader.GetString(reader.GetOrdinal("Password"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "PasswordEncrytType") && Convert.IsDBNull(reader["PasswordEncrytType"]) == false)
//                {
//                    entity.PasswordEncrytType = (EncryptTypes)reader.GetInt32(reader.GetOrdinal("PasswordEncrytType"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "PasswordEncrytSalt") && Convert.IsDBNull(reader["PasswordEncrytSalt"]) == false)
//                {
//                    entity.PasswordEncrytSalt = reader.GetString(reader.GetOrdinal("PasswordEncrytSalt"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserNameCN") && Convert.IsDBNull(reader["UserNameCN"]) == false)
//                {
//                    entity.UserNameCN = reader.GetString(reader.GetOrdinal("UserNameCN"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserNameEN") && Convert.IsDBNull(reader["UserNameEN"]) == false)
//                {
//                    entity.UserNameEN = reader.GetString(reader.GetOrdinal("UserNameEN"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "userNameFirst") && Convert.IsDBNull(reader["userNameFirst"]) == false)
//                {
//                    entity.UserNameFirst = reader.GetString(reader.GetOrdinal("userNameFirst"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "userNameLast") && Convert.IsDBNull(reader["userNameLast"]) == false)
//                {
//                    entity.UserNameLast = reader.GetString(reader.GetOrdinal("userNameLast"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserNameMiddle") && Convert.IsDBNull(reader["UserNameMiddle"]) == false)
//                {
//                    entity.UserNameMiddle = reader.GetString(reader.GetOrdinal("UserNameMiddle"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "DepartmentID") && Convert.IsDBNull(reader["DepartmentID"]) == false)
//                {
//                    entity.DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "DepartmentGuid") && Convert.IsDBNull(reader["DepartmentGuid"]) == false)
//                {
//                    entity.DepartmentGuid = reader.GetGuid(reader.GetOrdinal("DepartmentGuid"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "DepartmentCode") && Convert.IsDBNull(reader["DepartmentCode"]) == false)
//                {
//                    entity.DepartmentCode = reader.GetString(reader.GetOrdinal("DepartmentCode"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "AreaCode") && Convert.IsDBNull(reader["AreaCode"]) == false)
//                {
//                    entity.AreaCode = reader.GetString(reader.GetOrdinal("AreaCode"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserEmail") && Convert.IsDBNull(reader["UserEmail"]) == false)
//                {
//                    entity.UserEmail = reader.GetString(reader.GetOrdinal("UserEmail"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserType") && Convert.IsDBNull(reader["UserType"]) == false)
//                {
//                    entity.UserType = (UserTypes)reader.GetInt32(reader.GetOrdinal("UserType"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserStatus") && Convert.IsDBNull(reader["UserStatus"]) == false)
//                {
//                    entity.UserStatus = (UserStatuses)reader.GetInt32(reader.GetOrdinal("UserStatus"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "MaritalStatus") && Convert.IsDBNull(reader["MaritalStatus"]) == false)
//                {
//                    entity.MaritalStatus = (MaritalStatuses)reader.GetInt32(reader.GetOrdinal("MaritalStatus"));
//                }

//                if (DataReaderHelper.IsExistField(reader, "UserRemark") && Convert.IsDBNull(reader["UserRemark"]) == false)
//                {
//                    entity.UserRemark = reader.GetString(reader.GetOrdinal("UserRemark"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserCardID") && Convert.IsDBNull(reader["UserCardID"]) == false)
//                {
//                    entity.UserCardID = reader.GetString(reader.GetOrdinal("UserCardID"));
//                }
//                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserCardIDIssued"))
//                {
//                    entity.UserCardIDIssued = reader.GetString(reader.GetOrdinal("UserCardIDIssued"));
//                }
//                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DriversLicenceNumber"))
//                {
//                    entity.DriversLicenceNumber = reader.GetString(reader.GetOrdinal("DriversLicenceNumber"));
//                }
//                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DriversLicenceNumberIssued"))
//                {
//                    entity.DriversLicenceNumberIssued = reader.GetString(reader.GetOrdinal("DriversLicenceNumberIssued"));
//                }
//                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PassportCode"))
//                {
//                    entity.PassportCode = reader.GetString(reader.GetOrdinal("PassportCode"));
//                }
//                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PassportCodeIssued"))
//                {
//                    entity.PassportCodeIssued = reader.GetString(reader.GetOrdinal("PassportCodeIssued"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserSex") && Convert.IsDBNull(reader["UserSex"]) == false)
//                {
//                    entity.UserSex = (Sexes)reader.GetInt32(reader.GetOrdinal("UserSex"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserBirthDay") && Convert.IsDBNull(reader["UserBirthDay"]) == false)
//                {
//                    entity.UserBirthDay = reader.GetDateTime(reader.GetOrdinal("UserBirthDay"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserMobileNO") && Convert.IsDBNull(reader["UserMobileNO"]) == false)
//                {
//                    entity.UserMobileNO = reader.GetString(reader.GetOrdinal("UserMobileNO"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserRegisterDate") && Convert.IsDBNull(reader["UserRegisterDate"]) == false)
//                {
//                    entity.UserRegisterDate = reader.GetDateTime(reader.GetOrdinal("UserRegisterDate"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserAgreeDate") && Convert.IsDBNull(reader["UserAgreeDate"]) == false)
//                {
//                    entity.UserAgreeDate = reader.GetDateTime(reader.GetOrdinal("UserAgreeDate"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserWorkStartDate") && Convert.IsDBNull(reader["UserWorkStartDate"]) == false)
//                {
//                    entity.UserWorkStartDate = reader.GetDateTime(reader.GetOrdinal("UserWorkStartDate"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserWorkEndDate") && Convert.IsDBNull(reader["UserWorkEndDate"]) == false)
//                {
//                    entity.UserWorkEndDate = reader.GetDateTime(reader.GetOrdinal("UserWorkEndDate"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "CompanyMail") && Convert.IsDBNull(reader["CompanyMail"]) == false)
//                {
//                    entity.CompanyMail = reader.GetString(reader.GetOrdinal("CompanyMail"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserTitle") && Convert.IsDBNull(reader["UserTitle"]) == false)
//                {
//                    entity.UserTitle = reader.GetString(reader.GetOrdinal("UserTitle"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserPosition") && Convert.IsDBNull(reader["UserPosition"]) == false)
//                {
//                    entity.UserPosition = reader.GetString(reader.GetOrdinal("UserPosition"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "WorkTelphone") && Convert.IsDBNull(reader["WorkTelphone"]) == false)
//                {
//                    entity.WorkTelphone = reader.GetString(reader.GetOrdinal("WorkTelphone"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "HomeTelephone") && Convert.IsDBNull(reader["HomeTelephone"]) == false)
//                {
//                    entity.HomeTelephone = reader.GetString(reader.GetOrdinal("HomeTelephone"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserPhoto") && Convert.IsDBNull(reader["UserPhoto"]) == false)
//                {
//                    entity.UserPhoto = reader.GetString(reader.GetOrdinal("UserPhoto"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserMacAddress") && Convert.IsDBNull(reader["UserMacAddress"]) == false)
//                {
//                    entity.UserMacAddress = reader.GetString(reader.GetOrdinal("UserMacAddress"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserLastIP") && Convert.IsDBNull(reader["UserLastIP"]) == false)
//                {
//                    entity.UserLastIP = reader.GetString(reader.GetOrdinal("UserLastIP"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "UserLastDateTime") && Convert.IsDBNull(reader["UserLastDateTime"]) == false)
//                {
//                    entity.UserLastDateTime = reader.GetDateTime(reader.GetOrdinal("UserLastDateTime"));
//                }
//                if (DataReaderHelper.IsExistField(reader, "BrokerKey") && Convert.IsDBNull(reader["BrokerKey"]) == false)
//                {
//                    entity.BrokerKey = reader.GetString(reader.GetOrdinal("BrokerKey"));
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
//        #endregion
//    }
//}
