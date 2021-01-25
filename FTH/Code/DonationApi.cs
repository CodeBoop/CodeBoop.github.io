using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace FTH.Code
{
    public class DonationApi : Api
    {
        public DonationApi(HttpClient client) : base(client)
        {
        }



        public Task<IEnumerable<DonationDto>> Summary()
        {
            return Client.GetFromJsonAsync<IEnumerable<DonationDto>>("Donations/Summary");
        }

        /*public Task Create(DonationPromiseDto dto)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            return Client.PostAsync("Donations/Create", httpContent);
        }*/


    }
}
