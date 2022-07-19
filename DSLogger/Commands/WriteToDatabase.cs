using System;
using System.Data.SqlClient;

namespace DSLogger.Commands
{
    class WriteToDatabase : IWriteFileCommand
    {
        public string TableName { get; private set; }

        LoggerDatabase db;
        SqlConnection connection;



        public WriteToDatabase(string tableNameIn, string textToWriteIn)
        {
            TableName = tableNameIn;
            db = new LoggerDatabase();
            TextToWrite = textToWriteIn;
            connection = db.Connection;
        }

        public string TextToWrite { get; set; }

        public bool CanExecute()
        {
            if (db.CheckConnection() && TableExists())
            {

                return true;
            }
            else
            {
                WriteBackup("Connection with the database could not be established.");
                return false;
            }

            
        }

        public void Execute()
        {

            string _sqlWrite = @$"INSERT INTO {TableName} VALUES(@date, @log);";
            db = new LoggerDatabase();
            connection = db.Connection;
            using (connection)
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(_sqlWrite, connection))
                    {
                        command.Parameters.AddWithValue("@date", DateTime.Now);
                        command.Parameters.AddWithValue("@log", TextToWrite);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    WriteBackup(e.Message);
                }

                connection.Close();
            }


            
        }

        public bool TableExists()
        {
            string _sqlTableExistsQuery = @$"SELECT TABLE_NAME FROM [{connection.Database}].INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';";

            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(_sqlTableExistsQuery, connection))
                {

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (TableName == reader.GetString(0))
                        {
                            reader.Close();
                            connection.Close();
                            return true;
                        }
                    }

                }

                connection.Close();
                return false;
            }
        }

        //Creates a backup file so data isnt lost if something goes wrong
        private void WriteBackup(string _errorMessage)
        {
            Logger log = new Logger("", $"{TableName}_Backup");

            log.WriteToLog(_errorMessage);
            log.WriteToLog(TextToWrite);
        }

    }
}
