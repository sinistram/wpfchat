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
using WPFChat.Client.ChatServer;
using WPFChat.Library;

namespace WPFChat.Client
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        private event EventHandler<GenericEventArgs<String>> m_SendMessage;
        public event EventHandler<GenericEventArgs<String>> SendMessage
        {
            add
            {
                m_SendMessage += value ;
            }
            remove
            {
                m_SendMessage -= value;
            }
        }

        private void OnSendMessage(string message)
        {
            if (m_SendMessage != null)
                m_SendMessage(this, new GenericEventArgs<string>(message));
        }

        private string m_IdToTalkTo;
        public string IdToTalkTo
        {
            get { return m_IdToTalkTo; }
            set { m_IdToTalkTo = value; }
        }

        public ChatControl()
        {
            InitializeComponent();
        }

        public void AddMessage(string id, string message)
        {
            string formattedMessage = id + ": " + message + Environment.NewLine;
            txtChat.Text += formattedMessage;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewMessage.Text.Trim().Length > 0)
            {
                OnSendMessage(txtNewMessage.Text.Trim());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
