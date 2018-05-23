using CorsoWpf.DigitalManager.DomainModel;
using CorsoWpf.DigitalManager.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CorsoWpf.DigitalManager.ViewModels.VM;
using CorsoWpf.DigitalManager.Repository;
using GalaSoft.MvvmLight.Command;
using System.Threading;
using System.Collections.ObjectModel;

namespace CorsoWpf.DigitalManager.ViewModels
{
    public class MainMenuViewModel : ApplicationViewModelBase
    {
        private IRepository repo = new JsonRepository();
        private HttpClient client = new HttpClient();

        private string currentUser;
        public string CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                base.RaisePropertyChanged();
                this.SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<PersonVM> items;
        public ObservableCollection<PersonVM> Items
        {
            get { return items; }
            set { items = value;
                base.RaisePropertyChanged();
            }
        }

        private bool isDownloading;
        public bool IsDownloading
        {
            get { return isDownloading; }
            set { isDownloading = value;
                base.RaisePropertyChanged();
            }
        }

        private PersonVM selectedPerson;
        public PersonVM SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value;
                base.RaisePropertyChanged();
                this.DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public MainMenuViewModel()
        {
            this.DeleteCommand = new RelayCommand(DeleteCommandExecute, DeleteCommandCanExecute);
            this.SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
            this.AddCommand = new RelayCommand(() => { this.Items.Insert(0, new PersonVM(new Person())); });

            this.CurrentUser = "(guest)";
            Messenger.Default.Register<LoginSuccessfulMessage>(this, manageLogin);
        }

        private bool DeleteCommandCanExecute()
        {
            return this.SelectedPerson != null;
        }

        private void DeleteCommandExecute()
        {
            QuestionMessage msg = new QuestionMessage();
            msg.Title = "Conferma!";
            msg.Message = $"Sei sicuro di voler eliminare:\r\n{this.SelectedPerson.FirstName} {this.SelectedPerson.LastName} ?";
            msg.Yes = () => this.Items.Remove(this.SelectedPerson);
            msg.No = null;

            Messenger.Default.Send(msg);
        }

        private bool SaveCommandCanExecute()
        {
            return this.CurrentUser != "(guest)";
        }

        private async void SaveCommandExecute()
        {
            var people = this.Items.Select(p => p.InternalInstance).ToList();
            bool result = await repo.Save(people, "E:\\Anagrafica.json");

            ShowMessage msg = new ShowMessage();
            msg.Title = "Conferma!";
            msg.Message = "Salvataggio completato!";
            Messenger.Default.Send(msg);
        }

        // Metodo che viene eseguito quando avviene un login
        // Messaggio broadcast
        private void manageLogin(LoginSuccessfulMessage obj)
        {
            Debug.WriteLine("manageLogin");
            this.CurrentUser = obj.Username;
            downloadData();
        }

        private async void downloadData()
        {
            this.IsDownloading = true;      

#if DEBUG
            await Task.Delay(2000);
#endif

            List<Person> people = await repo.Load();
            this.Items = new ObservableCollection<PersonVM>(people.Select(p => new PersonVM(p)).ToList());
            this.IsDownloading = false;
        }

        public override void Dispose()
        {
            // Staccati dall'ascolto del messaggio LoginSuccessfulMessage
            Messenger.Default.Unregister<LoginSuccessfulMessage>(this, manageLogin);

            if (this.Items != null && this.Items.Any())
            {
                foreach (var item in this.Items)
                {
                    item.Dispose();
                }
            }

            client.Dispose();
            client = null;
        }
    }
}