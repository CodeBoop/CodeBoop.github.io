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
            var currentData = (string)Database.StringGet("CachedOrders");
            IEnumerable<Donation> donations = null;
            if (currentData.IsNullOrWhiteSpace())
            {

                var eventApi = new EventBriteApi();
                donations = await eventApi.Orders();
                Database.StringSet("CachedOrders", JsonConvert.SerializeObject(donations), new TimeSpan(0, 5, 0));
            }
            else
            {
                donations = JsonConvert.DeserializeObject<IEnumerable<Donation>>(currentData);
            }

            return new OkObjectResult(donations);
        }

    }
}
