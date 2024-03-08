using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Models.DTOs.Internal
{


    public class WebApiConfiguration
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string TenantId { get; set; }
        public string ResourceUri { get; set; }
        public string ServiceRoot { get; set; }

        public WebApiConfiguration(string clientId, string secret, string tenantId, string resourceUri, string serviceRoot)
        {
            ClientId = clientId;
            Secret = secret;
            TenantId = tenantId;
            ResourceUri = resourceUri;
            ServiceRoot = serviceRoot;
        }


    }
}
