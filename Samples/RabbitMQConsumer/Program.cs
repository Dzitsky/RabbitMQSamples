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
					var message = Encoding.UTF8.GetString(body.ToArray());

					//throw new Exception("Всё пропало!");

					Console.WriteLine(" [x] Received {0}", message);
				};

				
				channel.BasicConsume(queue: "hello",
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
				UserName = "niyvrxot",
				Password = "NmOSQmD8PLvjH5eX2ovHuqY3OR53BAv9",
				VirtualHost = "niyvrxot",
				HostName = "woodpecker.rmq.cloudamqp.com"
			};
			IConnection conn = factory.CreateConnection();
			return conn;
		}

	}
}
