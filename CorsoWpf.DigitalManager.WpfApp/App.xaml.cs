using CorsoWpf.DigitalManager.Messages;
using CorsoWpf.DigitalManager.ViewModels;
using CorsoWpf.DigitalManager.WpfApp.Dialogs;
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
            Messenger.Default.Register<QuestionMessage>(this, ask);
        }

        private void ask(QuestionMessage obj)
        {
            ConfirmWindow wnd = new ConfirmWindow();
            wnd.Title = obj.Message;
            bool result = wnd.ShowDialog().GetValueOrDefault();
            obj.Parameter = wnd.sldValue.Value;

            if (result)
            {
                obj.Yes?.Invoke();
            }
            else
            {
                obj.No?.Invoke();
            }

            //MessageBoxResult result = MessageBox.Show(obj.Message, obj.Title,
            //    MessageBoxButton.YesNo, MessageBoxImage.Question);

            //if (result == MessageBoxResult.Yes)
            //{
            //    obj.Yes?.Invoke();
            //}
            //else
            //{
            //    obj.No?.Invoke();
            //}
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
                if (wnd.Resources.Contains("viewmodel"))
                {
                    var vm = wnd.Resources["viewmodel"] as ApplicationViewModelBase;
                }

                wnd.Closed += Wnd_Closed;

                if (obj.Modal)
                {
                    wnd.ShowDialog();
                }
                else
                {
                    wnd.Show();
                }
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