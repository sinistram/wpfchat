using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WPFChat.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                //WPFChat.Client.MainWindow.MainWindow mainWindow = new WPFChat.Client.MainWindow.MainWindow();
                //App.Current.Run(mainWindow);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
