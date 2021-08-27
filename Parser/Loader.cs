using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Loader
    {
        public static async Task<string> GetCategoriesJson() 
        {
            HttpClientHandler handler = new();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");


            HttpContent content = new StringContent("{\"baseCode\":\"7306\",\"langId\":\"92\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetModelNameList() 
        {
            HttpClientHandler handler = new();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");


            HttpContent content = new StringContent("{\"productId\":\"10\",\"displacementType\":\"1\",\"baseCode\":\"7306\",\"langId\":\"92\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetModelYearsList() 
        {
            HttpClientHandler handler = new();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient client = new(handler);

            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");


            HttpContent content = new StringContent("{\"productId\":\"10\",\"modelName\":\"YQ50\",\"nickname\":\"AEROX\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetModelVariant() 
        {
            HttpClientHandler handler = new();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient client = new(handler);

            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");


            HttpContent content = new StringContent("{\"productId\":\"10\",\"calledCode\":\"1\",\"modelName\":\"YQ50\",\"nickname\":\"AEROX\",\"modelYear\":\"2012\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetPartsPositions() 
        {
            HttpClientHandler handler = new();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient client = new(handler);

            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");


            HttpContent content = new StringContent("{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"1BX7\",\"modelYear\":\"2012\",\"productNo\":\"010\",\"colorType\":\"A\",\"modelName\":\"YQ50\",\"prodCategory\":\"11\",\"calledCode\":\"1\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }
    }
}
