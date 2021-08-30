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
            string obj = await Loader.GetCategoriesJson();

            Parser.ParseCategories(obj);
            /*string s = "C:/Users/AppData/Roaming/.Data/objects/1233as...";
            Console.WriteLine(s);
            Console.WriteLine(s.Remove(s.IndexOf(obj)));
            Console.WriteLine(s.Remove(s.IndexOf(obj) + obj.Length));
            Console.WriteLine(s.Substring(s.IndexOf(obj)));
            Console.WriteLine(s.Substring(s.IndexOf(obj) + obj.Length));*/

            Console.ReadKey();
        }
    }
}
