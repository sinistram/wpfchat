﻿#pragma checksum "..\..\MainChatWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "96CFCF66188FB5757AACFDE7A6AEA7CE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace WPFChat.Client {
    
    
    /// <summary>
    /// MainChatWindow
    /// </summary>
    public partial class MainChatWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\MainChatWindow.xaml"
        internal System.Windows.Controls.TextBox txtChat;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\MainChatWindow.xaml"
        internal System.Windows.Controls.TextBox txtNewMessage;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\MainChatWindow.xaml"
        internal System.Windows.Controls.Button btnSend;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPFChat.Client;component/mainchatwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainChatWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtChat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtNewMessage = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\MainChatWindow.xaml"
            this.txtNewMessage.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtNewMessage_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnSend = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\MainChatWindow.xaml"
            this.btnSend.Click += new System.Windows.RoutedEventHandler(this.btnSend_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
