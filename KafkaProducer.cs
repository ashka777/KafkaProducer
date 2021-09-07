using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace KafkaProducer
{
    public class KafkaProducer : IHostedService
    {
        private IProducer<string, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;
        

        public KafkaProducer(ILogger<KafkaProducer> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            { 
                BootstrapServers = $"{HostBuilder.property[0]}:{HostBuilder.property[1]}" //"10.0.9.245:9092"
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 1; ++i)
            {
                //var key = "1";
                var value = string.Format(@"<Cashiers Id=""4"" Shop=""Дикси10"" Name=""Кассирмян Иван Продавчавчав"" Code=""10010010"" InsDate=""2021 - 08 - 26T16: 32:52.220""/>");//numtest
                _logger.LogInformation(value);

                await _producer.ProduceAsync(topic: HostBuilder.property[2] /*"locTest"*/, new Message<string, string>()
                {
                    // Key = key,
                    Value = value
                }, cancellationToken);
            }
            Console.WriteLine("Flush.");
            _producer.Flush(TimeSpan.FromSeconds(5));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
