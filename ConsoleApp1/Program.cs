using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        public static RedisChannel canal = "perguntas";
        public static IConnectionMultiplexer client = ConnectionMultiplexer.Connect("191.232.234.20");
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var db = client.GetDatabase();

            var sub = client.GetSubscriber();
            sub.Subscribe(canal, (ch, msg) =>
            {
                if (msg.ToString().StartsWith("P"))
                    GeraResposta(msg.ToString());
            });

            Console.ReadKey();
        }

        public static void GeraResposta(string msg)
        {
            var pub = client.GetSubscriber();
            var pergunta = msg.Split(':');
            string x = pergunta[0].Substring(0, 1);
            var resposta = int.TryParse(x, out int c) ? (int.Parse(x) * 2).ToString() : "Não sei sugiro procurar no google"; //getRespostaGoogle(msg);

            var db = client.GetDatabase();
            db.HashSet(pergunta[0], "Joao - GianCarlo",resposta);
        }

        //public static string getRespostaGoogle(string msg)
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.Headers.Add("user-agent", "Only a test!");

        //    string apiKey = "AIzaSyDiRCI-3Djsw8qTrXIYF7AIqnCSyyHmFmM";
        //    string cx = "009431853094902135308:axmjfgvmaam";


        //    //var results = webClient.DownloadString(String.Format("https://www.google.com.au/search?q={0}&alt=json", msg));
        //    var results = webClient.DownloadString(String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&alt=json", apiKey, cx, msg));

        //    // web.Dispose();
        //    return results;
        //}
    }
}

