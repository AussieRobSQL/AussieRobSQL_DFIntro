using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net;
using System.Text;

namespace getAESTDSTFlag
{
    public static class Functions
    {
        [FunctionName("getAESTDSTFlag")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage req, TraceWriter log)
        {

            DateTime theTime = DateTime.Now;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            Response response = new Response() { isDST = tzi.IsDaylightSavingTime(theTime) };

            return new HttpResponseMessage(HttpStatusCode.OK)
            {

                Content = new StringContent(JsonConvert.SerializeObject(response, Formatting.Indented), Encoding.UTF8, "application/json")

            };

        }
        private class Response
        {
            public bool isDST { get; set; }
        }
    }
}
