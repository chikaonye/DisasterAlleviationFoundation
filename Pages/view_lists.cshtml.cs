using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class disaster_listModel : PageModel
    {
        public List<GoodsDonation2> listgoodsDonations = new List<GoodsDonation2>();
        public List<Monetary_Donation2> listmonetaryDonations = new List<Monetary_Donation2>();
        public List<Disasters2> listdisasters = new List<Disasters2>();
        public void OnGet()
        {
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
                                Monetary_Donation2 monetaryDonations = new Monetary_Donation2();
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
                    string sql = "SELECT * FROM GoodsDonations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsDonation2 goodsDonations = new GoodsDonation2();
                                goodsDonations.Id = reader.GetInt32(0);
                                goodsDonations.DonationDate = reader.GetDateTime(1);
                                goodsDonations.NumberOfItems = reader.GetInt32(2);
                                goodsDonations.Category = reader.GetString(3);
                                goodsDonations.Description = reader.GetString(4);

                                listgoodsDonations.Add(goodsDonations);
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
                    string sql = "SELECT * FROM Disasters";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Disasters2 disasters = new Disasters2();
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
        }
    }
}

public class Disasters2
{
    public int Id;
    public DateTime startDate;
    public DateTime endDate;
    public string? location;
    public string? description;
    public string? requiredAidTypes;
}

public class GoodsDonation2
{
    public int Id;
    public DateTime DonationDate;
    public string? Category;
    public int NumberOfItems;
    public string? Description;
    public bool IsAnonymous;
}

public class Monetary_Donation2
{
    public int Id;
    public DateTime DonationDate2;
    public int Amount;
    public bool IsAnonymous;
}
