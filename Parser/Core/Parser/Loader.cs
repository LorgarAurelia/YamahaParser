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

        public static async Task<string> GetModelNameList()
        {
            var client = PostClient.Create();


            HttpContent content = new StringContent("{\"productId\":\"10\",\"displacementType\":\"1\",\"baseCode\":\"7306\",\"langId\":\"92\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/ ", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
        }

        public static async Task<string> GetModelYearsList()
        {
            var client = PostClient.Create();

            HttpContent content = new StringContent("{\"productId\":\"10\",\"modelName\":\"YQ50\",\"nickname\":\"AEROX\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}", Encoding.UTF8, "application/json");
            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", content);

            var jsonInString = await answer.Content.ReadAsStringAsync();
            return jsonInString;
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
