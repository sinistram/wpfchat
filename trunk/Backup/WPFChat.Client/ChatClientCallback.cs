using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WPFChat.Library;
using WPFChat.Client.ChatServer;

namespace WPFChat.Client
{
    class ChatClientCallback : IChatServerCallback
    {
        static InstanceContext site = new InstanceContext(new ChatClientCallback());

        private event EventHandler<GenericEventArgs<KeyValuePair<string, string>>> m_ReceivedMessage;
        public event EventHandler<GenericEventArgs<KeyValuePair<string, string>>> ReceivedMessage
        {
            add
            {
                m_ReceivedMessage += value;
            }
            remove
            {
                m_ReceivedMessage -= value;
            }
        }
        private void OnReceivedMessage(string loginIdFrom, string message)
        {
            if (m_ReceivedMessage != null)
                m_ReceivedMessage(this, new GenericEventArgs<KeyValuePair<string, string>>(new KeyValuePair<string, string>(loginIdFrom, message)));
        }


        private event EventHandler<GenericEventArgs<string[]>> m_ReceivedUserList;
        public event EventHandler<GenericEventArgs<string[]>> ReceivedUserList
        {
            add
            {
                m_ReceivedUserList += value;
            }
            remove
            {
                m_ReceivedUserList -= value;
            }
        }
        private void OnReceivedUserList(string[] loggedInUsers)
        {
            if (m_ReceivedUserList != null)
                m_ReceivedUserList(this, new GenericEventArgs<string[]>(loggedInUsers));
        }


        #region IChatClientCallback Members

        public void ReceiveMessage(string loginIdFrom, string message)
        {
            OnReceivedMessage(loginIdFrom, message);
        }

        public void ReceiveUserList(string[] loggedInUsers)
        {
            OnReceivedUserList(loggedInUsers);
        }

        #endregion
    }
}
