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
    /// Interaction logic for MainChatWindow.xaml
    /// </summary>
    public partial class MainChatWindow : UserControl
    {
        private event EventHandler<GenericEventArgs<String>> m_SendMessage;
        public event EventHandler<GenericEventArgs<String>> SendMessage
        {
            add
            {
                m_SendMessage += value;
            }
            remove
            {
                m_SendMessage -= value;
            }
        }

        public MainChatWindow()
        {
            InitializeComponent();
        }

        public void AddMessage(string id, string message)
        {
            string formattedMessage = id + ": " + message + Environment.NewLine;
            txtChat.Text += formattedMessage;
        }

        private void SendChatMessage()
        {
            if (txtNewMessage.Text.Trim().Length > 0)
            {
                OnSendMessage(txtNewMessage.Text.Trim());
                txtNewMessage.Clear();
            }
        }

        private void OnSendMessage(string message)
        {
            if (m_SendMessage != null)
                m_SendMessage(this, new GenericEventArgs<string>(message));
        }

        private void txtNewMessage_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendChatMessage();
        }
    }
}
