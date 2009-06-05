#define LOGCALLS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFChat.Library;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace WPFChat.Server
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    class ChatServer : IChatServer
    {
        public ChatServer()
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            Console.WriteLine(
                String.Format("Server object created @ {0}", DateTime.Now.ToLongTimeString())
                );
        }


        #region IChatServer Members

        public ClientInfo[] LoginClient(string loginId, string avatar)
        {

#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            Console.WriteLine("logging in: {0}", loginId);

            return ClientsRepository.Login(loginId, avatar);

        }

        public void LogoffClient(string loginId)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            Console.WriteLine("logging out: {0}", loginId);

            ClientsRepository.Logoff(loginId);
        }

        public void SendMessage(string loginIdFrom, string loginIdTo, string message)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            Console.WriteLine("Sending message from {0} to {1}", loginIdFrom, loginIdTo);

            IChatClientCallback clientCallback = ClientsRepository.GetCallback(loginIdTo, false);

            if (clientCallback != null)
                clientCallback.ReceiveMessage(loginIdFrom, message);
        }

        public Avatar[] GetAvatars()
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            return Avatars.GetAll();
        }

        public void BroadcastMessage(string loginIdFrom, string message)
        {
#if LOGCALLS
            Utils.LogCurrentMethodCall();
#endif

            foreach (IChatClientCallback clientCallback in ClientsRepository.GetAllCallbacks())
            {
                if (clientCallback != null)
                    clientCallback.ReceiveBroadcastMessage(loginIdFrom, message);
            }
        }

        #endregion
    }
}
