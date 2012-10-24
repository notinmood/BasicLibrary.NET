using System.IO;
using System.Web;
using HiLand.Utility.IO;

namespace HiLand.Utility.Web
{
    public class DownloadHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origenalFileFullName"></param>
        /// <param name="clientFileName"></param>
        public static void Down(string origenalFileFullName, string clientFileName)
        {
            string fileString = origenalFileFullName;//HttpContext.Current.Server.MapPath(origenalFileFullName);
            FileStream fileStream = new FileStream(fileString, FileMode.Open);
            string origenalFileExtesion = Path.GetExtension(fileString);
            Down(fileStream, origenalFileExtesion, clientFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="origenalFileExtesion"></param>
        /// <param name="clientFileName"></param>
        public static void Down(Stream stream, string origenalFileExtesion, string clientFileName)
        {
            string fileContentType = ContentType.GetContentTypeInfo(origenalFileExtesion);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(clientFileName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", stream.Length.ToString());
            HttpContext.Current.Response.ContentType = fileContentType;

            IOHelper.BlocklyCircularlyOperateStream(stream, FlushBytesToResponse);

            stream.Close();
            HttpContext.Current.Response.OutputStream.Close();
            HttpContext.Current.Response.End();//非常重要，没有这句的话，页面的HTML代码将会保存到文件中
            HttpContext.Current.Response.Close();
        }

        private static bool FlushBytesToResponse(object sender,OperateStreamEnventArgs args)
        {
            HttpContext.Current.Response.BinaryWrite(args.BytesReaded);
            HttpContext.Current.Response.OutputStream.Flush();
            return true;
        }

        /// <summary>
        /// 文件的直传下载方式
        /// </summary>
        /// <param name="origenalFileFullName"></param>
        /// <param name="clientFileName"></param>
        /// <remarks>
        /// 微软为Response对象提供了一个新的方法TransmitFile来解决使用Response.BinaryWrite
        /// 下载超过400mb的文件时导致Aspnet_wp.exe进程回收而无法成功下载的问题。
        /// </remarks>
        public static void TransmitFile(string origenalFileFullName, string clientFileName)
        {
            string origenalFileExtesion = Path.GetExtension(origenalFileFullName);
            string fileContentType = ContentType.GetContentTypeInfo(origenalFileExtesion);
            HttpContext.Current.Response.ContentType = fileContentType;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+HttpUtility.UrlEncode(clientFileName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.TransmitFile(origenalFileFullName);
        }
    }
}