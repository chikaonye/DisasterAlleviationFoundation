using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class goods_donoModel : PageModel
    {
        public GoodsDonation goodsDonations = new GoodsDonation();
        public string errorMsg = "";
        public string successMsg = "";

        
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            goodsDonations.DonationDate = DateTime.Parse(Request.Form["DonationDate"]);
            goodsDonations.Category = Request.Form["Category"];
            goodsDonations.NumberOfItems = int.Parse(Request.Form["NumberOfItems"]);
            goodsDonations.Description = Request.Form["Description"];


            
            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO GoodsDonations " +
                                  "(DonationDate, Category, NumberOfItems, Description) VALUES " +
                                  "(@DonationDate, @Category, @NumberOfItems, @Description);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DonationDate", goodsDonations.DonationDate);
                        command.Parameters.AddWithValue("@Category", goodsDonations.Category);
                        command.Parameters.AddWithValue("@NumberOfItems", goodsDonations.NumberOfItems);
                        command.Parameters.AddWithValue("@Description", goodsDonations.Description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

                     
                    goodsDonations.Category = "";
                    goodsDonations.NumberOfItems = 0;
                    goodsDonations.Description = "";

                    successMsg = "Donation successfully added.";
 
            }

        }
    }

    public class GoodsDonation
    {
    public int Id;
    public DateTime DonationDate;
    public string? Category;
    public int NumberOfItems;
    public string? Description;
    public bool IsAnonymous;
    }

