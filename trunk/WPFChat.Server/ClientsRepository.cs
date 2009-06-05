#define LOGCALLS
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
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            try
            {
                Console.WriteLine("Entering lock...");
                lock (m_Locker)
                {
                    if (!m_Clients.ContainsKey(loginId))
                    {
                        ClientInfo newClientInfo = new ClientInfo
                            {
                                LoginId = loginId,
                                Callback = OperationContext.Current.GetCallbackChannel<IChatClientCallback>(),
                                Avatar = Avatars.Get(avatar)
                            };

                        //When client connection check fails, logoff client
                        newClientInfo.ConnectionCheckFailed = delegate(ClientInfo cliInfo)
                        {
                            Logoff(cliInfo.LoginId);
                            Console.WriteLine("Connection with client {0} was lost.", cliInfo.LoginId);
                        };

                        m_Clients.Add(
                            loginId,
                            newClientInfo
                            );

                        NotifyOthersOfMe(loginId);
                    }

                    return m_Clients.Select(p => p.Value).ToArray();
                }
            }
            finally
            {
                Console.WriteLine("Exiting lock...");
            }
        }

        public static void Logoff(string loginId)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            Console.WriteLine("Entering lock...");
            lock (m_Locker)
            {
                if (m_Clients.ContainsKey(loginId))
                {
                    m_Clients[loginId].Dispose();
                    m_Clients.Remove(loginId);

                    //notify others that i'm gone.
                    NotifyOthersOfMe(loginId);
                }
            }
            Console.WriteLine("Exiting lock...");
        }

        public static bool IsLoggedIn(string loginId)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            try
            {
                Console.WriteLine("Entering lock...");
                lock (m_Locker)
                {
                    return m_Clients.ContainsKey(loginId);
                }
            }
            finally
            {
                Console.WriteLine("Exiting lock...");
            }
        }

        public static IChatClientCallback[] GetAllCallbacks()
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
                return m_Clients.Select(p => p.Value.Callback).ToArray();
        }

        public static IChatClientCallback GetCallback(string loginId, bool useLock)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
            if (useLock)
            {
                try
                {
                    Console.WriteLine("Entering lock...");
                    lock (m_Locker)
                    {
                        if (m_Clients.ContainsKey(loginId))
                            return m_Clients[loginId].Callback;
                        else
                            return null;
                    }
                }
                finally
                {
                    Console.WriteLine("Exiting lock...");
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
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif
            //id = id of client to notify
            foreach (string id in m_Clients.Keys)
            {
                
                if (id != loginId)
                {
                    //get all others logged in users
                    var loginsToNotifyOf = m_Clients.Select(p=>p.Value);

                    //send notification with user list
                    IChatClientCallback callback = GetCallback(id, false);
                    callback.ReceiveUserList(loginsToNotifyOf.ToArray());
                }
            }
        }

    }
}
