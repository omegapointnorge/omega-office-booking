using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionSlackIntergration
{
    public static class FunctionSlackIntergration
    {
        [FunctionName("FunctionSlackIntergration")]
        public static async Task RunAsync(
        [TimerTrigger("0 */1 * * * *")] TimerInfo myTimer,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = "you name";

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
