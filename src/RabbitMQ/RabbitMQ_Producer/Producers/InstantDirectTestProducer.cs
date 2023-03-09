using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using RabbitMQ.Client;

namespace Producer.Producers
{
    public class InstantDirectTestProducer
    {
        public static void Produce(IConnection connectionInfo)
        {
            var counter = 1;
            using (var connection = connectionInfo)
            using (var channel = connection.CreateModel())
            {
                const string exchangeName = "exchange.direct.instant";
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

                Console.WriteLine(connection.ChannelMax);
                do
                {
                    // counter = 0;
                    do
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));

                        var message = new MessageDto()
                        {
                            Content = $"Message from publisher with direct instant exchange!"
                        };
                        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                        

                        channel.BasicPublish(exchange: exchangeName,
                            routingKey: "cars.1",
                            basicProperties: null,
                            body: body);

                        Console.WriteLine($"Message is sent into Default Exchange [N:{counter++}]");
                    } while (true);
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
            }
        }
    }
}