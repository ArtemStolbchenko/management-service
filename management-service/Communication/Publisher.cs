namespace management_service.Communication
{
    using management_service.Model;
    using management_service.Utilities;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using System.Text;

    public static class Publisher
    {
        private static bool _isInitialized = false;
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private const string PUBLISH_NAME = "internal.exchange", ARCHIVE_EXCHANGE = "internal.archive";
        private const string ROUTING_KEY = "internal.publish.routing-key";
        private const string PUBLISH_QUEUE = "publish.queue", ARCHIVE_QUEUE = "archive.queue";
        private static void Initialize()
        {
            if (!_isInitialized)
            {
                _factory = new ConnectionFactory { 
                    HostName = "20.126.244.61", 
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    DispatchConsumersAsync= true,
                };
                _connection = _factory.CreateConnection();
                //unlike channels, connections are expensive and should be reused
                AppDomain.CurrentDomain.ProcessExit += DisposeConnection;

                _isInitialized = true;
            }
        }
        private static void DisposeConnection(object? sender, EventArgs? e)
        {
            _connection.Close();
            _connection.Dispose();
        }
        public static void Publish(Story story)
        {
            SendToPublisher(GetBytes(story)); //disabled, since the Publisher service is 

            StoryDTO archiveStory = new StoryDTO(story);
            ArchiveRequestObject requestObject = new ArchiveRequestObject();
            requestObject.Type = Type.Article;
            requestObject.MethodName = "AddArticle";
            requestObject.Additionals = JsonConvert.SerializeObject(archiveStory);

            SendToArchive(GetBytes(requestObject));
        }
        private static byte[] GetBytes(object sourceObject)
        {
            string json = JsonConvert.SerializeObject(sourceObject);
            var bytes = Encoding.UTF8.GetBytes(json);
            return bytes;
        }
        private static void SendToArchive(byte[] message)
        {
            Send(ARCHIVE_QUEUE, ARCHIVE_EXCHANGE, ROUTING_KEY, message);
        }
        private static void SendToPublisher(byte[] message)
        {
            Send(PUBLISH_QUEUE, PUBLISH_NAME, ROUTING_KEY, message);
        }
        private static void Send(string queue, string exchange, string key, byte[] message)
        {
            Initialize();
            using var channel = _connection.CreateModel();
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic, durable: true);
                channel.QueueBind(queue: queue, exchange: exchange, routingKey: key);
                channel.BasicPublish(exchange: exchange,
                                     routingKey: key,
                                     basicProperties: null,
                                     body: message);
                Console.WriteLine($"[x] Sent {message}\n to {exchange}");
            }
        }
    }
}
