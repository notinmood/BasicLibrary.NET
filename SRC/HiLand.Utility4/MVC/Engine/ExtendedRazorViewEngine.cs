using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HiLand.Utility4.MVC.Engine
{
    /// <summary>
    /// 为Razor的视图引擎扩展view的查找路径功能
    /// 添加视图引擎搜索路径有两种方式
    /// 1、在web.config的appSetting内配置
    ///     -ViewSearchPath.Full.x    完全视图搜索路径（x可以是任何值，可以有多条，但x不要重复）
    ///     -ViewSearchPath.Partial.y 部分视图搜索路径（y可以是任何值，可以有多条，但y不要重复）
    /// 2、调用本对象的方法配置路径
    ///     -engine.AddViewLocationFormat("~/Views/AA/{1}/{0}.cshtml");        //自定义的完全视图路径
    ///     -engine.AddPartialViewLocationFormat("~/Views/AA/{1}/{0}.cshtml"); //自定义的部分视图路径
    /// </summary>
    /// <example>
    /// 使用的时候，要放在MVC项目根
    /// 目录的Global.asax.cs的Application_Start()方法中
    /// 具体如下：
    ///     ExtendedRazorViewEngine engine = new ExtendedRazorViewEngine();
    ///     engine.AddViewLocationFormat("~/Views/AA/{1}/{0}.cshtml"); //自定义的视图路径
    ///     engine.AddPartialViewLocationFormat("~/Views/AA/{1}/{0}.cshtml"); //自定义的部分视图路径
    ///
    ///     engine.Act();
    /// </example>
    public class ExtendedRazorViewEngine : RazorViewEngine
    {
        /// <summary>
        /// 
        /// </summary>
        public ExtendedRazorViewEngine() {
            var appSettings = ConfigurationManager.AppSettings;
            ;
            foreach (var key in appSettings.AllKeys)
            {
                if (key.StartsWith("ViewSearchPath.Full.")) {
                    AddViewLocationFormat(appSettings[key]);
                }

                if (key.StartsWith("ViewSearchPath.Partial."))
                {
                    AddPartialViewLocationFormat(appSettings[key]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void AddViewLocationFormat(string path)
        {   
            List<string> existingPaths = new List<string>(ViewLocationFormats);
            existingPaths.Add(path);

            ViewLocationFormats = existingPaths.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void AddPartialViewLocationFormat(string path)
        {
            List<string> existingPaths = new List<string>(PartialViewLocationFormats);
            existingPaths.Add(path);

            PartialViewLocationFormats = existingPaths.ToArray();
        }

        /// <summary>
        /// 让新的视图引擎生效
        /// </summary>
        public void Act() {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(this);
        }
    }
}
