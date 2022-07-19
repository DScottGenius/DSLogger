using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DSLogger
{
    class LoggerDatabase
    {

        public LoggerDatabase()
        {
            ConnectionString = "";
            ConnectionString = GetConnectionStringFromSettings(ConnectionString);
            CreateConnection();
        }

        public LoggerDatabase(string connectionString)
        {
            ConnectionString = connectionString;
            CreateConnection();
        }

        private string GetConnectionStringFromSettings(string _connectionStringIn)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

            _connectionStringIn = config.GetConnectionString("mainDB");

            return _connectionStringIn;
        }

        public void CreateConnection()
        {
            Connection = new SqlConnection(ConnectionString);
        }
        public bool CheckConnection()
        {
            bool canConnect = false;
            try
            {
                Connection.Open();
                canConnect = true;
                Connection.Close();
            }
            catch (System.Exception e)
            {
                Logger log = new Logger("", nameof(Logger));
                log.WriteToLog(e.Message);
                Connection.Close();
                canConnect = false;
                throw;
            }
            return canConnect;
        }
        public string ConnectionString { get; private set; }

        public SqlConnection Connection { get; private set; }


    }
}
