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

namespace initView.Properties
{
    /// <summary>
    /// ForgetPass.xaml 的交互逻辑
    /// </summary>
    public partial class ForgetPass : Window
    {
        public ForgetPass()
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


    }
}
