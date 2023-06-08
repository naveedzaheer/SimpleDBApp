using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SimpleDBApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IConfiguration _configuration;

        public int RowCount { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DB_CONNECTION");
                Console.WriteLine(connectionString);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text; // Sets the command type to use
                    cmd.CommandText = "select count(*) from dbo.Persons";
                    cmd.Connection = conn;

                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        object objCount = cmd.ExecuteScalar();
                        int iCount = (int)objCount;
                        RowCount = iCount;
                        Console.WriteLine($"Total Rows {iCount}");
                    }
                }
            }
            catch (Exception ex)    
            {
                Console.Write(ex.Message);
            }
            finally
            {

            }
        }
    }
}