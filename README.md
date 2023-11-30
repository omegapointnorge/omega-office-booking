# Office Booking

Internal project for booking desks in the Oslo office. This project is created with ASP.NET Core and React as a Single-Page Application (SPA). Microsoft Tenant ID is used as authentication mechanism for users.

## Local Build

When deploying the application, we use Azure Key Vault to provide the different secrets that we need in order to get the application to work. Locally, we recommend using user-secrets to store the secrets locally on the computer. This is to avoid pushing secrets within the code.

Get the secrets from the keyvault and store them with:

```bash
dotnet user-secrets set "AzureAd__ClientId" "XXXXXX.."
dotnet user-secrets set "AzureAd__ClientSecret" "XXXXXX.."
dotnet user-secrets set "AzureAd__TentanId" "XXXXXX.."
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Initial Catalog=OfficeBookingDB; Data Source=localhost,<Port_Number>; Persist Security Info=True;User ID=SA;Password= <Your_Password>; TrustServerCertificate=True""
```
Prerequisit of the this is that you have docker installed in your PC,
When you first time run the docker, you can run the script named scriptDockerConnect.ps1. 

After the image is loaded, then you can run "docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=2Secure*Password2" -p 1450:1433 --name sqlserverdb -h mysqlserver -d mcr.microsoft.com/mssql/server:2019-latest". 
Note: the port number 1450 need to match the number of your connection in Azure data studio. 

After the image is pulled and DB is connected, you can run the .Net cli "dotnet ef database update" for migration the database to your local machine.
##

## Additional information

You will need access to the subscription used for the project, as well as the app registration within Microsoft Azure AD. Contact a server admin to get that working.

Get the permissions right away to avoid bottlenecks!

Figma design: https://www.figma.com/file/NC9ZwuocdWNaLCxDIrJkfY/Office-Booking?type=design&node-id=0%3A1&mode=design&t=My2Yu9A58c5mDaE5-1

## Contributing

This project was originally started by Håkon Bøckman, Mostafa Aziz, Vicky Huang & Synne Kjærvik. You can contact them for additional information if needed.
