using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFChat.Library;
using System.ServiceModel;

namespace WPFChat.Server
{
    class ClientsRepository
    {
        private static object m_Locker = new object();

        private static Dictionary<String, ClientInfo> m_Clients = new Dictionary<string,ClientInfo>();

        public static ClientInfo[] Login(string loginId, string avatar)
        {
            lock (m_Locker)
            {
                if (!m_Clients.ContainsKey(loginId))
                {
                    m_Clients.Add(
                        loginId,
                        new ClientInfo
                        {
                            LoginId=loginId,
                            Callback=OperationContext.Current.GetCallbackChannel<IChatClientCallback>(),
                            Avatar=Avatars.Get(avatar)
                        }
                        );

                    NotifyOthersOfMe(loginId);
                }

                return m_Clients.Where(p => p.Key != loginId).Select(p => p.Value).ToArray();
            }
        }

        public static void Logoff(string loginId)
        {
            lock (m_Locker)
            {
                if (m_Clients.ContainsKey(loginId))
                    m_Clients.Remove(loginId);
            }
        }

        public static bool IsLoggedIn(string loginId)
        {
            lock (m_Locker)
            {
                return m_Clients.ContainsKey(loginId);
            }
        }

        public static IChatClientCallback GetCallback(string loginId)
        {
            return GetCallback(loginId, true);
        }

        private static IChatClientCallback GetCallback(string loginId, bool useLock)
        {
            if (useLock)
            {
                lock (m_Locker)
                {
                    if (m_Clients.ContainsKey(loginId))
                        return m_Clients[loginId].Callback;
                    else
                        return null;
                }
            }
            else
            {
                    if (m_Clients.ContainsKey(loginId))
                        return m_Clients[loginId].Callback;
                    else
                        return null;
            }
        }

        private static void NotifyOthersOfMe(string loginId)
        {
            //id = id of client to notify
            foreach (string id in m_Clients.Keys)
            {
                //do not notify self
                if (id != loginId)
                {
                    //get all others logged in users
                    var loginsToNotifyOf = m_Clients.Where(p => p.Key != id).Select(p=>p.Value);

                    //send notification with user list
                    IChatClientCallback callback = GetCallback(id, false);
                    callback.ReceiveUserList(loginsToNotifyOf.ToArray());
                }
            }
        }

    }
}
