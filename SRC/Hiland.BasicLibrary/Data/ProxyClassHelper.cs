﻿//using Castle.DynamicProxy;
//using Hiland.BasicLibrary.AOP.Interceptor;
//using Hiland.BasicLibrary.Entity;

//namespace Hiland.BasicLibrary.Data
//{
//    /// <summary>
//    /// 代理类操作辅助类
//    /// </summary>
//    public class ProxyClassHelper
//    {
//        /// <summary>
//        /// 获取空实体对象
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <returns></returns>
//        public static TEntity GetEmpty<TEntity>()
//            where TEntity : class,IEmpty
//        {
//                ProxyGenerator proxy = new ProxyGenerator();
//                TEntity empty = proxy.CreateClassProxy<TEntity>(new EmptyObjectInterceptor());
//                empty.IsEmpty = true;

//                return empty;
//        }
//    }
//}
