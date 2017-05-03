using Microsoft.Net.Http.Server;
using System;
using System.Text;

namespace MiniWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            ProcessRequest();
            Console.WriteLine("Success to start!");
            Console.Read();
        }

        static async void ProcessRequest()
        {
            int count = 0;
            var settings = new WebListenerSettings();
            settings.UrlPrefixes.Add("http://localhost:9000");
            using (WebListener listener = new WebListener(settings))
            {
                listener.Start();
                while (true)
                {
                    var context = await listener.AcceptAsync();
                    byte[] bytes = Encoding.ASCII.GetBytes(
                        "ConnectionId"+context.Request.ToString()+
                        "\nHeaders"+context.Request.Headers                        
                        +"\n"+DateTime.Now);
                    context.Response.ContentLength = bytes.Length;
                    context.Response.ContentType = "text/plain";
                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    context.Dispose();
                    Console.WriteLine("Request==>{0}", ++count);
                }
            }
        }
    }
}