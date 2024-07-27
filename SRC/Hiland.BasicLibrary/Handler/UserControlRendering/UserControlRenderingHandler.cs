//using System;
//using System.Collections.Generic;
//using System.Web;
//using System.Web.UI;
//using System.Reflection;
//using System.ComponentModel;
//using Hiland.BasicLibrary.UI;

//namespace Hiland.BasicLibrary.Handler
//{
//    /// <summary>
//    /// 将用户控件转化成HTML代码的处理程序
//    /// </summary>
//    /// <remarks>
//    /// 使用时有一定的格式要求:
//    /// 0.更多使用技巧参阅:http://www.cnblogs.com/JeffreyZhao/archive/2008/07/13/user_control_rendering.html
//    /// 1.必须给这个handler传递一个control参数(其需要为控件的全名称,即包括路径),其表示要将哪个用户控件生成HTML代码
//    /// 2.使用的有两种方式,a.在web.config中配置handler信息.
//    ///                         b.可以创建一个继承于本handler的 ashx文件.
//    /// 3.使用的时候,如果要让url中参数能够自动匹配控件内属性,需要在属性上加入以下特性 UserControlRenderingPropertyAttribute,比如
//    ///     [UserControlRenderingProperty(Key = "page", Source = UserControlRenderingPropertySource.QueryString)]
//    ///     public int PageIndex { get; set; }
//    ///     上面这段代码就能保证PageIndex属性自动跟url中的参数page匹配.
//    /// </remarks>
//    public class UserControlRenderingHandler : IHttpHandler
//    {
//        public void ProcessRequest(HttpContext context)
//        {
//            string controlPath = context.Request["control"];

//            var viewManager = new UserControlViewManager<UserControl>();
//            var control = viewManager.LoadViewControl(controlPath);

//            SetPropertyValues(control, context);

//            context.Response.ContentType = "text/html";
//            context.Response.Write(viewManager.RenderView(control));
//        }


//        public bool IsReusable
//        {
//            get
//            {
//                return true;
//            }
//        }


//        private static Dictionary<Type, Dictionary<PropertyInfo, List<UserControlRenderingPropertyAttribute>>> s_metadataCache =
//            new Dictionary<Type, Dictionary<PropertyInfo, List<UserControlRenderingPropertyAttribute>>>();
//        private static Dictionary<PropertyInfo, object> s_defaultValueCache =
//            new Dictionary<PropertyInfo, object>();
//        private static object s_mutex = new object();

//        private static Dictionary<PropertyInfo, List<UserControlRenderingPropertyAttribute>> GetMetadata(Type type)
//        {
//            if (!s_metadataCache.ContainsKey(type))
//            {
//                lock (s_mutex)
//                {
//                    if (!s_metadataCache.ContainsKey(type))
//                    {
//                        s_metadataCache[type] = LoadMetadata(type);
//                    }
//                }
//            }

//            return s_metadataCache[type];
//        }

//        private static Dictionary<PropertyInfo, List<UserControlRenderingPropertyAttribute>> LoadMetadata(Type type)
//        {
//            var result = new Dictionary<PropertyInfo, List<UserControlRenderingPropertyAttribute>>();
//            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

//            foreach (var p in properties)
//            {
//                var attributes = p.GetCustomAttributes(typeof(UserControlRenderingPropertyAttribute), true);
//                if (attributes.Length > 0)
//                {
//                    UserControlRenderingPropertyAttribute[] tt = (UserControlRenderingPropertyAttribute[])attributes;
//                    result[p] = new List<UserControlRenderingPropertyAttribute>(tt);
//                }
//            }

//            return result;
//        }

//        private static object GetDefaultValue(PropertyInfo property)
//        {
//            if (!s_defaultValueCache.ContainsKey(property))
//            {
//                lock (s_mutex)
//                {
//                    if (!s_defaultValueCache.ContainsKey(property))
//                    {
//                        var attributes = property.GetCustomAttributes(typeof(DefaultValueAttribute), true);
//                        object value = attributes.Length > 0 ? ((DefaultValueAttribute)attributes[0]).Value : null;
//                        s_defaultValueCache[property] = value;
//                    }
//                }
//            }

//            return s_defaultValueCache[property];
//        }

//        private static void SetPropertyValues(UserControl control, HttpContext context)
//        {
//            var metadata = GetMetadata(control.GetType());
//            foreach (var property in metadata.Keys)
//            {
//                object value = GetValue(metadata[property], context) ?? GetDefaultValue(property);
//                if (value != null)
//                {
//                    property.SetValue(control, Convert.ChangeType(value, property.PropertyType), null);
//                }
//            }
//        }

//        private static object GetValue(IEnumerable<UserControlRenderingPropertyAttribute> metadata, HttpContext context)
//        {
//            foreach (var att in metadata)
//            {
//                var collection = (att.Source == UserControlRenderingPropertySource.QueryString) ? context.Request.QueryString : context.Request.Params;
//                object value = collection[att.Key];

//                if (value != null) return value;
//            }

//            return null;
//        }
//    }
//}
