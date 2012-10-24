using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.DynamicProxy;
using HiLand.Utility.AOP.Interceptor;
using HiLand.Utility.Data;

namespace WebApplicationConsole.GeneralValidateDemo.ModuleB.ModuleB1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProxyGenerator proxy = new ProxyGenerator();
            //1.测试保存前转换注入信息
            //Test t = proxy.CreateClassProxy<Test>(new SQLInjectionSaveBeforeInterceptor());
            //string s= t.Foo(8,"ss--sss");
            //s = s;
            
            //2.测试载入后转换注入信息
            //Test t = proxy.CreateClassProxy<Test>(new SQLInjectionLoadAfterInterceptor());
            //string s= t.Foo(9,"sssss");
            //s = s;

            //3.同时测试保存前和载入后转换注入信息
            //Test t = proxy.CreateClassProxy<Test>(new SQLInjectionSaveBeforeInterceptor(),new SQLInjectionLoadAfterInterceptor());
            //string s = t.Foo(8, "ss--sss");
            //s = s;

            Test t = new Test();
            t.Woo = "qingdao";
            Test p= SQLInjectionHelper.GetSafeEntityBeforeSave<Test>(t);
            Test w = p;

        }
    }

    public class Test
    {
        public virtual string Foo(int i, string s)
        {
            return s+ i.ToString();
        }

        private string woo = string.Empty;
        public string Woo 
        {
            get { return this.woo; }
            set { this.woo = value; }
        }
    }
}