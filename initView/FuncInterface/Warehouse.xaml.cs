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
using initView.Beans;

namespace initView.Properties
{
    /// <summary>
    /// RegistFrom.xaml 的交互逻辑
    /// </summary>
    public partial class Warehouse : Window
    {
        /*static borrowForm borrowForm;
        static clearBill clearBill;
        static feedBack feedBack;
        static Project proJect;*/

        public static DataSet getRecord { get; set; }
        ObservableCollection<Beans.kc_warehouse> warehouseList = new ObservableCollection<kc_warehouse>();
      
        //物品数量——下拉选择
        List<string> mNum = new List<string>{
                "1", "2", "3", "4", "5",
                "6", "7", "8", "9", "10",
                "11", "12", "13", "14", "15",
                "16", "17", "18", "19", "20"
        };
        /*
        * 鼠标操作，拖动窗口
        */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(WareHouse);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < WareHouse.ActualWidth && position.Y >= 0 && position.Y < WareHouse.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        public Warehouse()
        {
            num.ItemsSource = mNum;
            InitializeComponent();
        }

        public void LodaDate(object sender, RoutedEventArgs e)
        {
            int uID = int.Parse(getRecord.Tables[0].Rows[0][0].ToString());
            DataSet billRecord = new DataSet();
            string billInfo = "select * from kc_warehouse where uID ='" + uID + "' and deportRFlag ='0'";

            billRecord = DBTool.ExecuteQuery(billInfo);
            int count = billRecord.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string uName = billRecord.Tables[0].Rows[i][2].ToString();
                string depotMName = billRecord.Tables[0].Rows[i][3].ToString();
                int depotMNum = int.Parse(billRecord.Tables[0].Rows[i][4].ToString());
                string depotMParam = billRecord.Tables[0].Rows[i][6].ToString();
                string depotRemark = billRecord.Tables[0].Rows[i][7].ToString();
                string depotBTime = billRecord.Tables[0].Rows[i][7].ToString();

                warehouseList.Add(new kc_warehouse()
                {
                    uName = uName,
                    depotMName = depotMName,
                    depotMParam = depotMParam,
                    depotMNum = depotMNum,
                    depotRemark = depotRemark,
                    depotBTime = depotBTime
                }); ; ;
                ((this.FindName("dataGridView")) as DataGrid).ItemsSource = warehouseList;
            }
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

        private void DepotBorrow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
