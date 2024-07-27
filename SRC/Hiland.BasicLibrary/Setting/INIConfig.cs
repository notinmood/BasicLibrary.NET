using System.Runtime.InteropServices;
using System.Text;

namespace HiLand.Utility.Setting
{
    /// <summary>
    /// ini配置文件读写工具
    /// </summary>
    public class INIConfig
    {
        //文件INI名称 
        private string intFileFullPath;

        /// <summary>
        /// 写INI文件的API函数 
        /// </summary>
        /// <param name="sectionName">节的名字</param>
        /// <param name="key">键的名字</param>
        /// <param name="value"></param>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string sectionName, string key, string value, string fileFullName);

        /// <summary>
        /// 读INI文件的API函数 
        /// </summary>
        /// <param name="sectionName">节的名字</param>
        /// <param name="key">键的名字</param>
        /// <param name="defaultValue">如果INI文件中没有前两个参数指定的节名或键名,则将返回此默认值</param>
        /// <param name="returnValue">读取得到的参数值</param>
        /// <param name="cacheSize">参数返回值的缓冲大小</param>
        /// <param name="fileFullName">ini文件名称</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string sectionName, string key, string defaultValue, StringBuilder returnValue, int cacheSize, string fileFullName);


        /// <summary>
        /// 类的构造函数，传递INI文件名 
        /// </summary>
        /// <param name="iniFileFullName"></param>
        public INIConfig(string iniFileFullName)
        {
            this.intFileFullPath = iniFileFullName;
        }

        /// <summary>
        /// 写INI文件 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteValue(string sectionName, string key, string value)
        {
            WritePrivateProfileString(sectionName, key, value, this.intFileFullPath);
        }

        /// <summary>
        /// 读取INI文件指定值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadValue(string sectionName, string key)
        {
            StringBuilder returnValue = new StringBuilder(255);
            GetPrivateProfileString(sectionName, key, "", returnValue, 255, this.intFileFullPath);
            return returnValue.ToString();
        }

        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection()
        {
            WriteValue(null, null, null);
        }

        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="sectionName"></param>
        public void ClearSection(string sectionName)
        {
            WriteValue(sectionName, null, null);
        }
    }
}
