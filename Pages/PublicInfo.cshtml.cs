using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class PublicInfoModel : PageModel
    {
        public int TotalAmount { get; set; }
        public int TotalGoodsReceived { get; set; }
        public List<DisasterGoodsViewModel> DisasterGoodsList { get; set; }
        public List<DisasterMonetaryViewModel> DisasterMonetaryList { get; set; }
        public List<Disasters5> listdisasters = new List<Disasters5>();

        public void OnGet()
        {
            
            //total money received
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

            //total goods received
            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query the total number of goods received
                    string goodsCountSql = "SELECT SUM(NumberOfItems) FROM GoodsDonations";
                    using (SqlCommand goodsCountCommand = new SqlCommand(goodsCountSql, connection))
                    {
                        TotalGoodsReceived = (int)goodsCountCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            DisasterGoodsList = GetDisasterGoodsList();
            DisasterMonetaryList = GetDisasterMonetaryList();

            //active disasters
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
                                Disasters5 disasters = new Disasters5();
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

        private List<DisasterGoodsViewModel> GetDisasterGoodsList()
        {
            List<DisasterGoodsViewModel> result = new List<DisasterGoodsViewModel>();

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT D.description, D.location, G.Description AS GoodsDescription " +
                                 "FROM Disasters D " +
                                 "JOIN DisasterGoodsAllocations DA ON D.Id = DA.DisasterDescription " +
                                 "JOIN GoodsDonations G ON G.Id = DA.GoodsDescription;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisasterGoodsViewModel item = new DisasterGoodsViewModel();
                                item.DisasterDescription = $"{reader.GetString(0)}, {reader.GetString(1)}";

                                
                                if (reader.IsDBNull(2))
                                {
                                    item.GoodsDescription = "N/A";
                                }
                                else
                                {
                                    item.GoodsDescription = reader.GetString(2);
                                }

                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return result;
        }

       private List<DisasterMonetaryViewModel> GetDisasterMonetaryList()
        {
            List<DisasterMonetaryViewModel> result = new List<DisasterMonetaryViewModel>();

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT D.description, D.location, M.Amount AS MonetaryDescription " +
                                 "FROM Disasters D " +
                                 "JOIN DisasterMonetaryAllocations DA ON D.Id = DA.DisasterDescription " +
                                 "JOIN MonetaryDonations M ON M.Amount = DA.MonetaryDescription;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisasterMonetaryViewModel item1 = new DisasterMonetaryViewModel();
                                item1.DisasterDescription = $"{reader.GetString(0)}, {reader.GetString(1)}";


                                if (reader.IsDBNull(2))
                                {
                                    item1.MonetaryDescription = "N/A";
                                }
                                else
                                {
                                    item1.MonetaryDescription = reader.GetInt32(2).ToString();
                                }

                                result.Add(item1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

    }


    public class Disasters5
    {
        public int Id;
        public DateTime startDate;
        public DateTime endDate;
        public string? location;
        public string? description;
        public string? requiredAidTypes;
    }

    public class DisasterGoodsViewModel
    {
        public string DisasterDescription { get; set; }
        public string GoodsDescription { get; set; }
    }

    public class DisasterMonetaryViewModel
    {
        public string DisasterDescription { get; set; }
        public string MonetaryDescription { get; set; }
    }
}

