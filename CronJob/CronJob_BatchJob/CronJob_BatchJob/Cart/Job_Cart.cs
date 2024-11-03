using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CronJob_BatchJob.Cart
{
    public class Job_Cart
    {
        private readonly IConfiguration _configuration;
        public Job_Cart(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task UpdateCartOrder()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectDB");
                using (SqlConnection SqlConnection = new SqlConnection(connectionString))
                {
                    await SqlConnection.OpenAsync();
                    var selectTop = _configuration["Value:selectTop"];
                    var statusSelect = _configuration["Value:statusSelect"];
                    using (SqlCommand cmd = new SqlCommand($"SELECT TOP ({selectTop}) * FROM Carts WHERE Status = {statusSelect} Order By TimeOrder", SqlConnection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var timeOrder = (DateTime)reader["TimeOrder"];
                                if (DateTime.Now > timeOrder.AddMinutes(10))
                                {
                                    var statusUpdate = _configuration["Value:statusUpdate"];
                                    using (SqlCommand updateCmd = new SqlCommand($"UPDATE Carts SET Status = {statusUpdate} WHERE CartID = @CartId", SqlConnection))
                                    {
                                        updateCmd.Parameters.AddWithValue("@CartId", reader["CartID"]);
                                        await updateCmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }
    }
}
