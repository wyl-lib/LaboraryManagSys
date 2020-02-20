using System;
using System.Collections.Generic;
using System.Configuration;
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
        static Random random = new Random();
        static string randomCode = random.Next().ToString().Substring(0,6);

        public RegisteForm()
        {            
            InitializeComponent();
        }

        private void registeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DBUtil.IsNumeric(uIdTextBox.Text, 10))//uID验证
            {
                return;
            }
            int uID = int.Parse(uIdTextBox.Text);
            int uIdentityID = 2;//用户身份
            string uName = uNameTextBox.Text.Trim();//用户姓名
            string uProClass = uPassWordTextBox.Password.Trim();//用户专业班级
            if (!DBUtil.IsPhone(uPhoneTextBox.Text))//uPhone验证
            {
                return;
            }
            string uPhone = uPhoneTextBox.Text.Trim();
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

            if (randomCode == verifyCodeTextBox.Text)
            {
                MessageBox.Show("验证码验证成功！");
            }
            else
            {
                MessageBox.Show("验证码验证失败！");
                return;
            }

            /*
             *  向数据库中插入用户记录
             */
            string insertUser = "Insert into kc_user values("+uID+","+uIdentityID+",'"+uName+"','"+uProClass+"','"+uPhone+"','"+uPass+"','"+uPassBefore+ "','"+uEmail+"','"+uIdCard+"','"+uBankCard+"','"+uRegisteTime+"')";

            int returnCode = DBTool.ExecuteUpdate(insertUser);
            if (returnCode>0)
            {
                MessageBox.Show("Yes,恭喜注册成功！");
            }
            else
            {
                MessageBox.Show("Error,出现了某些错误导致注册失败！");
            }

        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            //1、校验邮箱非空  2、合法邮箱
            string mailTo = this.verifyEmailTextBox.Text.Trim();
            if (string.IsNullOrEmpty(mailTo))
            {
                MessageBox.Show("个人邮箱不能为空!");
            }
            else
            {
                randomCode = random.Next().ToString().Substring(0, 6);
                //发送验证码到用户邮箱
                Console.WriteLine("随机验证码 randomCode: " + randomCode);
                string uMesg = randomCode;
                string MessageSubject = "科创实验室管理系统_随机验证码";
                try
                {
                    DBUtil.btnEmailCode_Click(mailTo, uMesg, MessageSubject);

                    MessageBox.Show("新用户邮箱验证码_发送成功");
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

        private void SendEmailBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //1、校验邮箱非空  2、合法邮箱
            string mailTo = this.verifyEmailTextBox.Text.Trim();
            if (string.IsNullOrEmpty(mailTo))
            {
                MessageBox.Show("请确保邮箱的正确性!");
            }
            else
            {
                randomCode = random.Next().ToString().Substring(0, 6);
                //发送验证码到用户邮箱
                Console.WriteLine("随机验证码 randomCode: " + randomCode);
                string uMesg = randomCode;
                string MessageSubject = "科创实验室管理系统_随机验证码";
                try
                {
                    DBUtil.btnEmailCode_Click(mailTo, uMesg, MessageSubject);

                    MessageBox.Show("新用户邮箱验证码_发送成功");
                }
                catch
                {
                    return;
                }
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
    }
}
