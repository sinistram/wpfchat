#define LOGCALLS

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



        private event EventHandler<GenericEventArgs<KeyValuePair<string, string>>> m_ReceivedBroadcastMessage;
        public event EventHandler<GenericEventArgs<KeyValuePair<string, string>>> ReceivedBroadcastMessage
        {
            add
            {
                m_ReceivedBroadcastMessage += value;
            }
            remove
            {
                m_ReceivedBroadcastMessage -= value;
            }
        }
        private void OnReceivedBroadcastMessage(string loginIdFrom, string message)
        {
            if (m_ReceivedBroadcastMessage != null)
                m_ReceivedBroadcastMessage(this, new GenericEventArgs<KeyValuePair<string, string>>(new KeyValuePair<string, string>(loginIdFrom, message)));
        }

        private event EventHandler<GenericEventArgs<ClientInfo[]>> m_ReceivedUserList;
        public event EventHandler<GenericEventArgs<ClientInfo[]>> ReceivedUserList
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
        private void OnReceivedUserList(ClientInfo[] loggedInUsers)
        {
            if (m_ReceivedUserList != null)
                m_ReceivedUserList(this, new GenericEventArgs<ClientInfo[]>(loggedInUsers));
        }


        #region IChatServerCallback Members

        public void ReceiveMessage(string loginIdFrom, string message)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
            OnReceivedMessage(loginIdFrom, message);
        }

        public void ReceiveUserList(ClientInfo[] loggedInUsers)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
            OnReceivedUserList(loggedInUsers);
        }

        public bool CheckConnection()
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
            return true;
        }

        public void ReceiveBroadcastMessage(string loginIdFrom, string message)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
            OnReceivedBroadcastMessage(loginIdFrom, message);
        }

        #endregion
    }
}
