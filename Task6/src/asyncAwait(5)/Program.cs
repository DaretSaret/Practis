using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace asyncAwait_5_
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = new string[]
            {
            "https://google.com",
            "https://example.com",
            "https://microsoft.com"
            };

            using (HttpClient client = new HttpClient())
            {
                var task = new Task<string>[url.Length];

                for (int i = 0; i < url.Length; i++)
                {
                    task[i] = client.GetStringAsync(url[i]);
                }

                try
                {
                    var results = await Task.WhenAll(task);

                    for (int i = 0; i < results.Length; i++)
                    {
                        Console.WriteLine("Содержимое страницы: " + (url[i]) + " " + (results[i].Length) + " символов");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Ошибка при загрузке данных " + (e.Message));
                }
            }
        }
    }
}
