using initView.Properties;
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

namespace initView
{
    /// <summary>
    /// project.xaml 的交互逻辑
    /// </summary>
    public partial class Project : Window
    {
        List<string> pMemberList = new List<string>(){
                    "1", "2", "3", "4", "5","6"
            };
        List<string> pDegreeList = new List<string>(){
            "报名","其它","省优秀奖","省三等奖","省二等奖","省一等奖",
            "国家一等奖","国家二等奖","国家三等奖","国家特等奖"
        };

        /*
       * 鼠标操作，拖动窗口
       */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(ProjectInfo);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < ProjectInfo.ActualWidth && position.Y >= 0 && position.Y < ProjectInfo.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        public Project()
        {
            InitializeComponent();

            ComBox_pMember.ItemsSource = pMemberList;
            ComBox_pDegree.ItemsSource = pDegreeList;
        }

        private void SubmitProject_Click(object sender, RoutedEventArgs e)
        {
            //检测是否有空值
            TextBox_ptName.Text = "暂无";
            TextBox_pRemark.Text = "暂无";
            if (!CheckFrmTxt())
            {
                return;
            }
            //获取信息
            string pName = TextBox_pName.Text.Trim();
            string pType = TextBox_pType.Text.Trim();
            string teamName = TextBox_ptName.Text.Trim();
            string pricipal = TextBox_pricipal.Text.Trim();
            string pOthers = TextBox_pOthers.Text.Trim();
            string pRemark = TextBox_pRemark.Text.Trim();
            string pCreateTime = DateTime.Now.ToString().Trim();
            //comBox.Text
            int pMember = int.Parse(ComBox_pMember.Text.Trim());
            string pDegree = ComBox_pDegree.Text.Trim();

            //从数据库获取对应信息
            string projectInsert = "insert into kc_project values('" + pName + "','" + teamName + "','" + pType + "','" + pricipal + "','" + pOthers + "','" +pDegree + "','" + pRemark + "','" +pMember + "','" + pCreateTime + "')";
            int fin1 = DBTool.ExecuteUpdate(projectInsert);
            Console.WriteLine("添加个人借用记录数量: " + fin1);
            MessageBox.Show("操作成功", "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextBox_pOthers_GotFocus(object sender, RoutedEventArgs e)
        {
            string values = "姓名之间用顿号分隔  ( 、)";
            if (values.Equals(TextBox_pOthers.Text.Trim()))
            {
                TextBox_pOthers.Clear();
            }
            else
            {
                return;
            }
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

        private bool CheckFrmTxt()
        {
            //要加上每项字段的合法验证

            if (string.IsNullOrEmpty(this.TextBox_pName.Text.Trim()))
            {
                MessageBox.Show("项目名称不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.TextBox_pType.Text.Trim()))
            {
                MessageBox.Show("大赛名称不正确！");
                return false;
            }

           /* if (string.IsNullOrEmpty(this.TextBox_ptName.Text.Trim()))
            {
                MessageBox.Show("队伍名称不能为空！");
                return false;
            }*/

            if (string.IsNullOrEmpty(this.TextBox_pricipal.Text.Trim()))
            {
                MessageBox.Show("团队负责人不能为空！");
                return false;
            }

            /*if (string.IsNullOrEmpty(this.TextBox_pRemark.Text.Trim()))
            {
                MessageBox.Show("项目备注不能为空！");
                return false;
            }*/
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //关闭当前窗口
            this.Close();
        }

        private void TextBox_pOthers_LostFocus(object sender, RoutedEventArgs e)
        {
            string values = "姓名之间用顿号分隔  ( 、)";
            if (string.IsNullOrEmpty(TextBox_pOthers.Text.Trim()))
            {
                TextBox_pOthers.Text=values;
            }
            else
            {
                return;
            }
        }
    }

}
