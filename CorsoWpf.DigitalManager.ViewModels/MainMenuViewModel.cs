using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private string currentUser;
        public string CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                base.RaisePropertyChanged();
            }
        }

        public MainMenuViewModel()
        {

        }
    }
}