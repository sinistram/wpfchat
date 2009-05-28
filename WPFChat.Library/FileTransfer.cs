using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WPFChat.Library
{
    [DataContract]
    public class FileTransfer
    {
        [DataMember]
        public byte[] Data;

        [DataMember]
        public string FileName;
    }
}
