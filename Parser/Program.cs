using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string a = await Loader.GetCategoriesJson();
            Console.WriteLine(a);
            Console.ReadLine();
        }
    }
}
