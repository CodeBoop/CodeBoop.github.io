using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using FTH.Code;

namespace FTH
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.RootComponents.Add<App>("#app");
#if DEBUG
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7071/api/") });
#else
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://lemon-water-0f7de7b03.azurestaticapps.net/api/") });            
#endif


            builder.Services.AddScoped<DonationApi>();


            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
