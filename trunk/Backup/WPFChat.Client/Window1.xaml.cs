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

namespace WPFChat.Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ChatClientCallback clientCallback;
        private InstanceContext instanceContext;
        private ChatServer.ChatServerClient m_Client;

        public Window1()
        {
            InitializeComponent();

            clientCallback = new ChatClientCallback();
            instanceContext = new InstanceContext(clientCallback);
            m_Client = new WPFChat.Client.ChatServer.ChatServerClient(instanceContext);

            clientCallback.ReceivedMessage += new EventHandler<WPFChat.Library.GenericEventArgs<KeyValuePair<string, string>>>(clientCallback_ReceivedMessage);
            clientCallback.ReceivedUserList += new EventHandler<WPFChat.Library.GenericEventArgs<string[]>>(clientCallback_ReceivedUserList);

            this.Title = "Not logged in";
        }

        void clientCallback_ReceivedUserList(object sender, WPFChat.Library.GenericEventArgs<string[]> e)
        {
            LoadChatList(e.Data);
        }



        void clientCallback_ReceivedMessage(object sender, WPFChat.Library.GenericEventArgs<KeyValuePair<string, string>> e)
        {
            ChatControl cc = GetChatControl(e.Data.Key, true);
            cc.AddMessage(cc.IdToTalkTo, e.Data.Value);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLoginId.Text.Trim().Length != 0)
            {
                string[] chatClients = m_Client.LoginClient(txtLoginId.Text.Trim());

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
            if (GetChatControl(lvConnectedUsers.SelectedItem.ToString()) != null)
                return;

            ChatControl cc = new ChatControl();
            cc.IdToTalkTo = lvConnectedUsers.SelectedItem.ToString();
            cc.SendMessage += new EventHandler<WPFChat.Library.GenericEventArgs<string>>(cc_SendMessage);
            TabItem ti = new TabItem();
            ti.Content = cc;
            tabChatWindows.Items.Add(ti);
            tabChatWindows.SelectedItem = ti;
        }

        void cc_SendMessage(object sender, WPFChat.Library.GenericEventArgs<string> e)
        {
            m_Client.SendMessage(
                txtLoginId.Text.Trim(),
                ((ChatControl)sender).IdToTalkTo,
                e.Data);

            ((ChatControl)sender).AddMessage(txtLoginId.Text.Trim(), e.Data);

        }

        private void LoadChatList(string[] clients)
        {
            lvConnectedUsers.Items.Clear();
            foreach (string s in clients)
            {
                lvConnectedUsers.Items.Add(s);
            }
        }

        private ChatControl GetChatControl(string id, bool createIfNotFound)
        {
            foreach (TabItem tab in tabChatWindows.Items)
            {
                if (id == ((ChatControl)tab.Content).IdToTalkTo)
                {
                    tabChatWindows.SelectedItem = tab;
                    return ((ChatControl)tab.Content);
                }
            }
            if (createIfNotFound)
            {
                return CreateChatControl(id);
            }
            else
                return null;
        }

        private ChatControl GetChatControl(string id)
        {
            return GetChatControl(id, false);
        }

        private ChatControl CreateChatControl(string id)
        {
            ChatControl cc = new ChatControl();
            cc.IdToTalkTo = id;
            cc.SendMessage += new EventHandler<WPFChat.Library.GenericEventArgs<string>>(cc_SendMessage);
            TabItem ti = new TabItem();
            ti.Header = id;
            ti.Content = cc;
            tabChatWindows.Items.Add(ti);
            tabChatWindows.SelectedItem = ti;

            return cc;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
