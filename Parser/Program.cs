using Newtonsoft.Json;
using Parser.Core.SqlConnection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*string obj = await Loader.GetCategoriesJson();

            var parsedCat = Parser.ParseCategories(obj);
            SqlService.InsertCategories(parsedCat);*/

            /*var jsonParamsColection = SqlService.GetJsonParams();

            var jsonList = await Loader.GetModelNameList(jsonParamsColection);

            var result = Parser.ParseModelsList(jsonList);

            for (int i = 0; i < result.Nickname.Count; i++)
            {
                Console.WriteLine("PId " + result.ProductId[i] + "MName " + result.ModelName[i] + "DisMName " + result.DispModelName[i] + "nickname " + result.Nickname[i]);
            }

            SqlService.InsertModelList(result);*/

            var unVariants = SqlService.GetVariantsUnparseds();
            var variants = Parser.ParseVariants(unVariants);

            SqlService.InsertVariant(variants);


            /*Loader.GetModelYearsList(yearsJsonParams);*/

            /*foreach (var item in jsonList)
            {
                Console.WriteLine(item + "*******************************************\n");
            }**/

            Console.ReadKey();
        }
    }
}
