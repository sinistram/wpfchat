using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace WPFChat.Library
{
    [DataContract]
    public class Avatar
    {
        [DataMember]
        public string ImageName;

        [DataMember]
        public byte[] ImageBuffer;

        public Avatar(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            FileInfo fi = new FileInfo(path);
            this.ImageName = fi.Name;

            using (FileStream stream = File.OpenRead(path))
            {
                ImageBuffer = Utils.ReadFully(stream, 1024);
            }
        }
    }
}
