﻿#pragma checksum "..\..\..\Cart\checkOutWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A9F93B1CD6DBDB77FFF7140C175137A8B38EE39B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL.Cart;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace PL.Cart {
    
    
    /// <summary>
    /// checkOutWindow
    /// </summary>
    public partial class checkOutWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ClientName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ClientEmail;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AddressForDelivery;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ClientNametxt;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ClientEmailtxt;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressForDeliverytxt;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Cart\checkOutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OrderBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/cart/checkoutwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Cart\checkOutWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ClientName = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.ClientEmail = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.AddressForDelivery = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ClientNametxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ClientEmailtxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.AddressForDeliverytxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.OrderBtn = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Cart\checkOutWindow.xaml"
            this.OrderBtn.Click += new System.Windows.RoutedEventHandler(this.OrderBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

