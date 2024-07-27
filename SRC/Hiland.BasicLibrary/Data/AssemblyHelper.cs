using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 应用程序集操作辅助类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 获取程序集的编译时间
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果要正确获取程序集的编译时间，则必须设置如下格式的信息[assembly: AssemblyVersion("1.0.*")]
        /// 即版本的最后两位使用*标示。（通常情况下版本的格式为[assembly: AssemblyVersion("1.0.0.0")]）
        /// </remarks>
        public static DateTime GetCompiledTime(Assembly assembly)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            Version version = assembly.GetName().Version;

            return new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
        }
    }
}
