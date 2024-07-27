//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web.UI;
//using System.IO;
//using System.Web;

//namespace Hiland.BasicLibrary.UI
//{
//    /// <summary>
//    /// 获取User Control生成的HTML代码
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <remarks>
//    /// ViewManager中只有两个方法：LoadViewControl和RenderView。
//    /// LoadViewControl方法的作用是创建一个 Control实例并返回，RenderView方法的作用则就是生成HTML了。
//    /// 这个实现方式的技巧在于使用了一个新建的Page对象作为生成控件的 “容器”，
//    /// 而最后其实我们是将Page对象的整个生命周期运行一遍，并且将结果输出。
//    /// 由于这个空的Page对象不会产生任何其他代码，因此我们得到的，就是用户控件生成的代码了。
//    /// </remarks>
//    public class UserControlViewManager<T> where T : UserControl
//    {
//        private Page pageHolder;

//        public T LoadViewControl(string path)
//        {
//            this.pageHolder = new Page();
//            return (T)this.pageHolder.LoadControl(path);
//        }

//        public string RenderView(T control)
//        {
//            StringWriter output = new StringWriter();

//            this.pageHolder.Controls.Add(control);
//            HttpContext.Current.Server.Execute(this.pageHolder, output, false);

//            return output.ToString();
//        }
//    }
//}
