using Confluent.Kafka;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KafkaProducer.HostBuilder;

namespace Kafka
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                args = new string[] { "10.0.28.77", "9092", "locTest" };
                Console.WriteLine($"Не были явно указаны параметры: 'сервер_порт_topic',\n будут использоваться: '{args[0]}_{args[1]}_{args[2]}'.");
                Console.WriteLine($"Для продолжения нажмите любую клавишу");
                Console.ReadKey();
            }
            CreateHostBuilder(args).Build().Run();

            //Console.WriteLine("End.");
            //Console.ReadLine();
        }
    }
}
