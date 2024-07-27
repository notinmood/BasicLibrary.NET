using System.Net;

namespace Hiland.BasicLibrary.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerHelper
    {
        /// <summary>
        /// 服务器是否在正常运行
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsRunning(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }

            return false;

        }
    }
}
