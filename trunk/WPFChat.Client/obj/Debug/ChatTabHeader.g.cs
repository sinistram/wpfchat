﻿#pragma checksum "..\..\ChatTabHeader.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3568F9A1C49A03D5869346A71A0C128C"
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
    /// ChatTabHeader
    /// </summary>
    public partial class ChatTabHeader : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\ChatTabHeader.xaml"
        internal System.Windows.Controls.Image imgAvatar;
        
        #line default
        #line hidden
        
        
        #line 6 "..\..\ChatTabHeader.xaml"
        internal System.Windows.Controls.TextBlock txtLoginId;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\ChatTabHeader.xaml"
        internal System.Windows.Controls.Button btnCloseChat;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFChat.Client;component/chattabheader.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChatTabHeader.xaml"
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
            this.imgAvatar = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.txtLoginId = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btnCloseChat = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
