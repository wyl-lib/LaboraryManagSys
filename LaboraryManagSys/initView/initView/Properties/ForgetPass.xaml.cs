using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace initView.Properties
{
    /// <summary>
    /// ForgetPass.xaml 的交互逻辑
    /// </summary>
    public partial class ForgetPass : Window
    {
        private static readonly Random random = new Random();
        static string randomCode = random.Next().ToString().Substring(0, 6);

        public ForgetPass()
        {
            InitializeComponent();
            /*
            uPasstextBox.TextDecorations = new TextDecorationCollection(new TextDecoration[] {new TextDecoration()
            {
                Location = TextDecorationLocation.Strikethrough,
                Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.White, 10f)
                {
                    DashCap = PenLineCap.Round,
                    StartLineCap = PenLineCap.Round,
                    EndLineCap = PenLineCap.Round,
                    DashStyle = new DashStyle(new double[] { 0.0, 1.2 }, 0.6f)
                }
            }
            });*/
        }

        /*
        * 鼠标操作，拖动窗口
        */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(BackPass);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < BackPass.ActualWidth && position.Y >= 0 && position.Y < BackPass.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        

        private void NewPassText_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void NewPassText_Again_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            //获取信息
            string uAcount = uIDorPhone.Text.Trim().ToUpper();
            string uEmail = uEmailBox.Text.Trim().ToUpper();
            string passwordPassed = uBeforePassTextBox.Password.Trim();
            string uPass = newPassText.Password.Trim();
            string uPass_Again = newPassText_Again.Password.Trim();
            string emailCode = verifyCodeTextBox.Text.ToString().Trim();

            //首先验证学号/手机号的合法性
            if (!DBUtil.IsNumeric(uAcount, true))
            {
                MessageBox.Show("uAcount error");
                return;
            }

            //其次验证邮箱正确性以及验证码
            if (!DBUtil.IsEmial(uEmail))
            {
                MessageBox.Show("uEmail error");
                return;
            }
            if (emailCode.Equals(randomCode))
            {
                MessageBox.Show("randomCode error");
                return;
            }

            //然后验证密码是否一致
            if (!uPass.Equals(uPass_Again))
            {
                MessageBox.Show("uPass error");
                return;
            }

            string changePass = "update kc_user set uPass = '"+ uPass + "' where uID = ( SELECT uID FROM kc_user where(uID= '" + uAcount + "' or uPhone ='" + uAcount + "') and uEmail = '" + uEmail + "'" +
                " and(uPass=  '" + passwordPassed + "' or uPassBefore ='" + passwordPassed + "'))";
            try
            {
                //首先查询是否重复，先提示警告用户自行选择是数量添加还是取消
                int fin = DBTool.ExecuteUpdate(changePass);
                MessageBox.Show("操作成功", "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch
            {
                //查询到不重复
                return;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            /* string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
             if (message.Equals("Cancel"))
             {
                 return;
             }
             //完全退出
             this.Close();
             System.Environment.Exit(System.Environment.ExitCode);*/
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //关闭当前窗体
            this.Close();
        }

        private void SendEmailBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //1、校验邮箱非空  2、合法邮箱
            if (!DBUtil.IsEmial(uEmailBox.Text.Trim()))//uEmail验证
            {
                MessageBox.Show("请确保邮箱的正确性!");
                return;
            }

            else
            {
                //发送验证码到用户邮箱
                randomCode = random.Next().ToString().Substring(0, 6);
                Console.WriteLine("随机验证码 randomCode: " + randomCode);
                string mailTo = this.uEmailBox.Text.Trim();
                string uMesg = "【济大泉院科技创新实验室】欢迎注册，您本次的验证码为：" + randomCode + "，该验证码5分钟内有效，请勿泄露给其他人！";
                string MessageSubject = "科创实验室管理系统_随机验证码";
                try
                {
                    DBUtil.BtnEmailCode_Click(mailTo, uMesg, MessageSubject);
                }
                catch
                {
                    return;
                }
            }
        }

        private void SendEmailBlock_MouseLeave(object sender, MouseEventArgs e)
        {
           
            sendEmailBlock.TextDecorations = null;
            sendEmailBlock.FontWeight = FontWeights.Normal;
        }

        private void SendEmailBlock_MouseMove(object sender, MouseEventArgs e)
        {
            sendEmailBlock.FontWeight = FontWeights.Bold;
            sendEmailBlock.TextDecorations = TextDecorations.Underline;
        }

        private void NewPassText_Again_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (newPassText.Password == newPassText_Again.Password)
            {
                textBlock.Text = "✔";
            }
            else
            {
                textBlock.Text = "✘";
            }
        }
    }
}
