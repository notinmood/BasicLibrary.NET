//using System;
//using System.Drawing;
//using System.IO;
//using System.Web;
//using System.Web.SessionState;
//using Hiland.BasicLibrary.Data;
//using Hiland.BasicLibrary.UI;

//namespace Hiland.BasicLibrary.Handler
//{
//    public class AuthCodeHandler : IHttpHandler, IRequiresSessionState
//    {
//        private Bitmap CreateImage(string authCodeString)
//        {
//            /* -----------------------------绘制图片的样式 ------------------------------------*/

//            int width = authCodeString.Length * 21;
//            int height = 30;
//            Random rad = new Random();
//            Bitmap bmp = new Bitmap(width, height);
//            using (Graphics grp = Graphics.FromImage(bmp))// 在图片上绘制图形
//            {
//                grp.Clear(AuthCode.backColor);//填充bmp的背景色
//                grp.DrawRectangle(new Pen(Color.Red, 1), 0, 0, width - 1, height - 1);//绘制边框
//                int num = width * height;
//                for (int i = 0; i < num; i++)//在图片的指定坐标上画上有颜色的圆点
//                {
//                    int x = rad.Next(width);
//                    int y = rad.Next(height);
//                    int r = rad.Next(255);
//                    int g = rad.Next(255);
//                    int b = rad.Next(255);
//                    Color c = Color.FromArgb(r, g, b);
//                    bmp.SetPixel(x, y, c);//在图片的指定坐标上画上有颜色的圆点
//                }

//                /*-------------------------- 在图片绘制字符串------------------------------------ */

//                Font f = new Font("Arial", 20, FontStyle.Bold);//定义字体
//                Brush br = new SolidBrush(AuthCode.foreColor);//定义画笔的颜色 及字体的颜色
//                for (int i = 0; i < authCodeString.Length; i++)
//                {
//                    string s = authCodeString.Substring(i, 1);//单个单个的将字画到图片上
//                    Point p = new Point(i * 20 + rad.Next(3), rad.Next(3) + 1);//字体出现的位置（坐标）
//                    grp.DrawString(s, f, br, p);//绘制字符串
//                }
//                grp.Dispose();
//            }
//            return bmp;
//        }


//        /// <summary>
//        /// 是否可以处理远程的HTTP请求
//        /// </summary>
//        public bool IsReusable
//        {
//            get { return true; }
//        }

//        /// <summary>
//        /// 将验证码图片发送给WEB浏览器
//        /// </summary>
//        /// <param name="context"></param>
//        public void ProcessRequest(HttpContext context)
//        {
//            int charCount = AuthCode.charCount;
//            using (MemoryStream ms = new MemoryStream()) //  创建内存流(初始长度为0 自动扩充)
//            {
//                string authCodeString = RandomHelper.GetRandomString(AuthCode.authCodeCharStyle,charCount);// 获得验证码字符
//                context.Session.Add("HiLand.AuthCodeValue", authCodeString);//将验证码字符保存到session里面
//                Bitmap theBitmap = CreateImage(authCodeString);// 获得验证码图片
//                theBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);//将位图写入内存流
//                context.Response.ClearContent();  //清除缓冲区里的所有内容输出
//                context.Response.ContentType = "image/jpeg"; //需要输出图象信息 要修改HTTP头
//                context.Response.BinaryWrite(ms.ToArray()); //将内存流写入HTTP输出流
//                theBitmap.Dispose(); //释放资源
//                ms.Close();//释放资源
//                ms.Dispose();//释放资源
//            }
//            context.Response.End();
//        }
//    }
//}
