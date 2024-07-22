using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace HiLand.Utility.IO.Extend
{
    public class FileWrapper
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
              string lpFileName,
              uint dwDesiredAccess,
              uint dwShareMode,
              IntPtr SecurityAttributes,
              uint dwCreationDisposition,
              uint dwFlagsAndAttributes,
              IntPtr hTemplateFile
              );

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
           string lpFileName,
           FileAccesses dwDesiredAccess,
           FileShares dwShareMode,
           IntPtr lpSecurityAttributes,
           CreationDispositions dwCreationDisposition,
           FileOperateAttributes dwFlagsAndAttributes,
           IntPtr hTemplateFile);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
           string fileName,
           [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess,
           [MarshalAs(UnmanagedType.U4)] FileShare fileShare,
           IntPtr securityAttributes,
           [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
           int flags,
           IntPtr template);

        #region 文件打开和关闭
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        #endregion
    }
}