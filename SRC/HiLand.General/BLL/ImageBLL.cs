using System;
using System.Collections.Generic;
using System.IO;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;
using HiLand.Utility.IO;
using HiLand.Utility.Web;

namespace HiLand.General.BLL
{
    public class ImageBLL : BaseBLL<ImageBLL, ImageEntity, ImageDAL>
    {
        /// <summary>
        /// 根据所有者和关联信息（比如新闻的Guid，产品的Guid等）获取图片列表
        /// </summary>
        /// <param name="ownerGuid">所有者Guid</param>
        /// <param name="relativeGuid">关联信息Guid（比如新闻的Guid，产品的Guid等）</param>
        /// <returns></returns>
        public List<ImageEntity> GetList(Guid ownerGuid, Guid relativeGuid)
        {
            string whereClause = " 1=1 ";
            if (ownerGuid != Guid.Empty)
            {
                whereClause += string.Format(" AND OwnerGuid='{0}' ", ownerGuid);
            }

            if (relativeGuid != Guid.Empty)
            {
                whereClause += string.Format(" AND RelativeGuid='{0}' ", relativeGuid);
            }

            return base.GetList(whereClause, " ImageIsMain DESC,ImageOrder ASC ");
        }

        /// <summary>
        /// 根据所有者和关联信息（比如新闻的Guid，产品的Guid等）获取主图片（或者第一幅图片）
        /// </summary>
        /// <param name="ownerGuid"></param>
        /// <param name="relativeGuid"></param>
        /// <returns></returns>
        public ImageEntity GetMainImage(Guid ownerGuid, Guid relativeGuid)
        {
            ImageEntity result = null;
            List<ImageEntity> imageList = GetList(ownerGuid, relativeGuid);
            if (imageList != null && imageList.Count > 0)
            {
                result = imageList[0];
            }

            return result;
        }

        public override bool Delete(ImageEntity model)
        {
            if (model == null)
            {
                return false;
            }
            else
            {
                //1.删除文件
                DeleteImageFile(model);
                //2.删除数据库记录
                return base.Delete(model);
            }
        }

        /// <summary>
        /// 删除图片文件
        /// </summary>
        /// <param name="imageGuid"></param>
        public void DeleteImageFile(Guid imageGuid)
        {
            if (imageGuid != Guid.Empty)
            {
                ImageEntity model = ImageBLL.Instance.Get(imageGuid);
                if (model != null)
                {
                    DeleteImageFile(model);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void DeleteImageFile(ImageEntity model)
        {
            if (model != null)
            {
                try
                {
                    string fullVirtualPath = PathHelper.CombineForVirtual(ImageEntity.ImageVirtualBasePath, model.ImageRelativePath);
                    string fullNativePath = RequestHelper.CurrentRequest.MapPath(fullVirtualPath);

                    string fullNativeDir = Path.GetDirectoryName(fullNativePath);
                    string searchPartten= String.Format("{0}*{1}",Path.GetFileNameWithoutExtension(fullNativePath),Path.GetExtension(fullNativeDir));
                    
                    //删除图片的时候，也要同时删除这些图片的缩略图（缩略图的命名格式为 ****_xxx_yyy,其中****表示原图的名词，xxx表示缩略图的宽带，yyy表示缩略图的高度）
                    foreach (string currentItem in Directory.GetFiles(fullNativeDir, searchPartten))
                    {
                        File.Delete(currentItem);
                    }
                }
                catch 
                {
                    
                }
            }
        }
    }
}
