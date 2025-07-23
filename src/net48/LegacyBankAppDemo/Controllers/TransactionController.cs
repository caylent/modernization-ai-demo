using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;

namespace LegacyBankAppDemo.Controllers
{
    public class TransactionController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public string Get(int accountId)
        {
            string result = "Transaction History:\n";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM Transactions WHERE FromAccount = '{accountId}' OR ToAccount = '{accountId}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result += $"From: {reader["FromAccount"]}, To: {reader["ToAccount"]}, Amount: {reader["Amount"]}\n";
                }
            }
            return result;
        }

        public string Post([FromBody] string from, [FromBody] string to, [FromBody] decimal amount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"INSERT INTO Transactions (FromAccount, ToAccount, Amount) VALUES ('{from}', '{to}', {amount})";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            return "Transfer completed.";
        }
    }
}