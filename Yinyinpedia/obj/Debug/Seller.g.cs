﻿#pragma checksum "..\..\Seller.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "938A65B99EE69D6BD7320340882F6CA1937131E5"
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
    /// Seller
    /// </summary>
    public partial class Seller : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock header;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock saldo;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tarik;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button profile;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logout;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button product;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button order;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button shipping;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button chat;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Seller.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button viewReport;
        
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
            System.Uri resourceLocater = new System.Uri("/Yinyinpedia;component/seller.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Seller.xaml"
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
            this.header = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.saldo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.tarik = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\Seller.xaml"
            this.tarik.Click += new System.Windows.RoutedEventHandler(this.Tarik_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.profile = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\Seller.xaml"
            this.profile.Click += new System.Windows.RoutedEventHandler(this.Profile_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.logout = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\Seller.xaml"
            this.logout.Click += new System.Windows.RoutedEventHandler(this.Logout_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.product = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\Seller.xaml"
            this.product.Click += new System.Windows.RoutedEventHandler(this.Product_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.order = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\Seller.xaml"
            this.order.Click += new System.Windows.RoutedEventHandler(this.Order_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.shipping = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Seller.xaml"
            this.shipping.Click += new System.Windows.RoutedEventHandler(this.Shipping_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.chat = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Seller.xaml"
            this.chat.Click += new System.Windows.RoutedEventHandler(this.Chat_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.viewReport = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\Seller.xaml"
            this.viewReport.Click += new System.Windows.RoutedEventHandler(this.ViewReport_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

