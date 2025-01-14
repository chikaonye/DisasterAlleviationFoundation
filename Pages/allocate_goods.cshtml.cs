using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class allocate_goodsModel : PageModel
    {
        public int TotalAmount { get; set; }
        public List<Disasters4> listdisasters = new List<Disasters4>();
        public List<GoodsDonation3> listgoodsDonations = new List<GoodsDonation3>();

        public DisasterGoodsAllocation disasterGoodsAllocation = new DisasterGoodsAllocation();
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
                                Disasters4 disasters = new Disasters4();
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
                    string sql = "SELECT * FROM GoodsDonations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsDonation3 goodsDonations = new GoodsDonation3();
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
            disasterGoodsAllocation.Id = Guid.NewGuid().ToString();
            disasterGoodsAllocation.GoodsDescription = Request.Form["goods"];
            disasterGoodsAllocation.DisasterDescription = Request.Form["disaster"];

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO DisasterGoodsAllocations " +
                                  "(Id, DisasterDescription, GoodsDescription) VALUES " +
                                  "(@Id, @DisasterDescription, @GoodsDescription);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", disasterGoodsAllocation.Id);
                        command.Parameters.AddWithValue("@DisasterDescription", disasterGoodsAllocation.DisasterDescription);
                        command.Parameters.AddWithValue("@GoodsDescription", disasterGoodsAllocation.GoodsDescription);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            successMsg = "Goods successfully allocated.";
        }


    }
}

public class Disasters4
{
    public int Id;
    public DateTime startDate;
    public DateTime endDate;
    public string? location;
    public string? description;
    public string? requiredAidTypes;
}

public class GoodsDonation3
{
    public int Id;
    public DateTime DonationDate;
    public string? Category;
    public int NumberOfItems;
    public string? Description;
    public bool IsAnonymous;
}

public class DisasterGoodsAllocation
{
    public string? Id { get; set; }
    public string? DisasterDescription { get; set; }
    public string? GoodsDescription { get; set; }
}

