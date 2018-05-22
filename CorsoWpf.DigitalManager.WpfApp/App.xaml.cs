using CorsoWpf.DigitalManager.Messages;
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
            Messenger.Default.Register<OpenNewViewMessage>(this, openNewView);
        }

        private void openNewView(OpenNewViewMessage obj)
        {
            Window wnd = null;

            string name = "CorsoWpf.DigitalManager.WpfApp." + obj.WindowName + "Window";
            Type t = Type.GetType(name);

            if (t != null)
            {
                wnd = Activator.CreateInstance(t) as Window;
            }

            if (wnd != null)
            {
                if (obj.Modal)
                    wnd.ShowDialog();
                else
                    wnd.Show();
            }
        }
    }
}
