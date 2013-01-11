using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HiLand.Utility.IO;
using HiLand.Utility.Setting;

namespace HiLand.Utility.Logging
{
    /// <summary>
    /// 平面文件类型的日志
    /// </summary>
    public class FileLoger : ILoger
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(ILogEntity log)
        {
            Log(log.ToString(), false);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(string log)
        {
            Log(log, true);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="isAutoAppendAddonData">是否自动附加日志的附加信息（比如时间等）</param>
        public void Log(string log, bool isAutoAppendAddonData)
        {
            string businessLogFileName = Config.GetAppSetting("businessLogFileName", "~/Upload/Log/businessLog.txt");
            string businessLogFileFullName = IOHelper.GetNativeFilePath(businessLogFileName, false);
            if (isAutoAppendAddonData == true)
            {
                log = string.Format("{0}-{1}\r\n--------\r\n", DateTime.Now.ToString(), log);
            }

            FileHelper.WriteContentToFile(businessLogFileFullName, log);
        }
    }
}
