using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumer
{
    internal class Program
    {
		static void Main()
		{
			using (var connection = GetRabbitConnection())
			using (var channel = connection.CreateModel())
			{
				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, ea) =>
				{
					var body = ea.Body;
					string message = Encoding.UTF8.GetString(body.ToArray());

					//throw new Exception("Всё пропало!");

					Console.WriteLine(" [x] Received {0}", message);
				};

				
				channel.BasicConsume(queue: "Say.Hello",
									 autoAck: true,
									 consumer: consumer);

				Console.WriteLine(" Press [enter] to exit.");
				Console.ReadLine();
			}
		}

		static private IConnection GetRabbitConnection()
		{
			ConnectionFactory factory = new ConnectionFactory
			{
                UserName = "guest",
                Password = "guest",
                //VirtualHost = "hyvbmeov",
                HostName = "woodpecker.rmq.cloudamqp.com"
            };
			IConnection conn = factory.CreateConnection();
			return conn;
		}

	}
}
