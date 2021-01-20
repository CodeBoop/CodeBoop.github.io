using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestApi.API
{
    public class PayPalApi
    {
        private HttpClient Client { get; }

        public PayPalApi(string accessToken)
        {
            Client = new HttpClient()
            {
#if DEBUG
                BaseAddress = new Uri("https://api.sandbox.paypal.com/v2/"),
#else
                BaseAddress = new Uri("https://api.sandbox.paypal.com/v2/"),
#endif
                Timeout = new TimeSpan(0,0,20)

            };

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<PayPalTransaction> GetOrder(string paypalId)
        {
            var task = (Client.GetAsync($"checkout/orders/{paypalId}"));
            var result = await task;
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PayPalTransaction>(json,
                new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() } });
        }



    }
}
