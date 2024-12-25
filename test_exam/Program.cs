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
            using (StreamWriter file = new StreamWriter("debug.log", true))
            {
                WebRequest request = WebRequest.Create("https://perm-300.ru/afisha");
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                Console.WriteLine(response.StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                ParsingHtml(responseFromServer);
                //Console.WriteLine($"{responseFromServer}");

                reader.Close();
                dataStream.Close();
                response.Close();
                Console.Read();
            }

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
                var description = newsItem.Descendants("div").FirstOrDefault(n => !n.HasClass("shadow"))?.InnerText.Trim();
                Console.WriteLine($"Заголовок: {name}");
                Console.WriteLine($"Дата: {date}");
                Console.WriteLine($"Описание: {description}");
                Console.WriteLine(new string('-', 40));
            }
        }

    }
}
