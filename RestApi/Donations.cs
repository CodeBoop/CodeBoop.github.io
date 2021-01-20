using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

namespace RestApi
{
    public class Donations
    {

        private IDonationService DonationService { get; }
        private IAccessTokenService AccessTokenService { get; }
        private IRandom<string> RandomString { get; }

        public Donations(IDonationService donationService, IAccessTokenService accessTokenService, IRandom<string> randomString)
        {
            DonationService = donationService;
            AccessTokenService = accessTokenService;
            RandomString = randomString;
        }

        [FunctionName("Donations_Get")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Donations")] HttpRequest req,
            ILogger log)
        {
            var items = await DonationService.Get();
            return new OkObjectResult(items.ToDto());
        }

        [FunctionName("Donations_Summary")]
        public async Task<IActionResult> Summary(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Donations/Summary")] HttpRequest req,
            ILogger log)
        {
            var top10T = DonationService.Get(10);
            var totalT = DonationService.Total();
            var countT = DonationService.Count();
            var largestT = DonationService.GetLargest();

            await Task.WhenAll(top10T, totalT, countT, largestT);

            var last10Donations = top10T.Result;
            var largest = largestT.Result;

            if(last10Donations.Any())
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

        [FunctionName("Donations_Create")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "Donations/Create")] HttpRequest req,
            ILogger log)
        {

            var donation = await req.FromBody<DonationPromiseDto>(log);
            if (donation == null)
                return new BadRequestErrorMessageResult("Donation JSON could not be parsed");

            if (donation.PaypalId.IsNullOrWhiteSpace())
            {
                log.LogCritical($"Donation from {donation.Email} - {donation.Name} has no paypal id");
                return new BadRequestErrorMessageResult("Donation has no Paypal Id");
            }

            var accessToken = await AccessTokenService.PayPal();

            var paypalApi = new PayPalApi(accessToken.Token);

            var payPalTransaction = await paypalApi.GetOrder(donation.PaypalId);
            var firstItem = payPalTransaction?.PurchaseUnits.FirstOrDefault();


            if (payPalTransaction==null || payPalTransaction.Status != Status.COMPLETED) {
                log.LogCritical($"Donation with PayPalId {donation.PaypalId} was not completed");
                return new BadRequestErrorMessageResult("PayPal Transaction has not been completed");
            }

            if ((firstItem?.Amount?.DecimalValue ?? 0) < 1)
            {
                log.LogCritical($"Donation with PayPalId {donation.PaypalId} had a transaction value of less then £1");
                return new BadRequestErrorMessageResult("PayPal Transaction is for an invalid amount");
            }
            if (!firstItem.Amount.CurrencyCode.Equals("GBP", StringComparison.CurrentCultureIgnoreCase))
            {
                log.LogCritical($"Donation with PayPalId {donation.PaypalId} is not in GBP");
                return new BadRequestErrorMessageResult("PayPal Transaction is for an invalid amount");
            }

            var don = new Donation()
            {
                Anon = donation.Anon,
                Comment = donation.Comment,
                Email = donation.Email,
                Firstname = donation.Firstname(),
                Lastname = donation.Lastname(),
                Total = firstItem.Amount.DecimalValue.Value,
                PayPayToken = donation.PaypalId,
                PassPhrase = (await RandomString.Generate(3)).ToProperCase().Join("")
            };

            await DonationService.Create(don);

            return new OkResult();

        }

    }
}
