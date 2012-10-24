using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace HiLand.Utility.Drawing
{
    public class Thumbnail
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImageFullName">源图路径（物理路径）</param>
        /// <param name="thumbnailFullName">缩略图路径（物理路径）</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        public static Stream MakeThumbnail(string originalImageFullName, int thumbnailWidth, int thumbnailHeight)
        {
            return MakeThumbnail(originalImageFullName, thumbnailWidth, thumbnailHeight, ThumbnailCutModes.Auto);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImageFullName">源图路径（物理路径）</param>
        /// <param name="thumbnailFullName">缩略图路径（物理路径）</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static Stream MakeThumbnail(string originalImageFullName, int thumbnailWidth, int thumbnailHeight, ThumbnailCutModes thumbCutMode)
        {
            Image originalImage = Image.FromFile(originalImageFullName);
            return MakeThumbnail(originalImage, thumbnailWidth, thumbnailHeight, thumbCutMode);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImageStream">源图路径的数据流</param>
        /// <param name="thumbnailFullName">缩略图路径（物理路径）</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        public static Stream MakeThumbnail(Stream originalImageStream, int thumbnailWidth, int thumbnailHeight)
        {
            Image originalImage = Image.FromStream(originalImageStream);
            return MakeThumbnail(originalImage, thumbnailWidth, thumbnailHeight, ThumbnailCutModes.Auto);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImageStream">源图的数据流</param>
        /// <param name="thumbnailFullName">缩略图路径（物理路径）</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static Stream MakeThumbnail(Stream originalImageStream, int thumbnailWidth, int thumbnailHeight, ThumbnailCutModes thumbCutMode)
        {
            Image originalImage = Image.FromStream(originalImageStream);
            return MakeThumbnail(originalImage, thumbnailWidth, thumbnailHeight, thumbCutMode);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImage">源图Image信息</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        /// <returns></returns>
        public static Stream MakeThumbnail(Image originalImage, int thumbnailWidth, int thumbnailHeight)
        {
            return MakeThumbnail(originalImage,  thumbnailWidth,  thumbnailHeight,ThumbnailCutModes.Auto);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImage">源图Image信息</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        /// <param name="thumbCutMode"></param>
        /// <returns></returns>
        public static Stream MakeThumbnail(Image originalImage, int thumbnailWidth, int thumbnailHeight, ThumbnailCutModes thumbCutMode)
        {
            Stream newImageStream = null;
            if (originalImage != null)
            {
                bool isNeedCut = true;

                //约束的宽高
                int cw = thumbnailWidth;
                int ch = thumbnailHeight;

                //原图的宽高
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                //新图的宽高
                int dw = thumbnailWidth;
                int dh = thumbnailHeight;

                switch (thumbCutMode)
                {
                    case ThumbnailCutModes.HW://指定高宽缩放（可能变形）
                        if (cw > ow && ch > oh)
                        {
                            isNeedCut = false;
                        }
                        else
                        {
                            dw = cw;
                            dh = ch;
                        }
                        break;
                    case ThumbnailCutModes.W://指定宽，高按比例                    
                        if (cw > ow)
                        {
                            isNeedCut = false;
                        }
                        else
                        {
                            dw = thumbnailWidth;
                            dh = originalImage.Height * thumbnailWidth / originalImage.Width;
                        }
                        break;
                    case ThumbnailCutModes.H://指定高，宽按比例
                        if (ch > oh)
                        {
                            isNeedCut = false;
                        }
                        else
                        {
                            dh = thumbnailHeight;
                            dw = originalImage.Width * thumbnailHeight / originalImage.Height;
                        }
                        break;
                    case ThumbnailCutModes.Auto://指定高宽裁减（不变形）    
                    default:
                        if (ch > oh && cw > ow)
                        {
                            isNeedCut = false;
                        }
                        else
                        {
                            double or = (double)ow / (double)oh;
                            double cr = (double)cw / (double)ch;
                            if (or > cr)
                            {
                                dw = cw;
                                dh = oh * dw / ow;
                            }
                            else
                            {
                                dh = ch;
                                dw = ow * dh / oh;
                            }
                        }
                        break;
                }

                if (isNeedCut == false)
                {
                    dw = ow;
                    dh = oh;
                }

                //新建一个bmp图片
                Image bitmap = new Bitmap(dw, dh);

                //新建一个画板
                Graphics g = Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, dw, dh), new Rectangle(0, 0, ow, oh), GraphicsUnit.Pixel);

                try
                {
                    newImageStream = new MemoryStream();
                    //以jpg格式保存缩略图
                    bitmap.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
            }

            return newImageStream;
        }
    }
}
