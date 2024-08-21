namespace WebApi_rabbitmq.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
