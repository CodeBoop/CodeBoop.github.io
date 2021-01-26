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



        public Task<DonationSummaryDto> Summary()
        {
            return Client.GetFromJsonAsync<DonationSummaryDto>("Donations/Summary");
        }


    }
}
