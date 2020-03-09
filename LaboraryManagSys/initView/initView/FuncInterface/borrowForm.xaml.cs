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
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data;

namespace initView.Properties
{
    /// <summary>
    /// RegistFrom.xaml 的交互逻辑
    /// </summary>
    public partial class borrowForm : Window
    {
        public static DataSet getRecord { get; set; }

        /*
        * 鼠标操作，拖动窗口
        */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(BorrowInfo);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < BorrowInfo.ActualWidth && position.Y >= 0 && position.Y < BorrowInfo.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        public borrowForm()
        {
            List<string> mName_Bill = new List<string>();
            List<string> mName_Tool = new List<string>();
            List<string> mName_Paper = new List<string>();
            List<string> mName_Other = new List<string>();
            List<string> List_mName = new List<string>() { "元件类" , "工具类" , "纸张类" , "其它类" };

            //列表打头信息
            mName_Bill.Add("======元件类======");
            mName_Tool.Add("======工具类======");
            mName_Paper.Add("======纸张类======");
            mName_Other.Add("======其它类======");

            //物品数量——下拉选择
            List<string> mNum = new List<string>{
                    "1", "2", "3", "4", "5",
                    "6", "7", "8", "9", "10",
                    "11", "12", "13", "14", "15",
                    "16", "17", "18", "19", "20"
            };
            InitializeComponent();

            comBox_mName.Text = mName_Bill[0];
            comBox_mParam.Text = "参数请选择";
            comBox_mNum.Text = "1";
            int typeID = 1;
            while (typeID < 5)
            {
                //从数据库获取对应信息
                DataSet billRecord = new DataSet();
                string billInfo = "select mName from kc_materialInfo where mTypeID='"+ typeID +"' group by mName";
                billRecord = DBTool.ExecuteQuery(billInfo);

                //该类型物品的所有种类
                int count = billRecord.Tables[0].Rows.Count;
                TreeViewItem item = new TreeViewItem() { Header = List_mName[typeID-1] };
    
                switch (typeID)
                {
                    case 1:
                        for (int c = 1; c <= count; c++)
                        {
                            mName_Bill.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                            item.Items.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                        }
                        break;
                    case 2:
                        for (int c = 1; c <= count; c++)
                        {
                            mName_Tool.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                            item.Items.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                        }
                        break;
                    case 3:
                        for (int c = 1; c <= count; c++)
                        {
                            mName_Paper.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                            item.Items.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                        }
                        break;
                    default:
                        for (int c = 1; c <= count; c++)
                        {
                            mName_Other.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                            item.Items.Add(billRecord.Tables[0].Rows[c - 1][0].ToString().Trim());
                        }
                        break;
                }
                treeView.Items.Add(item);
                typeID++;
            }
            List_mName.Clear();
            List_mName=mName_Bill;
            List_mName.AddRange(mName_Tool);
            List_mName.AddRange(mName_Paper);
            List_mName.AddRange(mName_Other);

            comBox_mName.ItemsSource = List_mName;
            comBox_mNum.ItemsSource = mNum;
        }


        private void borrowMaterial_Click(object sender, RoutedEventArgs e)
        {            
            //通用信息;库存数量不管充不充足都通用的
            string mName = comBox_mName.Text.Trim();
            int mNum = int.Parse(comBox_mNum.Text.Trim());
            string mParam = comBox_mParam.Text.Trim();
            int uID = int.Parse(getRecord.Tables[0].Rows[0][0].ToString().Trim());
            string uName = getRecord.Tables[0].Rows[0][2].ToString().Trim();
            DataSet billRecord = new DataSet();
            //最后提交时要判断是否还有余量
            string billInfo = "select mID from kc_materialInfo where mName='"+mName+"' and mParam='"+mParam+"' and mNum>'"+mNum+"'";
            billRecord = DBTool.ExecuteQuery(billInfo);

            //如果数量超出库存走else处理
            if (billRecord.Tables[0].Rows.Count > 0)
            {
                string message = MessageBox.Show("数量为 "+mNum + " ,请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
                if (message.Equals("Cancel"))
                {
                    return;
                }
                //更新库存数
                string materuakInfo = "update kc_materialInfo set mNum -='" + mNum + "'  where mName = '" + mName + "' and mParam = '" + mParam + "'";
                int fin = DBTool.ExecuteUpdate(materuakInfo);
                Console.WriteLine("更新库存数量: " + fin);

                //添加个人借用记录
                int mID = int.Parse(billRecord.Tables[0].Rows[0][0].ToString().Trim());
                
                string mBorrowTime = DateTime.Now.ToString().Trim();
                String mCheckoutTime = "";
                string billStatistics = "insert into kc_billStatistics values(" + mID + ",'" + mName + "'" +
                    ",'" + mParam + "'," + mNum + "," + uID + ",'" + uName + "','" + mBorrowTime + "','" +mCheckoutTime+ "')";
                int fin1 = DBTool.ExecuteUpdate(billStatistics);
                Console.WriteLine("添加个人借用记录数量: " + fin1);
                MessageBox.Show("操作成功","操作提示",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //数量超出库存
                //1、提醒用户物品数量不足
                string message = MessageBox.Show(mName+" 的数量不足 "+mNum+" ,是否提醒管理员？", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
                if (message.Equals("Cancel"))
                {
                    return;
                }
                //2、发送消息至管理员进货处理
                try
                {
                    string mailTo = "1346788525@qq.com";//接受反馈的管理员
                    string uMesg = mName + " With " + mParam + " 、 ";//mName、mParam
                    uMesg += uName + " 、 ";//uName
                    uMesg += getRecord.Tables[0].Rows[0][3].ToString().Trim() + " 、 ";//uProClass
                    uMesg += getRecord.Tables[0].Rows[0][7].ToString().Trim() + "    FeedBack End!!      ";//uEmail
                    string MessageSubject = "科创信息管理系统_物品库存不足提醒";
                    DBUtil.BtnEmailCode_Click(mailTo, uMesg, MessageSubject);
                }
                catch
                {
                    return;
                }
            }
        }

        private void ComBox_mName_LostFocus(object sender, RoutedEventArgs e)
        {
            //物品参数——下拉选择
            List<string> List_mParam = new List<string>();
            //从数据库获取对应信息
            DataSet billRecord = new DataSet();
            string mName = comBox_mName.Text.Trim();
            string billInfo = "select mParam from kc_materialInfo where mName='" + mName + "'";
            billRecord = DBTool.ExecuteQuery(billInfo);

            //该类型物品的所有种类
            int count = billRecord.Tables[0].Rows.Count;
            for (int c = 1; c <= count; c++)
            {
                List_mParam.Add(billRecord.Tables[0].Rows[c - 1][0].ToString());
            }
            comBox_mParam.ItemsSource = List_mParam;
            // Console.WriteLine("again!!");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //完全退出
            this.Close();
        }

    }
    public class Material
    {
        public Material()
        {
            Items = new List<string>
            {
                "0805贴片电阻","按键开关","六角铜柱"
            };
            ItemsParam = new List<string>
            {
                "1K","10K","3*4*5","M3*12mm+6"
            };
            ItemsNum = new List<string>
            {
                "1", "2", "3", "4", "5",
                "6", "7", "8", "9", "10",
            };
        }
        public List<string> Items { get; set; }
        public List<string> ItemsParam { get; set; }
        public List<string> ItemsNum { get; set; }
    }
}
