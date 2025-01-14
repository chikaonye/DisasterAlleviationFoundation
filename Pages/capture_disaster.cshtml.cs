using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class capture_disasterModel : PageModel
    {
        public Disasters disasters = new Disasters();
        public string errorMsg = "";
        public string successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            disasters.startDate = DateTime.Parse(Request.Form["startDate"]);
            disasters.endDate = DateTime.Parse(Request.Form["endDate"]);
            disasters.location = Request.Form["location"];
            disasters.description = Request.Form["description"];
            disasters.requiredAidTypes = Request.Form["requiredAidTypes"];

try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Disasters " +
                                  "(StartDate, EndDate, Location, Description, AidTypes) VALUES " +
                                  "(@startDate, @endDate, @location, @description, @requiredAidTypes);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@startDate", disasters.startDate);
                        command.Parameters.AddWithValue("@endDate", disasters.endDate);
                        command.Parameters.AddWithValue("@location", disasters.location);
                        command.Parameters.AddWithValue("@description", disasters.description);
                        command.Parameters.AddWithValue("@requiredAidTypes", disasters.requiredAidTypes);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }


            disasters.location = "";
            disasters.requiredAidTypes = "";
            disasters.description = "";

            successMsg = "Disaster successfully added.";

        }

    }
}

public class Disasters
{
    public int Id;
    public DateTime startDate;
    public DateTime endDate;
    public string? location;
    public string? description;
    public string? requiredAidTypes;
}
