//using System;
//using System.Collections.Generic;
//using System.Text;
//using Castle.Core.Interceptor;
//using System.Reflection;
//using Hiland.BasicLibrary.Data;

//namespace Hiland.BasicLibrary.AOP.Interceptor
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class SQLInjectionBeforeSaveInterceptor : IInterceptor
//    {
//        public void Intercept(IInvocation invocation)
//        {
//            ParameterInfo[] paras = invocation.Method.GetParameters();
//            int argLength = paras.Length;
//            if (argLength > 0)
//            {
//                for (int i = 0; i < argLength; i++)
//                {
//                    ParameterInfo para = paras[i];
//                    Type paraType = para.ParameterType;
//                    if (paraType == typeof(String))
//                    {
//                        string paraOriginalValue = Convert.ToString(invocation.GetArgumentValue(i));
//                        string paraConveredValue = SQLInjectionHelper.GetSafeSqlBeforeSave(paraOriginalValue);
//                        invocation.SetArgumentValue(i, paraConveredValue);
//                    }
//                }
//            }

//            invocation.Proceed();
//        }
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    public class SQLInjectionAfterLoadInterceptor : IInterceptor
//    {
//        public void Intercept(IInvocation invocation)
//        {
//            invocation.Proceed();
            
//            Type returnType = invocation.Method.ReturnType;
//            if (returnType == typeof(String))
//            {
//                string returnOriginalValue = Convert.ToString(invocation.ReturnValue);
//                string returnConveredValue = SQLInjectionHelper.RecoverOriginalSqlAfterLoad(returnOriginalValue);
//                invocation.ReturnValue = returnConveredValue;
//            }
//        }
//    }
//}
