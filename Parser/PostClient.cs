using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class PostClient
    {
        public static HttpClient Create() 
        {
            HttpClientHandler handler = new();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            return client;
        }
    }
}
