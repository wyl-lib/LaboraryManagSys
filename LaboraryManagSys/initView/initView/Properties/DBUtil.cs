using Caliburn.Micro;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        /*
         * 发送邮件时的配置
         */
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

        /*
         * 检测学号是否合法以及是否重复
         */
        public static bool IsNumeric(string str,int num)
        {
            try
            {
                if (str == null || str.Length != num)
                {
                    MessageBox.Show("学号/手机号输入有误,请检查!");
                    return false;
                }
                ASCIIEncoding ascii = new ASCIIEncoding();//new ASCIIEncoding 的实例
                byte[] bytestr = ascii.GetBytes(str); //把string类型的参数保存到数组里

                foreach (byte c in bytestr) //遍历这个数组里的内容
                {
                    if (c < 48 || c > 57)  //判断是否为数字
                    {
                        MessageBox.Show("学号/手机号含有非数字,请检查!");
                        return false;
                    }
                    //备注 数字，字母的ASCII码对照表
                    /*
                    0~9数字对应十进制48－57
                    a~z字母对应的十进制97－122十六进制61－7A
                    A~Z字母对应的十进制65－90十六进制41－5A
                    */
                }
            }
            catch
            {
                MessageBox.Show("系统遇到某个错误码1004,请通知管理员!");
                return false;
            }
            //学号是否已被注册
            DataSet record = new DataSet();
            try
            {
                int uId = int.Parse(str.ToString().Trim());
                string uInfo = "select * from kc_user where uID='" + uId + "'";
                string uName = record.Tables[0].Rows[0][2].ToString();
            }
            catch
            {
                return true;
            }
            finally
            {
                record.Dispose();
            }
            return true;
        }
        /*
        * 检测身份证号是否合法
        */
        public static bool IsIdCard(string idCardNumber)
        {
            string idNumber = idCardNumber;
            bool result = true;
            try
            {
                if (idNumber.Length != 18)
                {
                    return false;
                }
                long n = 0;
                if (long.TryParse(idNumber.Remove(17), out n) == false
                    || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证  
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(idNumber.Remove(2)) == -1)
                {
                    return false;//省份验证  
                }
                string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证  
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = idNumber.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
                {
                    MessageBox.Show("身份证校验失败！");
                    return false;//校验码验证  
                }
                return true;//符合GB11643-1999标准 
            }
            catch
            {
                MessageBox.Show("系统遇到某个错误码1002,请通知管理员!");
                result = false;
            }
            return result;
        }

        /*
         * 检测手机号码是否合法
         */
        public static bool IsPhone(string input)
        {
            try
            {
                if (input.Length < 11)
                {
                    MessageBox.Show("手机号输入有误！");
                    return false;
                }
                //电信手机号码正则
                string dianxin = @"^1[3578][01379]\d{8}$";
                Regex regexDX = new Regex(dianxin);
                //联通手机号码正则
                string liantong = @"^1[34578][01256]\d{8}";
                Regex regexLT = new Regex(liantong);
                //移动手机号码正则
                string yidong = @"^(1[012345678]\d{8}|1[345678][012356789]\d{8})$";
                Regex regexYD = new Regex(yidong);
                if (regexDX.IsMatch(input) || regexLT.IsMatch(input) || regexYD.IsMatch(input))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("手机号输入有误！");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("系统遇到某个错误码1003,请通知管理员!");
                return false;
            }
        }

        /*
         * 检测邮箱号是否合法
         */
        public static bool IsEmial(string emailAddress)
        {
            //有“@”
            if (emailAddress.IndexOf("@") == -1)
            {
                Console.WriteLine("输入的字符串中 没有@ ！");
                return false;
            }

            //只有一个“@”
            if (emailAddress.IndexOf("@") != emailAddress.LastIndexOf("@"))
            {
                Console.WriteLine("输入的字符串中 有多个@ ！");
                return false;
            }

            //有“.”
            if (emailAddress.IndexOf(".") == -1)
            {
                Console.WriteLine("输入的字符串中 没有.  ！");
                return false;
            }

            //“@”出现在第一个“.”之前
            if (emailAddress.IndexOf("@") > emailAddress.IndexOf("."))
            {
                Console.WriteLine("输入的字符串中 @没有出现在.之前！");
                return false;
            }

            //“@”不可以是第一个元素
            if (emailAddress.StartsWith("@"))
            {
                Console.WriteLine("输入的字符串中 @是第一个元素！");
                return false;
            }

            //“.”不可以是最后一位
            if (emailAddress.EndsWith("."))
            {
                Console.WriteLine("输入的字符串中 .是最后一位！");
                return false;
            }

            //不能出现“@.”
            if (emailAddress.IndexOf("@.") != -1)
            {
                Console.WriteLine("输入的字符串中 出现了@. !");
                return false;
            }

            //不能出现“..”
            if (emailAddress.IndexOf("..") != -1)
            {
                Console.WriteLine("输入的字符串中 出现了.. !");
                return false;
            }

            return true;
        }
        
        /// <summary>   
        /// 判断输入的字符串是否是一个合法的Email地址   
        /// </summary>   
        /// <param name="input"></param>   
        /// <returns></returns>   
        public static bool IsEmail_2(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}