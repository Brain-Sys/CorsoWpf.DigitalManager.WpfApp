using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CorsoWpf.DigitalManager.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Messenger.Default.Register<int>(this, openNewView);
        }

        private void openNewView(int obj)
        {
            if (obj == 1)
            {
                MainMenuWindow wnd = new MainMenuWindow();
                wnd.Show();
            }
        }
    }
}
