using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQProduser
{
	internal class Program
	{
		static void Main()
		{
			using (var connection = GetRabbitConnection())
			using (var channel = connection.CreateModel())
			{
				for (int i = 0; i < 10; i++)
				{
					string message = DateTime.UtcNow.ToString();

					var body = Encoding.UTF8.GetBytes(message);

					channel.BasicPublish(exchange: "",
										 routingKey: "Say.Hello",
										 basicProperties: null,
										 body: body);
					
					Console.WriteLine($"Отправлено {message}");
				}
			}

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}

		static private IConnection GetRabbitConnection()
		{
			ConnectionFactory factory = new ConnectionFactory
			{
				UserName = "guest",
				Password = "guest",
				VirtualHost = "VirtualHost",
				HostName = ""
            };
			IConnection conn = factory.CreateConnection();
			return conn;
		}
	}
}