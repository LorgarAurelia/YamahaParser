﻿using Newtonsoft.Json;
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
            string obj = await Loader.GetCategoriesJson();

            var parsedCat = Parser.ParseCategories(obj);
            SqlService.InsertCategories(parsedCat);



            Console.ReadKey();
        }
    }
}
