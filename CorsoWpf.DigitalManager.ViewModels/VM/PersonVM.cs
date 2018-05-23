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
        public Person InternalInstance { get; private set; }

        public int ID
        {
            get { return InternalInstance.ID; }
            set
            {
                InternalInstance.ID = value;
                base.RaisePropertyChanged();
            }
        }

        public string FirstName
        {
            get { return InternalInstance.FirstName; }
            set
            {
                InternalInstance.FirstName = value;
                base.RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get { return InternalInstance.LastName; }
            set
            {
                InternalInstance.LastName = value;
                base.RaisePropertyChanged();
            }
        }

        public Uri Nation
        {
            get
            {
                string code = InternalInstance.Nation?.ToLower();
                string url = $"http://flags.fmcdn.net/data/flags/w580/{code}.png";
                return new Uri(url, UriKind.RelativeOrAbsolute);
            }
            //set
            //{
            //    InternalInstance.Nation = value;
            //    base.RaisePropertyChanged();
            //}
        }

        public double Weight
        {
            get { return InternalInstance.Weight; }
            set
            {
                InternalInstance.Weight = value;
                base.RaisePropertyChanged();
                base.RaisePropertyChanged(nameof(Status));
            }
        }

        public string Status
        {
            get
            {
                string result = string.Empty;

                if (this.Weight <= 20)
                    result = "LOW";
                else if (this.Weight > 20 && this.Weight <= 60)
                    result = "MEDIUM";
                else
                    result = "HIGH";

                return result;
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

            this.InternalInstance = person;
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