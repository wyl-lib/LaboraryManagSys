using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace initView.Properties
{
    class DBUtil
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public class VerifyCodeHelper
        {
            public static Bitmap CreateVerifyCode(out string code)              //生成验证码,返回为BitMap类型
            {
                //建立Bitmap对象，绘图
                Bitmap bitmap = new Bitmap(200, 60);
                Graphics graph = Graphics.FromImage(bitmap);
                graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, 200, 60);
                Font font = new Font(System.Drawing.FontFamily.GenericSerif, 48, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
                Random r = new Random();
                string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";

                StringBuilder sb = new StringBuilder();

                //添加随机的五个字母
                for (int x = 0; x < 5; x++)
                {
                    string letter = letters.Substring(r.Next(0, letters.Length - 1), 1);
                    sb.Append(letter);
                    graph.DrawString(letter, font, new SolidBrush(System.Drawing.Color.Black), x * 38, r.Next(0, 15));
                }
                code = sb.ToString();

                //混淆背景
                System.Drawing.Pen linePen = new System.Drawing.Pen(new SolidBrush(System.Drawing.Color.Black), 2);
                for (int x = 0; x < 6; x++)
                    graph.DrawLine(linePen, new System.Drawing.Point(r.Next(0, 199), r.Next(0, 59)), new System.Drawing.Point(r.Next(0, 199), r.Next(0, 59)));
                return bitmap;
            }
        }

        public class ImageFormatConvertHelper                   // 从bitmap转换成ImageSource  
        {
            [DllImport("gdi32.dll", SetLastError = true)]
            private static extern bool DeleteObject(IntPtr hObject);
            public static ImageSource ChangeBitmapToImageSource(Bitmap bitmap)
            {
                //Bitmap bitmap = icon.ToBitmap();  
                IntPtr hBitmap = bitmap.GetHbitmap();
                ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                if (!DeleteObject(hBitmap))
                {
                    throw new System.ComponentModel.Win32Exception();
                }
                return wpfBitmap;
            }
            public static string GetImage()
            {
                string code = "";
                Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
                ImageSource imageSource = ChangeBitmapToImageSource(bitmap);
                //Image.Source = imageSource;
                return code;
            }
        }

        /*C#发送邮箱服务
         * 
            mailFrom : 发件人地址 
            mailTo : 收件人地址
            MessageBody : 邮件内容
            MessageSubject : 邮件主题
             */
        public static void btnEmailCode_Click(string mailTo, string MessageBody, string MessageSubject)
        {
            string mailFrom = "1346788525@qq.com";
            MailAddress MessageFrom = new MailAddress(mailFrom); //发件人邮箱地址
            string activeCode = Guid.NewGuid().ToString().Substring(0, 6);//生成激活码     
            Console.WriteLine("生成激活码 activeCode: " + activeCode);

            if (Send(mailTo, MessageFrom, MessageSubject, MessageBody))
            {
                Console.WriteLine("发送邮件成功");
            }
            else
            {
                Console.WriteLine("发送邮件失败");
            }

        }
        public static bool Send(string MessageTo, MailAddress MessageFrom, string MessageSubject, string MessageBody)
        {
            MailMessage message = new MailMessage();
            message.From = MessageFrom;
            message.To.Add(MessageTo); //收件人邮箱地址可以是多个以实现群发、群发群发！！！！
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            message.IsBodyHtml = true; //是否为html格式
            message.Priority = MailPriority.High; //发送邮件的优先等级
            SmtpClient sc = new SmtpClient();
            sc.Host = "smtp.qq.com"; //指定发送邮件的服务器地址或IP
            sc.Port = 25; //指定发送邮件端口
            /*
            * email :发件人的邮箱账号
            * vwfiljgpayxifeba：这个是你发送邮件的一个令牌，你需要在邮箱中申请之后会给你一个这样的令牌(字符串)
            * **/
            string email = "1346788525@qq.com";
            sc.Credentials = new System.Net.NetworkCredential(email, "vwfiljgpayxifeba"); //指定登录服务器的用户名和密码(发件人的邮箱登陆密码)
            try
            {
                sc.Send(message); // 发送邮件
                return true;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            //已释放此对象。
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (SmtpFailedRecipientException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)  //邮件发送被取消
            {
                MessageBox.Show("发送被取消!");
            }
            if (e.Error != null)   //邮件发送失败
            {
                MessageBox.Show("发送失败!");
            }
            else   //发送成功
            {
                MessageBox.Show("发送成功!");
            }
        }

    }
}