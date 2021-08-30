using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Parser
{
    class SqlService : ConnectionToDB
    {
        public static void InsertDispacement(DispacementData data) 
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.DisplacementType.Count; i++)
            {
                query = $"INSERT INTO [Displacement] (productId, DisplacementType, Displacement) VALUES (N'{data.ProductId[i]}', N'{data.DisplacementType[i]}', N'{data.Displacement[i]}')";

                SqlCommand command = new(query,sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }
        }

        public static void InsertCategories(Categories data)
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.ProductId.Count; i++)
            {
                query = $"INSERT INTO [Categories] (productId, ProductName) values (N'{data.ProductId[i]}', N'{data.ProductName[i]}')";

                SqlCommand command = new(query,sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }
        }
    }
}
