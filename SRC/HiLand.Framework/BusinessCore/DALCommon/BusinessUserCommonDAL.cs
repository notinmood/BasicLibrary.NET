using System;
using System.Collections.Generic;
using System.Data;
using HiLand.Framework.BusinessCore.DAL;
using HiLand.Framework.BusinessCore.Enum;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.DataBase;
using HiLand.Utility.Enums;
using HiLand.Utility.Paging;
using HiLand.Utility.Security;
using HiLand.Utility.Serialization;
using HiLand.Utility.Setting;

namespace HiLand.Framework.BusinessCore.DALCommon
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransaction"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TDataReader"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public class BusinessUserCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>
        : BaseComputerDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>, IBusinessUserDAL
        where TConnection : class,IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TTransaction : IDbTransaction
        where TDataReader : class, IDataReader
        where TParameter : IDataParameter, IDbDataParameter, new()
    {
        #region 基本信息
        /// <summary>
        /// 判断用户的用户名和EMail是否在系统内存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        public virtual bool IsExistUser(string userName, string userEMail,string userIDCard)
        {
            bool isSuccessful = true;
            isSuccessful = IsExistUserName(userName);
            if (isSuccessful == false)
            {
                return false;
            }

            isSuccessful = IsExistUserEMail(userEMail);
            if (isSuccessful == false)
            {
                return false;
            }

            isSuccessful = IsExistUserIDCard(userIDCard);
            if (isSuccessful == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断用户账号是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual bool IsExistUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            string commnadString = string.Format("select count(1) from [CoreUser] where [UserName]={0}UserName", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserName", userName) };
            return HelperExInstance.IsExist(commnadString, sqlParas);
        }

        /// <summary>
        /// 判断用户的EMail是否存在
        /// </summary>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        public virtual bool IsExistUserEMail(string userEMail)
        {
            if (string.IsNullOrEmpty(userEMail))
            {
                return false;
            }

            string commnadString = string.Format("select count(1) from [CoreUser] where [UserEmail]={0}UserEmail", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserEmail", userEMail) };
            return HelperExInstance.IsExist(commnadString, sqlParas);
        }

        /// <summary>
        /// 判断用户的身份证是否存在
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <returns></returns>
        public virtual bool IsExistUserIDCard(string userIDCard)
        {
            if (string.IsNullOrEmpty(userIDCard))
            {
                return false;
            }

            string commnadString = string.Format("select count(1) from [CoreUser] where [UserCardID]={0}UserCardID", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserCardID", userIDCard) };
            return HelperExInstance.IsExist(commnadString, sqlParas);
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public virtual BusinessUser CreateUser(IBusinessUser entity, out CreateUserRoleStatuses status)
        {
            //在创建实体时如果实体的Guid尚未指定，那么给其赋初值
            if (entity.UserGuid == Guid.Empty)
            {
                entity.UserGuid = GuidHelper.NewGuid();
            }

            //如果未指定注册日期，那么就将当前日期作为其注册日期
            if (entity.UserRegisterDate == DateTimeHelper.Min)
            {
                entity.UserRegisterDate = DateTime.Now;
            }

            bool isExist = true;
            status = CreateUserRoleStatuses.Successful;

            //1.判断用户账号是否存在
            isExist = IsExistUserName(entity.UserName);
            if (isExist == true)
            {
                status = CreateUserRoleStatuses.FailureDuplicateName;
                return null;
            }

            //2.判断用户的EMail是否存在
            bool isEMailMustUnique = Config.GetAppSetting<bool>("isEMailMustUnique", true);
            if (isEMailMustUnique == true)
            {
                isExist = IsExistUserEMail(entity.UserEmail);
                if (isExist == true)
                {
                    status = CreateUserRoleStatuses.FailureDuplicateEMail;
                    return null;
                }
            }

            //3.判断用户的身份证是否存在
            bool isIDCardMustUnique = Config.GetAppSetting<bool>("isIDCardMustUnique", true);
            if (isEMailMustUnique == true)
            {
                isExist = IsExistUserIDCard(entity.UserCardID);
                if (isExist == true)
                {
                    status = CreateUserRoleStatuses.FailureDuplicateIDCard;
                    return null;
                }
            }

            //4.具体创建用户
            string commandText = string.Format(@"INSERT INTO [CoreUser] (
                [UserGuid],
                [UserName],
                [UserCode],
                [Password],
                [PasswordEncrytType],
                [PasswordEncrytSalt],
                [UserNameCN],
                [UserNameEN],
                [userNameFirst],
                [userNameLast],
                [UserNameMiddle],
                [DepartmentID],
                [DepartmentGuid],
                [DepartmentCode],
                [DepartmentUserType],
                [UserFullPath],
                [AreaCode],
                [UserEmail],
                [UserType],
                [UserStatus],
                [MaritalStatus],
                [UserRemark],
                [UserCardID],
                [UserCardIDIssued],
                [DriversLicenceNumber],
                [DriversLicenceNumberIssued],
                [PassportCode],
                [PassportCodeIssued],
                [UserSex],
                [UserBirthDay],
                [UserMobileNO],
                [UserRegisterDate],
                [UserAgreeDate],
                [UserWorkStartDate],
                [UserWorkEndDate],
                [CompanyMail],
                [UserTitle],
                [UserPosition],
                [WorkTelphone],
                [HomeTelephone],
                [UserPhoto],
                [UserMacAddress],
                [UserLastIP],
                [UserLastDateTime],
                [BrokerKey],
                [EnterpriseKey],
                [UserHeight],
                [UserWeight],
                [UserNation],
                [UserCountry],
                [UserEducationalBackground],
                [UserEducationalSchool],
			    [SocialSecurityNumber],
                [PropertyNames],
                [PropertyValues]) 
           VALUES (
                {0}UserGuid,
                {0}UserName,
                {0}UserCode,
                {0}Password,
                {0}PasswordEncrytType,
                {0}PasswordEncrytSalt,
                {0}UserNameCN,
                {0}UserNameEN,
                {0}userNameFirst,
                {0}userNameLast,
                {0}UserNameMiddle,
                {0}DepartmentID,
                {0}DepartmentGuid,
                {0}DepartmentCode,
                {0}DepartmentUserType,
                {0}UserFullPath,
                {0}AreaCode,
                {0}UserEmail,
                {0}UserType,
                {0}UserStatus,
                {0}MaritalStatus,
                {0}UserRemark,
                {0}UserCardID,
                {0}UserCardIDIssued,
                {0}DriversLicenceNumber,
                {0}DriversLicenceNumberIssued,
                {0}PassportCode,
                {0}PassportCodeIssued,
                {0}UserSex,
                {0}UserBirthDay,
                {0}UserMobileNO,
                {0}UserRegisterDate,
                {0}UserAgreeDate,
                {0}UserWorkStartDate,
                {0}UserWorkEndDate,
                {0}CompanyMail,
                {0}UserTitle,
                {0}UserPosition,
                {0}WorkTelphone,
                {0}HomeTelephone,
                {0}UserPhoto,
                {0}UserMacAddress,
                {0}UserLastIP,
                {0}UserLastDateTime,
                {0}BrokerKey,
                {0}EnterpriseKey,
                {0}UserHeight,
                {0}UserWeight,
                {0}UserNation,
                {0}UserCountry,
                {0}UserEducationalBackground,
			    {0}UserEducationalSchool,
			    {0}SocialSecurityNumber,
                {0}PropertyNames,
                {0}PropertyValues)",
            ParameterNamePrefix);

            if (entity.UserGuid == Guid.Empty)
            {
                entity.UserGuid = Guid.NewGuid();
            }
            TParameter[] sqlParas = PrepareParasAll(entity, true);

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

                entity = Get(entity.UserGuid);
            }
            catch
            {
                status = CreateUserRoleStatuses.FailureUnknowReason;
            }

            return entity as BusinessUser;
        }


        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool UpdateUser(IBusinessUser entity)
        {
            string commandText = string.Format(@"Update [CoreUser] Set   
					[UserName] = {0}UserName,
					[UserCode] = {0}UserCode,
					[UserNameCN] = {0}UserNameCN,
					[UserNameEN] = {0}UserNameEN,
					[userNameFirst] = {0}userNameFirst,
					[userNameLast] = {0}userNameLast,
					[UserNameMiddle] = {0}UserNameMiddle,
					[DepartmentID] = {0}DepartmentID,
					[DepartmentGuid] = {0}DepartmentGuid,
					[DepartmentCode] = {0}DepartmentCode,
                    [DepartmentUserType]= {0}DepartmentUserType,
                    [UserFullPath]= {0}UserFullPath,
					[AreaCode] = {0}AreaCode,
					[UserEmail] = {0}UserEmail,
					[UserType] = {0}UserType,
					[UserStatus] = {0}UserStatus,
                    [MaritalStatus]={0}MaritalStatus,
					[UserRemark] = {0}UserRemark,
					[UserCardID] = {0}UserCardID,
                    [UserCardIDIssued] = {0}UserCardIDIssued,
					[DriversLicenceNumber] = {0}DriversLicenceNumber,
					[DriversLicenceNumberIssued] ={0}DriversLicenceNumberIssued,
					[PassportCode] = {0}PassportCode,
					[PassportCodeIssued] = {0}PassportCodeIssued,
					[UserSex] = {0}UserSex,
					[UserBirthDay] = {0}UserBirthDay,
					[UserMobileNO] = {0}UserMobileNO,
					[UserRegisterDate] ={0}UserRegisterDate,
					[UserAgreeDate] = {0}UserAgreeDate,
					[UserWorkStartDate] = {0}UserWorkStartDate,
					[UserWorkEndDate] = {0}UserWorkEndDate,
					[CompanyMail] = {0}CompanyMail,
					[UserTitle] = {0}UserTitle,
                    [UserPosition]= {0}UserPosition,
					[WorkTelphone] = {0}WorkTelphone,
					[HomeTelephone] = {0}HomeTelephone,
					[UserPhoto] = {0}UserPhoto,
					[UserMacAddress] = {0}UserMacAddress,
					[UserLastIP] = {0}UserLastIP,
					[UserLastDateTime] = {0}UserLastDateTime,
					[BrokerKey]= {0}BrokerKey,
                    [EnterpriseKey]={0}EnterpriseKey,
                    [UserHeight]={0}UserHeight,
                    [UserWeight]={0}UserWeight,
                    [UserNation]={0}UserNation,
                    [UserCountry]={0}UserCountry,
                    [UserEducationalBackground]={0}UserEducationalBackground,
				    [UserEducationalSchool] = {0}UserEducationalSchool,
				    [SocialSecurityNumber] = {0}SocialSecurityNumber,
                    [PropertyNames] = {0}PropertyNames,
					[PropertyValues] = {0}PropertyValues
             Where [UserGuid] = {0}UserGuid", ParameterNamePrefix);

            TParameter[] sqlParas = PrepareParasAll(entity, false);

            return HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
        }

        /// <summary>
        /// 变更用户的全路径
        /// </summary>
        /// <param name="originalFullPath"></param>
        /// <param name="newFullpath"></param>
        public bool ChangeFullPath(string originalFullPath, string newFullpath)
        {
            string commandText = string.Format(@"Update [CoreUser] Set   
					UserFullPath= REPLACE(UserFullPath,{0}originalFullPath,{0}newFullpath)
            WHERE UserFullPath like {0}originalFullPathLeftLike", ParameterNamePrefix);

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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public virtual bool DeleteUser(Guid userGuid)
        {
            string commandText = string.Format("DELETE FROM [CoreUser] WHERE [UserGuid] = {0}UserGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserGuid", userGuid) };
            return HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);

            //TODO:xieran20121001 删除用户时需要删除用户关联的信息（比如权限数据）
        }

        /// <summary>
        /// 改变用户的状态
        /// </summary>
        /// <param name="newUserStatus"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public virtual bool SetUserStatus(Guid userGuid, UserStatuses newUserStatus)
        {
            string commandText = string.Format("Update [CoreUser] Set  [UserStatus] = {0}UserStatus WHERE  [UserGuid] = {0}UserGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserStatus", Convert.ToInt32(newUserStatus)), GenerateParameter("UserGuid", userGuid) };
            return HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
        }

        private TParameter[] PrepareParasAll(IBusinessUser entity, bool isIncludePasswordInfo)
        {
            List<TParameter> list = new List<TParameter>(){
                GenerateParameter("UserGuid",entity.UserGuid),
                GenerateParameter("UserName",entity.UserName??string.Empty),
                GenerateParameter("UserCode",entity.UserCode??string.Empty),
                GenerateParameter("UserNameCN",entity.UserNameCN??string.Empty),
                GenerateParameter("UserNameEN",entity.UserNameEN??string.Empty),
                GenerateParameter("userNameFirst",entity.UserNameFirst??string.Empty),
                GenerateParameter("userNameLast",entity.UserNameLast??string.Empty),
                GenerateParameter("UserNameMiddle",entity.UserNameMiddle??string.Empty),
                GenerateParameter("DepartmentID",entity.DepartmentID),
                GenerateParameter("DepartmentGuid",entity.DepartmentGuid),
                GenerateParameter("DepartmentCode",entity.DepartmentCode??string.Empty),
                GenerateParameter("DepartmentUserType",entity.DepartmentUserType),
                GenerateParameter("UserFullPath",entity.UserFullPath),
                GenerateParameter("AreaCode",entity.AreaCode??string.Empty),
                GenerateParameter("UserEmail",entity.UserEmail??string.Empty),
                GenerateParameter("UserType",entity.UserType),
                GenerateParameter("UserStatus",entity.UserStatus),
                GenerateParameter("MaritalStatus",entity.MaritalStatus),
                GenerateParameter("UserRemark",entity.UserRemark??string.Empty),
                GenerateParameter("UserCardID",entity.UserCardID??string.Empty),
                GenerateParameter("UserCardIDIssued",entity.UserCardIDIssued??string.Empty),
			    GenerateParameter("DriversLicenceNumber",entity.DriversLicenceNumber??string.Empty),
			    GenerateParameter("DriversLicenceNumberIssued",entity.DriversLicenceNumberIssued??string.Empty),
			    GenerateParameter("PassportCode",entity.PassportCode??string.Empty),
			    GenerateParameter("PassportCodeIssued",entity.PassportCodeIssued??string.Empty),
                GenerateParameter("UserSex",entity.UserSex),
                GenerateParameter("UserBirthDay",entity.UserBirthDay),
                GenerateParameter("UserMobileNO",entity.UserMobileNO??string.Empty),
                GenerateParameter("UserRegisterDate",entity.UserRegisterDate),
                GenerateParameter("UserAgreeDate",entity.UserAgreeDate),
                GenerateParameter("UserWorkStartDate",entity.UserWorkStartDate),
                GenerateParameter("UserWorkEndDate",entity.UserWorkEndDate),
                GenerateParameter("CompanyMail",entity.CompanyMail??string.Empty),
                GenerateParameter("UserTitle",entity.UserTitle??string.Empty),
                GenerateParameter("UserPosition",entity.UserPosition??string.Empty),
                GenerateParameter("WorkTelphone",entity.WorkTelphone??string.Empty),
                GenerateParameter("HomeTelephone",entity.HomeTelephone??string.Empty),
                GenerateParameter("UserPhoto",entity.UserPhoto??string.Empty),
                GenerateParameter("UserMacAddress",entity.UserMacAddress??string.Empty),
                GenerateParameter("UserLastIP",entity.UserLastIP??string.Empty),
                GenerateParameter("UserLastDateTime",entity.UserLastDateTime),
                GenerateParameter("BrokerKey",entity.BrokerKey??string.Empty),
                GenerateParameter("EnterpriseKey",entity.EnterpriseKey??string.Empty),
                GenerateParameter("UserHeight",entity.UserHeight),
                GenerateParameter("UserWeight",entity.UserWeight),
                GenerateParameter("UserNation",entity.UserNation??string.Empty),
                GenerateParameter("UserCountry",entity.UserCountry??string.Empty),
                GenerateParameter("UserEducationalBackground",entity.UserEducationalBackground),
                GenerateParameter("UserEducationalSchool",entity.UserEducationalSchool?? String.Empty),
			    GenerateParameter("SocialSecurityNumber",entity.SocialSecurityNumber?? String.Empty),
                GenerateParameter("PropertyNames",""),
                GenerateParameter("PropertyValues","")
            };

            if (isIncludePasswordInfo == true)
            {
                list.Add(GenerateParameter("Password", entity.Password ?? string.Empty));
                list.Add(GenerateParameter("PasswordEncrytType", entity.PasswordEncrytType));
                list.Add(GenerateParameter("PasswordEncrytSalt", entity.PasswordEncrytSalt));
            }

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

        /// <summary>
        /// 更新用户的最后访问信息
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="lastIP"></param>
        /// <param name="lastTime"></param>
        public virtual void UpdateLastInfo(Guid userGuid, string lastIP, DateTime lastTime)
        {
            string commandText = string.Format("UPDATE [CoreUser]   SET [UserLastIP] = {0}UserLastIP,[UserLastDateTime] = {0}UserLastDateTime WHERE [UserGuid] = {0}UserGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] {
                GenerateParameter("UserGuid",userGuid),
                GenerateParameter("UserLastIP",lastIP),
                GenerateParameter("UserLastDateTime",lastTime)
            };

            HelperExInstance.ExecuteNonQuery(commandText, sqlParas);
        }

        /// <summary>
        /// 修改用户的口令
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="newPassword"></param>
        /// <param name="passwordEncrytType"></param>
        /// <param name="passwordEncrytSalt"></param>
        public virtual bool ChangePassword(Guid userGuid, string newPassword, int passwordEncrytType, string passwordEncrytSalt)
        {
            string commandText = string.Format("UPDATE [CoreUser]   SET[Password] = {0}Password,[PasswordEncrytType] = {0}PasswordEncrytType,[PasswordEncrytSalt] = {0}PasswordEncrytSalt WHERE [UserGuid] = {0}UserGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] {
                GenerateParameter("UserGuid",userGuid),
                GenerateParameter("Password",newPassword),
                GenerateParameter("PasswordEncrytType",passwordEncrytType),
                GenerateParameter("PasswordEncrytSalt",passwordEncrytSalt)
            };

            return HelperExInstance.ExecuteSingleRowNonQuery(commandText, sqlParas);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userAccount">其可以是用户的UserName,也可以是其EMail</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual BusinessUser Login(string userAccount, string password, out LoginStatuses status)
        {
            BusinessUser entity = null;// IBusinessUser.Empty;
            LoginStatuses statusForLoginWithUserName = LoginStatuses.Successful;
            entity = LoginWithUserName(userAccount, password, out statusForLoginWithUserName);
            if (statusForLoginWithUserName == LoginStatuses.Successful || statusForLoginWithUserName == LoginStatuses.FailureUserDenied)
            {
                status = statusForLoginWithUserName;
                return entity;
            }

            LoginStatuses statusForLoginWithUserEMail = LoginStatuses.Successful;
            entity = LoginWithUserEMail(userAccount, password, out statusForLoginWithUserEMail);
            if (statusForLoginWithUserEMail == LoginStatuses.Successful || statusForLoginWithUserName == LoginStatuses.FailureUserDenied)
            {
                status = statusForLoginWithUserEMail;
                return entity;
            }

            LoginStatuses statusForLoginWithUserIDCard = LoginStatuses.Successful;
            entity = LoginWithUserIDCard(userAccount, password, out statusForLoginWithUserIDCard);
            if (statusForLoginWithUserIDCard == LoginStatuses.Successful || statusForLoginWithUserName == LoginStatuses.FailureUserDenied)
            {
                status = statusForLoginWithUserIDCard;
                return entity;
            }

            if (statusForLoginWithUserName == LoginStatuses.FailureNoName &&
                statusForLoginWithUserEMail == LoginStatuses.FailureNoEMail &&
                statusForLoginWithUserIDCard == LoginStatuses.FailureNoIDCard)
            {
                status = LoginStatuses.FailureNoAccount;
                return entity;
            }

            status = LoginStatuses.FailureNotMatchPassword;
            return entity;
        }

        /// <summary>
        /// 使用用户名称和口令登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual BusinessUser LoginWithUserName(string userName, string password, out LoginStatuses status)
        {
            bool isSuccessful = true;

            string commandText = string.Empty;
            TParameter[] sqlParas = null;

            //1.判断用户名称是否存在
            commandText = string.Format("SELECT COUNT(1) FROM [CoreUser] WHERE [UserName]={0}UserName", ParameterNamePrefix);
            sqlParas = new TParameter[] { GenerateParameter("UserName", userName) };
            isSuccessful = HelperExInstance.IsExist(commandText, sqlParas);
            if (isSuccessful == false)
            {
                status = LoginStatuses.FailureNoName;
                return BusinessUser.Empty;
            }

            //2.判断用户名称跟口令是否匹配  //3.判断用户状态是否已被禁用
            commandText = string.Format("SELECT * FROM [CoreUser] WHERE [UserName]={0}UserName", ParameterNamePrefix);
            sqlParas = new TParameter[] { GenerateParameter("UserName", userName) };
            TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas);
            if (reader != null && reader.Read() == true)
            {
                IBusinessUser entity = Load(reader);
                string passwordInSystem = entity.Password;
                entity.Password = password;
                entity = DealWithPassword(entity);
                string passwordEncrypted = entity.Password;
                if (passwordInSystem != passwordEncrypted)
                {
                    status = LoginStatuses.FailureNotMatchPassword;
                    return BusinessUser.Empty;
                }

                if (entity.UserStatus != UserStatuses.Normal)
                {
                    status = LoginStatuses.FailureUserDenied;
                    return BusinessUser.Empty;
                }
            }

            status = LoginStatuses.Successful;
            return GetByUserName(userName);
        }

        /// <summary>
        /// 使用Email和口令进行登录
        /// </summary>
        /// <param name="userEMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual BusinessUser LoginWithUserEMail(string userEMail, string password, out LoginStatuses status)
        {
            bool isSuccessful = true;

            string commandText = string.Empty;
            TParameter[] sqlParas = null;

            //1.判断用户EMail是否存在
            commandText = string.Format("SELECT COUNT(1) FROM [CoreUser] WHERE [UserEmail]={0}UserEmail", ParameterNamePrefix);
            sqlParas = new TParameter[] { GenerateParameter("UserEmail", userEMail) };
            isSuccessful = HelperExInstance.IsExist(commandText, sqlParas);
            if (isSuccessful == false)
            {
                status = LoginStatuses.FailureNoEMail;
                return BusinessUser.Empty;
            }

            //2.判断用户EMail跟口令是否匹配 //3.判断用户状态是否已被禁用
            commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [UserEmail]={0}UserEmail", ParameterNamePrefix);
            sqlParas = new TParameter[] { GenerateParameter("UserEmail", userEMail) };
            TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas);
            if (reader != null && reader.Read() == true)
            {
                IBusinessUser entity = Load(reader);
                string passwordInSystem = entity.Password;
                entity.Password = password;
                entity = DealWithPassword(entity);
                string passwordEncrypted = entity.Password;
                if (passwordInSystem != passwordEncrypted)
                {
                    status = LoginStatuses.FailureNotMatchPassword;
                    return BusinessUser.Empty;
                }

                if (entity.UserStatus != UserStatuses.Normal)
                {
                    status = LoginStatuses.FailureUserDenied;
                    return BusinessUser.Empty;
                }
            }

            status = LoginStatuses.Successful;
            return GetByUserEMail(userEMail);
        }

        /// <summary>
        /// 使用身份证和口令进行登录
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual BusinessUser LoginWithUserIDCard(string userIDCard, string password, out LoginStatuses status)
        {
            bool isSuccessful = true;

            string commandText = string.Empty;
            TParameter[] sqlParas = null;

            //1.判断用户UserCardID是否存在
            commandText = string.Format("SELECT COUNT(1) FROM [CoreUser] WHERE [UserCardID]={0}UserCardID", ParameterNamePrefix);
            sqlParas = new TParameter[] { GenerateParameter("UserCardID", userIDCard) };
            isSuccessful = HelperExInstance.IsExist(commandText, sqlParas);
            if (isSuccessful == false)
            {
                status = LoginStatuses.FailureNoEMail;
                return BusinessUser.Empty;
            }

            //2.判断用户UserCardID跟口令是否匹配 //3.判断用户状态是否已被禁用
            commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [UserCardID]={0}UserCardID", ParameterNamePrefix);
            sqlParas = new TParameter[] { GenerateParameter("UserCardID", userIDCard) };
            TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas);
            if (reader != null && reader.Read() == true)
            {
                IBusinessUser entity = Load(reader);
                string passwordInSystem = entity.Password;
                entity.Password = password;
                entity = DealWithPassword(entity);
                string passwordEncrypted = entity.Password;
                if (passwordInSystem != passwordEncrypted)
                {
                    status = LoginStatuses.FailureNotMatchPassword;
                    return BusinessUser.Empty;
                }

                if (entity.UserStatus != UserStatuses.Normal)
                {
                    status = LoginStatuses.FailureUserDenied;
                    return BusinessUser.Empty;
                }
            }

            status = LoginStatuses.Successful;
            return GetByUserIDCard(userIDCard);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public virtual BusinessUser Get(Guid userGuid)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [UserGuid] = {0}UserGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserGuid", userGuid) };
            return Get(commandText, sqlParas);
        }

        /// <summary>
        /// 获取用户(根据用户账号)
        /// </summary>
        /// <param name="userAccount">此处的账号可以是用户名或者用户EMail，IDCard</param>
        /// <returns></returns>
        public virtual BusinessUser Get(string userAccount)
        {
            BusinessUser entity = GetByUserName(userAccount);

            if (entity.IsEmpty == true)
            {
                entity = GetByUserEMail(userAccount);
            }

            if (entity.IsEmpty == true)
            {
                entity = GetByUserIDCard(userAccount);
            }

            return entity;
        }

        /// <summary>
        /// 获取用户(根据用户名)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual BusinessUser GetByUserName(string userName)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [UserName] = {0}UserName", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserName", userName) };
            return Get(commandText, sqlParas);
        }

        /// <summary>
        /// 获取用户(根据用户EMail)
        /// </summary>
        /// <param name="userEMail"></param>
        /// <returns></returns>
        public virtual BusinessUser GetByUserEMail(string userEMail)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [UserEmail] = {0}UserEmail", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserEmail", userEMail) };
            return Get(commandText, sqlParas);
        }

        /// <summary>
        /// 获取用户(根据用户IDCard)
        /// </summary>
        /// <param name="userIDCard"></param>
        /// <returns></returns>
        public virtual BusinessUser GetByUserIDCard(string userIDCard)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [UserCardID] = {0}UserCardID", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("UserCardID", userIDCard) };
            return Get(commandText, sqlParas);
        }

        private BusinessUser Get(string commandText, TParameter[] sqlParas)
        {
            BusinessUser entity = BusinessUser.Empty;

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
        /// 获取总的条目
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public virtual int GetTotalCount(string whereClause)
        {
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            string commandText = string.Format("SELECT COUNT(1) FROM [CoreUser] WHERE {0}", whereClause);
            return Convert.ToInt32(HelperExInstance.ExecuteScalar(commandText));
        }

        /// <summary>
        /// 根据部门FullPath获取用户Guid集合
        /// </summary>
        /// <param name="departmentFullPath">部门编码</param>
        /// <param name="isIncludeSubDepartment">是否包含子部门人员</param>
        /// <returns></returns>
        public virtual List<Guid> GetUserGuidsByDepartment(string departmentFullPath, bool isIncludeSubDepartment)
        {
            string commandText = string.Empty;
            TParameter[] sqlParas = null;

            if (isIncludeSubDepartment == true)
            {
                commandText = string.Format("SELECT [UserGuid] FROM [CoreUser] WHERE ( [UserFullPath] like {0}departmentFullPathDirect OR [UserFullPath] like {0}departmentFullPathSub)", ParameterNamePrefix);
                sqlParas = new TParameter[] { 
                    GenerateParameter("departmentFullPathDirect", string.Format("{0}||%",departmentFullPath)),
                    GenerateParameter("departmentFullPathSub", string.Format("{0}//%",departmentFullPath))
                };
            }
            else
            {
                commandText = string.Format("SELECT [UserGuid] FROM [CoreUser] WHERE ( [UserFullPath] like {0}departmentFullPathDirect )", ParameterNamePrefix);
                sqlParas = new TParameter[] { 
                    GenerateParameter("departmentFullPathDirect", string.Format("{0}||%",departmentFullPath))
                };
            }

            List<Guid> guidList = new List<Guid>();
            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Guid userGuid = Guid.Empty;
                        if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserGuid"))
                        {
                            userGuid = reader.GetGuid(reader.GetOrdinal("UserGuid"));
                        }

                        if (userGuid != Guid.Empty)
                        {
                            guidList.Add(userGuid);
                        }
                    }
                }
            }

            return guidList;
        }

        /// <summary>
        /// 根据部门FullPath获取用户集合
        /// </summary>
        /// <param name="departmentFullPath">部门编码</param>
        /// <param name="isIncludeSubDepartment">是否包含子部门人员</param>
        /// <returns></returns>
        public virtual List<BusinessUser> GetUsersByDepartment(string departmentFullPath, bool isIncludeSubDepartment)
        {
            string commandText = string.Empty;
            TParameter[] sqlParas = null;

            if (isIncludeSubDepartment == true)
            {
                commandText = string.Format("SELECT * FROM [CoreUser] WHERE ( [UserFullPath] like {0}departmentFullPathDirect OR [UserFullPath] like {0}departmentFullPathSub)", ParameterNamePrefix);
                sqlParas = new TParameter[] { 
                    GenerateParameter("departmentFullPathDirect", string.Format("{0}||%",departmentFullPath)),
                    GenerateParameter("departmentFullPathSub", string.Format("{0}//%",departmentFullPath))
                };
            }
            else
            {
                commandText = string.Format("SELECT * FROM [CoreUser] WHERE ( [UserFullPath] like {0}departmentFullPathDirect )", ParameterNamePrefix);
                sqlParas = new TParameter[] { 
                    GenerateParameter("departmentFullPathDirect", string.Format("{0}||%",departmentFullPath))
                };
            }

            return GetUsers(commandText, sqlParas);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentCode">部门编码</param>
        /// <returns></returns>
        public virtual List<BusinessUser> GetUsersByDepartment(string departmentCode)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [DepartmentCode] = {0}DepartmentCode", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("DepartmentCode", departmentCode) };

            return GetUsers(commandText, sqlParas);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public virtual List<BusinessUser> GetUsersByDepartment(int departmentID)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [DepartmentID] = {0}DepartmentID", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("DepartmentID", departmentID) };

            return GetUsers(commandText, sqlParas);
        }

        /// <summary>
        /// 根据部门获取用户集合
        /// </summary>
        /// <param name="departmentGuid">部门GUID</param>
        /// <returns></returns>
        public virtual List<BusinessUser> GetUsersByDepartment(Guid departmentGuid)
        {
            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE  [DepartmentGuid] = {0}DepartmentGuid", ParameterNamePrefix);
            TParameter[] sqlParas = new TParameter[] { GenerateParameter("DepartmentGuid", departmentGuid) };

            return GetUsers(commandText, sqlParas);
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        public virtual List<BusinessUser> GetList(string whereClause)
        {
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = " 1=1 ";
            }

            string commandText = string.Format("SELECT * FROM [CoreUser] WHERE {0}", whereClause);

            return GetUsers(commandText, null);
        }

        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <param name="commandText">sql命令文本</param>
        /// <param name="sqlParas">sql参数数组</param>
        /// <returns></returns>
        private static List<BusinessUser> GetUsers(string commandText, TParameter[] sqlParas)
        {
            List<BusinessUser> userList = new List<BusinessUser>();
            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        BusinessUser entity = Load(reader);
                        userList.Add(entity);
                    }
                }
            }

            return userList;
        }

        /// <summary>
        /// 获取一批用户
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <returns></returns>
        public virtual PagedEntityCollection<BusinessUser> GetPagedCollection(int startIndex, int endIndex, string whereClause, string orderClause)
        {
            PagedEntityCollection<BusinessUser> collection = new PagedEntityCollection<BusinessUser>();
            using (TDataReader reader = HelperExInstance.ExecuteReaderBySP("usp_Core_User_SelectPaging", CommonGeneralInstance.PrepareParameterPaging(startIndex, endIndex, whereClause, orderClause)))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        BusinessUser entity = Load(reader);
                        collection.Records.Add(entity);
                    }
                }
            }

            return collection;
        }

        #endregion

        #region 角色
        /// <summary>
        /// 获取用户所拥有的角色
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public virtual List<BusinessRole> GetRoles(Guid userGuid)
        {
            List<BusinessRole> roleList = new List<BusinessRole>();

            string commandText = "SELECT r.* FROM [CoreUserInRole] as rs left join [CoreRole] r ON rs.RoleGuid= r.RoleGuid WHERE rs.[UserGuid] = @UserGuid ";
            TParameter[] sqlParas = new TParameter[] 
            { 
                GenerateParameter("UserGuid", userGuid)
            };

            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        BusinessRole entity = BusinessRoleCommonDAL<TTransaction, TConnection, TCommand, TDataReader, TParameter>.Load(reader);
                        if (entity != null)
                        {
                            roleList.Add(entity);
                        }
                    }
                }
            }

            return roleList;
        }

        /// <summary>
        /// 更新用户所属的角色
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="roleGuidList"></param>
        public virtual void UpdateUserRoles(Guid userGuid, List<Guid> roleGuidList)
        {
            string deleteClause = string.Format("DELETE FROM [CoreUserInRole]  WHERE [UserGuid]='{0}' ;", userGuid);
            string insertClause = string.Empty;
            foreach (Guid roleGuid in roleGuidList)
            {
                insertClause += string.Format(@"INSERT INTO [CoreUserInRole] ([UserGuid] ,[RoleGuid]) VALUES ( '{0}','{1}');", userGuid, roleGuid);
            }

            string commondText = deleteClause + insertClause;
            HelperExInstance.ExecuteNonQuery(commondText);
        }
        #endregion

        #region 辅助方法
        //TODO:xieran20121108 口令处理这个地方考虑加一个重载，参数为口令，口令加密算法，口令加密盐的简单实体
        
        /// <summary>
        /// 处理用户口令的加密逻辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static BusinessUser DealWithPassword(IBusinessUser entity)
        {
            string passwordEncrytSalt = entity.PasswordEncrytSalt;
            if (string.IsNullOrEmpty(entity.PasswordEncrytSalt))
            {
                passwordEncrytSalt = RandomHelper.GetRandomString(CharCategories.NumberAndChar, 6);
            }

            EncryptTypes encryptType = entity.PasswordEncrytType;//.NoEncrypt;
            if (encryptType == EncryptTypes.UnSet)
            {
                string encryptTypeString = Config.GetAppSetting("userPaswordEncryptType");
                if (string.IsNullOrEmpty(encryptTypeString) == false)
                {
                    encryptTypeString = encryptTypeString.ToLower();
                    switch (encryptTypeString)
                    {
                        case "md5":
                        case "md5encrypt":
                            encryptType = EncryptTypes.MD5Encrypt;
                            break;
                        case "hash":
                        case "hashencrypt":
                            encryptType = EncryptTypes.HashEncrypt;
                            break;
                        default:
                            encryptType = EncryptTypes.NoEncrypt;
                            break;
                    }
                }
                else
                {
                    encryptType = EncryptTypes.NoEncrypt;
                }
            }

            string passwordOriginal = entity.Password;
            string passwordNeedEncrypt = entity.Password + passwordEncrytSalt;

            entity.PasswordEncrytType = encryptType;

            switch (encryptType)
            {
                case EncryptTypes.HashEncrypt:
                    entity.PasswordEncrytSalt = passwordEncrytSalt;
                    entity.Password = EncryptService.SHA1(passwordNeedEncrypt);
                    break;
                case EncryptTypes.MD5Encrypt:
                    entity.PasswordEncrytSalt = passwordEncrytSalt;
                    entity.Password = EncryptService.MD5(passwordNeedEncrypt);
                    break;
                case EncryptTypes.NoEncrypt:
                default:
                    entity.PasswordEncrytSalt = string.Empty;
                    entity.PasswordEncrytType = EncryptTypes.NoEncrypt;
                    break;
            }

            return entity as BusinessUser;
        }

        public static BusinessUser Load(TDataReader reader)
        {
            BusinessUser entity = new BusinessUser();
            if (reader != null && reader.IsClosed == false)
            {
                if (DataReaderHelper.IsExistField(reader, "UserID") && Convert.IsDBNull(reader["UserID"]) == false)
                {
                    entity.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserGuid") && Convert.IsDBNull(reader["UserGuid"]) == false)
                {
                    entity.UserGuid = reader.GetGuid(reader.GetOrdinal("UserGuid"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserName") && Convert.IsDBNull(reader["UserName"]) == false)
                {
                    entity.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserCode") && Convert.IsDBNull(reader["UserCode"]) == false)
                {
                    entity.UserCode = reader.GetString(reader.GetOrdinal("UserCode"));
                }
                if (DataReaderHelper.IsExistField(reader, "Password") && Convert.IsDBNull(reader["Password"]) == false)
                {
                    entity.Password = reader.GetString(reader.GetOrdinal("Password"));
                }
                if (DataReaderHelper.IsExistField(reader, "PasswordEncrytType") && Convert.IsDBNull(reader["PasswordEncrytType"]) == false)
                {
                    entity.PasswordEncrytType = (EncryptTypes)reader.GetInt32(reader.GetOrdinal("PasswordEncrytType"));
                }
                if (DataReaderHelper.IsExistField(reader, "PasswordEncrytSalt") && Convert.IsDBNull(reader["PasswordEncrytSalt"]) == false)
                {
                    entity.PasswordEncrytSalt = reader.GetString(reader.GetOrdinal("PasswordEncrytSalt"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserNameCN") && Convert.IsDBNull(reader["UserNameCN"]) == false)
                {
                    entity.UserNameCN = reader.GetString(reader.GetOrdinal("UserNameCN"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserNameEN") && Convert.IsDBNull(reader["UserNameEN"]) == false)
                {
                    entity.UserNameEN = reader.GetString(reader.GetOrdinal("UserNameEN"));
                }
                if (DataReaderHelper.IsExistField(reader, "userNameFirst") && Convert.IsDBNull(reader["userNameFirst"]) == false)
                {
                    entity.UserNameFirst = reader.GetString(reader.GetOrdinal("userNameFirst"));
                }
                if (DataReaderHelper.IsExistField(reader, "userNameLast") && Convert.IsDBNull(reader["userNameLast"]) == false)
                {
                    entity.UserNameLast = reader.GetString(reader.GetOrdinal("userNameLast"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserNameMiddle") && Convert.IsDBNull(reader["UserNameMiddle"]) == false)
                {
                    entity.UserNameMiddle = reader.GetString(reader.GetOrdinal("UserNameMiddle"));
                }
                if (DataReaderHelper.IsExistField(reader, "DepartmentID") && Convert.IsDBNull(reader["DepartmentID"]) == false)
                {
                    entity.DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID"));
                }
                if (DataReaderHelper.IsExistField(reader, "DepartmentGuid") && Convert.IsDBNull(reader["DepartmentGuid"]) == false)
                {
                    entity.DepartmentGuid = reader.GetGuid(reader.GetOrdinal("DepartmentGuid"));
                }
                if (DataReaderHelper.IsExistField(reader, "DepartmentCode") && Convert.IsDBNull(reader["DepartmentCode"]) == false)
                {
                    entity.DepartmentCode = reader.GetString(reader.GetOrdinal("DepartmentCode"));
                }

                if (DataReaderHelper.IsExistField(reader, "DepartmentUserType") && Convert.IsDBNull(reader["DepartmentUserType"]) == false)
                {
                    entity.DepartmentUserType = (DepartmentUserTypes)reader.GetInt32(reader.GetOrdinal("DepartmentUserType"));
                }

                if (DataReaderHelper.IsExistField(reader, "UserFullPath") && Convert.IsDBNull(reader["UserFullPath"]) == false)
                {
                    entity.UserFullPath = reader.GetString(reader.GetOrdinal("UserFullPath"));
                }

                if (DataReaderHelper.IsExistField(reader, "AreaCode") && Convert.IsDBNull(reader["AreaCode"]) == false)
                {
                    entity.AreaCode = reader.GetString(reader.GetOrdinal("AreaCode"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserEmail") && Convert.IsDBNull(reader["UserEmail"]) == false)
                {
                    entity.UserEmail = reader.GetString(reader.GetOrdinal("UserEmail"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserType") && Convert.IsDBNull(reader["UserType"]) == false)
                {
                    entity.UserType = (UserTypes)reader.GetInt32(reader.GetOrdinal("UserType"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserStatus") && Convert.IsDBNull(reader["UserStatus"]) == false)
                {
                    entity.UserStatus = (UserStatuses)reader.GetInt32(reader.GetOrdinal("UserStatus"));
                }
                if (DataReaderHelper.IsExistField(reader, "MaritalStatus") && Convert.IsDBNull(reader["MaritalStatus"]) == false)
                {
                    entity.MaritalStatus = (MaritalStatuses)reader.GetInt32(reader.GetOrdinal("MaritalStatus"));
                }

                if (DataReaderHelper.IsExistField(reader, "UserRemark") && Convert.IsDBNull(reader["UserRemark"]) == false)
                {
                    entity.UserRemark = reader.GetString(reader.GetOrdinal("UserRemark"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserCardID") && Convert.IsDBNull(reader["UserCardID"]) == false)
                {
                    entity.UserCardID = reader.GetString(reader.GetOrdinal("UserCardID"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserCardIDIssued"))
                {
                    entity.UserCardIDIssued = reader.GetString(reader.GetOrdinal("UserCardIDIssued"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DriversLicenceNumber"))
                {
                    entity.DriversLicenceNumber = reader.GetString(reader.GetOrdinal("DriversLicenceNumber"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "DriversLicenceNumberIssued"))
                {
                    entity.DriversLicenceNumberIssued = reader.GetString(reader.GetOrdinal("DriversLicenceNumberIssued"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PassportCode"))
                {
                    entity.PassportCode = reader.GetString(reader.GetOrdinal("PassportCode"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "PassportCodeIssued"))
                {
                    entity.PassportCodeIssued = reader.GetString(reader.GetOrdinal("PassportCodeIssued"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserSex") && Convert.IsDBNull(reader["UserSex"]) == false)
                {
                    entity.UserSex = (Sexes)reader.GetInt32(reader.GetOrdinal("UserSex"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserBirthDay") && Convert.IsDBNull(reader["UserBirthDay"]) == false)
                {
                    entity.UserBirthDay = reader.GetDateTime(reader.GetOrdinal("UserBirthDay"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserMobileNO") && Convert.IsDBNull(reader["UserMobileNO"]) == false)
                {
                    entity.UserMobileNO = reader.GetString(reader.GetOrdinal("UserMobileNO"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserRegisterDate") && Convert.IsDBNull(reader["UserRegisterDate"]) == false)
                {
                    entity.UserRegisterDate = reader.GetDateTime(reader.GetOrdinal("UserRegisterDate"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserAgreeDate") && Convert.IsDBNull(reader["UserAgreeDate"]) == false)
                {
                    entity.UserAgreeDate = reader.GetDateTime(reader.GetOrdinal("UserAgreeDate"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserWorkStartDate") && Convert.IsDBNull(reader["UserWorkStartDate"]) == false)
                {
                    entity.UserWorkStartDate = reader.GetDateTime(reader.GetOrdinal("UserWorkStartDate"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserWorkEndDate") && Convert.IsDBNull(reader["UserWorkEndDate"]) == false)
                {
                    entity.UserWorkEndDate = reader.GetDateTime(reader.GetOrdinal("UserWorkEndDate"));
                }
                if (DataReaderHelper.IsExistField(reader, "CompanyMail") && Convert.IsDBNull(reader["CompanyMail"]) == false)
                {
                    entity.CompanyMail = reader.GetString(reader.GetOrdinal("CompanyMail"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserTitle") && Convert.IsDBNull(reader["UserTitle"]) == false)
                {
                    entity.UserTitle = reader.GetString(reader.GetOrdinal("UserTitle"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserPosition") && Convert.IsDBNull(reader["UserPosition"]) == false)
                {
                    entity.UserPosition = reader.GetString(reader.GetOrdinal("UserPosition"));
                }
                if (DataReaderHelper.IsExistField(reader, "WorkTelphone") && Convert.IsDBNull(reader["WorkTelphone"]) == false)
                {
                    entity.WorkTelphone = reader.GetString(reader.GetOrdinal("WorkTelphone"));
                }
                if (DataReaderHelper.IsExistField(reader, "HomeTelephone") && Convert.IsDBNull(reader["HomeTelephone"]) == false)
                {
                    entity.HomeTelephone = reader.GetString(reader.GetOrdinal("HomeTelephone"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserPhoto") && Convert.IsDBNull(reader["UserPhoto"]) == false)
                {
                    entity.UserPhoto = reader.GetString(reader.GetOrdinal("UserPhoto"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserMacAddress") && Convert.IsDBNull(reader["UserMacAddress"]) == false)
                {
                    entity.UserMacAddress = reader.GetString(reader.GetOrdinal("UserMacAddress"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserLastIP") && Convert.IsDBNull(reader["UserLastIP"]) == false)
                {
                    entity.UserLastIP = reader.GetString(reader.GetOrdinal("UserLastIP"));
                }
                if (DataReaderHelper.IsExistField(reader, "UserLastDateTime") && Convert.IsDBNull(reader["UserLastDateTime"]) == false)
                {
                    entity.UserLastDateTime = reader.GetDateTime(reader.GetOrdinal("UserLastDateTime"));
                }
                if (DataReaderHelper.IsExistField(reader, "BrokerKey") && Convert.IsDBNull(reader["BrokerKey"]) == false)
                {
                    entity.BrokerKey = reader.GetString(reader.GetOrdinal("BrokerKey"));
                }
                if (DataReaderHelper.IsExistField(reader, "EnterpriseKey") && Convert.IsDBNull(reader["EnterpriseKey"]) == false)
                {
                    entity.EnterpriseKey = reader.GetString(reader.GetOrdinal("EnterpriseKey"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserHeight"))
                {
                    entity.UserHeight = reader.GetDecimal(reader.GetOrdinal("UserHeight"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserWeight"))
                {
                    entity.UserWeight = reader.GetDecimal(reader.GetOrdinal("UserWeight"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserNation"))
                {
                    entity.UserNation = reader.GetString(reader.GetOrdinal("UserNation"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserCountry"))
                {
                    entity.UserCountry = reader.GetString(reader.GetOrdinal("UserCountry"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserEducationalBackground"))
                {
                    entity.UserEducationalBackground = (EducationalBackgrounds)reader.GetInt32(reader.GetOrdinal("UserEducationalBackground"));
                }

                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "UserEducationalSchool"))
                {
                    entity.UserEducationalSchool = reader.GetString(reader.GetOrdinal("UserEducationalSchool"));
                }
                if (DataReaderHelper.IsExistFieldAndNotNull(reader, "SocialSecurityNumber"))
                {
                    entity.SocialSecurityNumber = reader.GetString(reader.GetOrdinal("SocialSecurityNumber"));
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
        #endregion
    }
}
