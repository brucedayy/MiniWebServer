using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:9000/";
            Console.WriteLine(GetWebHtmlUrl(url));
            Console.Read();
        }
        public static string GetWebHtmlUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            req.AllowAutoRedirect = false;
            CookieContainer cc = new CookieContainer();
            req.CookieContainer = cc;
            using (WebResponse wrs = req.GetResponse())
            {
                Stream strm = wrs.GetResponseStream();
                StreamReader sr = new StreamReader(strm);
                string htmlstr = sr.ReadToEnd();
                return htmlstr;
            }
        }
    }
}
