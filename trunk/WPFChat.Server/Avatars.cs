using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFChat.Library;
using System.IO;
using System.Reflection;

namespace WPFChat.Server
{
    public static class Avatars
    {
        private static List<Avatar> m_Avatars;
        public static Avatar[] GetAll()
        {
            if (m_Avatars == null)
            {
                m_Avatars = new List<Avatar>();
                DirectoryInfo dirInfo = new DirectoryInfo(
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        "Avatars"));

                foreach (FileInfo fileInfo in dirInfo.GetFiles())
                {
                    Avatar avatar = new Avatar(fileInfo.FullName);
                    m_Avatars.Add(avatar);
                }
            }

            return m_Avatars.ToArray();
        }

        internal static Avatar Get(string avatar)
        {
            return m_Avatars.FirstOrDefault(p => p.ImageName == avatar);
        }
    }
}
