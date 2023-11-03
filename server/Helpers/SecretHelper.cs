using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace server.Helpers;

public class SecretHelper
{
    private readonly SecretClient _secretClient;

    public SecretHelper(string keyVaultName)
    {
        var kvUri = "https://" + keyVaultName + ".vault.azure.net";

        _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
    }

    public async Task<string?> FetchSecret(string secretName)
    {
        var secret = await _secretClient.GetSecretAsync(secretName);

        return secret.Value.Value.ToString();
    }
}