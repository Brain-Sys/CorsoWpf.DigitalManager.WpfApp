using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CorsoWpf.DigitalManager.DomainModel;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace CorsoWpf.DigitalManager.Repository
{
    public class JsonRepository : IRepository
    {
        public async Task<List<Person>> Load(string source = "http://download.vivendobyte.net/people.json")
        {
            string json = null;
            List<Person> result = new List<Person>();

            if (!File.Exists("E:\\Anagrafica.json"))
            {
                using (HttpClient client = new HttpClient())
                {
                    json = await client.GetStringAsync(source);
                }
            }
            else
            {
                json = File.ReadAllText("E:\\Anagrafica.json");
            }

            result = JsonConvert.DeserializeObject<List<Person>>(json).Skip(0).Take(10).ToList();

            return result;
        }

        public bool Save(List<Person> list, string destination)
        {
            Debug.WriteLine("Save Repo : " + Thread.CurrentThread.ManagedThreadId);
            string json = string.Empty;
            json = JsonConvert.SerializeObject(list);
            File.WriteAllText(destination, json);

            return true;
        }
    }
}