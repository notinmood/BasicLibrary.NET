using System;
using System.IO.Compression;
using System.Web;
using System.Web.UI;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Module
{
    /// <summary>
    /// 处理页面压缩的Module
    /// </summary>
    public class CompressModule : IHttpModule
    {
        public void Dispose()
        {
            //
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;
           
            if ((context.Context.CurrentHandler is Page) && string.IsNullOrEmpty(context.Request.ServerVariables["HTTP_X_MICROSOFTAJAX"]))
            {
                CompressTypes compressType = GetCompressType(context);
                if (compressType == CompressTypes.GZip)
                {
                    context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                    context.Response.AppendHeader("Content-Encoding", compressType.ToString().ToLower());
                }

                if (compressType == CompressTypes.Deflate)
                {
                    context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
                    context.Response.AppendHeader("Content-Encoding", compressType.ToString().ToLower());
                }
            }
        }

        private CompressTypes GetCompressType(HttpApplication context)
        {
            string acceptEncoding = context.Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding))
            {
                return CompressTypes.None;
            }

            CompressTypes result = CompressTypes.None;
            acceptEncoding = acceptEncoding.ToUpper();
            switch (acceptEncoding)
            {
                case "GZIP":
                    result = CompressTypes.GZip;
                    break;
                case "DEFLATE":
                    result = CompressTypes.Deflate;
                    break;
                default:
                    result = CompressTypes.None;
                    break;
            }

            return result;
        }
    }
}
