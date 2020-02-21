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
    /// MaterialInfo.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialInfo : Window
    {
        List<string> pMemberList = new List<string>(){"1", "2", "3"};

        public MaterialInfo()
        {
            InitializeComponent();
            mTypeIDBox.ItemsSource = pMemberList;
        }

        /*
        * 鼠标操作，拖动窗口
        */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(Material);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < Material.ActualWidth && position.Y >= 0 && position.Y < Material.ActualHeight)
                {
                    this.DragMove();
                }
            }
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

        private void SubmitProject_Click(object sender, RoutedEventArgs e)
        {
            //获取信息
            string mName = mNameBox.Text.Trim().ToUpper();
            string mParam = mParamBox.Text.Trim().ToUpper();
            int mNum = int.Parse(mNumBox.Text.Trim());
            try
            {
                //首先查询是否重复，先提示警告用户自行选择是数量添加还是取消
                string sqlQuery = "select * from kc_materialInfo where mName='"+mName+"' and  mParam='"+mParam+"'";
                DataSet billRecord = new DataSet();
                billRecord = DBTool.ExecuteQuery(sqlQuery);
                mName = billRecord.Tables[0].Rows[0][1].ToString();
                string message = MessageBox.Show("物品信息已经存在库中，添加数量还是取消!", "温馨提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
                if (message.Equals("Cancel"))
                {
                    return;
                }
                else
                {
                    //添加数量
                    string MaterialUpdate = "update kc_materialInfo set mNum+='"+ mNum +"' where mName='"+ mName +"' and  mParam='"+ mParam +"'";
                    int fin1 = DBTool.ExecuteUpdate(MaterialUpdate);
                    MessageBox.Show("操作成功", "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            catch
            {
                //查询到不重复.Go on
            }
            int mTypeID = int.Parse(mTypeIDBox.Text.Trim());
            float mPrice = float.Parse(mPriceBox.Text.Trim());
            string mRemark = mRemarkBox.Text.Trim();
            string mLocation = mLocationBox.Text.Trim() + "";
            //插入物品信息
            try
            {
                string materialInsert = "insert into kc_materialInfo values('" + mName + "','" + mParam + "','" +
                    mTypeID + "','" + mNum + "','" + mPrice + "','" + mRemark + "','" + mLocation + "')";
                int fin1 = DBTool.ExecuteUpdate(materialInsert);
                //Console.WriteLine("添加个人借用记录数量: " + fin1);
                MessageBox.Show("操作成功", "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Have Error Of This Programe!");
            }
        }
    }
}
