using CorsoWpf.DigitalManager.DomainModel;
using CorsoWpf.DigitalManager.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        private List<Person> items;
        public List<Person> Items
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

        private void manageLogin(LoginSuccessfulMessage obj)
        {
            this.CurrentUser = obj.Username;
            downloadData();
        }

        private async void downloadData()
        {
            this.IsDownloading = true;
            HttpClient client = new HttpClient();

#if DEBUG
            await Task.Delay(10000);
#endif

            string json = await client.GetStringAsync("http://download.vivendobyte.net/people.json");
            this.Items = JsonConvert.DeserializeObject<List<Person>>(json);
            this.IsDownloading = false;
        }
    }
}