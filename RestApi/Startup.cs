using System;
using System.Collections.Generic;
using System.Text;
using DapperRepos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

//[assembly: FunctionsStartup(typeof(RestApi.Startup))]
//namespace RestApi
//{
//    public class Startup : FunctionsStartup
//    {
//        public override void Configure(IFunctionsHostBuilder builder)
//        {
//            /*builder.Services.AddSingleton(new ConnectionStringProvider(Environment.GetEnvironmentVariable("DefaultConnection")));*/
//            /*builder.Services.AddScoped<IDonationRepository, DonationRepository>();
//            builder.Services.AddScoped<IDonationService, DonationService>();

//            builder.Services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
//            builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();

//            builder.Services.AddScoped<IRandom<string>, RandomWordService>();*/

//        }
//    }
//}
