using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services.Interfaces
{
    public class AzureQueueMessageSender : IAzureQueueMessageSender
    {
        private readonly string _connectionString;

        public AzureQueueMessageSender(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ServiceBus");
        }

        public async Task SendMessageAsync(string message, string queueName)
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            var client = new ServiceBusClient(_connectionString, clientOptions);
            var sender = client.CreateSender(queueName);

            var messageBatch = await sender.CreateMessageBatchAsync();
            if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
            {
                throw new Exception($"The message is too large to fit in the batch.");
            }

            try
            {
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
