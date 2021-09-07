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

        public static List<ModelVariantJsonContent> GetVariantJson()
        {
            ConnectionToDB sqlClient = new();
            string query = "select productId, modelName, nickname, modelYears from Models as m join ModelYears as my on my.modelId = m.id";

            List<ModelVariantJsonContent> jsonContent = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ModelVariantJsonContent row = new();

                row.ProductId = reader[0].ToString().Trim();
                row.ModelName = reader[1].ToString().Trim().Replace("+", "'");
                row.Nickname = reader[2].ToString().Trim().Replace("+", "'");
                row.ModelYear = reader[3].ToString().Trim();

                jsonContent.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

            return jsonContent;
        }

        public static void InsertUnparsedVariant(List<string> data)
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.Count; i++)
            {
                string markedJson = data[i].Replace("'","+");
                query = $"INSERT INTO [UnparsedModelVariant] (json) values (N'{markedJson}')";

                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }

        public static List<ModelVariantJsonContent> GetRestart()
        {
            ConnectionToDB sqlClient = new();
            string lastId = "0";
            string queryLastId = "select count(id) from UnparsedModelVariant";
            

            SqlCommand command = new(queryLastId, sqlClient.sqlConnection);

            SqlDataReader readerOfLastIndex = command.ExecuteReader();

            while (readerOfLastIndex.Read())
            {
                lastId = readerOfLastIndex[0].ToString().Trim();
            }
            readerOfLastIndex.Close();

            List<ModelVariantJsonContent> jsonContent = new();

            string query = "select productId, modelName, nickname, modelYears from Models as m join ModelYears as my on my.modelId = m.id where my.id > " + lastId;

            command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ModelVariantJsonContent row = new();

                row.ProductId = reader[0].ToString().Trim();
                row.ModelName = reader[1].ToString().Trim().Replace("+", "'");
                row.Nickname = reader[2].ToString().Trim().Replace("+", "'");
                row.ModelYear = reader[3].ToString().Trim();

                jsonContent.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

            return jsonContent;
        }

        public static List<VariantsUnparsed> GetVariantsUnparseds()
        {
            ConnectionToDB sqlClient = new();
            string query = "select id, json from UnparsedModelVariant ";

            List<VariantsUnparsed> jsonCollection = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                VariantsUnparsed row = new();

                row.YearsId = reader[0].ToString().Trim();
                row.Json = reader[1].ToString().Trim();

                jsonCollection.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

            return jsonCollection;
        }

        public static void InsertVariant(ModelsVariantData data) 
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.ModelTypeCode.Count; i++)
            {
                query = $"INSERT INTO [Variants] (modelTypeCode, productNo, colorType, colorName, prodCategory, prodPictureNo, prodPictureFileURL, YearsId) values (N'{data.ModelTypeCode[i]}', N'{data.ProductNo[i]}', N'{data.ColorType[i]}', N'{data.ColorName[i]}', N'{data.ProdCategory[i]}', N'{data.ProdPictureNo[i]}', N'{data.ProdPictureFileURL[i]}', {data.YearsId[i]})";

                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }

        public static List<GetPositionJson> GetPositionJson()
        {
            ConnectionToDB sqlClient = new();
            string query = "select m.productId,v.modelTypeCode, my.modelYears, v.productNo, v.colorType, m.modelName, v.prodCategory, v.Id from Variants as v join ModelYears as my on v.YearsId = my.Id join Models as m on my.modelId = m.Id";

            List<GetPositionJson> jsonContent = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GetPositionJson row = new();

                row.ProductId = reader[0].ToString().Trim();
                row.ModelTypeCode = reader[1].ToString().Trim();
                row.ModelYear = reader[2].ToString().Trim();
                row.ProductNo = reader[3].ToString().Trim();
                row.ColorType = reader[4].ToString().Trim();
                row.ModelName = reader[5].ToString().Trim().Replace("+", "'");
                row.ProdCategory = reader[6].ToString().Trim();
                row.VariantId = reader[7].ToString().Trim();

                jsonContent.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

            return jsonContent;
        }

        public static void InsertCataloge(Cataloge data)
        {
            ConnectionToDB sqlClient = new();
            string query;

            for (int i = 0; i < data.FigName.Count; i++)
            {
                query = $"INSERT INTO [Cataloge] (figName, figNo, illustNo, figBranchNo, illustFileURL, VariantId, catalogNo) values (N'{data.FigName[i]}', N'{data.FigNo[i]}', N'{data.IllustNo[i]}', N'{data.FigBranchNo[i]}', N'{data.IllustFileURL[i]}', {data.VariantId[i]}, N'{data.CatalogNo[i]}')";
                
                SqlCommand command = new(query, sqlClient.sqlConnection);

                command.ExecuteNonQuery();
            }

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");
        }

        public static List<GetPositionJson> GetRestartCataloge()
        {
            ConnectionToDB sqlClient = new();
            string lastId = "0";
            string queryLastId = "SELECT TOP 1 VariantId FROM Cataloge ORDER BY Id DESC ";


            SqlCommand command = new(queryLastId, sqlClient.sqlConnection);

            SqlDataReader readerOfLastIndex = command.ExecuteReader();

            while (readerOfLastIndex.Read())
            {
                lastId = readerOfLastIndex[0].ToString().Trim();
            }
            readerOfLastIndex.Close();

            string query = $"select m.productId,v.modelTypeCode, my.modelYears, v.productNo, v.colorType, m.modelName, v.prodCategory, v.Id from Variants as v join ModelYears as my on v.YearsId = my.Id join Models as m on my.modelId = m.Id where v.id > {lastId}";

            List<GetPositionJson> jsonContent = new();

            command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GetPositionJson row = new();

                row.ProductId = reader[0].ToString().Trim();
                row.ModelTypeCode = reader[1].ToString().Trim();
                row.ModelYear = reader[2].ToString().Trim();
                row.ProductNo = reader[3].ToString().Trim();
                row.ColorType = reader[4].ToString().Trim();
                row.ModelName = reader[5].ToString().Trim().Replace("+", "'");
                row.ProdCategory = reader[6].ToString().Trim();
                row.VariantId = reader[7].ToString().Trim();

                jsonContent.Add(row);
            }
            reader.Close();

            sqlClient.sqlConnection.Close();
            if (sqlClient.sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");

            return jsonContent;
        }
    }
}
