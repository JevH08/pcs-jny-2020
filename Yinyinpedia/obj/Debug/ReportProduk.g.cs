﻿#pragma checksum "..\..\ReportProduk.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7835C10C058DD4C3606E97DB0B6096DA5B88D4DE8ECAA64E90C991BF533BD9CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RootLibrary.WPF.Localization;
using SAPBusinessObjects.WPF.Viewer;
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
using Yinyinpedia;


namespace Yinyinpedia {
    
    
    /// <summary>
    /// ReportProduk
    /// </summary>
    public partial class ReportProduk : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\ReportProduk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ReportProduk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton bestSelling;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ReportProduk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton unverified;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ReportProduk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox category;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ReportProduk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button generate;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ReportProduk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer viewerCR;
        
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
            System.Uri resourceLocater = new System.Uri("/Yinyinpedia;component/reportproduk.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReportProduk.xaml"
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
            this.back = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\ReportProduk.xaml"
            this.back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bestSelling = ((System.Windows.Controls.RadioButton)(target));
            
            #line 19 "..\..\ReportProduk.xaml"
            this.bestSelling.Checked += new System.Windows.RoutedEventHandler(this.BestSelling_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.unverified = ((System.Windows.Controls.RadioButton)(target));
            
            #line 20 "..\..\ReportProduk.xaml"
            this.unverified.Checked += new System.Windows.RoutedEventHandler(this.Unverified_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.category = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.generate = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\ReportProduk.xaml"
            this.generate.Click += new System.Windows.RoutedEventHandler(this.Generate_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.viewerCR = ((SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

