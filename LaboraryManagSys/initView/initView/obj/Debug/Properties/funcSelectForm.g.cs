﻿#pragma checksum "..\..\..\Properties\funcSelectForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6DBAF2F474A008A431FB3637E04F95DBC14F05A800CB17F879D90E5BA961209A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// funcSelectForm
    /// </summary>
    public partial class funcSelectForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid funSelect;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ground;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button borrowComponent;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearButoon;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button programeButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button adviseButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridView;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Properties\funcSelectForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tip;
        
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
            System.Uri resourceLocater = new System.Uri("/initView;component/properties/funcselectform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Properties\funcSelectForm.xaml"
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
            
            #line 8 "..\..\..\Properties\funcSelectForm.xaml"
            ((initView.Properties.funcSelectForm)(target)).Loaded += new System.Windows.RoutedEventHandler(this.LodaDate);
            
            #line default
            #line hidden
            return;
            case 2:
            this.funSelect = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ground = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.borrowComponent = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\Properties\funcSelectForm.xaml"
            this.borrowComponent.Click += new System.Windows.RoutedEventHandler(this.borrow_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.clearButoon = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Properties\funcSelectForm.xaml"
            this.clearButoon.Click += new System.Windows.RoutedEventHandler(this.ClearBill_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.programeButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Properties\funcSelectForm.xaml"
            this.programeButton.Click += new System.Windows.RoutedEventHandler(this.Programe_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.adviseButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Properties\funcSelectForm.xaml"
            this.adviseButton.Click += new System.Windows.RoutedEventHandler(this.Advise_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Properties\funcSelectForm.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dataGridView = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.tip = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

