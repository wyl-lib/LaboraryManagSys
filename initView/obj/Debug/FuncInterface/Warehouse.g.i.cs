﻿#pragma checksum "..\..\..\FuncInterface\Warehouse.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0D6F2EEE613BE1C817E313D08DF65AE4AF9C43A3EDAC83F464F66625F023F476"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using initView.Properties;


namespace initView.Properties {
    
    
    /// <summary>
    /// Warehouse
    /// </summary>
    public partial class Warehouse : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WareHouse;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image E_mail_jpg;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button depotBorrow;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox materialName;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox materialParam;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox note;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox num;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridView;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\FuncInterface\Warehouse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox returnTime;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/initView;component/funcinterface/warehouse.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\FuncInterface\Warehouse.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.WareHouse = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.E_mail_jpg = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.depotBorrow = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\FuncInterface\Warehouse.xaml"
            this.depotBorrow.Click += new System.Windows.RoutedEventHandler(this.DepotBorrow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\FuncInterface\Warehouse.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.ExitButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.materialName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.materialParam = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.note = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.num = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.dataGridView = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.returnTime = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

