using Newtonsoft.Json;
using Project.RabbitMQ.Infrastructure.Interface;
using RabbitMQ.Client;
using System.Text;

namespace Project.RabbitMQ.Infrastructure
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public static String EXCHANGE_DIRECT_NAME = "direct-exchange";
        public static String QUEUE_DIRECT_NAME_1 = "direct-queue-1";
        public static String ROUTING_DIRECT_KEY_1 = "direct-key-1";

        public static String EXCHANGE_FANOUT_NAME = "fanout-exchange";
        public static String QUEUE_FANOUT_NAME_1 = "fanout-queue-1";

        public static String EXCHANGE_TOPIC_NAME = "topic-exchange";
        public static String QUEUE_TOPIC_NAME_1 = "topic-queue-1";
        public static String ROUTING_TOPIC_KEY_1 = "topic-key-1";
        public void SendProductDirectExchange<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //Declara o tipo do exchange e suas caracteristicas para messages
            channel.ExchangeDeclare(EXCHANGE_DIRECT_NAME, type: ExchangeType.Direct, false);
            channel.QueueDeclare(QUEUE_DIRECT_NAME_1, exclusive: false);

            // Ligação entre uma fila e um exchange com RouterKey
            channel.QueueBind(QUEUE_DIRECT_NAME_1, EXCHANGE_DIRECT_NAME, ROUTING_DIRECT_KEY_1);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: EXCHANGE_DIRECT_NAME, routingKey: ROUTING_DIRECT_KEY_1, body: body);
        }

        public void SendProductFanoutExchange<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //Declara o tipo do exchange e suas caracteristicas para messages
            channel.ExchangeDeclare(EXCHANGE_FANOUT_NAME, type: ExchangeType.Fanout, false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: EXCHANGE_FANOUT_NAME, routingKey: string.Empty, basicProperties: null, body: body);
        }

        public void SendProductTopicExchange<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(EXCHANGE_TOPIC_NAME, type: ExchangeType.Topic);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: EXCHANGE_TOPIC_NAME, routingKey: ROUTING_TOPIC_KEY_1, basicProperties: null, body: body);
        }
    }
}
