using Microsoft.Data.SqlClient;

namespace API.Configuration
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }

        public DatabaseConfig()
        {
            DotNetEnv.Env.Load();

            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            string dbServer = configuration["DB_SERVER"] ?? "localhost";
            string dbPort = configuration["DB_PORT"] ?? "5434";
            string dbName = configuration["DB_NAME"] ?? "u_mandaditos";
            string dbUser = configuration["DB_USER"] ?? "u_mandaditos";
            string dbPassword = configuration["DB_PASSWORD"] ?? "u_mandaditos";

            ConnectionString = $"Server={dbServer},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
        }

        public bool TestConnection()
        {
            using var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
