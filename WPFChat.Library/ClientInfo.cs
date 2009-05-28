using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Timers;

namespace WPFChat.Library
{
    [DataContract]
    public class ClientInfo : IDisposable
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

        private Timer m_Timer;

        public ClientInfo()
        {
            m_Timer = new Timer();
            m_Timer.Interval = Constants.ClientCheckInterval;
            m_Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            m_Timer.Start();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Checking conn stat for {0}", this.LoginId);

            bool IsAlive; 

            try
            {
                IsAlive = Callback.CheckConnection();
            }
            catch (Exception ex)
            {
                IsAlive = false;
            }

            if (!IsAlive && ConnectionCheckFailed != null )
                ConnectionCheckFailed(this);
        }

        public ConnectionCheckFailedDelegate ConnectionCheckFailed;

        public delegate void ConnectionCheckFailedDelegate(ClientInfo clientInfo);

        #region IDisposable Members

        public void Dispose()
        {
            m_Timer.Stop();
            m_Timer.Dispose();
        }

        #endregion
    }
}
