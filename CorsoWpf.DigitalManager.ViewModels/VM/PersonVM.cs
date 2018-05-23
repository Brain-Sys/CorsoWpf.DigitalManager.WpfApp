using CorsoWpf.DigitalManager.DomainModel;
using CorsoWpf.DigitalManager.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorsoWpf.DigitalManager.ViewModels.VM
{
    public class PersonVM : ApplicationViewModelBase
    {
        Person internalInstance;

        public int ID
        {
            get { return internalInstance.ID; }
            set
            {
                internalInstance.ID = value;
                base.RaisePropertyChanged();
            }
        }

        public string FirstName
        {
            get { return internalInstance.FirstName; }
            set
            {
                internalInstance.FirstName = value;
                base.RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get { return internalInstance.LastName; }
            set
            {
                internalInstance.LastName = value;
                base.RaisePropertyChanged();
            }
        }

        public Uri Nation
        {
            get
            {
                string code = internalInstance.Nation.ToLower();
                string url = $"http://flags.fmcdn.net/data/flags/w580/{code}.png";
                return new Uri(url, UriKind.RelativeOrAbsolute);
            }
        }

        public double Weight
        {
            get { return internalInstance.Weight; }
            set
            {
                internalInstance.Weight = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand ExportCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        public PersonVM(Person person)
        {
            this.ExportCommand = new RelayCommand(ExportCommandExecute);

            this.internalInstance = person;
        }

        /// <summary>
        /// Export the current instance of the Person
        /// </summary>
        private async void ExportCommandExecute()
        {
            this.IsBusy = true;

            await Task.Run(async () =>
            {

                int n = new Random((int)DateTime.Now.Ticks).Next(1, 10);
                await Task.Delay(n * 1000);

                ShowMessage msg = new ShowMessage();
                msg.Title = "Conferma";
                msg.Message = "Esportazione completata!";
                Messenger.Default.Send(msg);
            });

            this.IsBusy = false;
        }
    }
}