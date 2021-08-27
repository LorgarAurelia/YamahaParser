using System;
using System.Net;
using System.Threading.Tasks;
using YamahaParser.Core.Parser;

namespace YamahaParser
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string catLink = Parser.GetCatalogLink();
            var cat = await Loader.GetJsonOfCatAsync(catLink);
            Console.WriteLine(cat);

            Console.ReadKey();
        }
    }
}
