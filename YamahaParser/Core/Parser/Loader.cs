using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YamahaParser.Core.Parser
{
    class Loader
    {
        
        public static string GetMainParts() 
        {
            HttpClientHandler handler = new();
            
            HttpClient client = new(handler);
            var response = client.GetAsync("https://www.yamaha-motor.eu/gb/en/services/yamaha-offers-you/parts-catalogue/#/").Result;
            string outputInString =  response.Content.ReadAsStringAsync().Result;

            return outputInString; 
        }

        public static async Task<string> GetJsonOfCatAsync(string link)
        {
            var contentJson = new JSONtoSend
            {
                baseCode = 7306,
                langId = 92
            };

            string json = JsonConvert.SerializeObject(contentJson);
            //var jsonBody = new StringContent(json, Encoding.UTF8, "application/json");

            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            /*ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // local dev, just approve all certs
                return errors == SslPolicyErrors.None;
            };*/
            //System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13 | SecurityProtocolType.Tls11;

            HttpClientHandler handler = new();
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Default;
            //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            
            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Add("Host", "parts.yamaha-motor.co.jp");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var answer = await client.PostAsync(link, content);

            var str = await answer.Content.ReadAsStringAsync();

            //var request = new HttpRequestMessage(HttpMethod.Post, link);
            //request.Headers.Host = "parts.yamaha-motor.co.jp";

            //var productValue = new ProductInfoHeaderValue("Mozilla", "5.0");
            //var commentValue = new ProductInfoHeaderValue("(Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            //request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
            //request.Headers.UserAgent.Add(commentValue);

           // request.Content = jsonBody;


            //var response = await client.SendAsync(request);

            //string outputInString = response.Content.ReadAsStringAsync().Result;
            //outputInString = "test";

            return str;
        }


        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
