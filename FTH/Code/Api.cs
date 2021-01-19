using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FTH.Code
{
    public class Api
    {

        protected HttpClient Client { get; set; }


        public Api(HttpClient client)
        {
            Client = client;
        }


    }
}
