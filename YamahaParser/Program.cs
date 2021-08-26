using System;
using System.Net;
using YamahaParser.Core.Parser;

namespace YamahaParser
{
    class Program
    {
        static void Main(string[] args)
        {

            string catLink = Parser.GetCatalogLink();
            string cat = Loader.GetCategories(catLink);
            Console.WriteLine(cat);

            Console.ReadKey();
        }
    }
}
