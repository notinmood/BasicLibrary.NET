using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace WebResourceCollection
{
    public partial class SendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                MailMessage mailMessage = GetMail();

                smtpClient.Send(mailMessage);
            }
        }

       

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("notinmood@gmail.com", "zoomsoft.xier");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mailMessage = GetMail();
                smtpClient.Send(mailMessage);
            }
        }

        private static MailMessage GetMail()
        {
            MailMessage mailMessage = new MailMessage();
            MailAddress fromAddress = new MailAddress("notinmood@gmail.com", "解然");
            MailAddress toAddress = new MailAddress("develope@foxmail.com", "解小然");

            mailMessage.From = fromAddress;
            mailMessage.To.Add(toAddress);
            mailMessage.Body = "你好nihao";
            mailMessage.Subject = "你好";
            
            /*
             * string plainTextBody = "如果你邮件客户端不支持HTML格式，或者你切换到“普通文本”视图，将看到此内容";   
            mm.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextBody, null, "text/plain"));   
  
            ////HTML格式邮件的内容   
            string htmlBodyContent = "如果你的看到<b>这个</b>， 说明你是在以 <span style=\"color:red\">HTML</span> 格式查看邮件<br><br>";   
            htmlBodyContent += "<a href=\"http://www.fenbi360.net粉笔编程网</a> <img src=\"cid:weblogo\">";   //注意此处嵌入的图片资源   
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(htmlBodyContent, null, "text/html");   
  
              
            LinkedResource lrImage = new LinkedResource(@"d:\1.jpg", "image/gif");   
            lrImage.ContentId = "weblogo"; //此处的ContentId 对应 htmlBodyContent 内容中的 cid: ，如果设置不正确，请不会显示图片   
            htmlBody.LinkedResources.Add(lrImage);   
  
            mm.AlternateViews.Add(htmlBody);   
  
            ////要求回执的标志   
            mm.Headers.Add("Disposition-Notification-To", "test@163.com");   
  
            ////自定义邮件头   
            mm.Headers.Add("X-Website", "http://www.fenbi360.net");   
  
            ////针对 LOTUS DOMINO SERVER，插入回执头   
            mm.Headers.Add("ReturnReceipt", "1");   
  
            mm.Priority = MailPriority.Normal; //优先级   
            mm.ReplyTo = new MailAddress("test2@163.com", "我自己");   
  
            ////如果发送失败，SMTP 服务器将发送 失败邮件告诉我   
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;   
             * 
             */

            return mailMessage;
        }
    }
}