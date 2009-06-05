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
using System.ServiceModel;
using WPFChat.Library;
using System.IO;

namespace WPFChat.Client.MainWindow
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatClientCallback clientCallback;
        private InstanceContext instanceContext;
        private ChatServer.ChatServerClient m_Client;
        private MainChatWindow m_MainChatWindow;

        public MainWindow()
        {
            InitializeComponent();

            clientCallback = new ChatClientCallback();
            instanceContext = new InstanceContext(clientCallback);
            m_Client = new WPFChat.Client.ChatServer.ChatServerClient(instanceContext);

            clientCallback.ReceivedMessage += new EventHandler<WPFChat.Library.GenericEventArgs<KeyValuePair<string, string>>>(clientCallback_ReceivedMessage);
            clientCallback.ReceivedUserList += new EventHandler<WPFChat.Library.GenericEventArgs<ClientInfo[]>>(clientCallback_ReceivedUserList);
            clientCallback.ReceivedBroadcastMessage += new EventHandler<GenericEventArgs<KeyValuePair<string, string>>>(clientCallback_ReceivedBroadcastMessage);

            this.Title = "Not logged in";

            #region Populate avatar combo
            try
            {
                Avatar[] serverAvatars = m_Client.GetAvatars();
                foreach (Avatar avatar in serverAvatars)
                {
                    cmbAvatars.Items.Add(
                        Utils.GetImageFromAvatar(avatar)
                        );
                }
            }
            catch (Exception ex)
            {
                //todo: log error
            }
            #endregion

            //create dummy chat control
            //CreateChatControl("test");

            CreateMainChatWindow();
        }

        void clientCallback_ReceivedBroadcastMessage(object sender, GenericEventArgs<KeyValuePair<string, string>> e)
        {
            m_MainChatWindow.AddMessage(e.Data.Key, e.Data.Value);
        }

        void clientCallback_ReceivedUserList(object sender, WPFChat.Library.GenericEventArgs<ClientInfo[]> e)
        {
            LoadChatList(e.Data);



            for (int i = tabChatWindows.Items.Count-1; i>=0; i--)
            {
                TabItem ti = (TabItem)tabChatWindows.Items[i];
                if (ti.Content is ChatControl)
                {
                    string id = ((ChatControl)ti.Content).IdToTalkTo;

                    if ((e.Data.Where(p => p.LoginId == id)).Count() == 0)
                    {
                        tabChatWindows.Items.RemoveAt(i);
                    }
                }
            }
        }



        void clientCallback_ReceivedMessage(object sender, GenericEventArgs<KeyValuePair<string, string>> e)
        {
            ChatControl cc = GetChatControl(e.Data.Key, true);
            cc.AddMessage(cc.IdToTalkTo, e.Data.Value);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLoginId.Text.Trim().Length != 0)
            {
                string avatarName = "";
                if (cmbAvatars.SelectedItem != null)
                {
                    avatarName = (((Image)cmbAvatars.SelectedItem).Tag as Avatar).ImageName;
                }

                ClientInfo[] chatClients = m_Client.LoginClient(txtLoginId.Text.Trim(), avatarName);

                LoadChatList(chatClients);

                txtLoginId.IsReadOnly = true;
                this.Title = txtLoginId.Text.Trim();
                
            }
            
        }

        private void btnLogOff_Click(object sender, RoutedEventArgs e)
        {
            m_Client.LogoffClient(txtLoginId.Text.Trim());
            txtLoginId.IsReadOnly = false;
            lvConnectedUsers.Items.Clear();
            tabChatWindows.Items.Clear();
            this.Title = "Not logged in"; 
        }

        private void lvConnectedUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetChatControl(((UserListItem)lvConnectedUsers.SelectedItem).ClientInfo.LoginId, true);
        }


        void cc_SendMessage(object sender, WPFChat.Library.GenericEventArgs<string> e)
        {
            m_Client.SendMessage(
                txtLoginId.Text.Trim(),
                ((ChatControl)sender).IdToTalkTo,
                e.Data);

            ((ChatControl)sender).AddMessage(txtLoginId.Text.Trim(), e.Data);

        }

        private void LoadChatList(ClientInfo[] clients)
        {
            lvConnectedUsers.Items.Clear();
            foreach (ClientInfo c in clients)
            {
                UserListItem uli = new UserListItem(c);
                lvConnectedUsers.Items.Add(uli);
            }


        }

        private ClientInfo GetClientInfo(string id)
        {
            foreach (UserListItem uli in lvConnectedUsers.Items)
            {
                if (uli.ClientInfo.LoginId == id)
                    return uli.ClientInfo;
            }

            return null;
        }


        private ChatControl GetChatControl(string id, bool createIfNotFound)
        {
            foreach (TabItem tab in tabChatWindows.Items)
            {
                if (tab.Content is ChatControl && 
                    id == ((ChatControl)tab.Content).IdToTalkTo)
                {
                    tabChatWindows.SelectedItem = tab;
                    return ((ChatControl)tab.Content);
                }
            }
            if (createIfNotFound)
            {
                return CreateChatControl(GetClientInfo(id));
            }
            else
                return null;
        }

        private ChatControl GetChatControl(string id)
        {
            return GetChatControl(id, false);
        }

        private ChatControl CreateChatControl(ClientInfo cli)
        {
            ChatControl cc = new ChatControl();
            cc.IdToTalkTo = cli.LoginId;
            cc.SendMessage += new EventHandler<WPFChat.Library.GenericEventArgs<string>>(cc_SendMessage);
            TabItem ti = new TabItem();

            StackPanel pnlHeader = new StackPanel();
            pnlHeader.Orientation = Orientation.Horizontal;

            ChatTabHeader tabHeader = new ChatTabHeader();

            tabHeader.txtLoginId.Text = cli.LoginId;
            tabHeader.btnCloseChat.Click += delegate(object sender, RoutedEventArgs e)
            {
                tabChatWindows.Items.Remove(ti);
            };
         
            tabHeader.Tag = cli;
            tabHeader.imgAvatar.Source = Utils.GetImageFromAvatar(cli.Avatar).Source;

            ti.Header = tabHeader;
            ti.Content = cc;
            tabChatWindows.Items.Add(ti);
            tabChatWindows.SelectedItem = ti;

            return cc;
        }

        private void CreateMainChatWindow()
        {
            m_MainChatWindow = new MainChatWindow();
            m_MainChatWindow.SendMessage += new EventHandler<GenericEventArgs<string>>(mcw_SendMessage);
            TabItem ti = new TabItem();
            ti.Header = "Main Chanel";
            ti.Content = m_MainChatWindow;
            tabChatWindows.Items.Add(ti);
            tabChatWindows.SelectedItem = ti;
        }

        void mcw_SendMessage(object sender, GenericEventArgs<string> e)
        {
            m_Client.BroadcastMessage(txtLoginId.Text.Trim(), e.Data);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeButtonRectangle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
