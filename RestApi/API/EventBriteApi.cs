using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Domain.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestApi.API
{
    public class EventBriteApi
    {


        private HttpClient Client { get; }

        public EventBriteApi()
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.eventbriteapi.com/v3/")
            };

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("EventBriteApiKey"));
        }


        public async Task<IEnumerable<Donation>> Orders()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            var req = await Client.GetAsync($"events/137912330493/orders?token={Environment.GetEnvironmentVariable("EventBriteApiKey")}&expand=attendees");

            req.EnsureSuccessStatusCode();

            var json = await req.Content.ReadAsStringAsync();

            var summary = JsonConvert.DeserializeObject<EventBriteOrderPage>(json);

            return summary.orders.Select(i =>
            {

                var qs = FindCommentAndDonationType(i.attendees.FirstOrDefault()?.answers);

                return new Donation()
                {
                    Total = i.costs.base_price.ValueDecimal,
                    Firstname = i.first_name,
                    Lastname = i.last_name,
                    DisplayType = qs.Item2,
                    Comment = qs.Item1
                };
            }).ToArray();
        }

        private (string, AnonType) FindCommentAndDonationType(IEnumerable<CustomQuestion> qs)
        {

            var anonType = AnonType.DontShow;

            var anonDonationQ = qs?.FirstOrDefault(i => i.Id == 39667949);
            var commentQ = qs?.FirstOrDefault(i=>i.Id== 39667951);
            if (anonDonationQ != null && !anonDonationQ.answer.IsNullOrWhiteSpace())
            {
                var showName = anonDonationQ.answer.Contains("Show my name");
                var showValue = anonDonationQ.answer.Contains("Show my donation value");

                if (showValue && showName)
                    anonType = AnonType.ShowAll;
                else if (showValue)
                    anonType = AnonType.ShowDonationValue;
                else if (showName)
                    anonType = AnonType.ShowMyName;
            }

            return (commentQ?.answer, anonType);
        }


        public async Task<DonationSummary> Summary()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var req = await Client.GetAsync("organizations/508860274091/reports/sales/?event_ids=137912330493");

            req.EnsureSuccessStatusCode();

            var json = await req.Content.ReadAsStringAsync();

            var summary = JsonConvert.DeserializeObject<EventBriteSummary>(json,
                new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() } });

            return new DonationSummary() {Count = summary.Quantity, Total = summary.Net};

        }



    }
}
