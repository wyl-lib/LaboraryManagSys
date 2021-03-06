﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// fun.xaml 的交互逻辑
    /// </summary>
    public partial class AdminFuncForm : Window
    {
        static MaterialInfo materialInfo;
        static borrowForm borrowForm;
        static clearBill clearBill;
        static Project proJect;

        public static DataSet getRecord { get; set; }
        ObservableCollection<kc_billStatistics> userBorrowList = new ObservableCollection<kc_billStatistics>();

        /*
         * 鼠标操作，拖动窗口
         */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)//鼠标操作，拖动窗口
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(AdminFunc);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < AdminFunc.ActualWidth && position.Y >= 0 && position.Y < AdminFunc.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        public AdminFuncForm()
        {
            InitializeComponent();
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            //打开借用元件界面
            borrowForm = new borrowForm();
            borrowForm.Show();
        }

        public void LodaDate(object sender, RoutedEventArgs e)
        {
            int uID = int.Parse(getRecord.Tables[0].Rows[0][0].ToString());
            DataSet billRecord = new DataSet();
            string billInfo = "select * from kc_billStatistics where uID ='"+uID+"'";
            billRecord = DBTool.ExecuteQuery(billInfo);
            int count = billRecord.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                //int mID = int.Parse(billRecord.Tables[0].Rows[i][1].ToString());
                string mName = billRecord.Tables[0].Rows[i][2].ToString();
                string mParam = billRecord.Tables[0].Rows[i][3].ToString();
                int mNum = int.Parse(billRecord.Tables[0].Rows[i][4].ToString());
                string uName = billRecord.Tables[0].Rows[i][6].ToString();
                string mBorrowTime = billRecord.Tables[0].Rows[i][7].ToString();

                userBorrowList.Add(new kc_billStatistics(){
                    //mID = mID,
                    mName = mName,
                    mParam = mParam,
                    mNum = mNum,
                    uName = uName,
                    mBorrowTime = mBorrowTime
                });
                ((this.FindName("dataGridView")) as DataGrid).ItemsSource = userBorrowList;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
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

        private void ItemEntry_Click(object sender, RoutedEventArgs e)
        {
            materialInfo = new MaterialInfo();
            materialInfo.Show();
        }
        /*
         * 资金流动
         */
        private void MoneyRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Programe_Click(object sender, RoutedEventArgs e)
        {
            proJect = new Project();
            proJect.Show();
        }

        private void ClearBill_Click(object sender, RoutedEventArgs e)
        {
            int uID = int.Parse(getRecord.Tables[0].Rows[0][0].ToString());
            string uPayment = "select kc_billStatistics.mID,kc_billStatistics.mNum," +
                "mPrice,kc_billStatistics.mNum* mPrice as hj,uName from kc_materialInfo," +
                "kc_billStatistics where kc_materialInfo.mID = kc_billStatistics.mID and uID='" + uID + "'";
            DataSet billRecord = new DataSet();
            billRecord = DBTool.ExecuteQuery(uPayment);
            int count = billRecord.Tables[0].Rows.Count;
            float mToltal = 0;//付款总计金额
            //float num = 0;
            for (int i = 0; i < count; i++)
            {
                //num = float.Parse(billRecord.Tables[0].Rows[i][3].ToString());
                mToltal += float.Parse(billRecord.Tables[0].Rows[i][3].ToString());
            }
            Console.WriteLine("mToltal: " + mToltal);
            string message = MessageBox.Show("总金额为" + mToltal + "请确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //Alipay Code
            clearBill = new clearBill();
            clearBill.Show();
        }
    }

}
