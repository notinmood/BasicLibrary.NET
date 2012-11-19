using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using HiLand.Utility.IO;

namespace HiLand.Utility4.Office
{
    /// <summary>
    /// Word文档操作辅助器
    /// </summary>
    public static class WordHelper
    {
        /// <summary>
        /// 替换DOCX文件中某个子文件的全部内容
        /// </summary>
        /// <param name="docxStream">docx文件流</param>
        /// <param name="innerfileFullName">docx内部子文件全名称</param>
        /// <param name="newStreamContent">新的内容流</param>
        public static void ReplaceSteamContent(Stream docxStream, string innerfileFullName, Stream newStreamContent)
        {
            try
            {
                //打开docx文件  
                using (Package zip = Package.Open(docxStream, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    foreach (PackagePart part in zip.GetParts())
                    {
                        //寻找内部文件  
                        if (part.Uri.OriginalString == innerfileFullName)
                        {
                            string contentType = part.ContentType;
                            CompressionOption option = part.CompressionOption;
                            PackageRelationshipCollection relations = part.GetRelationships();

                            //删除原文档
                            zip.DeletePart(part.Uri);

                            //创建一个新的文档
                            Uri tempUri = PackUriHelper.CreatePartUri(new Uri(innerfileFullName, UriKind.Relative));
                            PackagePart tempPart = zip.CreatePart(tempUri, contentType, option);

                            //将修改后的文档保存
                            using (Stream stream = tempPart.GetStream())
                            {
                                IOHelper.BlocklyCircularlyOperateStream(newStreamContent, BlocklyWriteToInnerStream, stream);
                                stream.Flush();
                                stream.Close();
                            }

                            //创建tempart与各xml文件之间的关系,非常重要  
                            foreach (PackageRelationship relation in relations)
                            {
                                tempPart.CreateRelationship(relation.TargetUri, relation.TargetMode, relation.RelationshipType, relation.Id);
                            }

                            zip.Close();
                            break;
                        }
                    }
                }
            }
            catch
            {
                //throw new Exception(ex.Message);
            }
        }

        private static bool BlocklyWriteToInnerStream(object sender, OperateStreamEnventArgs args)
        {
            Stream innerStream = (Stream)args.CallBackObject;
            innerStream.Write(args.BytesReaded, 0, args.BytesReaded.Length);
            innerStream.Flush();
            return true;
        }

        /// <summary>
        /// 获取DOCX文件中某个子文件的文本内容
        /// </summary>
        /// <param name="docxStream">docx文件流</param>
        /// <param name="innerfileFullName">docx内部子文件全名称</param>
        /// <returns></returns>
        public static string GetTextContent(Stream docxStream, string innerfileFullName)
        {
            string content = string.Empty;
            try
            {
                //打开docx文件  
                using (Package zip = Package.Open(docxStream, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    foreach (PackagePart part in zip.GetParts())
                    {
                        //寻找内部xml文件  
                        if (part.Uri.OriginalString == innerfileFullName)
                        {
                            using (StreamReader sr = new StreamReader(part.GetStream()))
                            {
                                content = sr.ReadToEnd();
                                sr.Close();
                            }

                            zip.Close();
                            break;
                        }
                    }
                }
            }
            catch
            {
                //throw new Exception(ex.Message);
            }
            return content;
        }

        /// <summary>
        /// 替换DOCX文件中某个子文件的文本内容
        /// </summary>
        /// <param name="docxStream">docx文件流</param>
        /// <param name="innerfileFullName">docx内部子文件全名称</param>
        /// <param name="originalContent">原文本内容</param>
        /// <param name="newContent">新的文本内容</param>
        public static void ReplaceTextContent(Stream docxStream, string innerfileFullName, string originalContent, string newContent)
        {
            try
            {
                //打开docx文件  
                using (Package zip = Package.Open(docxStream, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    foreach (PackagePart part in zip.GetParts())
                    {
                        //寻找内部xml文件  
                        if (part.Uri.OriginalString == innerfileFullName)
                        {
                            string content = null;
                            using (StreamReader sr = new StreamReader(part.GetStream()))
                            {
                                content = sr.ReadToEnd();
                                //替换内容
                                if (content != null)
                                {
                                    content = content.Replace(originalContent, newContent);
                                }
                                sr.Close();
                            }

                            string contentType = part.ContentType;
                            CompressionOption option = part.CompressionOption;
                            PackageRelationshipCollection relations = part.GetRelationships();

                            //删除xml文档
                            zip.DeletePart(part.Uri);

                            //创建一个新的xml文档
                            Uri tempUri = PackUriHelper.CreatePartUri(new Uri(innerfileFullName, UriKind.Relative));
                            PackagePart tempPart = zip.CreatePart(tempUri, contentType, option);

                            //将修改后的xml文档保存
                            using (StreamWriter sw = new StreamWriter(tempPart.GetStream()))
                            {
                                sw.Write(content);
                                sw.Flush();
                                sw.Close();
                            }

                            //创建tempart与各xml文件之间的关系,非常重要  
                            foreach (PackageRelationship relation in relations)
                            {
                                tempPart.CreateRelationship(relation.TargetUri, relation.TargetMode, relation.RelationshipType, relation.Id);
                            }

                            zip.Close();
                            break;
                        }
                    }
                }
            }
            catch
            {
                //throw new Exception(ex.Message);
            }
        }
    }
}
