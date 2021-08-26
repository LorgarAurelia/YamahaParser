
namespace YamahaParser.Core
{
    class Headers
    {
        public static string  Host = "parts.yamaha-motor.co.jp";

        public static string[] UserAgent = new string[2] { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0" };

        public static string[] Accept = new string[2] { "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8" };

        public static string[] AcceptLanguage = new string[2] { "Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3" };

        public static string[] AcceptEncoding = new string[2] { "Accept-Encoding", "gzip, deflate, br" };

        public static string[] ContetType = new string[2] { "Content-Type", "application/json" };

        public static string[] ContentLength = new string[2] { "Content-Length", "33" };

        public static string[] Origin = new string[2] { "Origin", "https://ypec-sss.yamaha-motor.co.jp" };

        public static string[] Connection = new string[2] { "Connection", "keep-alive" };

        public static string[] Referer = new string[2] { "Referer", "https://ypec-sss.yamaha-motor.co.jp/" };

        public static string[] SecFetchDest = new string[2] { "Sec-Fetch-Dest", "empty" };

        public static string[] SecFetchMode = new string[2] { "Sec-Fetch-Mode", "cors" };

        public static string[] SecFetchSite = new string[2] { "Sec-Fetch-Site", "same-site" };
    }
}
