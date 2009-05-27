using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WPFChat.Library
{
    [ServiceContract(CallbackContract = typeof(IChatClientCallback))]
    public interface IChatServer
    {
        [OperationContract]
        string[] LoginClient(string loginId);

        [OperationContract(IsOneWay=true)]
        void LogoffClient(string loginId);

        [OperationContract]
        void SendMessage(string loginIdFrom, string loginIdTo, string message);
    }

    public interface IChatClientCallback
    {
        [OperationContract(IsOneWay=true)]
        void ReceiveMessage(string loginIdFrom, string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveUserList(string[] loggedInUsers);
    }
}
