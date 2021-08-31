using Parser.Core.SqlConnection;
using Parser.Core.Parser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Loader
    {
        
        public static async Task<string> GetCategoriesJson()
        {
            var client = PostClient.Create();


            HttpContent content = new StringContent("{\"baseCode\":\"7306\",\"langId\":\"92\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<List<string>> GetModelNameList(List<ModelJsonContent> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            foreach (var item in jsonParamsCollection)
            {
                int randomTimeout = Randomizer.RandomInt(1000,3000);
                var client = PostClient.Create();
                string json = "{\"productId\":\"" + item.ProductId + "\",\"displacementType\":\"" + item.DisplacementType + "\",\"baseCode\":\"7306\",\"langId\":\"92\"}";

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/ ", content);

                var jsonInString = await answer.Content.ReadAsStringAsync();
                jsonCollection.Add(jsonInString);

                System.Threading.Thread.Sleep(randomTimeout);//Трэдслип нужен, чтобы сервер не рубил соединение
            }
            
            return jsonCollection;
        }

        public static async Task<List<string>> GetModelYearsList(List<YearsJsonContent> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            foreach (var item in jsonParamsCollection)
            {
                int randomTimeout = Randomizer.RandomInt(1000,3000);
                var client = PostClient.Create();

                string json = "{\"productId\":\"" + item.ProductId + "\",\"modelName\":\"" + item.ModelName + "\",\"nickname\":\"" + item.Nickname + "\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}";

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", content);

                var jsonInString = await answer.Content.ReadAsStringAsync();
                jsonCollection.Add(jsonInString);

                System.Threading.Thread.Sleep(randomTimeout);
            }
            
            return jsonCollection;
        }

        public static async Task<string> GetModelVariant()
        {
            var client = PostClient.Create();

            HttpContent content = new StringContent("{\"productId\":\"10\",\"calledCode\":\"1\",\"modelName\":\"YQ50\",\"nickname\":\"AEROX\",\"modelYear\":\"2012\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetSetsPositions()
        {
            var client = PostClient.Create();

            HttpContent content = new StringContent("{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"1BX7\",\"modelYear\":\"2012\",\"productNo\":\"010\",\"colorType\":\"A\",\"modelName\":\"YQ50\",\"prodCategory\":\"11\",\"calledCode\":\"1\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetSetParts()
        {
            var client = PostClient.Create();

            HttpContent content = new StringContent("{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"1BX7\",\"modelYear\":\"2012\",\"productNo\":\"010\",\"colorType\":\"A\",\"modelName\":\"YQ50\",\"vinNoSearch\":\"false\",\"figNo\":\"01\",\"figBranchNo\":\"1\",\"catalogNo\":\"1L1BX300E1\",\"illustNo\":\"1BX1300 - J010\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"domOvsId\":\"2\",\"greyModelSign\":false,\"cataPBaseCode\":\"7451\",\"currencyCode\":\"GBP\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_text/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }
    }
}