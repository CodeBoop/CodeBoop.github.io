using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RestApi.Helpers
{
    public static class RequestHelpers
    {

        public static async Task<T> FromBody<T>(this HttpRequest req, ILogger log) where T:class
        {
            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogDebug($"Converting {content}");
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                log.LogError(ex,"Unable to convert object");
                return null;
            }
        }

    }
}
