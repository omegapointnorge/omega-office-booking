using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http;
using System.Collections.Generic;

namespace FunctionSlackIntergration
{
    public static class FunctionSlackIntergration
    {
        private static readonly HttpClient client = new HttpClient();
        [FunctionName("FunctionSlackIntergration")]
        public static async Task<IActionResult> RunAsync([TimerTrigger("0 */2 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            Thread.Sleep(10000); // Th
            var values = new Dictionary<string, string>
                  {
                      { "text", "Hello, Call from Function app."},
                      { "channel", "alert-office-booking" }
                  };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://hooks.slack.com/services/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", content);

            var responseString = await response.Content.ReadAsStringAsync();
            log.LogInformation($"2  C# Timer trigger function executed at: {DateTime.Now}");
            return new OkObjectResult(responseString);
        }
    }
}
