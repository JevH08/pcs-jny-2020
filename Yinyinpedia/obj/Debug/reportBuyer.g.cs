﻿#pragma checksum "..\..\reportBuyer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6C97055760A94AECC6945EBF25939A5F604BC71A"
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
    /// reportBuyer
    /// </summary>
    public partial class reportBuyer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\reportBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\reportBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox start;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\reportBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox end;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\reportBuyer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button generate;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\reportBuyer.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Yinyinpedia;component/reportbuyer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\reportBuyer.xaml"
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
            
            #line 14 "..\..\reportBuyer.xaml"
            this.back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.start = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.end = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.generate = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\reportBuyer.xaml"
            this.generate.Click += new System.Windows.RoutedEventHandler(this.Generate_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.viewerCR = ((SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

