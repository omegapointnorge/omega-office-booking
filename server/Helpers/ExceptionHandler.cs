using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server.Response;

namespace server.Helpers
{
    public static class ExceptionHandler
    {
        public static UserBookingResponse HandleDbUpdateException(UserBookingResponse response, DbUpdateException ex)
        {
            // Handle specific database-related exceptions
            if (ex.InnerException is SqlException sqlException)
            {
                // Check for specific SQL Server error codes and handle accordingly
                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    // Unique key violation (duplicate entry)
                    response.Error = "User booking already exists.";
                }
                else
                {
                    // Handle other SQL Server error codes or provide a generic error message
                    response.Error = "Error while saving changes to the database.";
                }
            }
            else
            {
                // Handle other database-related exceptions or provide a generic error message
                response.Error = "Error while saving changes to the database.";
            }
            return response;
        }
    }
}
