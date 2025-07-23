using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;

namespace LegacyBankAppDemo.Controllers
{
    public class AccountController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public string Get(int id)
        {
            string result = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM Accounts WHERE AccountId = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = $"Account: {reader["AccountId"]}, Balance: {reader["Balance"]}";
                }
            }
            return result;
        }

        public string Put(int id, [FromBody] string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"UPDATE Accounts SET Email = '{email}' WHERE AccountId = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            return "Account updated.";
        }
    }
}