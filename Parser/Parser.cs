using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Parser
    {
        public static string JsonStringCleaner(string json) 
        {
            string result = json.Replace(":[{", "")
                .Replace("}],", "")
                .Replace("\",\"", " , ")
                .Replace("\":\""," : ")
                .Replace("},{", " , ")
                .Replace("\"", "");

            return result;
        }
        public static void ParseCategories(string json) 
        {
            DispacementData ParsedData = new();
            List<string> productId = new();
            List<string> displacementType = new();
            List<string> displacement = new();

            string[] displacementParsedData;

            string displacementData = json[json.IndexOf("\"displacementDataCollection\"").. json.IndexOf("\"productDataCollection\"")]
                .Replace("\"modelNameDataCollection\":null,\"modelYearDataCollection\":null,", "")
                .Replace("\"displacementDataCollection\"", "");

            displacementData = JsonStringCleaner(displacementData);

            displacementParsedData = displacementData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < displacementParsedData.Length; i++)
            {
                string row = displacementParsedData[i].Trim();

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

            for (int i = 0; i < ParsedData.ProductId.Count; i++)
            {
                Console.WriteLine($"ProductId: {ParsedData.ProductId[i]} Displacement Type: {ParsedData.DisplacementType[i]} Displacement: {ParsedData.Displacement[i]}");
            }
            

        }
    }
}
/*string[] pair = row.Split(new char[] { ':' }, 2);
                    Console.WriteLine("key: " + pair[0] + ", value: " + pair[1]);*/