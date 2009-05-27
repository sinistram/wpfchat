using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFChat.Library;
using System.ServiceModel;
using System.IO;
using System.Reflection;

namespace WPFChat.Server
{
    class ChatServer : IChatServer
    {
        #region IChatServer Members

        public ClientInfo[] LoginClient(string loginId, string avatar)
        {
            Console.WriteLine("logging in: {0}", loginId);

            return ClientsRepository.Login(loginId, avatar);

        }

        public void LogoffClient(string loginId)
        {
            Console.WriteLine("logging out: {0}", loginId);

            ClientsRepository.Logoff(loginId);
        }

        public void SendMessage(string loginIdFrom, string loginIdTo, string message)
        {
            Console.WriteLine("Sending message from {0} to {1}", loginIdFrom, loginIdTo);

            IChatClientCallback clientCallback = ClientsRepository.GetCallback(loginIdTo);

            if (clientCallback != null)
                clientCallback.ReceiveMessage(loginIdFrom, message);
        }

        public Avatar[] GetAvatars()
        {
            return Avatars.GetAll();
        }

        #endregion
    }
}
