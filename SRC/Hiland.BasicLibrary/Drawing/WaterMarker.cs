using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Hiland.BasicLibrary.Drawing
{
    public class WaterMarker
    {
        #region 添加图片型水印
        /// <summary>
        /// 在已经存在的画布上水印
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="watermarImagePath"></param>
        /// <param name="WaterMarkPositions"></param>
        /// <param name="alpha">透明度,取值0-1之间的小数</param>
        public static Graphics AddWaterMarkImage(Graphics graphics, int imageWidth, int imageHeight, string watermarImagePath, WaterMarkPositions WaterMarkPositions, float alpha)
        {
            if (graphics != null && File.Exists(watermarImagePath))
            {
                using (Image image = new Bitmap(watermarImagePath))
                {
                    using (ImageAttributes imageAttr = new ImageAttributes())
                    {
                        ColorMap map = new ColorMap();
                        map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
                        map.NewColor = Color.FromArgb(0, 0, 0, 0);
                        ColorMap[] mapArray = new ColorMap[] { map };
                        imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
                        float[][] numArray2 = new float[5][];
                        float[] numArray3 = new float[5];
                        numArray3[0] = 1f;
                        numArray2[0] = numArray3;
                        numArray3 = new float[5];
                        numArray3[1] = 1f;
                        numArray2[1] = numArray3;
                        numArray3 = new float[5];
                        numArray3[2] = 1f;
                        numArray2[2] = numArray3;
                        numArray3 = new float[5];
                        numArray3[3] = alpha;
                        numArray2[3] = numArray3;
                        numArray3 = new float[5];
                        numArray3[4] = 1f;
                        numArray2[4] = numArray3;
                        float[][] newColorMatrix = numArray2;
                        ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                        imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        int x = 0;
                        int y = 0;
                        switch (WaterMarkPositions)
                        {
                            case WaterMarkPositions.LeftTop:
                                x = 10;
                                y = 10;
                                break;

                            case WaterMarkPositions.CenterTop:
                                x = (imageWidth - image.Width) / 2;
                                y = 10;
                                break;

                            case WaterMarkPositions.RightTop:
                                x = imageWidth - image.Width - 10;
                                y = 10;
                                break;

                            case WaterMarkPositions.LeftMiddle:
                                x = 10;
                                y = (imageHeight - image.Height) / 2;
                                break;

                            case WaterMarkPositions.Center:
                                x = (imageWidth - image.Width) / 2;
                                y = (imageHeight - image.Height) / 2;
                                break;

                            case WaterMarkPositions.RightMiddle:
                                x = imageWidth - image.Width - 10;
                                y = (imageHeight - image.Height) / 2;
                                break;

                            case WaterMarkPositions.LeftBottom:
                                x = 10;
                                y = imageHeight - image.Height - 10;
                                break;

                            case WaterMarkPositions.CenterBottom:
                                x = (imageWidth - image.Width) / 2;
                                y = imageHeight - image.Height - 10;
                                break;

                            case WaterMarkPositions.RightBottom:
                                x = imageWidth - image.Width - 10;
                                y = imageHeight - image.Height - 10;
                                break;
                        }
                        graphics.DrawImage(image, new Rectangle(x, y, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                        image.Dispose();
                        imageAttr.Dispose();
                    }
                }
            }

            return graphics;
        }

        /// <summary>
        /// 在原图上添加水印
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="waterMarkImagePath"></param>
        /// <param name="WaterMarkPositions"></param>
        /// <param name="alpha">透明度,取值0-1之间的小数</param>
        /// <param name="waterMarkQuality">水印的质量，取值0-100</param>
        /// <returns></returns>
        public static Stream AddWaterMarkImage(Image sourceImage, string waterMarkImagePath, WaterMarkPositions WaterMarkPositions, float alpha, int waterMarkQuality)
        {
            Stream newImageStream = null;
            if (sourceImage == null || string.IsNullOrEmpty(waterMarkImagePath.TrimEnd(new char[] { ' ' })))
            {
                newImageStream = null;
            }
            else
            {
                int width = sourceImage.Width;
                int height = sourceImage.Height;
                using (Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                {
                    image.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawImage(sourceImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
                        AddWaterMarkImage(graphics, width, height, waterMarkImagePath, WaterMarkPositions, alpha);
                        newImageStream = new MemoryStream();
                        ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
                        EncoderParameters encoderParams = new EncoderParameters(1);
                        encoderParams = new EncoderParameters();
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)waterMarkQuality);
                        image.Save(newImageStream, encoderInfo, encoderParams);
                        image.Dispose();
                        //sourceImage.Dispose(); //这个原来的Image不应该在这里释放
                        graphics.Dispose();
                    }
                }
            }

            return newImageStream;
        }

        /// <summary>
        /// 在原图上添加水印
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="watermarImagePath"></param>
        /// <param name="WaterMarkPositions"></param>
        /// <param name="alpha">透明度,取值0-1之间的小数</param>
        /// <param name="waterMarkQuality">水印的质量，取值0-100</param>
        /// <returns></returns>
        public static Stream AddWaterMarkImage(Stream sourceStream, string watermarImagePath, WaterMarkPositions WaterMarkPositions, float alpha, int waterMarkQuality)
        {
            Stream newImageStream = null;
            Image sourceImage = null;
            try
            {
                sourceImage = Image.FromStream(sourceStream, true);
            }
            catch
            {
            }

            if (sourceImage != null)
            {
                newImageStream = AddWaterMarkImage(sourceImage, watermarImagePath, WaterMarkPositions, alpha, waterMarkQuality);
            }

            return newImageStream;
        }
        #endregion

        #region 添加文本型水印
        /// <summary>
        /// 在已经存在的画布上水印
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="waterMarkText"></param>
        /// <param name="WaterMarkPositions"></param>
        /// <param name="alpha">透明度,取值0-1之间的小数</param>
        /// <returns></returns>
        public static Graphics AddWaterMarkText(Graphics graphics, int imageWidth, int imageHeight, string waterMarkText, WaterMarkPositions WaterMarkPositions, float alpha)
        {
            if (graphics != null && !string.IsNullOrEmpty(waterMarkText.TrimEnd(new char[] { ' ' })))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int[] numArray = new int[] { 0x10, 14, 12, 10, 8, 6, 4, 3, 2, 1 };
                Font font = null;
                SizeF ef = new SizeF();
                for (int i = 0; i < numArray.Length; i++)
                {
                    font = new Font("Verdana", (float)numArray[i], FontStyle.Bold);
                    ef = graphics.MeasureString(waterMarkText, font);
                    if (((ushort)ef.Width) < ((ushort)(imageWidth * 0.8)))
                    {
                        break;
                    }
                }
                float height = ef.Height;
                float width = ef.Width;
                float x = 0f;
                float y = 0f;
                switch (WaterMarkPositions)
                {
                    case WaterMarkPositions.LeftTop:
                        x = imageWidth * 0.01f + width / 2f;
                        y = imageHeight * 0.01f;
                        break;

                    case WaterMarkPositions.CenterTop:
                        x = ((float)imageWidth) / 2f;
                        y = imageHeight * 0.01f;
                        break;

                    case WaterMarkPositions.RightTop:
                        x = imageWidth * 0.99f - width / 2f;
                        y = imageHeight * 0.01f;
                        break;

                    case WaterMarkPositions.LeftMiddle:
                        x = imageWidth * 0.01f + width / 2f;
                        y = ((float)imageHeight) / 2f - height / 2f;
                        break;

                    case WaterMarkPositions.Center:
                        x = ((float)imageWidth) / 2f;
                        y = ((float)imageHeight) / 2f - height / 2f;
                        break;

                    case WaterMarkPositions.RightMiddle:
                        x = imageWidth * 0.99f - width / 2f;
                        y = ((float)imageHeight) / 2f - height / 2f;
                        break;

                    case WaterMarkPositions.LeftBottom:
                        x = imageWidth * 0.01f + width / 2f;
                        y = imageHeight * 0.99f - height;
                        break;

                    case WaterMarkPositions.CenterBottom:
                        x = ((float)imageWidth) / 2f;
                        y = imageHeight * 0.99f - height;
                        break;

                    case WaterMarkPositions.RightBottom:
                        x = imageWidth * 0.99f - width / 2f;
                        y = imageHeight * 0.99f - height;
                        break;
                }
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(Convert.ToInt32((float)(255f * alpha)), 0, 0, 0)))
                {
                    graphics.DrawString(waterMarkText, font, brush, x + 1f, y + 1f, format);
                    brush.Dispose();
                }

                using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(0x99, 0xff, 0xff, 0xff)))
                {
                    graphics.DrawString(waterMarkText, font, brush2, x, y, format);
                    brush2.Dispose();
                }

            }

            return graphics;
        }

        /// <summary>
        /// 在原图上添加水印
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="waterMarkText"></param>
        /// <param name="WaterMarkPositions"></param>
        /// <param name="alpha">透明度,取值0-1之间的小数</param>
        /// <param name="waterMarkQuality">水印的质量，取值0-100</param>
        /// <returns></returns>
        public static Stream AddWaterMarkText(Image sourceImage, string waterMarkText, WaterMarkPositions WaterMarkPositions, float alpha, int waterMarkQuality)
        {
            Stream newImageStream = null;
            if (sourceImage == null || string.IsNullOrEmpty(waterMarkText.TrimEnd(new char[] { ' ' })))
            {
                newImageStream = null;
            }
            else
            {
                int width = sourceImage.Width;
                int height = sourceImage.Height;
                using (Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                {
                    image.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawImage(sourceImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
                        AddWaterMarkText(graphics, width, height, waterMarkText, WaterMarkPositions, alpha);
                        newImageStream = new MemoryStream();
                        ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
                        EncoderParameters encoderParams = new EncoderParameters(1);
                        encoderParams = new EncoderParameters();
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)waterMarkQuality);
                        image.Save(newImageStream, encoderInfo, encoderParams);
                        image.Dispose();
                        //sourceImage.Dispose(); //这个原来的Image不应该在这里释放
                        graphics.Dispose();
                    }
                }
            }

            return newImageStream;
        }

        /// <summary>
        /// 在原图上添加水印
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="waterMarkText"></param>
        /// <param name="WaterMarkPositions"></param>
        /// <param name="alpha">透明度,取值0-1之间的小数</param>
        /// <param name="waterMarkQuality">水印的质量，取值0-100</param>
        /// <returns></returns>
        public static Stream AddWaterMarkText(Stream sourceStream, string waterMarkText, WaterMarkPositions WaterMarkPositions, float alpha, int waterMarkQuality)
        {
            Stream newImageStream = null;
            Image sourceImage = null;
            try
            {
                sourceImage = Image.FromStream(sourceStream, true);
            }
            catch
            {
            }

            if (sourceImage != null)
            {
                newImageStream = AddWaterMarkText(sourceImage, waterMarkText, WaterMarkPositions, alpha, waterMarkQuality);
            }

            return newImageStream;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 通过Mime获取图像的编码（解码）器信息
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            mimeType = mimeType.ToLower();
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo info in imageEncoders)
            {
                if (info.MimeType.ToLower() == mimeType)
                {
                    return info;
                }
            }
            return null;
        }
        #endregion
    }
}
