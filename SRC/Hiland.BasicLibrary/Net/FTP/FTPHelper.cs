﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace HiLand.Utility.Net.FTP
{
    public class FTPHelper
    {
        private string ftpServerIP;
        private string ftpRemotePath;
        private string ftpUserID;
        private string ftpPassword;
        private string ftpURI;
 
        /// <summary>
        /// 连接FTP
        /// </summary>
        /// <param name="FtpServerIP">FTP连接地址</param>
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="FtpUserID">用户名</param>
        /// <param name="FtpPassword">密码</param>
        public FTPHelper(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }
 
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;
 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
            }
        }
 
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
 
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
 
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Download Error --> " + ex.Message);
            }
        }
 
 
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName)
        {
            try
            {
                string uri = ftpURI + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
 
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + fileName);
            }
        }
 
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folderName"></param>
        public void RemoveDirectory(string folderName)
        {
            try
            {
                string uri = ftpURI + folderName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
 
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + folderName);
            }
        }
 
        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
 
                //while (reader.Read() > 0)
                //{
 
                //}
                string line = reader.ReadLine();
                //line = reader.ReadLine();
                //line = reader.ReadLine();
 
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch
            {
                downloadFiles = null;
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFilesDetailList Error --> " + ex.Message);
                return downloadFiles;
            }
        }
 
        /// <summary>
        /// 获取当前目录下文件列表(仅文件)
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList(string mask)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
 
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                    {
 
                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_)
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")
                {
                    //Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileList Error --> " + ex.Message.ToString());
                }
                return downloadFiles;
            }
        }
 
        /// <summary>
        /// 获取当前目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetDirectoryList()
        {
            string[] drectory = GetFilesDetailList();
            string m = string.Empty;
            foreach (string str in drectory)
            {
                int dirPos = str.IndexOf("<DIR>");
                if (dirPos>0)
                {
                    /*判断 Windows 风格*/
                    m += str.Substring(dirPos + 5).Trim() + "\n";
                }
                else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                {
                    /*判断 Unix 风格*/
                    string dir = str.Substring(54).Trim();
                    if (dir != "." && dir != "..")
                    {
                        m += dir + "\n";
                    }
                }
            }
 
            char[] n = new char[] { '\n' };
            return m.Split(n);
        }
 
        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            string[] dirList = GetDirectoryList();
            foreach (string str in dirList)
            {
                if (str.Trim() == RemoteDirectoryName.Trim())
                {
                    return true;
                }
            }
            return false;
        }
 
        /// <summary>
        /// 判断当前目录下指定的文件是否存在
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        public bool FileExist(string RemoteFileName)
        {
            string[] fileList = GetFileList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }
 
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
 
                ftpStream.Close();
                response.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "MakeDir Error --> " + ex.Message);
            }
        }
 
        /// <summary>
        /// 获取指定文件大小
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;
 
                ftpStream.Close();
                response.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileSize Error --> " + ex.Message);
            }
            return fileSize;
        }
 
        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void ReName(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
 
                ftpStream.Close();
                response.Close();
            }
            catch
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "ReName Error --> " + ex.Message);
            }
        }
 
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void MovieFile(string currentFilename, string newDirectory)
        {
            ReName(currentFilename, newDirectory);
        }
 
        /// <summary>
        /// 切换当前目录
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <param name="IsRoot">true 绝对路径   false 相对路径</param>
        public void GotoDirectory(string DirectoryName, bool IsRoot)
        {
            if (IsRoot)
            {
                ftpRemotePath = DirectoryName;
            }
            else
            {
                ftpRemotePath += DirectoryName + "/";
            }
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }
    }
}
