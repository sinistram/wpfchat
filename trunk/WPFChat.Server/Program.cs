using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WPFChat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ChatServer));

            host.Open();

            Console.WriteLine("<<ENTER>> to close server");
            Console.ReadLine();

            host.Close();
        }
    }
}
