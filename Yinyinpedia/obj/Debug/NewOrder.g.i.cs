﻿#pragma checksum "..\..\NewOrder.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EB8CC071F05531CE2A4875815141C5BA6787625D"
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
    /// NewOrder
    /// </summary>
    public partial class NewOrder : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgNew;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox noTransc;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox category;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox numberItem;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox stock;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox price;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox subtotal;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button accpet;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button decline;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\NewOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock newOrder;
        
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
            System.Uri resourceLocater = new System.Uri("/Yinyinpedia;component/neworder.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewOrder.xaml"
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
            
            #line 14 "..\..\NewOrder.xaml"
            this.back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgNew = ((System.Windows.Controls.DataGrid)(target));
            
            #line 18 "..\..\NewOrder.xaml"
            this.dgNew.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DgNew_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.noTransc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.category = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.numberItem = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.stock = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.price = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.subtotal = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.accpet = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\NewOrder.xaml"
            this.accpet.Click += new System.Windows.RoutedEventHandler(this.Accpet_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.decline = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\NewOrder.xaml"
            this.decline.Click += new System.Windows.RoutedEventHandler(this.Decline_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.newOrder = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

