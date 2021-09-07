using Parser.Core.Parser;
using Parser.Core.SqlConnection;
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
                int randomTimeout = Randomizer.RandomInt(1000, 3000);
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

        public static async void GetModelYearsList(List<YearsJsonContent> jsonParamsCollection)
        {
            List<UnparsedYearsData> jsonCollection = new();
            int toPauseCounter = 1;
            int showCaseCounter = 1;
            
            foreach (var item in jsonParamsCollection)
            {
                UnparsedYearsData row = new();

                int randomTimeout = Randomizer.RandomInt(1000, 3000);
                int randomBigTimeout = Randomizer.RandomInt(9000,12000);

                var client = PostClient.Create();

                string json = "{\"productId\":\"" + item.ProductId + "\",\"modelName\":\"" + item.ModelName + "\",\"nickname\":\"" + item.Nickname + "\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}";

                row.ModelName = item.ModelName;

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                try
                {
                    var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", content);

                    row.Json = await answer.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(randomTimeout);

                    var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", content);

                    row.Json = await answer.Content.ReadAsStringAsync();
                }
                
                Console.WriteLine(row.Json + "************************************\n" + "Curent status: No of curent post = " + showCaseCounter + " Etaration before pause left = " + (10 - toPauseCounter) + "************************************\n");

                jsonCollection.Add(row);

                System.Threading.Thread.Sleep(randomTimeout);
                

                if ( showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 10 )
                {
                    System.Threading.Thread.Sleep(randomBigTimeout);
                    toPauseCounter = 1;
                    SqlService.InsertUnparsedData(jsonCollection);
                    jsonCollection = new();
                }
                showCaseCounter++;
                toPauseCounter++;
            }
        }

        public static async void GetModelVariant(List<ModelVariantJsonContent> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            int toPauseCounter = 1;
            int showCaseCounter = 1;

            try //перевести блок try catch внутрь *
            {
                foreach (var item in jsonParamsCollection)
                {

                    int randomTimeout = Randomizer.RandomInt(1000, 3000);
                    int randomBigTimeout = Randomizer.RandomInt(9000, 12000);

                    var client = PostClient.Create();

                    string json = "{\"productId\":\"" + item.ProductId + "\",\"calledCode\":\"1\",\"modelName\":\"" + item.ModelName + "\",\"nickname\":\"" + item.Nickname + "\",\"modelYear\":\"" + item.ModelYear + "\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}";

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    try
                    {
                        var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/", content);

                        jsonCollection.Add(await answer.Content.ReadAsStringAsync());
                    }
                    catch (Exception)
                    {
                        System.Threading.Thread.Sleep(randomTimeout);

                        var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/", content);

                        jsonCollection.Add(await answer.Content.ReadAsStringAsync());//*
                    }

                    Console.WriteLine("\n *************************************************************** \n Curent status: No of curent post = " + showCaseCounter + " Etaration before pause left = " + (10 - toPauseCounter) + "\n *************************************************************** \n");

                    System.Threading.Thread.Sleep(randomTimeout);

                    if (showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 10)
                    {
                        System.Threading.Thread.Sleep(randomBigTimeout);
                        toPauseCounter = 1;
                        SqlService.InsertUnparsedVariant(jsonCollection);
                        jsonCollection = new();
                    }
                    showCaseCounter++;
                    toPauseCounter++;
                }
            }
            catch (Exception)
            {
                var restartOptions = SqlService.GetRestart();
                GetModelVariant(restartOptions);
            }

            
            
        }

        public static async void GetSetsPositions(List<GetPositionJson> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            List<string> idCollection = new();
            int toPauseCounter = 1;
            int showCaseCounter = 1;
                foreach (var item in jsonParamsCollection)
                {
                    int randomTimeout = Randomizer.RandomInt(1000, 3000);
                    int randomBigTimeout = Randomizer.RandomInt(9000, 12000);

                    var client = PostClient.Create();

                    string json = "{\"productId\":\""+ item.ProductId + "\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"" + item.ModelTypeCode + "\",\"modelYear\":\"" + item.ModelYear + "\",\"productNo\":\"" + item.ProductNo + "\",\"colorType\":\"" + item.ColorType + "\",\"modelName\":\"" + item.ModelName + "\",\"prodCategory\":\"" + item.ProdCategory + "\",\"calledCode\":\"1\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}";
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/", content);

                        jsonCollection.Add(await answer.Content.ReadAsStringAsync());
                    }
                    catch (Exception)
                    {
                        System.Threading.Thread.Sleep(randomTimeout);

                        try
                        {
                            var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/", content);

                            jsonCollection.Add(await answer.Content.ReadAsStringAsync());
                        }
                        catch (Exception)
                        {
                            var restartoptions = SqlService.GetRestartCataloge();
                            GetSetsPositions(restartoptions);
                        }

                        
                    }
                    idCollection.Add(item.VariantId);

                    Console.WriteLine("\n ************************************************************************************** \n Curent status: No of curent post = " + showCaseCounter + " Etaration before pause left = " + (10 - toPauseCounter) + "\n ************************************************************************************** \n");

                    System.Threading.Thread.Sleep(randomTimeout);

                    if (showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 10)
                    {
                        System.Threading.Thread.Sleep(randomBigTimeout);
                        toPauseCounter = 1;
                        Parser.ParseCataloge(jsonCollection,idCollection);
                        jsonCollection = new();
                        idCollection = new();
                    }
                    showCaseCounter++;
                    toPauseCounter++;
                }
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