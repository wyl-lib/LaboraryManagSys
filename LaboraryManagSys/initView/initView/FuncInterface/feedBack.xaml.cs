using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
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
    /// feedBack.xaml 的交互逻辑
    /// </summary>
    public partial class feedBack : Window
    {
        public static DataSet getRecord { get; set; }
        public feedBack()
        {
            InitializeComponent();
        }

       /*
       * 鼠标操作，拖动窗口
       */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(feedEmail);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < feedEmail.ActualWidth && position.Y >= 0 && position.Y < feedEmail.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            string mailTo = "1346788525@qq.com";//接受反馈的管理员
            string uMesg = feedBackTextBox.Text+"    FeedBack End!!      ";
            uMesg += getRecord.Tables[0].Rows[0][0].ToString().Trim()+" 、 ";//uID
            uMesg += getRecord.Tables[0].Rows[0][2].ToString().Trim() + " 、 ";//uName
            uMesg += getRecord.Tables[0].Rows[0][3].ToString().Trim() + " 、 ";//uProClass
            uMesg += getRecord.Tables[0].Rows[0][7].ToString().Trim();//uEmail
            string MessageSubject = "科创实验室管理系统_用户反馈信息";
            DBUtil.BtnEmailCode_Click(mailTo, uMesg, MessageSubject);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //关闭当前窗体
            this.Close();
        }
    }
}
