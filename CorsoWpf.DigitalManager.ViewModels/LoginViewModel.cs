using CorsoWpf.DigitalManager.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Threading;

namespace CorsoWpf.DigitalManager.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private Timer timer;

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                base.RaisePropertyChanged();
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                base.RaisePropertyChanged();
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                base.RaisePropertyChanged();
            }
        }

        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                base.RaisePropertyChanged();
            }
        }

        // Command(s)
        public RelayCommand OpenAsGuestCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            this.OpenAsGuestCommand = new RelayCommand(OpenAsGuestCommandExecute);
            this.LoginCommand = new RelayCommand(LoginCommandExecute, LoginCommandCanExecute);

            this.Color = "Cyan";
            this.Username = "corsowpf";
            this.Password = "4Partecipanti";
            this.CurrentTime = DateTime.Now;

            // TimerCallback callback = new TimerCallback(updateCurrentTime);
            timer = new Timer(updateCurrentTime, 1, 0, 1000);
        }

        private bool LoginCommandCanExecute()
        {
            if (string.IsNullOrEmpty(this.Username)) return false;
            if (string.IsNullOrEmpty(this.Password)) return false;

            return true;
        }

        private void OpenAsGuestCommandExecute()
        {
            Messenger.Default.Send(new OpenNewViewMessage("MainMenu"));
        }

        private void LoginCommandExecute()
        {
            if (Username == "corsowpf" && Password == "4Partecipanti")
            {
                // Login OK
                this.Color = "Green";
                // Messaggio di broadcast (login avvenuto)
            }
            else
            {
                // Errore
                this.Color = "#FF0000";
            }
        }

        private void updateCurrentTime(object obj)
        {
            this.CurrentTime = DateTime.Now;
        }
    }
}