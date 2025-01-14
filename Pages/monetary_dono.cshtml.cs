using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages.Shared
{
    public class monetary_donoModel : PageModel
    {
        public Monetary_Donation monetaryDonations = new Monetary_Donation();
        public string errorMsg = "";
        public string successMsg = "";


        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            monetaryDonations.DonationDate2 = DateTime.Parse(Request.Form["DonationDate2"]);
            monetaryDonations.Amount = int.Parse(Request.Form["Amount"]);


            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO MonetaryDonations " +
                                  "(DonationDate, Amount) VALUES " +
                                  "(@DonationDate2, @Amount);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DonationDate2", monetaryDonations.DonationDate2);
                        command.Parameters.AddWithValue("@Amount", monetaryDonations.Amount);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }
            
            monetaryDonations.Amount = 0;

            successMsg = "Donation successfully captured";


        }

    }
}

public class Monetary_Donation
{
    public int Id;
    public DateTime DonationDate2;
    public int Amount;
    public bool IsAnonymous;
}

