using CorsoWpf.DigitalManager.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorsoWpf.DigitalManager.Repository
{
    public interface IRepository
    {
        Task<List<Person>> Load(string source = "http://download.vivendobyte.net/people.json");

        Task<bool> Save(List<Person> list, string destination);
    }
}