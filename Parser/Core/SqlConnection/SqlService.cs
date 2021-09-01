using Parser.Core.Parser;
using Parser.Core.SqlConnection;
using System;
using System.Collections.Generic;
using System.Data;
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

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
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

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
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
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

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

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }

        public static List<YearsJsonContent> GetModelJson()
        {
            ConnectionToDB sqlClient = new();
            string query = "select productId, modelName, nickname from Models order by productId";

            List<YearsJsonContent> jsonContent = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                YearsJsonContent row = new();

                row.ProductId = reader[0].ToString().Trim();
                row.ModelName = reader[1].ToString().Trim().Replace("+", "'");
                row.Nickname = reader[2].ToString().Trim().Replace("+", "'");

                jsonContent.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
            return jsonContent;
        }

        public static void InsertUnparsedData (List<UnparsedYearsData> data)
        {
            ConnectionToDB sqlClient = new();
            string query;

            foreach (var row in data)
            {
                query = $"insert into [YearsUnparsedJson] (json) values (N'{row.Json}')";
                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
                Console.WriteLine("Запрос отправлен \n");
            }

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }

        public static List<YearsUnparsedContent> GetUnparsedYearsJson()
        {
            ConnectionToDB sqlClient = new();
            string query = "select id, json from YearsUnparsedJson";

            List<YearsUnparsedContent> jsonCollection = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) 
            {
                YearsUnparsedContent row = new();

                row.ModelId = reader[0].ToString().Trim();
                row.Json = reader[1].ToString().Trim();

                jsonCollection.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

            return jsonCollection;
        }

        public static void InsertYears(YearsModelCollection data)
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.ModelYear.Count; i++)
            {
                query = $"INSERT INTO [ModelYears] (modelYears, modelId) values (N'{data.ModelYear[i]}', {data.ModelId[i]})";

                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }
    }
}
