using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.IO
{
    /// <summary>
    /// 文件内部格式
    /// </summary>
    /// <remarks>
    /// 对二进制文件的格式进行确认（不可以确认文本文件格式）
    /// </remarks>
    public enum FileFormats
    {
        UNKOWN = -1,
        ASPX = 239187,
        BAT = 64101,
        BMP = 6677,
        BTSEED = 10056,
        CHM = 7384,
        COM = 7790,
        CS = 117115,
        DLL = 7790,
        DOC = 208207,
        EXE = 7790,
        GIF = 7173,
        HLP = 6395,
        HTML = 6033,
        JPG = 255216,
        JS = 119105,
        LOG = 70105,
        PNG = 13780,
        RDP = 255254,
        PSD = 5666,
        PDF = 3780,
        RAR = 8297,
        REG = 8269,
        SQL = 255254,
        
        
        XML = 6063,
        XLS = 60104,

        //不可以确定文本文件
        //TXT = 161190,

        //新版office均采用zip压缩格式，因此无从辨析其具体信息
        //DOCX = 8075,
        //XLSX = 8075,
        ZIP = 8075,
    }
}
