# Office Booking

Internal project for booking desks in the Oslo office. This project is created with ASP.NET Core and React as a Single-Page Application (SPA). Microsoft Tenant ID is used as authentication mechanism for users.

## Local Build

When deploying the application, we use Azure Key Vault to provide the different secrets that we need in order to get the application to work. Locally, we recommend using user-secrets to store the secrets locally on the computer. This is to avoid pushing secrets within the code.

Get the secrets from the keyvault and store them with:

```bash
dotnet user-secrets set "AzureAd__ClientId" "XXXXXX.."
dotnet user-secrets set "AzureAd__ClientSecret" "XXXXXX.."
dotnet user-secrets set "AzureAd__TentanId" "XXXXXX.."
```

## Additional information

You will need access to the subscription used for the project, as well as the app registration within Microsoft Azure AD. Contact a server admin to get that working.

Get the permissions right away to avoid bottlenecks!

## Contributing

This project was originally started by Håkon Bøckman, Mostafa Aziz, Vicky Huang & Synne Kjærvik. You can contact them for additional information if needed.
