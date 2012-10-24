using System;
using System.IO;
using HiLand.Framework.FoundationLayer;
using HiLand.Utility.Data;
using HiLand.Utility.Drawing;
using HiLand.Utility.Enums;
using HiLand.Utility.IO;
using HiLand.Utility.Setting;
using HiLand.Utility.Web;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 图片保存实体（其可以为产品，新闻等提供统一的图片保存服务）
    /// </summary>
    public class ImageEntity : BaseModel<ImageEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ImageGuid" }; }
        }

        #region 实体信息
        private int imageID;
        public int ImageID
        {
            get { return imageID; }
            set { imageID = value; }
        }

        private Guid imageGuid = Guid.Empty;
        public Guid ImageGuid
        {
            get { return imageGuid; }
            set { imageGuid = value; }
        }

        private string imageName = String.Empty;
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        private Guid relativeGuid = Guid.Empty;
        public Guid RelativeGuid
        {
            get { return relativeGuid; }
            set { relativeGuid = value; }
        }

        private string imageCategoryCode = String.Empty;
        public string ImageCategoryCode
        {
            get { return imageCategoryCode; }
            set { imageCategoryCode = value; }
        }

        private string imageKind = String.Empty;
        public string ImageKind
        {
            get { return imageKind; }
            set { imageKind = value; }
        }

        private Guid ownerGuid = Guid.Empty;
        public Guid OwnerGuid
        {
            get { return ownerGuid; }
            set { ownerGuid = value; }
        }

        private string imageRelativePath = String.Empty;
        public string ImageRelativePath
        {
            get { return imageRelativePath; }
            set { imageRelativePath = value; }
        }

        private int imageSize;
        public int ImageSize
        {
            get { return imageSize; }
            set { imageSize = value; }
        }

        private int imageWidth;
        public int ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }

        private int imageHeight;
        public int ImageHeight
        {
            get { return imageHeight; }
            set { imageHeight = value; }
        }

        private Logics imageStatus;
        public Logics ImageStatus
        {
            get { return imageStatus; }
            set { imageStatus = value; }
        }

        private int imageOrder;
        public int ImageOrder
        {
            get { return imageOrder; }
            set { imageOrder = value; }
        }

        private Logics imageIsMain;
        public Logics ImageIsMain
        {
            get { return imageIsMain; }
            set { imageIsMain = value; }
        }

        private Logics canUsable;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private string imageType = String.Empty;
        public string ImageType
        {
            get { return imageType; }
            set { imageType = value; }
        }

        private DateTime createTime= DateTimeHelper.Min;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string imageDescription = String.Empty;
        public string ImageDescription
        {
            get { return imageDescription; }
            set { imageDescription = value; }
        }

        private int imageDownCount;
        public int ImageDownCount
        {
            get { return imageDownCount; }
            set { imageDownCount = value; }
        }

        private int imageDisplayCount;
        public int ImageDisplayCount
        {
            get { return imageDisplayCount; }
            set { imageDisplayCount = value; }
        }
        #endregion

        #region 扩展属性
        private static string imageVirtualBasePath = string.Empty;
        /// <summary>
        ///  保存图片的基虚路径
        /// </summary>
        public static string ImageVirtualBasePath
        {
            get
            {
                if (imageVirtualBasePath == string.Empty)
                {
                    imageVirtualBasePath = Config.GetAppSetting("imageBasePath");
                    if (string.IsNullOrEmpty(imageVirtualBasePath) == true)
                    {
                        imageVirtualBasePath = "~/Upload/";
                    }
                }

                return imageVirtualBasePath;
            }
        }

        /// <summary>
        /// 保存图片的完整虚路径
        /// </summary>
        public string ImageAllVirtualPath
        {
            get
            {
                return PathHelper.CombineForVirtual(ImageVirtualBasePath, ImageRelativePath);
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 返回缩略图的路径（如果此缩略图不存储，则需要先生成）
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string EnsureThumbnailAllVirtualPath(int width,int height)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(this.ImageRelativePath) == false)
            {
                string baseVirtualPath = ImageEntity.ImageVirtualBasePath;
                string dirName = Path.GetDirectoryName(this.ImageRelativePath);
                string originalFileName = Path.GetFileNameWithoutExtension(this.ImageRelativePath);
                string extensionName = Path.GetExtension(this.ImageRelativePath);
                string thumbnailFileName = string.Format("{0}_{1}_{2}{3}",originalFileName,width,height,extensionName);
                string baseNativePath = RequestHelper.CurrentRequest.MapPath(baseVirtualPath);
                string originalFullName = PathHelper.CombineForNative(baseNativePath, this.ImageRelativePath);
                string thubnailFullName = PathHelper.CombineForNative(baseNativePath, dirName, thumbnailFileName);

                string thubnailFullVirtualName = PathHelper.CombineForVirtual(baseVirtualPath, dirName, thumbnailFileName);
                result = RequestHelper.ResolveUrl(thubnailFullVirtualName);

                try
                {
                    if (File.Exists(thubnailFullName) == false)
                    {
                        Stream thubnailStream = Thumbnail.MakeThumbnail(originalFullName, width, height);
                        FileHelper.WriteStreamToFile(thubnailStream, thubnailFullName);
                    }
                }
                catch { }
            }

            return result;
        }
        #endregion
    }
}
