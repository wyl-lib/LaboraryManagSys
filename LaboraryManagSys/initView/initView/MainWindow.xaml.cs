using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using initView.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Drawing;
using static initView.Properties.DBUtil;
using login;

namespace initView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static RegisteForm registeFrom;
        static funcSelectForm selectForm;
        
        //创建people数组
        ObservableCollection<kc_user> peopleList = new ObservableCollection<kc_user>();
        string imageCode = "";
        
        public MainWindow()
        {
            InitializeComponent();
            //图形验证码
            ValidCode validCode;
            validCode = new ValidCode(4, ValidCode.CodeType.Alphas);
            verifyImageCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            imageCode = validCode.CheckCode.Trim().ToUpper();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)//鼠标操作，拖动窗口
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(gridRoot);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < gridRoot.ActualWidth && position.Y >= 0 && position.Y < gridRoot.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        //回车登陆事件，暂时有问题没有实现！！！
        private void txt_Pwd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    login_Click(loginButton, null);
                    break;

                default:
                    break;
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            int uID = 0;
            string uPhone = "";
            if (uIdTextBox.Text != "")
            {
                if (uIdTextBox.Text.Length == 10)//学号
                {
                    uID = int.Parse(uIdTextBox.Text);
                }
                else//手机号
                {
                    uPhone = uIdTextBox.Text;
                }
            }
            else
            {
                MessageBox.Show("学号/手机号输入有误,请检查!");
                return;
            }
            string uName = "";
            string uPass = uPasstextBox.Password;
            string uInfo = "select * from kc_user where uID='" + uID + "' or uPhone='" + uPhone + "' and uPass='" + uPass + "' ";

            DataSet record = new DataSet();
            selectForm = new funcSelectForm();
            //根据SQL执行后接受数据库DataSet返回值
            record = DBTool.ExecuteQuery(uInfo);
            try
            {
                 uName = record.Tables[0].Rows[0][2].ToString();
                if (record == null && record.Tables.Count <= 0 && record.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("用户名或密码有误,请检查!");
                    return;
                }
            }
            catch
            {
                return;
            }

            //传递数据到多个窗体中，以便数据操作
            funcSelectForm.getRecord = record;
            borrowForm.getRecord = record;
            feedBack.getRecord = record;

            //验证码是否正确
            string inputCode = verifyCode.Text.Trim().ToUpper();//大写
            Console.WriteLine("imageCode: " + imageCode);
            Console.WriteLine("inputCode: " + inputCode);

            //验证用户是否存在
 
            if (imageCode.Equals(inputCode))
            {
                Console.WriteLine("record: " + record);
                MessageBox.Show("你好," + uName, "登录成功");

                //隐藏当前窗口
                this.ShowInTaskbar = false;
                this.Visibility = Visibility.Hidden;

                //打开主功能界面
                selectForm.Show();
            }
            else
            {
                MessageBox.Show("验证码不正确!", "提示");
                verifyCode.Clear();//Close the dialog directly
            }
            //刷新图片验证码
            ValidCode validCode;
            validCode = new ValidCode(4, ValidCode.CodeType.Alphas);
            verifyImageCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            imageCode = validCode.CheckCode.Trim().ToUpper();

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //完全退出
            this.Close();
            System.Environment.Exit(System.Environment.ExitCode);
        }
        private void 注册textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //注册功能界面
            registeFrom = new RegisteForm();
            registeFrom.Show();
        }

        private void 注册textBlock_MouseMove(object sender, MouseEventArgs e)
        {
            注册textBlock.TextDecorations = TextDecorations.Underline;
        }

        private void 注册textBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            注册textBlock.TextDecorations = null;
        }

        private void 忘密textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void 忘密textBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            忘密textBlock.TextDecorations = null;
            忘密textBlock.FontWeight = FontWeights.Normal;
        }

        private void 忘密textBlock_MouseMove(object sender, MouseEventArgs e)
        {
            忘密textBlock.TextDecorations = TextDecorations.Underline;
            忘密textBlock.FontWeight = FontWeights.Bold;
        }

        private void 看不清textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //图片验证码部分
            ValidCode validCode;
            validCode = new ValidCode(4, ValidCode.CodeType.Alphas);
            verifyImageCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            imageCode = validCode.CheckCode.Trim().ToUpper();
        }

        private void 看不清textBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            看不清textBlock.TextDecorations = null;
            看不清textBlock.FontWeight = FontWeights.Normal;
        }

        private void 看不清textBlock_MouseMove(object sender, MouseEventArgs e)
        {
            看不清textBlock.TextDecorations = TextDecorations.Underline;
            看不清textBlock.FontWeight = FontWeights.Bold;
        }

        private void UIdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            string values = "请输入学号/手机号";

            if (uIdTextBox.Text.Equals(""))
            {
                uIdTextBox.Text = "请输入学号/手机号";
            }
            if (values.Equals(uIdTextBox.Text))
            {
                uIdTextBox.Clear();
            }
            else
            {
                return;
            }
        }

        private void UPasstextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
