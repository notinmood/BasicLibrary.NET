using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HiLand.Framework.WindowsFormsConsole.Cache;
using HiLand.Framework.WindowsFormsConsole.Tests;

namespace HiLand.Framework.WindowsFormsConsole
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CacheTest());
            //Application.Run(new CurrentUserTest());
            Application.Run(new TextBox中Validating事件验证());
        }
    }
}
