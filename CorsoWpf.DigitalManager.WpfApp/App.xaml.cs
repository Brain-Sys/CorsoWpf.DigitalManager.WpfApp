using CorsoWpf.DigitalManager.Messages;
using CorsoWpf.DigitalManager.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            Debug.WriteLine("Thread UI : " + Thread.CurrentThread.ManagedThreadId);
            Messenger.Default.Register<OpenNewViewMessage>(this, openNewView);
            Messenger.Default.Register<ShowMessage>(this, showMessage);
        }

        private void showMessage(ShowMessage obj)
        {
            MessageBox.Show(obj.Message, obj.Title,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void openNewView(OpenNewViewMessage obj)
        {
            Window wnd = null;

            string name = "CorsoWpf.DigitalManager.WpfApp." + obj.WindowName + "Window";
            Type t = Type.GetType(name);

            if (t != null)
            {
                WindowCollection windows = Application.Current.Windows;

                wnd = Activator.CreateInstance(t) as Window;
            }

            if (wnd != null)
            {
                wnd.Closed += Wnd_Closed;

                if (obj.Modal)
                    wnd.ShowDialog();
                else
                    wnd.Show();
            }
        }

        private void Wnd_Closed(object sender, EventArgs e)
        {
            if (this.Resources.Contains("viewmodel"))
            {
                var vm = this.Resources["viewmodel"] as ApplicationViewModelBase;
                vm?.Dispose();
            }
        }
    }
}