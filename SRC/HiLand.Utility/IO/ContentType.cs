using System.Collections.Generic;

namespace HiLand.Utility.IO
{
    public static class ContentType
    {
        private static Dictionary<string, string> list = new Dictionary<string, string>();
        //TODO:需要根据实际情况，继续添加其他类型的ContentType信息
        static ContentType()
        {
            list.Add("ez", "application/andrew-inset");
            list.Add("hqx", "application/mac-binhex40");
            list.Add("cpt", "application/mac-compactpro");
            list.Add("doc", "application/msword");
            list.Add("bin", "application/octet-stream");
            list.Add("dms", "application/octet-stream");
            list.Add("lha", "application/octet-stream");
            list.Add("lzh", "application/octet-stream");
            list.Add("exe", "application/octet-stream");
            list.Add("class", "application/octet-stream");
            list.Add("so", "application/octet-stream");
            list.Add("dll", "application/octet-stream");
            list.Add("oda", "application/oda");
            list.Add("pdf", "application/pdf");
            list.Add("ai", "application/postscript");
            list.Add("eps", "application/postscript");
            list.Add("ps", "application/postscript");
            list.Add("smi", "application/smil");
            list.Add("smil", "application/smil");
            list.Add("mif", "application/vnd.mif");
            list.Add("xls", "application/vnd.ms-excel");
            list.Add("ppt", "application/vnd.ms-powerpoint");
            list.Add("wbxml", "application/vnd.wap.wbxml");
            list.Add("wmlc", "application/vnd.wap.wmlc");
            list.Add("wmlsc", "application/vnd.wap.wmlscriptc");
            list.Add("bcpio", "application/x-bcpio");
            list.Add("vcd", "application/x-cdlink");
            list.Add("pgn", "application/x-chess-pgn");
            list.Add("cpio", "application/x-cpio");
            list.Add("csh", "application/x-csh");
            list.Add("dcr", "application/x-director");
            list.Add("dir", "application/x-director");
            list.Add("dxr", "application/x-director");
            list.Add("dvi", "application/x-dvi");
            list.Add("spl", "application/x-futuresplash");
            list.Add("gtar", "application/x-gtar");
            list.Add("hdf", "application/x-hdf");
            list.Add("js", "application/x-javascript");
            list.Add("skp", "application/x-koan");
            list.Add("skd", "application/x-koan");
            list.Add("skt", "application/x-koan");
            list.Add("skm", "application/x-koan");
            list.Add("latex", "application/x-latex");
            list.Add("nc", "application/x-netcdf");
            list.Add("cdf", "application/x-netcdf");
            list.Add("sh", "application/x-sh");
            list.Add("shar", "application/x-shar");
            list.Add("swf", "application/x-shockwave-flash");
            list.Add("sit", "application/x-stuffit");
            list.Add("sv4cpio", "application/x-sv4cpio");
            list.Add("sv4crc", "application/x-sv4crc");
            list.Add("tar", "application/x-tar");
            list.Add("tcl", "application/x-tcl");
            list.Add("tex", "application/x-tex");
            list.Add("texinfo", "application/x-texinfo");
            list.Add("texi", "application/x-texinfo");
            list.Add("t", "application/x-troff");
            list.Add("tr", "application/x-troff");
            list.Add("roff", "application/x-troff");
            list.Add("man", "application/x-troff-man");
            list.Add("me", "application/x-troff-me");
            list.Add("ms", "application/x-troff-ms");
            list.Add("ustar", "application/x-ustar");
            list.Add("src", "application/x-wais-source");
            list.Add("xhtml", "application/xhtml+xml");
            list.Add("xht", "application/xhtml+xml");
            list.Add("zip", "application/zip");
            list.Add("au", "audio/basic");
            list.Add("snd", "audio/basic");
            list.Add("mid", "audio/midi");
            list.Add("midi", "audio/midi");
            list.Add("kar", "audio/midi");
            list.Add("mpga", "audio/mpeg");
            list.Add("mp2", "audio/mpeg");
            list.Add("mp3", "audio/mpeg");
            list.Add("aif", "audio/x-aiff");
            list.Add("aiff", "audio/x-aiff");
            list.Add("aifc", "audio/x-aiff");
            list.Add("m3u", "audio/x-mpegurl");
            list.Add("ram", "audio/x-pn-realaudio");
            list.Add("rm", "audio/x-pn-realaudio");
            list.Add("rpm", "audio/x-pn-realaudio-plugin");
            list.Add("ra", "audio/x-realaudio");
            list.Add("wav", "audio/x-wav");
            list.Add("pdb", "chemical/x-pdb");
            list.Add("xyz", "chemical/x-xyz");
            list.Add("bmp", "image/bmp");
            list.Add("gif", "image/gif");
            list.Add("ief", "image/ief");
            list.Add("jpeg", "image/jpeg");
            list.Add("jpg", "image/jpeg");
            list.Add("jpe", "image/jpeg");
            list.Add("png", "image/png");
            list.Add("tiff", "image/tiff");
            list.Add("tif", "image/tiff");
            list.Add("djvu", "image/vnd.djvu");
            list.Add("djv", "image/vnd.djvu");
            list.Add("wbmp", "image/vnd.wap.wbmp");
            list.Add("ras", "image/x-cmu-raster");
            list.Add("pnm", "image/x-portable-anymap");
            list.Add("pbm", "image/x-portable-bitmap");
            list.Add("pgm", "image/x-portable-graymap");
            list.Add("ppm", "image/x-portable-pixmap");
            list.Add("rgb", "image/x-rgb");
            list.Add("xbm", "image/x-xbitmap");
            list.Add("xpm", "image/x-xpixmap");
            list.Add("xwd", "image/x-xwindowdump");
            list.Add("igs", "model/iges");
            list.Add("iges", "model/iges");
            list.Add("msh", "model/mesh");
            list.Add("mesh", "model/mesh");
            list.Add("silo", "model/mesh");
            list.Add("wrl", "model/vrml");
            list.Add("vrml", "model/vrml");
            list.Add("css", "text/css");
            list.Add("html", "text/html");
            list.Add("htm", "text/html");
            list.Add("asc", "text/plain");
            list.Add("txt", "text/plain");
            list.Add("rtx", "text/richtext");
            list.Add("rtf", "text/rtf");
            list.Add("sgml", "text/sgml");
            list.Add("sgm", "text/sgml");
            list.Add("tsv", "text/tab-separated-values");
            list.Add("wml", "text/vnd.wap.wml");
            list.Add("wmls", "text/vnd.wap.wmlscript");
            list.Add("etx", "text/x-setext");
            list.Add("xsl", "text/xml");
            list.Add("xml", "text/xml");
            list.Add("mpeg", "video/mpeg");
            list.Add("mpg", "video/mpeg");
            list.Add("mpe", "video/mpeg");
            list.Add("qt", "video/quicktime");
            list.Add("mov", "video/quicktime");
            list.Add("mxu", "video/vnd.mpegurl");
            list.Add("avi", "video/x-msvideo");
            list.Add("movie", "video/x-sgi-movie");
            list.Add("ice", "x-conference/x-cooltalk");
        }

        /// <summary>
        /// 根据扩展名获取文件的ContentType信息
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string GetContentTypeInfo(string fileExtension)
        {
                string result = string.Empty;
                if (list.ContainsKey(fileExtension))
                {
                    result= list[fileExtension];
                }
                return result;
        }
    }
}
