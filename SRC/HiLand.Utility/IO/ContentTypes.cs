using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using HiLand.Utility.Setting;

namespace HiLand.Utility.IO
{
    /// <summary>
    /// 文件MINE类型
    /// </summary>
    public static class ContentTypes
    {
        private static Dictionary<string, string> list = new Dictionary<string, string>();
        /// <summary>
        /// 静态构建函数
        /// </summary>
        static ContentTypes()
        {
            LoadDefaultMIME();
            LoadOuterMIME();
        }

        /// <summary>
        /// 根据扩展名获取文件的ContentType信息
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string GetContentType(string fileExtension)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(fileExtension) == false)
            {
                if (fileExtension.StartsWith("."))
                {
                    fileExtension = fileExtension.Substring(1);
                }
                if (list.ContainsKey(fileExtension))
                {
                    result = list[fileExtension];
                }
            }

            return result;
        }

        /// <summary>
        /// 载入缺省的ContentType信息
        /// </summary>
        private static void LoadDefaultMIME()
        {
            list["ez"] = "application/andrew-inset";
            list["hqx"] = "application/mac-binhex40";
            list["cpt"] = "application/mac-compactpro";
            list["doc"] = "application/msword";
            list["bin"] = "application/octet-stream";
            list["dms"] = "application/octet-stream";
            list["lha"] = "application/octet-stream";
            list["lzh"] = "application/octet-stream";
            list["exe"] = "application/octet-stream";
            list["class"] = "application/octet-stream";
            list["so"] = "application/octet-stream";
            list["dll"] = "application/octet-stream";
            list["oda"] = "application/oda";
            list["pdf"] = "application/pdf";
            list["ai"] = "application/postscript";
            list["eps"] = "application/postscript";
            list["ps"] = "application/postscript";
            list["smi"] = "application/smil";
            list["smil"] = "application/smil";
            list["mif"] = "application/vnd.mif";
            list["xls"] = "application/vnd.ms-excel";
            list["ppt"] = "application/vnd.ms-powerpoint";
            list["wbxml"] = "application/vnd.wap.wbxml";
            list["wmlc"] = "application/vnd.wap.wmlc";
            list["wmlsc"] = "application/vnd.wap.wmlscriptc";
            list["bcpio"] = "application/x-bcpio";
            list["vcd"] = "application/x-cdlink";
            list["pgn"] = "application/x-chess-pgn";
            list["cpio"] = "application/x-cpio";
            list["csh"] = "application/x-csh";
            list["dcr"] = "application/x-director";
            list["dir"] = "application/x-director";
            list["dxr"] = "application/x-director";
            list["dvi"] = "application/x-dvi";
            list["spl"] = "application/x-futuresplash";
            list["gtar"] = "application/x-gtar";
            list["hdf"] = "application/x-hdf";
            list["js"] = "application/x-javascript";
            list["skp"] = "application/x-koan";
            list["skd"] = "application/x-koan";
            list["skt"] = "application/x-koan";
            list["skm"] = "application/x-koan";
            list["latex"] = "application/x-latex";
            list["nc"] = "application/x-netcdf";
            list["cdf"] = "application/x-netcdf";
            list["sh"] = "application/x-sh";
            list["shar"] = "application/x-shar";
            list["swf"] = "application/x-shockwave-flash";
            list["sit"] = "application/x-stuffit";
            list["sv4cpio"] = "application/x-sv4cpio";
            list["sv4crc"] = "application/x-sv4crc";
            list["tar"] = "application/x-tar";
            list["tcl"] = "application/x-tcl";
            list["tex"] = "application/x-tex";
            list["texinfo"] = "application/x-texinfo";
            list["texi"] = "application/x-texinfo";
            list["t"] = "application/x-troff";
            list["tr"] = "application/x-troff";
            list["roff"] = "application/x-troff";
            list["man"] = "application/x-troff-man";
            list["me"] = "application/x-troff-me";
            list["ms"] = "application/x-troff-ms";
            list["ustar"] = "application/x-ustar";
            list["src"] = "application/x-wais-source";
            list["xhtml"] = "application/xhtml+xml";
            list["xht"] = "application/xhtml+xml";
            list["zip"] = "application/zip";
            list["au"] = "audio/basic";
            list["snd"] = "audio/basic";
            list["mid"] = "audio/midi";
            list["midi"] = "audio/midi";
            list["kar"] = "audio/midi";
            list["mpga"] = "audio/mpeg";
            list["mp2"] = "audio/mpeg";
            list["mp3"] = "audio/mpeg";
            list["aif"] = "audio/x-aiff";
            list["aiff"] = "audio/x-aiff";
            list["aifc"] = "audio/x-aiff";
            list["m3u"] = "audio/x-mpegurl";
            list["ram"] = "audio/x-pn-realaudio";
            list["rm"] = "audio/x-pn-realaudio";
            list["rpm"] = "audio/x-pn-realaudio-plugin";
            list["ra"] = "audio/x-realaudio";
            list["wav"] = "audio/x-wav";
            list["pdb"] = "chemical/x-pdb";
            list["xyz"] = "chemical/x-xyz";
            list["bmp"] = "image/bmp";
            list["gif"] = "image/gif";
            list["ief"] = "image/ief";
            list["jpeg"] = "image/jpeg";
            list["jpg"] = "image/jpeg";
            list["jpe"] = "image/jpeg";
            list["png"] = "image/png";
            list["tiff"] = "image/tiff";
            list["tif"] = "image/tiff";
            list["djvu"] = "image/vnd.djvu";
            list["djv"] = "image/vnd.djvu";
            list["wbmp"] = "image/vnd.wap.wbmp";
            list["ras"] = "image/x-cmu-raster";
            list["pnm"] = "image/x-portable-anymap";
            list["pbm"] = "image/x-portable-bitmap";
            list["pgm"] = "image/x-portable-graymap";
            list["ppm"] = "image/x-portable-pixmap";
            list["rgb"] = "image/x-rgb";
            list["xbm"] = "image/x-xbitmap";
            list["xpm"] = "image/x-xpixmap";
            list["xwd"] = "image/x-xwindowdump";
            list["igs"] = "model/iges";
            list["iges"] = "model/iges";
            list["msh"] = "model/mesh";
            list["mesh"] = "model/mesh";
            list["silo"] = "model/mesh";
            list["wrl"] = "model/vrml";
            list["vrml"] = "model/vrml";
            list["css"] = "text/css";
            list["html"] = "text/html";
            list["htm"] = "text/html";
            list["asc"] = "text/plain";
            list["txt"] = "text/plain";
            list["rtx"] = "text/richtext";
            list["rtf"] = "text/rtf";
            list["sgml"] = "text/sgml";
            list["sgm"] = "text/sgml";
            list["tsv"] = "text/tab-separated-values";
            list["wml"] = "text/vnd.wap.wml";
            list["wmls"] = "text/vnd.wap.wmlscript";
            list["etx"] = "text/x-setext";
            list["xsl"] = "text/xml";
            list["xml"] = "text/xml";
            list["mpeg"] = "video/mpeg";
            list["mpg"] = "video/mpeg";
            list["mpe"] = "video/mpeg";
            list["qt"] = "video/quicktime";
            list["mov"] = "video/quicktime";
            list["mxu"] = "video/vnd.mpegurl";
            list["avi"] = "video/x-msvideo";
            list["movie"] = "video/x-sgi-movie";
            list["ice"] = "x-conference/x-cooltalk";

            list["docx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            list["xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            list["pptx"] = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
        }

        /// <summary>
        /// 其他类型的ContentType可以从外部config中载入
        /// </summary>
        private static void LoadOuterMIME()
        {
            NameValueCollection contentTypeSection = Config.GetSection<NameValueCollection>("contentTypeSection");
            if (contentTypeSection != null)
            {
                foreach (string key in contentTypeSection.Keys)
                {
                    string keyFixed = key;
                    if (keyFixed.StartsWith("."))
                    {
                        keyFixed = keyFixed.Substring(1);
                    }
                    list[keyFixed] = contentTypeSection[key];
                }
            }
        }
    }
}
