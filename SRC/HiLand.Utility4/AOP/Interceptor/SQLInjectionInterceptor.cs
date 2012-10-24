//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Castle.DynamicProxy;
//using System.Reflection;
//using HiLand.Utility.Data;

//namespace HiLand.Utility4.AOP.Interceptor
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <remarks>
//    /// 本类的实现方式跟“HiLand.Utility.AOP.Interceptor”命名空间下的SQLInjectionBeforeSaveInterceptor 相同，
//    /// 所不一样的是，现在这个类型使用的是为dotNET4版本的Castle.DynamicProxy，而原来那个使用的是dotNET2.0版本的Castle.DynamicProxy2
//    /// 即是说：如果要创建4.0类型的应用程序，请使用本接获器；如果是创建2.0类型程序，请使用另外一个接获器。
//    /// </remarks>
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
