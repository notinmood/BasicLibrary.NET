using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Interceptor;

namespace HiLand.Utility.AOP.Interceptor
{
    /// <summary>
    /// 
    /// </summary>
    public class EmptyObjectInterceptor : IInterceptor
    {
        /// <summary>
        /// EmptyObject模式的动态代理拦截器
        /// </summary>
        /// <param name="invocation"></param>
        /// <remarks>
        /// 由于很多对空模式的处理都没有涉及到有返回值的方法,本逻辑亦未能处理(如果一个方法有返回值就直接调用远对象的方法)
        /// 但是,对于没有返回值的方法,本逻辑进行了拦截,拦截后不处理任何事情.
        /// (关于空模式的具体使用方法请参考本目录下的reamde.txt文件中的demo)
        /// </remarks>
        public void Intercept(IInvocation invocation)
        {
            Type T = invocation.Method.ReturnType;
            if (T.FullName == "System.Void")
            {
                //do nothing;
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
