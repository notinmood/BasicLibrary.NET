using System;
using System.IO;
using System.Web;
using HiLand.Utility.Setting;

namespace HiLand.Utility.Handler.Ajax
{
    /* 可以设置的配置项
     * ·AjaxHandlerTypeDescriptionPrefix 表示需要调用方法所在类型的前缀命名空间（可以省略，但是需要在URL中指定全名称）
     * ·AjaxHandlerTypeDescriptionDllName 表示需要调用方法所在的DLL的名称（可以省略，但是需要在URL中包含这个dll的名称）
     * 
     * 具体使用可以参考以下三种方法（\SampleConsoleWebApplication\HLAjaxHandlerDemo\TestPage.htm）
     * 无论哪种方法的，都必须保证url的第一个部分为识别AjaxHandler的路径，最后一个部分为方法的名称；中间的部分为对类型的识别。
     * //方法1.
                $.post("/HLAjaxHandlerDemo/WebApplicationConsole.HLAjaxHandlerDemo.Student,WebApplicationConsole/Add.aspx", { x: $("input[name='x']").val(), y: $("input[name='y']").val() }, function (result) {
                    $("#result").text(result);
                }, "text");

        //方法2.
                $.post("/HLAjaxHandlerDemo/WebApplicationConsole.HLAjaxHandlerDemo.Student/Add.aspx", { x: $("input[name='x']").val(), y: $("input[name='y']").val() }, function (result) {
                    $("#result").text(result);
                }, "text");

        //方法3.
                $.post("/HLAjaxHandlerDemo/WebApplicationConsole/HLAjaxHandlerDemo/Student/Add.aspx", { x: $("input[name='x']").val(), y: $("input[name='y']").val() }, function (result) {
                    $("#result").text(result);
                }, "text");
     */

    /// <summary>
    /// 允许前端Javascript中调用C#方法的Handler
    /// </summary>
    public class AjaxHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //根据请求的Url,通过反射取得处理该请求的类
            string requestUrl = context.Request.Url.AbsolutePath;
            string[] urlPartArray = requestUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string typeDescriptionPrefix = Config.GetAppSetting("AjaxHandlerTypeDescriptionPrefix");
            string typeDescriptionDllName = Config.GetAppSetting("AjaxHandlerTypeDescriptionDllName");
            string typeDescription = typeDescriptionPrefix;

            //不从第0个位置开始取值，是因为第0个文字是一个用来进行选择Handler的信息；不取最后一个值，是因为最后一个值为方法名称
            for (int x = 1; x < urlPartArray.Length - 1; x++)
            {
                typeDescription += "." + urlPartArray[x];
            }

            if (typeDescription.StartsWith(".") == true)
            {
                typeDescription = typeDescription.Substring(1);
            }

            //如果通过url传递的数据里面没有dll名称信息，那么需要给其附加上这个dll名称信息
            if (typeDescription.Contains(",") == false)
            {
                typeDescription = string.Format("{0},{1}", typeDescription, typeDescriptionDllName);
            }

            Type type = Type.GetType(typeDescription, false, true);
            if (type != null)
            {
                //取得类的无参数构造函数
                var constructor = type.GetConstructor(new Type[0]);
                //调用构造函数取得类的实例
                var obj = constructor.Invoke(null);
                //查找请求的方法
                var method = type.GetMethod(Path.GetFileNameWithoutExtension(requestUrl));
                if (method != null)
                {
                    var parameters = method.GetParameters();
                    object[] args = null;
                    if (parameters.Length > 0)
                    {
                        args = new object[parameters.Length];
                        for (int x = 0; x < parameters.Length; x++)
                        {
                            var parameterAttr = (Attribute.GetCustomAttribute(parameters[x], typeof(ParameterAttribute)) as ParameterAttribute) ?? new FormAttribute();
                            parameterAttr.Name = parameterAttr.Name ?? parameters[x].Name;
                            args[x] = parameterAttr.GetValue(context, parameters[x].ParameterType);
                        }
                    }
                    //执行方法并输出响应结果
                    context.Response.Write(method.Invoke(obj, args));
                }
            }
        }
    }
}
