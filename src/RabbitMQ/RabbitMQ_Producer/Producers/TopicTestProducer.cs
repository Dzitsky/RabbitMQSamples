using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Producer.Producers
{
    public class TopicTestProducer
    {
        public static void Produce(IConnection connectionInfo)
        {
            using (var connection = connectionInfo)
            using (var channel = connection.CreateModel())
            {
                const string exchangeName = "exchange.topic";
                channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);
                
                Console.WriteLine(connection.ChannelMax);

                var message = new MessageDto()
                {
                    Content = $"Message from publisher with topic exchange!"
                };
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                
                channel.BasicPublish(exchange: exchangeName,
                    routingKey: "cars.*",
                    basicProperties: null,
                    body: body);

                Console.WriteLine($"Message is sent into Default Exchange: {exchangeName}");
            }
        }
    }
}