using System;
using System.Data;
using System.Data.SqlClient;


namespace Parser
{
    class ConnectionToDB
    {
        private readonly string sqlParams = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Proger1\source\repos\YamahaParser\Parser\Yamaha.mdf;User ID=UserAdmin;Password=Lorgar17";

        public SqlConnection sqlConnection;

        public ConnectionToDB()
        {
            sqlConnection = new SqlConnection(sqlParams);
            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
                Console.WriteLine("Connection opened.");
        }
        ~ConnectionToDB()
        {
            sqlConnection.Close();
            if (sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }
    }
}
