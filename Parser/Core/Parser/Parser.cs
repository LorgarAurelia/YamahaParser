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

            foreach (var item in dataToParse)
            {
                string[] modelFromatedData;
                string modelData = item.Json.Replace("\"modelYearDataCollection\"", "");

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

        public static ModelsVariantData ParseVariants(List<VariantsUnparsed> dataToParse)
        {
            ModelsVariantData parsedData = new();
            parsedData.ModelTypeCode = new();
            parsedData.ProductNo = new();
            parsedData.ColorType = new();
            parsedData.ColorName = new();
            parsedData.ProdCategory = new();
            parsedData.ProdPictureNo = new();
            parsedData.ProdPictureFileURL = new();
            parsedData.YearsId = new();

            foreach (var item in dataToParse)
            {
                string[] modelFromatedData;
                string modelData = item.Json.Replace("\"modelDataCollection\"", "");

                modelData = JsonStringCleaner(modelData);
                modelFromatedData = modelData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < modelFromatedData.Length; i++)
                {
                    string row = modelFromatedData[i].Trim();

                    if (row.Length == 0)
                        continue;

                    string[] pair = row.Split(new char[] { ':' }, 2);

                    if (row.Contains("modelTypeCode"))
                    {
                        parsedData.ModelTypeCode.Add(pair[1]);
                        parsedData.YearsId.Add(item.YearsId);
                    }

                    if (row.Contains("productNo"))
                        parsedData.ProductNo.Add(pair[1]);

                    if (row.Contains("colorType"))
                        parsedData.ColorType.Add(pair[1]);

                    if (row.Contains("colorName"))
                        parsedData.ColorName.Add(pair[1]);

                    if (row.Contains("prodCategory"))
                        parsedData.ProdCategory.Add(pair[1]);

                    if (row.Contains("prodPictureNo"))
                        parsedData.ProdPictureNo.Add(pair[1]);

                    if (row.Contains("prodPictureFileURL"))
                        parsedData.ProdPictureFileURL.Add(pair[1]);
                }
            }
            return parsedData;
        }

        public static void ParseCataloge(List<string> dataToParse, List<string> idCollection)
        {
            Cataloge parsedData = new();
            parsedData.FigName = new();
            parsedData.FigNo = new();
            parsedData.IllustNo = new();
            parsedData.FigBranchNo = new();
            parsedData.IllustFileURL = new();
            parsedData.VariantId = new();
            parsedData.CatalogNo = new();

            string catalogNumber = "error";

            for (int i = 0; i < dataToParse.Count; i++)
            {
                if (dataToParse[i].Contains("We are sorry for your inconvenience but the system is busy now.\nYour access again after a while is appreciated")) //добавить инъекцию в бд не пробитых моделей
                    continue;
                string[] modelFromatedData;
                string modelData = dataToParse[i][dataToParse[i].IndexOf("\"catalogNo\"")..dataToParse[i].IndexOf("\"catalogLangDataCollection\"")]
                    .Replace("\"figDataCollection\"", "")
                    .Replace("catalogLangDataCollection", "");

                modelData = JsonStringCleaner(modelData);

                modelFromatedData = modelData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int Counter = 0; Counter < modelFromatedData.Length; Counter++)
                {
                    string row = modelFromatedData[Counter].Trim();

                    if (row.Length == 0)
                        continue;

                    string[] pair = row.Split(new char[] { ':' }, 2);

                    if (row.Contains("catalogNo"))
                        catalogNumber = pair[1];

                    if (row.Contains("figName"))
                    {
                        parsedData.FigName.Add(pair[1]);
                        parsedData.VariantId.Add(idCollection[i]);
                        parsedData.CatalogNo.Add(catalogNumber);
                    }

                    if (row.Contains("figNo"))
                        parsedData.FigNo.Add(pair[1]);

                    if (row.Contains("illustNo"))
                        parsedData.IllustNo.Add(pair[1]);

                    if (row.Contains("figBranchNo"))
                        parsedData.FigBranchNo.Add(pair[1]);

                    if (row.Contains("illustFileURL"))
                        parsedData.IllustFileURL.Add(pair[1]);
                }

            }
            SqlService.InsertCataloge(parsedData);
        }

        public static void ParseParts (List<string> dataToParse, List<string> idCollection)
        {
            PartsData parsedData = new();
            parsedData.RefNo = new();
            parsedData.CatalogId = new();
            parsedData.PartNo = new();
            parsedData.PartName = new();
            parsedData.Quantity = new();
            parsedData.Remarks = new();

            for (int i = 0; i < dataToParse.Count; i++)
            {
                if (dataToParse[i].Contains("We are sorry for your inconvenience but the system is busy now.\nYour access again after a while is appreciated")) //добавить инъекцию в бд не пробитых моделей
                    continue;
                string[] modelFromatedData;
                string modelData = dataToParse[i][dataToParse[i].IndexOf("\"partsDataCollection\"")..dataToParse[i].IndexOf("\"hotspotoDataCollection\"")]
                    .Replace("\"partsDataCollection\"", "")
                    .Replace("notesDataCollection", "")
                    .Replace("hotspotoDataCollection", "");

                modelData = JsonStringCleaner(modelData);

                modelFromatedData = modelData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int Counter = 0; Counter < modelFromatedData.Length; Counter++)
                {
                    string row = modelFromatedData[Counter].Trim();

                    if (row.Length == 0)
                        continue;

                    string[] pair = row.Split(new char[] { ':' }, 2);

                    if (row.Contains("partNo"))
                    {
                        parsedData.PartNo.Add(pair[1]);
                        parsedData.CatalogId.Add(idCollection[i]);
                    }

                    if (row.Contains("refNo"))
                        parsedData.RefNo.Add(pair[1]);

                    if (row.Contains("partName"))
                        parsedData.PartName.Add(pair[1]);

                    if (row.Contains("quantity"))
                        parsedData.Quantity.Add(pair[1]);

                    if (row.Contains("remarks"))
                        parsedData.Remarks.Add(pair[1]);
                }

            }
            SqlService.InsertParts(parsedData);
        }
    }
}