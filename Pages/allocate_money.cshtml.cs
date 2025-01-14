using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class allocate_moneyModel : PageModel
    {
        public int TotalAmount { get; set; }
        public List<Disasters3> listdisasters = new List<Disasters3>();
        public List<Monetary_Donation3> listmonetaryDonations = new List<Monetary_Donation3>();

        public DisasterMonetaryAllocation disasterMonetaryAllocation = new DisasterMonetaryAllocation();
        public string errorMsg = "";
        public string successMsg = "";
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "SELECT * FROM Disasters";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Disasters3 disasters = new Disasters3();
                                disasters.Id = reader.GetInt32(0);
                                disasters.startDate = reader.GetDateTime(1);
                                disasters.endDate = reader.GetDateTime(2);
                                disasters.location = reader.GetString(3);
                                disasters.description = reader.GetString(4);
                                disasters.requiredAidTypes = reader.GetString(5);

                                listdisasters.Add(disasters);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "SELECT * FROM MonetaryDonations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Monetary_Donation3 monetaryDonations = new Monetary_Donation3();
                                monetaryDonations.Id = reader.GetInt32(0);
                                monetaryDonations.DonationDate2 = reader.GetDateTime(1);
                                monetaryDonations.Amount = reader.GetInt32(2);

                                listmonetaryDonations.Add(monetaryDonations);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "SELECT SUM(Amount) FROM MonetaryDonations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                TotalAmount = reader.GetInt32(0);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void OnPost()
        {
            OnGet();
            disasterMonetaryAllocation.Id = Guid.NewGuid().ToString();
            disasterMonetaryAllocation.MonetaryDescription = Request.Form["amount"];
            disasterMonetaryAllocation.DisasterDescription = Request.Form["disaster"];

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO DisasterMonetaryAllocations " +
                                  "(Id, DisasterDescription, MonetaryDescription) VALUES " +
                                  "(@Id, @DisasterDescription, @MonetaryDescription);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", disasterMonetaryAllocation.Id);
                        command.Parameters.AddWithValue("@DisasterDescription", disasterMonetaryAllocation.DisasterDescription);
                        command.Parameters.AddWithValue("@MonetaryDescription", disasterMonetaryAllocation.MonetaryDescription);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            successMsg = "Money successfully allocated.";
        }

    }
}

public class Disasters3
{
    public int Id;
    public DateTime startDate;
    public DateTime endDate;
    public string? location;
    public string? description;
    public string? requiredAidTypes;
}

public class Monetary_Donation3
{
    public int Id;
    public int Amount;
    public DateTime DonationDate2;
    public bool IsAnonymous;
}

public class DisasterMonetaryAllocation
{
    public string? Id { get; set; }
    public string? DisasterDescription { get; set; }
    public string? MonetaryDescription { get; set; }
}

