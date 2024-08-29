using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace WebApi_rabbitmq.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            string exchangeName = "productExchange";
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: exchangeName, routingKey: "", body: body);
        }
    }
}
