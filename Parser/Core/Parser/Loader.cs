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
        public static async Task<string> JsonLoader(string uri, string postJson, int randomTimeout)
        {
            string jsonOut = string.Empty;
            var client = PostClient.Create();

            HttpContent content = new StringContent(postJson, Encoding.UTF8, "application/json");

            try
            {
                var answer = await client.PostAsync(uri, content);

                jsonOut = await answer.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(randomTimeout);

                try
                {
                    var answer = await client.PostAsync(uri, content);

                    jsonOut = await answer.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    if (uri.Contains("catalog_text"))
                    {
                        var restartoptions = SqlService.GetPartsJson();
                        GetSetParts(restartoptions);
                    }

                    if (uri.Contains("catalog_index"))
                    {
                        var restartoptions = SqlService.GetRestartCataloge();
                        GetSetsPositions(restartoptions);
                    }

                    if (uri.Contains("model_list"))
                    {
                        var restartOptions = SqlService.GetRestart();
                        GetModelVariant(restartOptions);
                    }

                    //Добавить рестарт GetModelYearsList
                }
            }

            return jsonOut;
        }
        /// <summary>
        /// Выводит информацию по текущим запросам
        /// </summary>
        /// <param name="showCaseCounter"></param>
        /// <param name="toPauseCounter"></param>
        public static void PrintStat(int showCaseCounter, int toPauseCounter)
        {
            Console.WriteLine("\n ************************************************************************************** \n Curent status: No of curent post = " + showCaseCounter + " Etaration before pause left = " + (20 - toPauseCounter) + "\n ************************************************************************************** \n");
        }

        public static async void GetCategoriesJson()  //запустить дома и проверить результат
        {
            int randomTimeout = Randomizer.RandomInt(1000, 3000);

            string json = "{\"baseCode\":\"7306\",\"langId\":\"92\"}";
            string uri = "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/ ";

            var jsonInString = await JsonLoader(uri,json,randomTimeout);

            Parser.ParseCategories(jsonInString);
            Parser.ParseDisplacement(jsonInString);
        }

        public static async void GetModelNameList(List<ModelJsonContent> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            List<string> idCollection = new();
            foreach (var item in jsonParamsCollection)
            {
                int randomTimeout = Randomizer.RandomInt(1000, 3000);

                string uri = "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/ ";

                string json = "{\"productId\":\"" + item.ProductId + "\",\"displacementType\":\"" + item.DisplacementType + "\",\"baseCode\":\"7306\",\"langId\":\"92\"}";

                var jsonInString = await JsonLoader(uri,json,randomTimeout);

                jsonCollection.Add(jsonInString);
                idCollection.Add(item.DisplacementId);

                System.Threading.Thread.Sleep(randomTimeout);
            }

            Parser.ParseModelsList(jsonCollection, idCollection);
        }


        /// <summary>
        /// Выгружает с сайта данные по годам у моделей. Принимает лист параметров для запросов
        /// </summary>
        /// <param name="jsonParamsCollection"></param>
        public static async void GetModelYearsList(List<YearsJsonContent> jsonParamsCollection)
        {
            List<UnparsedYearsData> jsonCollection = new();
            int toPauseCounter = 1;
            int showCaseCounter = 1;

            foreach (var item in jsonParamsCollection)
            {
                UnparsedYearsData row = new();

                int randomTimeout = Randomizer.RandomInt(1000, 3000);
                int randomBigTimeout = Randomizer.RandomInt(9000, 12000);

                string jsonAnswer;
                string uri = "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/";

                

                string json = "{\"productId\":\"" + item.ProductId + "\",\"modelName\":\"" + item.ModelName + "\",\"nickname\":\"" + item.Nickname + "\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}";

                row.ModelName = item.ModelName;

                jsonAnswer = await JsonLoader(uri,json,randomTimeout);

                row.Json = jsonAnswer;
                jsonCollection.Add(row);

                PrintStat(showCaseCounter,toPauseCounter);

                System.Threading.Thread.Sleep(randomTimeout);


                if (showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 10)
                {
                    System.Threading.Thread.Sleep(randomBigTimeout);
                    toPauseCounter = 0;
                    SqlService.InsertUnparsedData(jsonCollection);
                    jsonCollection = new();
                }
                showCaseCounter++;
                toPauseCounter++;
            }
        }

        /// <summary>
        /// Выгружает с сайта набор вариантов одной конекретной модели. Принимает лист параметров для запросов
        /// </summary>
        /// <param name="jsonParamsCollection"></param>
        public static async void GetModelVariant(List<ModelVariantJsonContent> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            int toPauseCounter = 1;
            int showCaseCounter = 1;

            foreach (var item in jsonParamsCollection)
            {

                int randomTimeout = Randomizer.RandomInt(1000, 3000);
                int randomBigTimeout = Randomizer.RandomInt(9000, 12000);

                string jsonAnswer = string.Empty;
                string uri = "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/";

                var client = PostClient.Create();

                string json = "{\"productId\":\"" + item.ProductId + "\",\"calledCode\":\"1\",\"modelName\":\"" + item.ModelName + "\",\"nickname\":\"" + item.Nickname + "\",\"modelYear\":\"" + item.ModelYear + "\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}";

                jsonAnswer = await JsonLoader(uri,json,randomTimeout);

                PrintStat(showCaseCounter, toPauseCounter);

                jsonCollection.Add(jsonAnswer);

                System.Threading.Thread.Sleep(randomTimeout);

                if (showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 20)
                {
                    System.Threading.Thread.Sleep(randomBigTimeout);

                    toPauseCounter = 0;
                    SqlService.InsertUnparsedVariant(jsonCollection);
                    jsonCollection = new();
                }
                showCaseCounter++;
                toPauseCounter++;
            }
        }

        /// <summary>
        /// Выгружает с сайта списки зап частей. Принимает лист параметров для пост запросов
        /// </summary>
        /// <param name="jsonParamsCollection"></param>
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

                string jsonAnswer;

                string uri = "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/"; 

                string json = "{\"productId\":\"" + item.ProductId + "\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"" + item.ModelTypeCode + "\",\"modelYear\":\"" + item.ModelYear + "\",\"productNo\":\"" + item.ProductNo + "\",\"colorType\":\"" + item.ColorType + "\",\"modelName\":\"" + item.ModelName + "\",\"prodCategory\":\"" + item.ProdCategory + "\",\"calledCode\":\"1\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}";

                jsonAnswer = await JsonLoader(uri, json, randomTimeout);

                idCollection.Add(item.VariantId);

                jsonCollection.Add(jsonAnswer);

                PrintStat(showCaseCounter, toPauseCounter);

                System.Threading.Thread.Sleep(randomTimeout);

                if (showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 20)
                {
                    System.Threading.Thread.Sleep(randomBigTimeout);
                    toPauseCounter = 0;
                    Parser.ParseCataloge(jsonCollection, idCollection);
                    jsonCollection = new();
                    idCollection = new();
                }
                showCaseCounter++;
                toPauseCounter++;
            }
        }

        /// <summary>
        /// Выгружает в парсер с сайта детали конкретной части. Принимает лист параметров для пост запросов
        /// </summary>
        /// <param name="jsonParamsCollection"></param>
        public static async void GetSetParts(List<PartsJsonParameters> jsonParamsCollection)
        {
            List<string> jsonCollection = new();
            List<string> idCollection = new();
            int toPauseCounter = 1;
            int showCaseCounter = 1;
            foreach (var item in jsonParamsCollection)
            {
                int randomTimeout = Randomizer.RandomInt(500, 1500);
                int randomBigTimeout = Randomizer.RandomInt(7000, 10000);

                var client = PostClient.Create();

                string json = "{\"productId\":\"" + item.ProductId + "\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"" + item.ModelTypeCode + "\",\"modelYear\":\"" + item.ModelYears + "\",\"productNo\":\"" + item.ProductNo + "\",\"colorType\":\"" + item.ColorType + "\",\"modelName\":\"" + item.ModelName + "\",\"vinNoSearch\":\"false\",\"figNo\":\"" + item.FigNo + "\",\"figBranchNo\":\"" + item.FigBranchNo + "\",\"catalogNo\":\"" + item.CatalogNo + "\",\"illustNo\":\"" + item.IllustNo + "\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"domOvsId\":\"2\",\"greyModelSign\":false,\"cataPBaseCode\":\"7451\",\"currencyCode\":\"GBP\"}";
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_text/", content);

                    jsonCollection.Add(await answer.Content.ReadAsStringAsync());
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(randomTimeout);

                    try
                    {
                        var answer = await client.PostAsync("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_text/", content);

                        jsonCollection.Add(await answer.Content.ReadAsStringAsync());
                    }
                    catch (Exception)
                    {
                        jsonParamsCollection = null;
                        var restartoptions = SqlService.GetPartsJson();
                        GetSetParts(restartoptions);
                        return;
                    }
                }

                idCollection.Add(item.CatalogeId);

                Console.WriteLine("\n ************************************************************************************** \n Curent status: No of curent post = " + showCaseCounter + " Etaration before pause left = " + (20 - toPauseCounter) + "\n ************************************************************************************** \n");

                System.Threading.Thread.Sleep(randomTimeout);

                if (showCaseCounter == jsonParamsCollection.Count || toPauseCounter == 20)
                {
                    System.Threading.Thread.Sleep(randomBigTimeout);
                    toPauseCounter = 0;
                    Parser.ParseParts(jsonCollection, idCollection); 
                    jsonCollection = new();
                    idCollection = new();
                }
                showCaseCounter++;
                toPauseCounter++;
            } 
        }
    }
}


