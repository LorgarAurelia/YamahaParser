﻿using System;
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
                .Replace("\"", "");

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
    }
}