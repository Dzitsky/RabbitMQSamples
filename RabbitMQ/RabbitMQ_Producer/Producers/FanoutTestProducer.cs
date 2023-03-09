using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Producer.Producers
{
    public class FanoutTestProducer
    {
        public static void Produce(IConnection connectionInfo)
        {
            using (var connection = connectionInfo)
            using (var channel = connection.CreateModel())
            {
                const string exchangeName = "exchange.fanout";
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
                
                Console.WriteLine(connection.ChannelMax);
                
                var message = new MessageDto()
                {
                    Content = $"Message from publisher with fanout exchange!"
                };
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                
                channel.BasicPublish(exchange: exchangeName,
                    routingKey: "",
                    basicProperties: null,
                    body: body);

                Console.WriteLine($"Message is sent into Default Exchange: {exchangeName}");
            }
        }
    }
}