﻿#pragma checksum "..\..\cartBuyer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E3575D0D2357B00AAF85D7A3991B15951A7B2250"
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
    /// cartBuyer
    /// </summary>
    public partial class cartBuyer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phoneNumber;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox address;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox city;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox shipping;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock promoDelivery;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock grandtotal;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button topup;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button done;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\cartBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/Yinyinpedia;component/cartbuyer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\cartBuyer.xaml"
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
            this.name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.phoneNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.address = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.city = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.shipping = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\cartBuyer.xaml"
            this.shipping.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Shipping_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.promoDelivery = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.grandtotal = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.topup = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\cartBuyer.xaml"
            this.topup.Click += new System.Windows.RoutedEventHandler(this.Topup_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.done = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\cartBuyer.xaml"
            this.done.Click += new System.Windows.RoutedEventHandler(this.Done_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cancel = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\cartBuyer.xaml"
            this.cancel.Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

