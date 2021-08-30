using Parser.Core.SqlConnection;
using System.Collections.Generic;
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

                SqlCommand command = new(query, sqlClient.sqlConnection);

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

                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }
        }

        public static List<ModelJsonContent> GetJsonParams()
        {
            ConnectionToDB sqlClient = new();
            string query = "select dis.productId, DisplacementType from Displacement as dis join Categories as cat on cat.productId = dis.productId order by dis.productId";

            List<ModelJsonContent> content = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ModelJsonContent row = new();

                row.ProductId = reader[0].ToString();
                row.DisplacementType = reader[1].ToString();

                content.Add(row);
            }

            return content;
        }
    }
}
