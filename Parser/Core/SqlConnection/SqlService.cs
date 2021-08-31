using Parser.Core.Parser;
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

                row.ProductId = reader[0].ToString().Trim();
                row.DisplacementType = reader[1].ToString().Trim();

                content.Add(row);
            }

            return content;
        }

        public static void InsertModelList(ModelCollection data)
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.ProductId.Count; i++)
            {
                query = $"INSERT INTO [Models] (productId, modelName, dispModelName, nickname) values (N'{data.ProductId[i]}', N'{data.ModelName[i]}', N'{data.DispModelName[i]}', N'{data.Nickname[i]}')";

                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }
        }

        
    }
}
