using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RestApi
{
    public class Donations
    {

        private IDonationService DonationService { get; }

        public Donations(IDonationService donationService)
        {
            DonationService = donationService;
        }

        [FunctionName("Total")]
        public async Task<IActionResult> Total(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var total = await DonationService.Total();
            return new OkObjectResult(total);
        }

        [FunctionName("Count")]
        public async Task<IActionResult> Count(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var total = await DonationService.Count();
            return new OkObjectResult(total);
        }

        [FunctionName("Get")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Donations")] HttpRequest req,
            ILogger log)
        {
            var items = await DonationService.Get();
            return new OkObjectResult(items.ToDto());
        }

        [FunctionName("Summary")]
        public async Task<IActionResult> Summary(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Donations/Summary")] HttpRequest req,
            ILogger log)
        {
            var top10T = DonationService.Get(10);
            var totalT = DonationService.Total();
            var countT = DonationService.Count();
            var largestT = DonationService.GetLargest();

            await Task.WhenAll(top10T, totalT, countT);

            var last10Donations = top10T.Result;
            var largest = largestT.Result;

            last10Donations = last10Donations.Where(i => i.Id != largest.Id).ToArray();

            return new OkObjectResult(
                new DonationSummaryDto()
                {
                    Total = totalT.Result,
                    Count = countT.Result,
                    Last10Donations = last10Donations.ToDto(),
                    Largest = largest.ToDto()
                }
            );
        }
    }
}
