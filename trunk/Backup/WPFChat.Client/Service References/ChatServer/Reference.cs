﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFChat.Client.ChatServer {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChatServer.IChatServer", CallbackContract=typeof(WPFChat.Client.ChatServer.IChatServerCallback))]
    public interface IChatServer {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatServer/LoginClient", ReplyAction="http://tempuri.org/IChatServer/LoginClientResponse")]
        string[] LoginClient(string loginId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatServer/LogoffClient")]
        void LogoffClient(string loginId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatServer/SendMessage", ReplyAction="http://tempuri.org/IChatServer/SendMessageResponse")]
        void SendMessage(string loginIdFrom, string loginIdTo, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IChatServerCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatServer/ReceiveMessage")]
        void ReceiveMessage(string loginIdFrom, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatServer/ReceiveUserList")]
        void ReceiveUserList(string[] loggedInUsers);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IChatServerChannel : WPFChat.Client.ChatServer.IChatServer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ChatServerClient : System.ServiceModel.DuplexClientBase<WPFChat.Client.ChatServer.IChatServer>, WPFChat.Client.ChatServer.IChatServer {
        
        public ChatServerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatServerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatServerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string[] LoginClient(string loginId) {
            return base.Channel.LoginClient(loginId);
        }
        
        public void LogoffClient(string loginId) {
            base.Channel.LogoffClient(loginId);
        }
        
        public void SendMessage(string loginIdFrom, string loginIdTo, string message) {
            base.Channel.SendMessage(loginIdFrom, loginIdTo, message);
        }
    }
}