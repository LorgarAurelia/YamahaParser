using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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

        public static string GetCategories(string link) 
        {
            HttpClientHandler handler = new();

            HttpClient client = new(handler);
            var response = client.GetAsync(link).Result;

            string outputInString = response.Content.ReadAsStringAsync().Result;

            return outputInString;
        }


        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
