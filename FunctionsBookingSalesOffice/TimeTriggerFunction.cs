
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionsD365.Models;
using AzureFunctionsD365.Service;
using Newtonsoft.Json.Linq;

namespace ReacquiringBookings
{
    public static class TimeTriggerFunction
    {
        //In this function we are going to use the msal approach,
        //Get the jwt token from given tenant user
        //Construct a request to an Azure API endpoint.
        //Include Authorization = Bearer < token > in your headers of the request

        [FunctionName("TimeTriggerFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string email = req.Query["email"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            email = email ?? data?.email;

            try
            {

                log.LogInformation(" Email is." + email);
                if (email == null ||  email == string.Empty)
                {
                    return new BadRequestObjectResult("This HTTP triggered function executed. Pass a name and email in the query string or in the request body for calling further operation.");
                }
                else
                {
                    //TODO: move to application setting and keyvault later
                    #region configuration

                    string tenantId = "8cfbbf8f-e810-40eb-b4fd-18fc82ba74b8";
                    string clientId = "34963513-4270-4ada-9fa5-be551bd0d39b";
                    string clientSecret = "ohu8Q~A7OXImHIJ4Zk_2OeRljAruFzgOYKMN7bL-";
                    string resourceUri = $"https://graph.microsoft.com";
                 
                    WebApiConfiguration allConfig = new WebApiConfiguration(clientId, clientSecret, tenantId, resourceUri, "https://graph.microsoft.com/v1.0/users");
                    #endregion  configuration

                    #region call service responseSync, HttpClient already implements this pattern
                    var response = MsalLoginService.RetrieveUsers(email, allConfig).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        log.LogInformation("The request IsSuccessStatusCode");
                        // Read the content as a string
                        string jsonContent = await response.Content.ReadAsStringAsync();
                     
                        //dynamic jsonObject = JObject.Parse(jsonContent);

                        // Access the 'id' property of the first item in the 'value' array
                        //string userId = jsonObject.value[0].id;
                        return new OkObjectResult(jsonContent);
                    }
                    else
                    {
                        log.LogInformation("The request failed ",
                            response.ReasonPhrase);

                        throw new Exception();
                    }
                    #endregion call service

                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("ERROR:" + e);
            }
        }
    }
}
