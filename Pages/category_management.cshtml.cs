using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class admin_category_managementModel : PageModel
    {
        public List<Categories> listcategories = new List<Categories>();
        public Categories addCategory = new Categories();
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
                    string sql = "SELECT * FROM GoodsCategories";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categories categories = new Categories();
                                categories.Id = reader.GetInt32(0);
                                categories.CategoryName = reader.GetString(1);

                                listcategories.Add(categories);
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
            addCategory.CategoryName = Request.Form["CategoryName"];

            try
            {
                string connectionString = "Data Source=djpromo7.database.windows.net;Initial Catalog=DJPromoWebsite;User ID=admin1;Password=Waitdeep31";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO GoodsCategories " +
                                  "(CategoryName) VALUES " +
                                  "(@CategoryName);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", addCategory.CategoryName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            addCategory.CategoryName = "";

            successMsg = "Category successfully added.";
        }


    }
}

    public class Categories 
    {
        public int Id;
        public string? CategoryName;
    }

