using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HiLand.Utility4.MVC.Engine
{
    /// <summary>
    /// 为Razor的视图引擎扩展view的查找路径功能
    /// </summary>
    /// <example>
    /// 使用的时候，要放在MVC项目根目录的Global.asax.cs的Application_Start()方法中
    /// 具体如下：
    ///     ExtendedRazorViewEngine engine = new ExtendedRazorViewEngine();
    ///     engine.AddViewLocationFormat("~/Views/AA/{1}/{0}.cshtml"); //自定义的视图路径
    ///     engine.AddPartialViewLocationFormat("~/Views/AA/{1}/{0}.cshtml"); //自定义的部分视图路径
    ///
    ///     ViewEngines.Engines.Clear();
    ///     ViewEngines.Engines.Add(engine);
    /// </example>
    public class ExtendedRazorViewEngine : RazorViewEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        public void AddViewLocationFormat(string paths)
        {
            List<string> existingPaths = new List<string>(ViewLocationFormats);
            existingPaths.Add(paths);

            ViewLocationFormats = existingPaths.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        public void AddPartialViewLocationFormat(string paths)
        {
            List<string> existingPaths = new List<string>(PartialViewLocationFormats);
            existingPaths.Add(paths);

            PartialViewLocationFormats = existingPaths.ToArray();
        }
    }
}
