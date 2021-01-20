using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace Domain.Services
{

    public class RandomWordService : IRandom<string>
    {

        private HttpClient Client { get; }

        public RandomWordService()
        {
            Client = new HttpClient() { BaseAddress = new Uri("https://random-word-api.herokuapp.com/word") };

        }

        public async Task<string> Generate()
        {
            return (await Generate(1)).FirstOrDefault();
        }

        public async Task<IEnumerable<string>> Generate(int count)
        {
            var url = $"?number={count}&swear=0";

            var request = await Client.GetAsync(url);
            request.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string[]>(await request.Content.ReadAsStringAsync());
        }

    }
}
