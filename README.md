# Office Booking

Internal project for booking desks in the Oslo office. This project is created with ASP.NET Core and React as a Single-Page Application (SPA). Microsoft Tenant ID is used as authentication mechanism for users.

## Local Build

When deploying the application, we use Azure Key Vault to provide the different secrets that we need in order to get the application to work. Locally, we recommend using user-secrets to store the secrets locally on the computer. This is to avoid pushing secrets within the code.

Get the secrets from the keyvault and store them with:

```bash
dotnet user-secrets set "AzureAd__ClientId" "XXXXXX.."
dotnet user-secrets set "AzureAd__ClientSecret" "XXXXXX.."
dotnet user-secrets set "AzureAd__TentanId" "XXXXXX.."
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Initial Catalog=OfficeBookingDB; Data Source=localhost,<Port_Number>; Persist Security Info=True;User ID=SA;Password= <Your_Password>; TrustServerCertificate=True"
dotnet user-secrets set "AzureAd__ClientSecret" "XXXXXX.."
dotnet user-secrets set "AzureAd__TentanId" "XXXXXX.."

dotnet user-secrets set "GOOGLE-CLOUD-PROJECT" "office-booking-1706538315753"
dotnet user-secrets set "GOOGLE-APPLICATION-CREDENTIALS" "XXXXXX.."




For macbook user, the image addressen need to be the address for Mac. change mcr.microsoft.com/mssql/server:2019-latest in the following commands
```
Prerequisit of the this is that you have the latest version docker desktop installed on your PC. This version will include Docker compose, which is required. 
Run the db by issuing the command 'docker compose up -d' in your terminal.

Note: The port numbers exposed are set in the docker-compose.yml file like this:

ports: 
    - "1450:1433"

The port to the left (1450 in this example) need to match the number of your connection in Azure data studio. 

After the image is pulled and DB is connected, you can run the .Net cli "dotnet ef database update" for migration the database to your local machine.
##

## Additional information

You will need access to the subscription used for the project, as well as the app registration within Microsoft Azure AD. Contact a server admin to get that working.

Get the permissions right away to avoid bottlenecks!

Figma design: https://www.figma.com/file/NC9ZwuocdWNaLCxDIrJkfY/Office-Booking?type=design&node-id=0%3A1&mode=design&t=My2Yu9A58c5mDaE5-1

## Contributing

This project was originally started by Håkon Bøckman, Mostafa Aziz, Vicky Huang & Synne Kjærvik. You can contact them for additional information if needed.
