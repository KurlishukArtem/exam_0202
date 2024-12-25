using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using HtmlAgilityPack;
using System.Net;

namespace test_exam
{
    public class Program
    {
        private static HttpClient _httpClient;
        private static string _cookie;
        static async Task Main(string[] args)
        {
            ParsingHtml(GetContent());
            Console.Read();
        }

        public static string GetContent()
        {
            string url = "https://perm-300.ru/afisha";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseFromServer = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseFromServer;
        }
        public static void ParsingHtml(string htmlCode)
        {
            var html = new HtmlDocument(); 
            html.LoadHtml(htmlCode);
            var Document = html.DocumentNode;
            var newsItems = Document.Descendants("a").Where(n => n.HasClass("eventshadow"));
            foreach (var newsItem in newsItems)
            {
                var date = newsItem.Descendants("div").FirstOrDefault(n => n.HasClass("eventdate"))?.InnerText.Trim();
                var name = newsItem.Descendants("div").FirstOrDefault(n => n.HasClass("eventtitle"))?.InnerText.Trim();
                var description = newsItem.Descendants("div").FirstOrDefault(n => n.HasClass("shadow"))?.InnerText.Trim();
                Console.WriteLine($"Заголовок: {name}");
                Console.WriteLine($"Дата: {date}");
                Console.WriteLine($"Описание: {description}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
