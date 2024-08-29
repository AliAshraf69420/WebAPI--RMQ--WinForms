using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Streamer.Hubs;
using Streamer.Models;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Streamer.RabbitMQ_client
{
    public class RabbitMQReceivercs : BackgroundService
    {
        
    private IConnection _connection;
        private IModel _channel;
        private readonly ConnectionFactory _factory;
        private readonly IHubContext<MyHub> _hubContext;
        public RabbitMQReceivercs(IHubContext<MyHub> hubContext)
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost" // Update with your RabbitMQ server details
            };
            _hubContext = hubContext;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // Create RabbitMQ connection and channel when the service starts
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "productExchange", type: ExchangeType.Fanout);
            // Declare the queue
            _channel.QueueDeclare(queue: "product_", exclusive: false);
            _channel.QueueBind(queue: "product_", exchange: "productExchange", routingKey: "");


            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create the consumer and start consuming messages
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = System.Text.Json.JsonSerializer.Deserialize<productClass>(message);
                await _hubContext.Clients.All.SendAsync("ReceiveProduct", order);
                Console.WriteLine($"Product message received: {message}");
            };

            // Consume the queue
            _channel.BasicConsume(queue: "product_", autoAck: true, consumer: consumer);

            // Keep the background service alive
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // Cleanup the RabbitMQ connection and channel when the service stops
            _channel?.Close();
            _connection?.Close();

            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
