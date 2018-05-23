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

namespace CorsoWpf.DigitalManager.ViewModels
{
    public class MainMenuViewModel : ApplicationViewModelBase
    {
        private HttpClient client = new HttpClient();

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

        private List<PersonVM> items;
        public List<PersonVM> Items
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

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value;
                base.RaisePropertyChanged();
            }
        }

        public MainMenuViewModel()
        {
            this.CurrentUser = "(guest)";
            Messenger.Default.Register<LoginSuccessfulMessage>(this, manageLogin);
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

            string json = await client.GetStringAsync("http://download.vivendobyte.net/people.json");
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(json);
            this.Items = people.Select(p => new PersonVM(p)).ToList();
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