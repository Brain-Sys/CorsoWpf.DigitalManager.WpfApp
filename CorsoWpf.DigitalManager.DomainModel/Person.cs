using Newtonsoft.Json;
using System;

namespace CorsoWpf.DigitalManager.DomainModel
{
    public class Person : IDisposable
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country")]
        public string Nation { get; set; }

        public double Weight { get; set; }

        public void Dispose()
        {
            // Free memory
        }
    }
}