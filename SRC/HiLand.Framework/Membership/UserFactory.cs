using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HiLand.Utility.Setting;
using HiLand.Utility.Cache;

namespace HiLand.Framework.Membership
{
    public class UserFactory
    {
        public static IUser CreateUser(string userName)
        {
            IUser user = CacheHelper.Access<string, IUser>(CoreCacheKeys.GetUserByNameKey(userName), CacheHelper.TenMintues, CreateUserDetails, userName);
            return user;
        }

        private static IUser CreateUserDetails(string userName)
        {
            Type userClassType = CacheHelper.Access<Type>(CoreCacheKeys.GetUserClassTypeKey(), CacheHelper.MaxMintues, GetUserClassType);
            return Activator.CreateInstance(userClassType, new object[] { userName }) as IUser;
        }

        private static Type GetUserClassType()
        {
            string userClassTypeString = Config.GetAppSetting("userClassType");
            
            if (string.IsNullOrEmpty(userClassTypeString))
            {
                //userClassTypeString = "HiLand.Framework.BusinessCore.BusinessUser,HiLand.Framework";
            }

            Type userClassType =  Type.GetType(userClassTypeString);
            
            return userClassType;
        }
    }
}
