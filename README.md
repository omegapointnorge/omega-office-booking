# Office Booking

Internal project for booking desks in the Oslo office. This project is developed using ASP.NET Core and React to create a Single-Page Application (SPA). Authentication for users is implemented using the Microsoft Identity Platform.

## Local Build

When deploying the application, we use Azure Key Vault to provide the various secrets for the application's functionality. We recommend using user secrets to store the secrets for local development. This practice is to prevent pushing secrets to Github by keeping them separate from the source code.



Get the secrets from the keyvault and store them with:

```bash
dotnet user-secrets set "AzureAd:ClientId" "XXXXXX.."
dotnet user-secrets set "AzureAd:ClientSecret" "XXXXXX.."
dotnet user-secrets set "AzureAd:TenantId" "XXXXXX.."
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Initial Catalog=OfficeBookingDB; Data Source=localhost,<Port_Number>; Persist Security Info=True;User ID=SA;Password= <Your_Password>; TrustServerCertificate=True"

dotnet user-secrets set "GOOGLE-CLOUD-PROJECT" "office-booking-1706538315753"
dotnet user-secrets set "GOOGLE-APPLICATION-CREDENTIALS" "XXXXXX.."

```
The content of secrets can be obtained by contacting [Contributing](#contributing)

The prerequisite for establishing a local database connection is having Docker Desktop installed on your PC.
Run the database by issuing the command 'docker compose up -d' in your terminal.

Note: The port numbers exposed are set in the docker-compose.yml file like this:

ports: 
    - "1450:1433"

The port to the left (1450 in this example) need to match the number of your connection in Azure data studio. 

After the image is pulled and database is connected, you can run the .Net cli "dotnet ef database update" for migration the database to your local machine.
##
## User roles information
# Normal user role
# Event admin user role

### Google Cloud Setup (reCAPTCHA)
This application relies on Google reCAPTCHA Enterprise. To use this service, a Google Cloud account has been created. Please note that the account had to be set up with a Gmail account under the domain "omegapoint.no". Thus, the account utilized for this purpose is Nils.Olav.johansen@omegapoint.no. For any changes or adjustments to the Google Cloud settings, please contact Nils Olav Johansen.

### Permissions and Setup Prerequisites in Azure

## Additional information

Azure setup prerequisites:
App registration within Microsoft Azure AD.
Permissions required:
You will need access to the subscription used for the project, as well as the access to this gitub

Contact a server admin to get that working(@Salah Waisi).

Get the permissions right away to avoid bottlenecks!

Figma design: https://www.figma.com/file/NC9ZwuocdWNaLCxDIrJkfY/Office-Booking?type=design&node-id=0%3A1&mode=design&t=My2Yu9A58c5mDaE5-1

## Contributing
<!-- Anchor for Contributing section -->

This project was originally started by Håkon Bøckman, Mostafa Aziz, Vicky Huang & Synne Kjærvik,  have been lately contributed by Nils Olav Kvelvane Johansen and Hiruth Marie Stautland. You can contact them for additional information if needed.
