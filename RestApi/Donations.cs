using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Models;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestApi.API;
using RestApi.Helpers;
using StackExchange.Redis;

namespace RestApi
{
    public class Donations
    {

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = Environment.GetEnvironmentVariable("RedisConnection");
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public static IDatabase Database => Connection.GetDatabase();

        [FunctionName("Donations_Summary")]
        public async Task<IActionResult> Summary(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Donations/Summary")] HttpRequest req,
            ILogger log)
        {
            // Redis usage
            var currentData = "";// (string)Database.StringGet("CachedSummary");

            DonationSummaryDto dto = null;
            try
            {
                if (currentData.IsNullOrWhiteSpace())
                {

                    var eventApi = new EventBriteApi();
                   // var donationsT = eventApi.Orders();
                    var summaryT = eventApi.Summary();

                  //  await Task.WhenAll(donationsT, summaryT);
                  await summaryT;
                    var summary = summaryT.Result;

                    dto = new DonationSummaryDto()
                    {
                        Count = summary.Count,
                        Total = summary.Total,
                        //LatestDonations = donationsT.Result.ToDto()
                    };

                    Database.StringSet("CachedSummary", JsonConvert.SerializeObject(dto), new TimeSpan(0, 5, 0));
                }
                else
                {
                    dto = JsonConvert.DeserializeObject<DonationSummaryDto>(currentData);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
          

            return new OkObjectResult(dto);
        }

    }
}
