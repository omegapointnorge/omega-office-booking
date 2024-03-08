using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Resources;
using System.Xml.Linq;
using System.Net.Http;
using AzureFunctionsD365.Models;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Newtonsoft.Json;

namespace AzureFunctionsD365.Service
{
    public class MsalLoginService
    {

        //TODO: to move the tenantId, string clientId, string clientSecret to WebApiConfiguration and fetch it from setting later
        public static async Task<string> GetAccessToken(string tenantId, string clientId, string clientSecret, string resourceUri)
        {
            try
            {
                string[] scopes = { resourceUri + "/.default" };

                var authBuilder = ConfidentialClientApplicationBuilder.Create(clientId)
                   .WithTenantId(tenantId)
                   .WithClientSecret(clientSecret)
                   .Build();

                var result = await authBuilder.AcquireTokenForClient(scopes)
                    .ExecuteAsync();
                return result.AccessToken;
            }
            catch (MsalUiRequiredException)
            {
                return "Error is caused by  your UserPrincipalName or Password in appsettings.config is incorrect:";
            }
            catch (Exception)
            {
                return "Error";
            }

        }

        public static async Task<HttpResponseMessage> RetrieveUsers(string userEmail, WebApiConfiguration allConfig)
        {
            // Get the access token that is required for authentication.
            var accessToken = await GetAccessToken(allConfig.TenantId, allConfig.ClientId, allConfig.Secret, allConfig.ResourceUri);
            if (accessToken.StartsWith("Error")) return new HttpResponseMessage(HttpStatusCode.BadGateway);
            // Create an HTTP message with the required Web API headers populated.
            var client = new HttpClient();
            // this query query towards no parent contact
            var resourceEndpoint = allConfig.ServiceRoot;
            var message = new HttpRequestMessage(HttpMethod.Get, $"{resourceEndpoint}?$top=999&$filter=mail eq '{userEmail}'");

            message.Headers.Add("OData-MaxVersion", "4.0");
            message.Headers.Add("OData-Version", "4.0");
            message.Headers.Add("Prefer", "odata.include-annotations=*");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.SendAsync(message);
            return result;
        }
    }
}