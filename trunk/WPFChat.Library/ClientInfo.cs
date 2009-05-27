using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WPFChat.Library
{
    [DataContract]
    public class ClientInfo
    {
        [DataMember]
        public string LoginId
        {
            get;
            set;
        }
        public IChatClientCallback Callback
        {
            get;
            set;
        }
        [DataMember]
        public Avatar Avatar
        {
            get;
            set;
        }
    }
}
