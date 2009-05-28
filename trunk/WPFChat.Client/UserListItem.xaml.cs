using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFChat.Library;

namespace WPFChat.Client
{
    /// <summary>
    /// Interaction logic for UserListItem.xaml
    /// </summary>
    public partial class UserListItem : UserControl
    {
        public ClientInfo ClientInfo {get;set;}

        public UserListItem(ClientInfo clientInfo)
        {
            this.ClientInfo = clientInfo;

            InitializeComponent();

            this.UserAvatar.Source = Utils.GetImageFromAvatar(ClientInfo.Avatar).Source;
            this.UserLoginId.Text = ClientInfo.LoginId;
        }
    }
}
