using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey(); 

            var redis = ConnectionMultiplexer.Connect("localhost");
            var db = redis.GetDatabase();

            db.StringSet("Ola", "Oi eu sei responder");
            var value = db.StringGet("Ola");

            var pub = redis.GetSubscriber();
            pub.Publish("Perguntas", "Qual é a pergunta que ele vai fazer?");

            var sub = redis.GetSubscriber();
            sub.Subscribe("Perguntas", (channel, msg) =>
            {
                msg.ToString();
            });

        }
    }
}
