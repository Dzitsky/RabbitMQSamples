using Producer.Producers;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectTestProducer.Produce(GetRabbitConnection());
            // FanoutTestProducer.Produce(GetRabbitConnection());
            // TopicTestProducer.Produce(GetRabbitConnection());
            // InstantDirectTestProducer.Produce(GetRabbitConnection());
        }

        static private IConnection GetRabbitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "ykziztbb",
                Password = "oZaUpy2Sru1P0b04K9ghjx3MSFpXTMIU",
                VirtualHost = "ykziztbb",
                HostName = "hawk.rmq.cloudamqp.com"
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
