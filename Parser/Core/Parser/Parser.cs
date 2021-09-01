using Parser.Core.Parser;
using Parser.Core.SqlConnection;
using System;
using System.Collections.Generic;

namespace Parser
{
    class Parser
    {
        private static string JsonStringCleaner(string json)
        {
            string result = json.Replace(":[{", "")
                .Replace("}],", "")
                .Replace("\",\"", " , ")
                .Replace("\":\"", " : ")
                .Replace("},{", " , ")
                .Replace("\"", "")
                .Replace("'", "+") //Заменяет одну чару содердимую в параметре для JSON, поскольку она влияет на синтаксис шарпа, для запроса в SQL. Вернуть её на сборке поста.
                .Replace("}]}", "")
                .Trim();

            return result;
        }
        public static DispacementData ParseDisplacement(string json)
        {
            DispacementData ParsedData = new();
            List<string> productId = new();
            List<string> displacementType = new();
            List<string> displacement = new();

            string[] displacementFormatedData;

            string displacementData = json[json.IndexOf("\"displacementDataCollection\"")..json.IndexOf("\"productDataCollection\"")]
                .Replace("\"modelNameDataCollection\":null,\"modelYearDataCollection\":null,", "")
                .Replace("\"displacementDataCollection\"", "");

            displacementData = JsonStringCleaner(displacementData);

            displacementFormatedData = displacementData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < displacementFormatedData.Length; i++)
            {
                string row = displacementFormatedData[i].Trim();

                if (row.Length == 0)
                    continue;

                string[] pair = row.Split(new char[] { ':' }, 2);
                if (row.Contains("productId"))
                    productId.Add(pair[1]);


                if (row.Contains("displacementType"))
                    displacementType.Add(pair[1]);
                else

                if (row.Contains("displacement "))
                    displacement.Add(pair[1]);
            }

            ParsedData.ProductId = productId;
            ParsedData.DisplacementType = displacementType;
            ParsedData.Displacement = displacement;

            return ParsedData;
        }

        public static Categories ParseCategories(string json)
        {
            Categories parsedCategories = new();
            parsedCategories.ProductName = new();
            parsedCategories.ProductId = new();

            string[] categoriesFromatedData;
            string categoriesData = json[json.IndexOf("\"productDataCollection\"")..json.IndexOf("\"menuData\"")]
                .Replace("\"productDataCollection\"", "\"menuData\"")
                .Replace("\"menuData\"", "");

            categoriesData = JsonStringCleaner(categoriesData);

            categoriesFromatedData = categoriesData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < categoriesFromatedData.Length; i++)
            {
                string row = categoriesFromatedData[i].Trim();

                if (row.Length == 0)
                    continue;

                string[] pair = row.Split(new char[] { ':' }, 2);

                if (row.Contains("productIdName"))
                    parsedCategories.ProductName.Add(pair[1]);
                else if (row.Contains("productId"))
                    parsedCategories.ProductId.Add(pair[1]);
            }

            return parsedCategories;
        }

        public static ModelCollection ParseModelsList(List<string> listOfModel)
        {
            ModelCollection parsedModelList = new();
            parsedModelList.ProductId = new();
            parsedModelList.ModelName = new();
            parsedModelList.DispModelName = new();
            parsedModelList.Nickname = new();

            foreach (var json in listOfModel)
            {
                string[] modelFromatedData;
                string modelData = json.Replace("\"modelNameDataCollection\"", "");

                modelData = JsonStringCleaner(modelData);

                modelFromatedData = modelData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < modelFromatedData.Length; i++)
                {
                    string row = modelFromatedData[i].Trim();

                    if (row.Length == 0)
                        continue;

                    string[] pair = row.Split(new char[] { ':' }, 2);

                    if (row.Contains("productId"))
                        parsedModelList.ProductId.Add(pair[1]);

                    if (row.Contains("modelName"))
                        parsedModelList.ModelName.Add(pair[1]);

                    if (row.Contains("dispModelName"))
                        parsedModelList.DispModelName.Add(pair[1]);

                    if (row.Contains("nickname"))
                        parsedModelList.Nickname.Add(pair[1]);
                }
            }
            return parsedModelList;
        }

        public static YearsModelCollection ParseYears(List<YearsUnparsedContent> dataToParse) 
        {
            YearsModelCollection parsedData = new();
            parsedData.ModelId = new();
            parsedData.ModelYear = new();

            List<string> jsonCollection = new();
            List<string> modelId = new();

            foreach (var item in dataToParse)
            {
                string[] modelFromatedData;
                string modelData = item.Json.Replace("\"modelYearDataCollection\"","");

                modelData = JsonStringCleaner(modelData);
                modelFromatedData = modelData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < modelFromatedData.Length; i++)
                {
                    string row = modelFromatedData[i].Trim();

                    if (row.Length == 0)
                        continue;

                    string[] pair = row.Split(new char[] { ':' }, 2);

                    if (row.Contains("modelYear"))
                    {
                        parsedData.ModelYear.Add(pair[1]);
                        parsedData.ModelId.Add(item.ModelId);
                    }
                }
            }
            return parsedData;
        }
    }
}