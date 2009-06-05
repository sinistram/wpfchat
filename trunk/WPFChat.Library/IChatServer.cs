using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFChat.Library
{
    [ServiceContract(CallbackContract = typeof(IChatClientCallback))]
    public interface IChatServer
    {
        [OperationContract]
        ClientInfo[] LoginClient(string loginId, string avatar);

        [OperationContract(IsOneWay=true)]
        void LogoffClient(string loginId);

        [OperationContract(IsOneWay=true)]
        void SendMessage(string loginIdFrom, string loginIdTo, string message);

        [OperationContract(IsOneWay=true)]
        void BroadcastMessage(string loginIdFrom, string message);

        [OperationContract]
        Avatar[] GetAvatars();
    }

    public interface IChatClientCallback
    {
        [OperationContract(IsOneWay=true)]
        void ReceiveMessage(string loginIdFrom, string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveBroadcastMessage(string loginIdFrom, string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveUserList(ClientInfo[] loggedInUsers);

        [OperationContract]
        bool CheckConnection();

        
    }
}
