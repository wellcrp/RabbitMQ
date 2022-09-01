namespace Project.RabbitMQ.Infrastructure.Interface
{
    public interface IRabbitMQProducer
    {
        void SendProductDirectExchange<T>(T message);
    }
}
