using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// borrowForm.xaml 的交互逻辑
    /// </summary>
    public partial class RegisteForm : Window
    {
        private static readonly Random random = new Random();
        static string randomCode = random.Next().ToString().Substring(0,6);

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)//鼠标操作，拖动窗口
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(Registed);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < Registed.ActualWidth && position.Y >= 0 && position.Y < Registed.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        public RegisteForm()
        {            
            InitializeComponent();
        }

        private void RegisteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DBUtil.IsNumeric(uIdTextBox.Text, false))//uID验证
            {
                return;
            }
            int uID = 2020000000;
            try
            {
                uID = int.Parse(uIdTextBox.Text.Trim());
            }
            catch
            {
                MessageBox.Show("学号第一位输入错误，请检查!");
                return;
            }
           
            int uIdentityID = 2;//用户身份
            string uName = uNameTextBox.Text.Trim();//用户姓名
            string uProClass = uPassWordTextBox.Password.Trim();//用户专业班级
            if (!DBUtil.IsPhone(uPhoneTextBox.Text))//uPhone验证
            {
                return;
            }
            string uPhone = uPhoneTextBox.Text.Trim();
            if (uPassWordTextBox.Password.Length < 6)
            {
                MessageBox.Show("警告: 密码位数小于六位!");
                return;
            }
            string uPass = uPassWordTextBox.Password.Trim();                     //现在密码
            string uPassBefore = uPassWordTextBox_Again.Password.Trim();        //曾用密码
            if (!uPass.Equals(uPassBefore))
            {
                MessageBox.Show("两次密码输入不一致!");
                return;
            }
            if (!DBUtil.IsEmial(verifyEmailTextBox.Text.Trim()))//uEmail验证
            {
                return;
            }
            string uEmail = verifyEmailTextBox.Text.Trim();
            if (!DBUtil.IsIdCard(uIDCardTextBox.Text))//uPhone验证
            {
                return;
            }
            string uIdCard = uIDCardTextBox.Text.Trim();
            string uBankCard = uBankCardTextBox.Text.Trim();//uBankCard验证
            string uRegisteTime = DateTime.Now.ToLocalTime().ToString().Trim();// 2008-9-4 0:00:00                

            if (randomCode != verifyCodeTextBox.Text.Trim())
            {
                MessageBox.Show("验证码验证失败！");
                return;
            }

            /*
             *  向数据库中插入用户记录
             */
            string insertUser = "Insert into kc_user values("+uID+","+uIdentityID+",'"+uName+"','"+uProClass+"','"+uPhone+"','"+uPass+"','"+uPassBefore+ "','"+uEmail+"','"+uIdCard+"','"+uBankCard+"','"+uRegisteTime+"')";
            try
            {
                int returnCode = DBTool.ExecuteUpdate(insertUser);
                if (returnCode > 0)
                {
                    MessageBox.Show("注册成功，欢迎加入。");
                }
            }
            catch
            {
                MessageBox.Show("系统遇到某个错误码1005,请通知管理员!");
            }
        }

        private void SendEmailBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //1、校验邮箱非空  2、合法邮箱
            if (!DBUtil.IsEmial(verifyEmailTextBox.Text.Trim()))//uEmail验证
            {
                MessageBox.Show("请确保邮箱的正确性!");
                return;
            }

            else
            {
                //发送验证码到用户邮箱
                randomCode = random.Next().ToString().Substring(0, 6);
                Console.WriteLine("随机验证码 randomCode: " + randomCode);
                string mailTo = this.verifyEmailTextBox.Text.Trim();
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //关闭当前窗口
            this.Close();
        }

        private void UPassWordTextBox_Again_LostFocus(object sender, RoutedEventArgs e)
        {
            if(uPassWordTextBox_Again.Password == uPassWordTextBox.Password)
            {
                textBlock.Text = "✔";
            }
            else
            {
                textBlock.Text = "✘";
            }
        }

        private void SendEmailBlock_MouseMove(object sender, MouseEventArgs e)
        {
            sendEmailBlock.FontWeight = FontWeights.Bold;
            sendEmailBlock.TextDecorations = TextDecorations.Underline;
        }

        private void SendEmailBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            sendEmailBlock.TextDecorations = null;
            sendEmailBlock.FontWeight = FontWeights.Normal;
        }

        private void UBankCardTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("由于位数较多，请再次确认!!!!");
        }

        private void UPassWordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (uPassWordTextBox.Password.Length < 6)
            {
                MessageBox.Show("警告: 密码位数小于六位!");
                return;
            }
        }
    }
}
